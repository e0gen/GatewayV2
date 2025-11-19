using System.Threading;
using Microsoft.EntityFrameworkCore;
using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Ef.Context;
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

    private ChargeAttempt MapToChargeAttempt(Ef.Entities.TblLogChargeAttempt entity)
    {
        return new ChargeAttempt
        {
            Id = entity.LogChargeAttemptsId,
            QueryString = entity.LcaQueryString ?? string.Empty,
            RequestForm = entity.LcaRequestForm ?? string.Empty,
        };
    }
}