using Microsoft.EntityFrameworkCore;
using Ezzygate.Infrastructure.Ef.Context;
using Ezzygate.Infrastructure.Repositories.Interfaces;

namespace Ezzygate.Infrastructure.Repositories;

public class PaymentPageSettingsRepository : IPaymentPageSettingsRepository
{
    private readonly EzzygateDbContext _context;

    public PaymentPageSettingsRepository(EzzygateDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsTipAllowedAsync(int companyId, CancellationToken cancellationToken = default)
    {
        var settings = await _context.TblCompanySettingsHosteds
            .AsNoTracking()
            .Where(s => s.CompanyId == companyId)
            .Select(s => new { s.IsTipAllowed })
            .FirstOrDefaultAsync(cancellationToken);

        return settings?.IsTipAllowed ?? false;
    }
}