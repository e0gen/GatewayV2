using Microsoft.Extensions.Logging;
using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Logging;
using Ezzygate.Infrastructure.Repositories.Interfaces;
using Ezzygate.Infrastructure.Scheduling;
using Ezzygate.Integrations.Core.Processing;

namespace Ezzygate.Integrations.Core.Scheduling;

public sealed class TimeoutFinalizeTask : IDelayedTask<TimeoutFinalizePayload>
{
    public const string EventName = "3DS_TIMEOUT";
    private readonly ILogger<TimeoutFinalizeTask> _logger;
    private readonly IIntegrationFinalizer _integrationFinalizer;
    private readonly IChargeAttemptRepository _chargeAttemptRepository;

    public TimeoutFinalizeTask(
        ILogger<TimeoutFinalizeTask> logger,
        IIntegrationFinalizer integrationFinalizer,
        IChargeAttemptRepository chargeAttemptRepository)
    {
        _logger = logger;
        _integrationFinalizer = integrationFinalizer;
        _chargeAttemptRepository = chargeAttemptRepository;
    }

    public async Task ExecuteAsync(TimeoutFinalizePayload payload, CancellationToken cancellationToken = default)
    {
        using var logger = _logger.GetScopedForIntegration(payload.Tag, "Timeout Job");

        logger.Info($"Event: {EventName} ReferenceCode: {payload.DebitReferenceCode}, LogChargeAttemptId: {payload.ChargeAttemptLogId}");

        try
        {
            var request = new FinalizeRequest
            {
                DebitReferenceCode = payload.DebitReferenceCode,
                ChargeAttemptLogId = payload.ChargeAttemptLogId,
                OperationType = OperationType.ForceFinalize,
                IsAutomatedRequest = true,
                AutomatedStatus = EventName,
                AutomatedCode = "99",
                AutomatedMessage = "Payment Failed, 3DS Verification timeout"
            };

            var result = await _integrationFinalizer.FinalizeAsync(request, cancellationToken);

            logger.SetShortMessage($"Event {EventName} finalized. Approval id: {result.ApprovalNumber} Code: {result.Code}");
            logger.Info($"Finalized approval id: {result.ApprovalNumber} TrxId: {result.TrxId} Code: {result.Code}");
        }
        catch (Exception ex)
        {
            logger.Error($"Failed to finalize by timeout: {ex.Message}");
            throw;
        }
    }
}