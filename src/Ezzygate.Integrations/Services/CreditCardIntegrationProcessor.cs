using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Repositories.Interfaces;
using Ezzygate.Infrastructure.Services;
using Ezzygate.Integrations.Abstractions;
using Ezzygate.WebApi.Models.Integration;

namespace Ezzygate.Integrations.Services;

public class CreditCardIntegrationProcessor : ICreditCardIntegrationProcessor
{
    private readonly ILogger<CreditCardIntegrationProcessor> _logger;
    private readonly IIntegrationProvider _integrationProvider;
    private readonly ITransactionRepository _transactionRepository;

    public CreditCardIntegrationProcessor(
        ILogger<CreditCardIntegrationProcessor> logger,
        IIntegrationProvider integrationProvider,
        ITransactionRepository transactionRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _integrationProvider = integrationProvider ?? throw new ArgumentNullException(nameof(integrationProvider));
        _transactionRepository = transactionRepository;
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
            var result = new IntegrationResult(context.LocatedTrx);
            result.NotificationResponse = await integration.GetNotificationResponseAsync(context, cancellationToken);
            return result;
        }

        if (!context.Terminal!.IsActive || !context.DebitCompany!.IsActive)
        {
            var result = new IntegrationResult
            {
                Code = "592",
                Message = "Terminal or Processor deactivated",
                DebitRefCode = context.DebitRefCode
            };

            var moveResult = await _transactionRepository.MoveTrxAsync(context.LocatedTrx.TrxId, result.Code,
                result.Message, context.LocatedTrx.BinCountry);

            result.TrxId = moveResult.TrxId;
            result.TrxType = moveResult.PendingTrx.TransType;

            await _transactionRepository.UpdateChargeAttemptAsync(context.LocatedTrx.TrxId, moveResult.TrxId,
                result.Code, result.Message);

            return result;
        }

        var processResult = await integration.ProcessTransactionAsync(context, cancellationToken);

        processResult.DebitRefCode = context.DebitRefCode;
        processResult.ApprovalNumber = context.LocatedTrx?.ApprovalNumber ?? processResult.ApprovalNumber;
        if (string.IsNullOrWhiteSpace(processResult.RedirectUrl))
            processResult.RedirectUrl = context.LocatedTrx?.RedirectUrl;
        processResult.DebitRefNum = context.DebitRefNum;
        if (string.IsNullOrWhiteSpace(processResult.NotificationResponse))
            processResult.NotificationResponse = "ok";

        return processResult;
    }

    private async Task<IntegrationResult> InternalProcessTransactionAsync(
        TransactionContext context,
        ICreditCardIntegration integration,
        CancellationToken cancellationToken)
    {
        var result = await integration.ProcessTransactionAsync(context, cancellationToken);
        result.DebitRefCode = context.DebitRefCode;
        result.DebitRefNum = context.DebitRefNum;

        if ((context.OpType == OperationType.AuthorizationCapture ||
             context.OpType == OperationType.AuthorizationRelease) && result.Code == "000")
        {
            // TODO: Implement pre-auth transaction status update
            // This would require access to the data context to update tblCompanyTransApprovals
            // For now, we'll log the successful operation
        }

        return result;
    }
}