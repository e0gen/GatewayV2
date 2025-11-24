namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface IEventPendingRepository
{
    Task DeleteByPendingTransactionIdAsync(int pendingTrxId, CancellationToken cancellationToken = default);

    Task CreateFeesEventAsync(int? transPassId, int? transPreAuthId, int? transFailId,
        CancellationToken cancellationToken = default);

    Task CreateInstallmentsAlertEventAsync(int? transPassId, int? transPreAuthId,
        CancellationToken cancellationToken = default);

    Task CreateClientNotificationEventAsync(int? transPassId, int? transPreAuthId,
        CancellationToken cancellationToken = default);

    Task CreateMerchantNotificationEventAsync(int? transPassId, int? transPreAuthId, int? transFailId,
        CancellationToken cancellationToken = default);
}