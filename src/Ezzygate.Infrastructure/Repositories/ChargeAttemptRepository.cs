using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Ef.Context;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Repositories.Interfaces;

namespace Ezzygate.Infrastructure.Repositories;

public class ChargeAttemptRepository : IChargeAttemptRepository
{
    private readonly EzzygateDbContext _context;

    public ChargeAttemptRepository(EzzygateDbContext context)
    {
        _context = context;
    }

    public async Task<ChargeAttempt?> GetByIdAsync(int logChargeAttemptId)
    {
        var entity = await _context.TblLogChargeAttempts
            .OrderBy(t => t.LogChargeAttemptsId)
            .FirstOrDefaultAsync(l => l.LogChargeAttemptsId == logChargeAttemptId);

        return entity != null ? MapToChargeAttempt(entity) : null;
    }

    public async Task<ChargeAttempt?> GetByTransactionIdAsync(int transactionId)
    {
        var entity = await _context.TblLogChargeAttempts
            .Where(l => l.LcaTransNum == transactionId)
            .OrderBy(t => t.LogChargeAttemptsId)
            .FirstOrDefaultAsync();

        return entity != null ? MapToChargeAttempt(entity) : null;
    }

    public async Task<bool> UpdateAsync(
        Expression<Func<int, bool>> byId,
        Action<ChargeAttemptUpdate> configure,
        CancellationToken cancellationToken = default)
    {
        var update = new ChargeAttemptUpdate();
        configure(update);

        var compiledPredicate = byId.Compile();
        return await UpdateEntityAsync(
            entity => compiledPredicate(entity.LogChargeAttemptsId),
            entity => ApplyUpdates(entity, update),
            cancellationToken,
            update.ThrowIfNotFound,
            update.NotFoundMessage);
    }

    public async Task<bool> UpdateByTransactionAsync(
        Expression<Func<int?, string?, bool>> byTransaction,
        Action<ChargeAttemptUpdate> configure,
        CancellationToken cancellationToken = default)
    {
        var update = new ChargeAttemptUpdate();
        configure(update);

        var compiledPredicate = byTransaction.Compile();
        return await UpdateEntityAsync(
            entity => compiledPredicate(entity.LcaTransNum, entity.LcaReplyCode),
            entity => ApplyUpdates(entity, update),
            cancellationToken,
            update.ThrowIfNotFound,
            update.NotFoundMessage);
    }

    private async Task<bool> UpdateEntityAsync(
        Expression<Func<Ef.Entities.TblLogChargeAttempt, bool>> predicate,
        Action<Ef.Entities.TblLogChargeAttempt> updateAction,
        CancellationToken cancellationToken = default,
        bool throwIfNotFound = false,
        string? notFoundMessage = null)
    {
        var entity = await _context.TblLogChargeAttempts
            .FirstOrDefaultAsync(predicate, cancellationToken);

        if (entity == null)
        {
            if (throwIfNotFound)
                throw new InvalidOperationException(notFoundMessage ?? "Entity not found");
            return false;
        }

        updateAction(entity);
        var rowsAffected = await _context.SaveChangesAsync(cancellationToken);
        return rowsAffected > 0;
    }

    private static void ApplyUpdates(Ef.Entities.TblLogChargeAttempt entity, ChargeAttemptUpdate update)
    {
        if (update.TransactionId.HasValue)
        {
            entity.LcaTransNum = update.TransactionId.Value;
            entity.LcaReplyCode = update.ReplyCode;
            entity.LcaReplyDesc = update.ReplyDescription;
        }

        if (update.RedirectFlag.HasValue)
            entity.IsRedirectApplied = update.RedirectFlag.Value;

        if (update.InnerRequest != null)
            entity.LcaInnerRequest = update.InnerRequest.Truncate(4000);

        if (update.InnerResponse != null)
            entity.LcaInnerResponse = update.InnerResponse.Truncate(4000);
    }

    private static ChargeAttempt MapToChargeAttempt(Ef.Entities.TblLogChargeAttempt entity)
    {
        return new ChargeAttempt
        {
            Id = entity.LogChargeAttemptsId,
            QueryString = entity.LcaQueryString ?? string.Empty,
            RequestForm = entity.LcaRequestForm ?? string.Empty,
            InnerRequest = entity.LcaInnerRequest,
            InnerResponse = entity.LcaInnerResponse
        };
    }
}