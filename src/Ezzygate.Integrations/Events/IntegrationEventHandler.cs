using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Logging;
using Ezzygate.Infrastructure.Repositories.Interfaces;
using Ezzygate.Infrastructure.Scheduling;
using Ezzygate.Integrations.Scheduling;

namespace Ezzygate.Integrations.Events;

public abstract class IntegrationEventHandler : IIntegrationEventHandler
{
    private readonly ILogger<IntegrationEventHandler> _logger;
    private readonly IIntegrationDataService _dataService;
    private readonly IDelayedTaskScheduler _taskScheduler;

    protected IntegrationEventHandler(
        ILogger<IntegrationEventHandler> logger,
        IIntegrationDataService dataService,
        IDelayedTaskScheduler taskScheduler)
    {
        _logger = logger;
        _dataService = dataService;
        _taskScheduler = taskScheduler;
    }

    public abstract string Tag { get; }
    protected virtual TimeSpan FinalizeDelay => TimeSpan.FromSeconds(30);
    public async Task HandleAsync(HttpRequest request, CancellationToken cancellationToken = default)
    {
        using var logger = _logger.GetScopedForIntegration(Tag, "Callback Event");

        try
        {
            var context = await ParseRequestAsync(request, cancellationToken);
            
            logger.Info($"Path: {context.UrlPath}");
            logger.Info($"Payload: {context.Payload}");

            if (!IsEventSupported(context.EventName))
            {
                logger.SetShortMessage($"Event: {context.EventName} not supported");
                return;
            }

            logger.Info($"Event: {context.EventName} Approval Id: {context.ApprovalNumber}");

            var pendingTrx = await _dataService.Transactions
                .GetPendingTrxByApprovalNumberAsync(context.ApprovalNumber);
            if (pendingTrx is null)
            {
                logger.SetShortMessage($"Event: {context.EventName} discarded");
                logger.Info("No pending transaction found");
                return;
            }

            var terminal = await _dataService.Terminals
                .GetByTerminalNumberAsync(pendingTrx.TerminalNumber);
            if (terminal is null)
            {
                logger.SetShortMessage($"Event: {context.EventName} discarded");
                logger.Warn("No terminal found");
                return;
            }
            if (!ValidateSignature(context, terminal))
            {
                logger.Error("Invalid signature header");
                return;
            }

            var logId = await _dataService.ChargeAttempts
                .GetByTransactionIdAsync(pendingTrx.Id);
            if (logId is null)
            {
                logger.Warn("No charge attempt found, cannot schedule finalization");
                return;
            }

            await _dataService.ChargeAttempts
                .UpdateByTransactionAsync((trxId, _) => trxId == pendingTrx.Id, u => u
                    .SetInnerResponse(context.Payload), cancellationToken);

            var payload = new CallbackFinalizePayload
            {
                Tag = Tag,
                EventName = context.EventName,
                ApprovalNumber = context.ApprovalNumber,
                DebitReferenceCode = pendingTrx.DebitReferenceCode,
                DebitReferenceNum = pendingTrx.DebitReferenceNum ?? string.Empty,
                ChargeAttemptLogId = logId.Id,
                AutomatedStatus = context.AutomatedStatus,
                AutomatedErrorMessage = context.AutomatedErrorMessage,
                AutomatedPayload = context.AutomatedPayload
            };

            _taskScheduler.Schedule<CallbackFinalizeTask, CallbackFinalizePayload>(payload, FinalizeDelay);
            
            logger.SetShortMessage($"Event: {context.EventName} scheduled finalization");
        }
        catch (Exception ex)
        {
            logger.Error($"Error handling event: {ex.Message}");
            throw;
        }
    }

    protected abstract Task<IntegrationEventContext> ParseRequestAsync(HttpRequest request, CancellationToken cancellationToken);
    protected abstract bool IsEventSupported(string eventName);
    protected abstract bool ValidateSignature(IntegrationEventContext context, Terminal terminal);
}
