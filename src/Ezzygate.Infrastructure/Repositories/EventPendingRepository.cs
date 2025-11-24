using Microsoft.EntityFrameworkCore;
using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Ef.Context;
using Ezzygate.Infrastructure.Repositories.Interfaces;

namespace Ezzygate.Infrastructure.Repositories;

public class EventPendingRepository : IEventPendingRepository
{
    private readonly EzzygateDbContext _context;

    public EventPendingRepository(EzzygateDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task DeleteByPendingTransactionIdAsync(int pendingTrxId, CancellationToken cancellationToken = default)
    {
        var events = await _context.EventPendings
            .Where(e => e.TransPendingId == pendingTrxId)
            .ToListAsync(cancellationToken);

        _context.EventPendings.RemoveRange(events);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateFeesEventAsync(int? transPassId, int? transPreAuthId, int? transFailId,
        CancellationToken cancellationToken = default)
    {
        var eventPending = new Ef.Entities.EventPending
        {
            EventPendingTypeId = (short)PendingEventType.FeesTransaction,
            InsertDate = DateTime.UtcNow,
            Parameters = string.Empty,
            TryCount = 3,
            TransPassId = transPassId,
            TransPreAuthId = transPreAuthId,
            TransFailId = transFailId
        };

        _context.EventPendings.Add(eventPending);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateInstallmentsAlertEventAsync(int? transPassId, int? transPreAuthId,
        CancellationToken cancellationToken = default)
    {
        var eventPending = new Ef.Entities.EventPending
        {
            EventPendingTypeId = (short)PendingEventType.GenerateInstallments,
            InsertDate = DateTime.UtcNow,
            Parameters = string.Empty,
            TryCount = 3,
            TransPassId = transPassId,
            TransPreAuthId = transPreAuthId
        };

        _context.EventPendings.Add(eventPending);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateClientNotificationEventAsync(int? transPassId, int? transPreAuthId,
        CancellationToken cancellationToken = default)
    {
        var eventPending = new Ef.Entities.EventPending
        {
            EventPendingTypeId = (short)PendingEventType.InfoEmailSendClient,
            InsertDate = DateTime.UtcNow,
            Parameters = string.Empty,
            TryCount = 3,
            TransPassId = transPassId,
            TransPreAuthId = transPreAuthId
        };

        _context.EventPendings.Add(eventPending);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateMerchantNotificationEventAsync(int? transPassId, int? transPreAuthId, int? transFailId,
        CancellationToken cancellationToken = default)
    {
        var eventPending = new Ef.Entities.EventPending
        {
            EventPendingTypeId = (short)PendingEventType.InfoEmailSendMerchant,
            InsertDate = DateTime.UtcNow,
            Parameters = string.Empty,
            TryCount = 3,
            TransPassId = transPassId,
            TransPreAuthId = transPreAuthId,
            TransFailId = transFailId
        };

        _context.EventPendings.Add(eventPending);
        await _context.SaveChangesAsync(cancellationToken);
    }
}