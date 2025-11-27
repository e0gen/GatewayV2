using Microsoft.Extensions.Logging;
using Ezzygate.Application.Integrations;
using Ezzygate.Application.Transactions;
using Ezzygate.Domain.Enums;
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

    public abstract Task<IntegrationResult> ProcessTransactionAsync(TransactionContext context,
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

    protected void ValidateDebitCompanyId(TransactionContext ctx, int debitCompanyId)
    {
        if (ctx.DebitCompany?.Id != debitCompanyId)
            throw new Exception("Invalid debit company");
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

    protected async Task SendNotificationAsync(PendingTransaction pendingTrx, IntegrationResult integrationResult)
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
                notificationResult.LogXml);
    }
}