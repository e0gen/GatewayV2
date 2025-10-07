using Microsoft.EntityFrameworkCore;
using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Ef.Context;
using Ezzygate.Infrastructure.Mappings;
using Ezzygate.Infrastructure.Repositories.Interfaces;

namespace Ezzygate.Infrastructure.Repositories;

public class TerminalRepository : ITerminalRepository
{
    private readonly EzzygateDbContext _context;

    public TerminalRepository(EzzygateDbContext context)
    {
        _context = context;
    }

    public async Task<Terminal?> GetByTerminalNumberAsync(string terminalNumber)
    {
        var entity = await _context.TblDebitTerminals
            .FirstOrDefaultAsync(t => t.TerminalNumber == terminalNumber);

        return entity?.ToDomain();
    }

    public async Task<Terminal?> GetByIdAsync(int terminalId)
    {
        var entity = await _context.TblDebitTerminals
            .FirstOrDefaultAsync(t => t.Id == terminalId);

        return entity?.ToDomain();
    }

    public async Task<DebitCompany?> GetDebitCompanyByIdAsync(byte debitCompanyId)
    {
        var entity = await _context.TblDebitCompanies
            .FirstOrDefaultAsync(d => d.DebitCompanyId == debitCompanyId);

        return entity?.ToDomain();
    }
}