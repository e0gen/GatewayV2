using Microsoft.EntityFrameworkCore;
using Ezzygate.Infrastructure.Ef.Context;
using Ezzygate.Infrastructure.Repositories.Interfaces;

namespace Ezzygate.Infrastructure.Repositories;

public class CompanyChargeAdminRepository : ICompanyChargeAdminRepository
{
    private readonly EzzygateDbContext _context;

    public CompanyChargeAdminRepository(EzzygateDbContext context)
    {
        _context = context;
    }

    public async Task<string?> GetNotifyProcessUrlAsync(int companyId)
    {
        return await _context.TblCompanyChargeAdmins
            .Where(admin => admin.CompanyId == companyId)
            .Select(admin => admin.NotifyProcessUrl)
            .SingleOrDefaultAsync();
    }
}

