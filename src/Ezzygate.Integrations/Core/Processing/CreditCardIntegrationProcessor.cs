using Microsoft.Extensions.Logging;
using Ezzygate.Application.Integrations;
using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Mappings;
using Ezzygate.Infrastructure.Repositories.Interfaces;
using Ezzygate.Infrastructure.Services;
using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Integrations.Core.Abstractions;

namespace Ezzygate.Integrations.Core.Processing;

public class CreditCardIntegrationProcessor : ICreditCardIntegrationProcessor
{
    private readonly ILogger<CreditCardIntegrationProcessor> _logger;
    private readonly IIntegrationProvider _integrationProvider;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ITransactionService _transactionService;
    private readonly IChargeAttemptRepository _chargeAttemptRepository;

    public CreditCardIntegrationProcessor(
        ILogger<CreditCardIntegrationProcessor> logger,
        IIntegrationProvider integrationProvider,
        ITransactionRepository transactionRepository,
        ITransactionService transactionService,
        IChargeAttemptRepository chargeAttemptRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _integrationProvider = integrationProvider ?? throw new ArgumentNullException(nameof(integrationProvider));
        _transactionRepository = transactionRepository;
        _transactionService = transactionService;
        _chargeAttemptRepository = chargeAttemptRepository;
    }

    public async Task<IntegrationResult> ProcessTransactionAsync(TransactionContext context,
        CancellationToken cancellationToken = default)
    {
        var integrationTag = context.DebitCompany?.IntegrationTag;
        var integration = _integrationProvider.GetIntegration<ICreditCardIntegration>(integrationTag);
        if (integration == null)
            return new IntegrationResult { Code = "503" };

        var result = context.OpType == OperationType.Finalize
            ? await InternalFinalizeTransactionAsync(context, integration, cancellationToken)
            : await InternalProcessTransactionAsync(context, integration, cancellationToken);

        result.CustomParams ??= new object();

        return result;
    }

    private async Task<IntegrationResult> InternalFinalizeTransactionAsync(
        TransactionContext context,
        ICreditCardIntegration integration,
        CancellationToken cancellationToken)
    {
        if (context.LocatedTrx.IsFinalized)
        {
            var result = context.LocatedTrx.ToIntegrationResult();
            result.NotificationResponse = await integration.GetNotificationResponseAsync(context, cancellationToken);
            return result;
        }

        if (!context.Terminal.IsActive || !context.DebitCompany.IsActive)
        {
            var result = new IntegrationResult
            {
                Code = "592",
                Message = "Terminal or Processor deactivated",
                DebitRefCode = context.DebitRefCode
            };

            var moveResult = await _transactionService.MoveTrxAsync(context.LocatedTrx.TrxId, result.Code,
                result.Message, context.LocatedTrx.BinCountry, cancellationToken);

            result.TrxId = moveResult.TrxId;
            result.TrxType = moveResult.PendingTrx.TransType;

            await _chargeAttemptRepository
                .UpdateByTransactionAsync((trxId, reply) => trxId == context.LocatedTrx.TrxId && reply == "553", u => u
                        .SetTransaction(moveResult.TrxId, result.Code, result.Message), cancellationToken);

            return result;
        }

        var finalizeResult = await integration.ProcessTransactionAsync(context, cancellationToken);

        finalizeResult.DebitRefCode = context.DebitRefCode;
        finalizeResult.ApprovalNumber = context.LocatedTrx?.ApprovalNumber ?? finalizeResult.ApprovalNumber;
        if (string.IsNullOrWhiteSpace(finalizeResult.RedirectUrl))
            finalizeResult.RedirectUrl = context.LocatedTrx?.RedirectUrl;
        finalizeResult.DebitRefNum = context.DebitRefNum;
        if (string.IsNullOrWhiteSpace(finalizeResult.NotificationResponse))
            finalizeResult.NotificationResponse = "ok";

        if (finalizeResult.Code == "553")
        {
            var updated = await _chargeAttemptRepository
                .UpdateAsync(id => id == context.ChargeAttemptLogId, u => u
                        .SetRedirectFlag(true), cancellationToken);
            if (!updated)
                _logger.LogWarning("Failed to update Redirect flag. ChargeAttemptLogId: '{ChargeAttemptLogId}'",
                    context.ChargeAttemptLogId);
        }

        return finalizeResult;
    }

    private async Task<IntegrationResult> InternalProcessTransactionAsync(
        TransactionContext context,
        ICreditCardIntegration integration,
        CancellationToken cancellationToken)
    {
        var processResult = await integration.ProcessTransactionAsync(context, cancellationToken);
        processResult.DebitRefCode = context.DebitRefCode;
        processResult.DebitRefNum = context.DebitRefNum;

        if (context.OpType is OperationType.AuthorizationCapture or OperationType.AuthorizationRelease
            && processResult.Code == "000")
        {
            var preAuthId = context.QueryStringParsed["TransApprovalID"] ?? context.FormDataParsed["TransApprovalID"];
            if (preAuthId == null)
                throw new Exception("No pre-auth id");

            var preAuthIdInt = int.Parse(preAuthId);
            var trx = await _transactionRepository.GetApprovalTrxAsync(preAuthIdInt, cancellationToken);
            if (trx == null)
                throw new Exception($"Pre-auth trx not found '{preAuthIdInt}'");

            await _transactionRepository.UpdateApprovalTrxAuthStatusAsync(preAuthIdInt, context.OpType, cancellationToken);
        }

        return processResult;
    }
}