using Microsoft.Extensions.Logging;
using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Logging;
using Ezzygate.Infrastructure.Scheduling;
using Ezzygate.Integrations.Core.Processing;

namespace Ezzygate.Integrations.Core.Scheduling;

public sealed class CallbackFinalizeTask : IDelayedTask<CallbackFinalizePayload>
{
    private readonly ILogger<CallbackFinalizeTask> _logger;
    private readonly IIntegrationFinalizer _finalizer;

    public CallbackFinalizeTask(
        ILogger<CallbackFinalizeTask> logger,
        IIntegrationFinalizer finalizer)
    {
        _logger = logger;
        _finalizer = finalizer;
    }

    public async Task ExecuteAsync(CallbackFinalizePayload payload, CancellationToken cancellationToken = default)
    {
        using var logger = _logger.GetScopedForIntegration(payload.Tag, "Callback Job");

        logger.Info($"Event: {payload.EventName} Approval Id: {payload.ApprovalNumber}");

        try
        {
            var request = new FinalizeRequest
            {
                DebitReferenceCode = payload.DebitReferenceCode,
                DebitReferenceNum = payload.DebitReferenceNum,
                ChargeAttemptLogId = payload.ChargeAttemptLogId,
                OperationType = OperationType.Finalize,
                IsAutomatedRequest = true,
                AutomatedStatus = payload.AutomatedStatus,
                AutomatedCode = payload.AutomatedCode,
                AutomatedMessage = payload.AutomatedMessage,
                AutomatedPayload = payload.AutomatedPayload
            };

            var result = await _finalizer.FinalizeAsync(request, cancellationToken);

            logger.SetShortMessage($"Event {payload.EventName} finalized. Approval id: {result.ApprovalNumber} Code: {result.Code}");
            logger.Info($"Finalized approval id: {result.ApprovalNumber} TrxId: {result.TrxId} Code: {result.Code}");
        }
        catch (Exception ex)
        {
            logger.Error($"Failed to finalize: {ex.Message}");
            throw;
        }
    }
}
