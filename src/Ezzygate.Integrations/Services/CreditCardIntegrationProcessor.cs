using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Services;
using Ezzygate.Integrations.Abstractions;
using Ezzygate.WebApi.Models.Integration;

namespace Ezzygate.Integrations.Services;

public class CreditCardIntegrationProcessor : ICreditCardIntegrationProcessor
{
    private readonly ILogger<CreditCardIntegrationProcessor> _logger;
    private readonly IIntegrationProvider _integrationProvider;

    public CreditCardIntegrationProcessor(
        ILogger<CreditCardIntegrationProcessor> logger,
        IIntegrationProvider integrationProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _integrationProvider = integrationProvider ?? throw new ArgumentNullException(nameof(integrationProvider));
    }

    public async Task<IntegrationResult> ProcessTransactionAsync(TransactionContext context,
        CancellationToken cancellationToken = default)
    {
        var integrationTag = context.DebitCompany?.IntegrationTag;
        var integration = _integrationProvider.GetCreditCardIntegration(integrationTag);
        if (integration == null)
        {
            _logger.LogError("Integration provider not found for tag: {IntegrationTag}", integrationTag);
            return new IntegrationResult
            {
                Code = "503",
                Message = "No integration provider configured",
                DebitRefCode = context.DebitRefCode
            };
        }

        var result = context.OpType == OperationType.Finalize
            ? await InternalFinalizeTransactionAsync(context, integration, cancellationToken)
            : await InternalProcessTransactionAsync(context, integration, cancellationToken);

        result.CustomParams ??= new object();

        _logger.LogInformation("Credit card transaction processed. Code: {Code}, DebitRefCode: {DebitRefCode}",
            result.Code, result.DebitRefCode);

        return result;
    }

    private async Task<IntegrationResult> InternalFinalizeTransactionAsync(
        TransactionContext context,
        ICreditCardIntegration integration,
        CancellationToken cancellationToken)
    {
        if (context.LocatedTrx is not null && context.LocatedTrx.IsFinalized)
        {
            var result = new IntegrationResult(context.LocatedTrx);
            result.NotificationResponse = await integration.GetNotificationResponseAsync(context, cancellationToken);
            return result;
        }

        if (!context.Terminal!.IsActive || !context.DebitCompany!.IsActive)
        {
            //TODO MoveTrx(ctx.LocatedTrx.TrxId, result.Code, result.Message, ctx.LocatedTrx.BinCountry, out var pendingTrx);
            //TODO Get movedTrxId and pendingTrx
            var movedTrxId = 0;
            var transType = 0;

            var result = new IntegrationResult
            {
                Code = "592",
                Message = "Terminal or Processor deactivated",
                DebitRefCode = context.DebitRefCode,
                TrxId = movedTrxId, //movedTrxId;
                TrxType = transType //pendingTrx.trans_type;
            };

            //TODO UpdateChargeAttempt(ctx.LocatedTrx.TrxId, movedTrxId, result.Code, result.Message);

            _logger.LogWarning("Terminal or processor deactivated for DebitRefCode: {DebitRefCode}",
                context.DebitRefCode);
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