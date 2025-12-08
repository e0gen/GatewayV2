using Microsoft.Extensions.Logging;
using Ezzygate.Application.Integrations;
using Ezzygate.Application.Transactions;
using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Logging;
using Ezzygate.Infrastructure.Notifications;
using Ezzygate.Infrastructure.Repositories.Interfaces;
using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Integrations.Core.Abstractions;

namespace Ezzygate.Integrations.Core;

public abstract class BaseIntegration : IIntegration
{
    protected readonly ILogger<BaseIntegration> Logger;
    protected readonly IIntegrationDataService DataService;
    private readonly INotificationClient _notificationClient;

    protected BaseIntegration(
        ILogger<BaseIntegration> logger,
        IIntegrationDataService dataService,
        INotificationClient notificationClient)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        DataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        _notificationClient = notificationClient ?? throw new ArgumentNullException(nameof(notificationClient));
    }

    public abstract string Tag { get; }

    public abstract Task<IntegrationResult> ProcessTransactionAsync(TransactionContext ctx,
        CancellationToken cancellationToken = default);

    public virtual Task<string> GetNotificationResponseAsync(TransactionContext context,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult("ok");
    }

    public virtual Task MaintainAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    protected static void ValidateDebitCompanyId(TransactionContext ctx, int debitCompanyId)
    {
        if (ctx.DebitCompany?.Id != debitCompanyId)
            throw new Exception("Invalid debit company");
    }

    protected async Task<MoveTransactionResult> CompleteFinalizationAsync(TransactionContext ctx,
        IntegrationResult result, CancellationToken cancellationToken = default)
    {
        var pendingTrx = await DataService.Transactions.GetPendingTrxByIdAsync(ctx.LocatedTrx.TrxId, cancellationToken);
        if (pendingTrx is null)
            throw new Exception($"Finalize pending trx '{ctx.LocatedTrx.TrxId}' not found");

        var moveResult = await DataService.TransactionService.MoveTrxAsync(
            ctx.LocatedTrx.TrxId,
            result.Code.NotNull(),
            result.Message,
            ctx.LocatedTrx.BinCountry,
            cancellationToken);

        result.TrxId = moveResult.TrxId;
        result.TrxType = moveResult.PendingTrx.TransType;
        result.CardStorageId = moveResult.PendingTrx.CcStorageId;
        result.ApprovalNumber ??= moveResult.PendingTrx.DebitApprovalNumber;

        await SendNotificationAsync(moveResult.PendingTrx, result, cancellationToken);

        await DataService.ChargeAttempts
            .UpdateByTransactionAsync((trxId, reply) => trxId == ctx.LocatedTrx.TrxId && reply == "553", u => u
                .SetTransaction(moveResult.TrxId, result.Code, result.Message), cancellationToken);

        await UpdatePendingEventsAsync(moveResult.TrxId, result.Code, moveResult.PendingTrx.Id,
            moveResult.PendingTrx.TransType, moveResult.PendingTrx.TransCreditType, moveResult.PendingTrx.CompanyId);

        if (ctx.IsAutomatedRequest)
        {
            await DataService.ChargeAttempts
                .UpdateAsync(id => id == ctx.ChargeAttemptLogId, u => u
                    .SetRedirectFlag(false), cancellationToken);
        }

        return moveResult;
    }

    protected async Task<IntegrationResult> AutoFinalizeTrxAsync(TransactionContext ctx,
        CancellationToken cancellationToken = default)
    {
        using var logger = Logger.GetScopedForIntegration(Tag, nameof(AutoFinalizeTrxAsync));

        var log = await DataService.ChargeAttempts.GetByIdAsync(ctx.ChargeAttemptLogId);
        if (log is null)
            throw new Exception($"Charge attempt log not found for id '{ctx.ChargeAttemptLogId}'");

        await DataService.ChargeAttempts
            .UpdateAsync(id => id == ctx.ChargeAttemptLogId, u => u
                .SetInnerRequest($"{ctx.AutomatedStatus},{log.InnerRequest}")
                .SetInnerResponse($"{ctx.AutomatedStatus},{log.InnerResponse}"), cancellationToken);

        var integrationResult = ctx.GetIntegrationResult();
        integrationResult.Code = ctx.AutomatedCode;
        integrationResult.Message = ctx.AutomatedMessage;

        await CompleteFinalizationAsync(ctx, integrationResult, cancellationToken);

        logger.SetShortMessage(
            $"Status: {ctx.AutomatedStatus} Approval Number: {integrationResult.ApprovalNumber} Pass/Fail Id: {integrationResult.TrxId} Code: {integrationResult.Code}");

        return integrationResult;
    }

    protected async Task UpdatePendingEventsAsync(int movedTrxId, string replyCode, int pendingTrxId,
        int pendingTrxType, int pendingTrxCreditType, int merchantId)
    {
        await DataService.EventPendings.DeleteByPendingTransactionIdAsync(pendingTrxId);

        int? transPassId = null;
        int? transPreAuthId = null;
        int? transFailId = null;

        switch (replyCode)
        {
            case "000" when pendingTrxType == 0 || pendingTrxType == 3:
                transPassId = movedTrxId;
                break;
            case "000" when pendingTrxType == 1:
                transPreAuthId = movedTrxId;
                break;
            case "000":
                throw new Exception($"Invalid trans type '{pendingTrxType}'");
            case "553":
                return;
            default:
                transFailId = movedTrxId;
                break;
        }

        await DataService.EventPendings.CreateFeesEventAsync(transPassId, transPreAuthId, transFailId);

        if (pendingTrxCreditType == (int)CreditType.Installments && replyCode == "000")
            await DataService.EventPendings.CreateInstallmentsAlertEventAsync(transPassId, transPreAuthId);

        var merchant = await DataService.Merchants.GetByIdAsync(merchantId);
        if (merchant == null) return;

        if (replyCode == "000")
        {
            if (merchant.IsSendUserConfirmationEmail)
                await DataService.EventPendings.CreateClientNotificationEventAsync(transPassId, transPreAuthId);

            if (merchant.IsMerchantNotifiedOnPass)
                await DataService.EventPendings.CreateMerchantNotificationEventAsync(transPassId, transPreAuthId, null);
        }
        else
        {
            if (merchant.IsMerchantNotifiedOnFail)
                await DataService.EventPendings.CreateMerchantNotificationEventAsync(null, null, transFailId);
        }
    }

    protected async Task SendNotificationAsync(PendingTransaction pendingTrx, IntegrationResult integrationResult, CancellationToken cancellationToken = default)
    {
        var notificationUrl = await DataService.CompanyChargeAdmins.GetNotifyProcessUrlAsync(pendingTrx.CompanyId);

        if (string.IsNullOrWhiteSpace(notificationUrl))
            return;

        var merchant = await DataService.Merchants.GetByIdAsync(pendingTrx.CompanyId);
        var merchantHashCode = merchant?.HashKey ?? string.Empty;

        var ccType = pendingTrx.PaymentMethod.HasValue
            ? (await DataService.PaymentMethods.GetByIdAsync(pendingTrx.PaymentMethod.Value))?.Abbreviation
            : null;

        var notificationData = new NotificationData
        {
            NotificationUrl = notificationUrl,
            MerchantId = pendingTrx.CompanyId,
            MerchantHashCode = merchantHashCode,
            TrxNum = integrationResult.TrxId,
            TrxType = integrationResult.TrxType,
            Amount = pendingTrx.TransAmount,
            Currency = pendingTrx.Currency,
            Order = pendingTrx.TransOrder,
            Payments = pendingTrx.TransPayments,
            Comment = pendingTrx.Comment,
            ReplyCode = integrationResult.Code,
            ReplyDesc = integrationResult.Message,
            CcType = ccType,
            MovedPendingId = pendingTrx.CompanyTransPendingId,
            ApprovalNumber = pendingTrx.DebitApprovalNumber
        };

        var notificationResult = await _notificationClient.SendNotificationAsync(notificationData);
        if (notificationResult.IsSuccess)
            await DataService.History.CreateAsync(
                40,
                notificationResult.MerchantId,
                notificationResult.TransactionId,
                notificationResult.LogXml,
                cancellationToken);
    }

    protected async Task<int?> CreateRecurringAsync(string resultCode, TransactionContext ctx, int movedTrxId, int pendingTrxType,
        int chargeAttemptLogId, CancellationToken cancellationToken = default)
    {
        if (resultCode != "000")
            return null;

        var chargeAttempt = await DataService.ChargeAttempts.GetByIdAsync(chargeAttemptLogId);
        if (chargeAttempt == null)
            return null;

        var queryString = chargeAttempt.QueryString;
        if (string.IsNullOrWhiteSpace(queryString))
            return null;

        if (!queryString.Contains("recurring", StringComparison.OrdinalIgnoreCase))
            return null;

        var recurringParams = new List<string>();
        var clientIp = string.Empty;
        var comment = string.Empty;

        var splitParams = queryString.Split('|');
        foreach (var splitParam in splitParams)
        {
            var splitValue = splitParam.Split('=');
            var paramName = splitValue[0].ToLowerInvariant();

            if (paramName.Contains("recurring"))
            {
                recurringParams.Add($"{splitParam}&");
                continue;
            }

            if (paramName.Contains("clientip") && splitValue.Length == 2)
            {
                clientIp = splitValue[1];
                continue;
            }

            if (paramName.Contains("comment") && splitValue.Length == 2)
            {
                comment = splitValue[1];
            }
        }

        var sqlParam = string.Join(string.Empty, recurringParams);
        if (string.IsNullOrWhiteSpace(sqlParam.Trim()))
            return null;

        var isPassTrx = pendingTrxType is 0 or 3;
        var isApprovalTrx = pendingTrxType == 1;
        var terminalId = ctx.Terminal!.Id;

        var recurringId = await DataService.Recurring.RecurringPreCreateSeries(
            isPassTrx, movedTrxId, sqlParam, comment, terminalId,
            clientIp, -1, string.Empty, cancellationToken);

        if (recurringId is null or <= 0)
            return null;

        if (isPassTrx)
            await DataService.Transactions.UpdatePassTrxRecurringAsync(
                movedTrxId, recurringId.Value, 1, cancellationToken);
        else if (isApprovalTrx)
            await DataService.Transactions.UpdateApprovalTrxRecurringAsync(
                movedTrxId, recurringId.Value, 1, cancellationToken);

        return recurringId;
    }
}