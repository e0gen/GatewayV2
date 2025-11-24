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
            .FirstOrDefaultAsync(l => l.LogChargeAttemptsId == logChargeAttemptId);

        return entity != null ? MapToChargeAttempt(entity) : null;
    }

    public async Task<bool> UpdateRedirectFlagAsync(int logChargeAttemptId, bool redirectFlag,
        CancellationToken cancellationToken = default)
    {
        var entity = await _context.TblLogChargeAttempts
            .FirstOrDefaultAsync(l => l.LogChargeAttemptsId == logChargeAttemptId, cancellationToken);

        if (entity == null) return false;

        entity.IsRedirectApplied = redirectFlag;
        var rowsAffected = await _context.SaveChangesAsync(cancellationToken);
        return rowsAffected > 0;
    }

    public async Task UpdatePendingChargeAttemptAsync(int pendingTrxId, int movedTrxId, string replyCode,
        string replyDescription, CancellationToken cancellationToken = default)
    {
        var chargeLog = await _context.TblLogChargeAttempts
            .SingleOrDefaultAsync(e => e.LcaTransNum == pendingTrxId && e.LcaReplyCode == "553", cancellationToken);
        if (chargeLog == null) return;

        chargeLog.LcaTransNum = movedTrxId;
        chargeLog.LcaReplyCode = replyCode;
        chargeLog.LcaReplyDesc = replyDescription;

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateInnerLogsAsync(int logChargeAttemptId, string? innerRequest, string? innerResponse,
        CancellationToken cancellationToken = default)
    {
        var entity = await _context.TblLogChargeAttempts
            .SingleOrDefaultAsync(l => l.LogChargeAttemptsId == logChargeAttemptId, cancellationToken);

        if (entity == null)
            throw new InvalidOperationException($"Charge attempt log with Id {logChargeAttemptId} not found");

        entity.LcaInnerRequest = innerRequest.Truncate(4000);
        entity.LcaInnerResponse = innerResponse.Truncate(4000);

        await _context.SaveChangesAsync(cancellationToken);
    }

    private ChargeAttempt MapToChargeAttempt(Ef.Entities.TblLogChargeAttempt entity)
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