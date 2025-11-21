using System;
using Microsoft.Extensions.Logging;
using Ezzygate.Integrations.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using Ezzygate.Application.Integrations;
using Ezzygate.Application.Transactions;
using Ezzygate.Infrastructure.Notifications;
using Ezzygate.Infrastructure.Repositories.Interfaces;
using Ezzygate.Infrastructure.Transactions;

namespace Ezzygate.Integrations.Services;

public abstract class BaseIntegration : IIntegration
{
    protected readonly ILogger<BaseIntegration> Logger;
    protected readonly IIntegrationDataService DataService;
    private readonly NotificationClient _notificationClient;

    protected BaseIntegration(
        ILogger<BaseIntegration> logger,
        IIntegrationDataService dataService,
        NotificationClient notificationClient)
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