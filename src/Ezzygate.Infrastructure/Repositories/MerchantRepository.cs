using Microsoft.EntityFrameworkCore;
using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Ef.Context;
using Ezzygate.Infrastructure.Repositories.Interfaces;

namespace Ezzygate.Infrastructure.Repositories;

public class MerchantRepository : IMerchantRepository
{
    private readonly EzzygateDbContext _context;

    public MerchantRepository(EzzygateDbContext context)
    {
        _context = context;
    }

    public async Task<Merchant?> GetByMerchantNumberAsync(string customerNumber)
    {
        var entity = await _context.TblCompanies
            .Include(c => c.Account)
            .FirstOrDefaultAsync(c => c.CustomerNumber == customerNumber);

        if (entity == null) return null;

        return new Merchant
        {
            Id = entity.Id,
            CustomerNumber = entity.CustomerNumber,
            CompanyName = entity.CompanyName,
            HashKey = entity.HashKey,
            AccountId = entity.AccountId,
            IsActive = entity.ActiveStatus == 1, // Assuming 1 means active
            Account = entity.Account != null ? new Account
            {
                AccountId = entity.Account.AccountId,
                AccountNumber = entity.Account.AccountNumber,
                Name = entity.Account.Name,
                HashKey = entity.Account.HashKey
            } : null
        };
    }

    public async Task<Merchant?> GetByIdAsync(int merchantId)
    {
        var entity = await _context.TblCompanies
            .Include(c => c.Account)
            .FirstOrDefaultAsync(c => c.Id == merchantId);

        if (entity == null) return null;

        return new Merchant
        {
            Id = entity.Id,
            CustomerNumber = entity.CustomerNumber,
            CompanyName = entity.CompanyName,
            HashKey = entity.HashKey,
            AccountId = entity.AccountId,
            IsActive = entity.ActiveStatus == 1, // Assuming 1 means active
            Account = entity.Account != null ? new Account
            {
                AccountId = entity.Account.AccountId,
                AccountNumber = entity.Account.AccountNumber,
                Name = entity.Account.Name,
                HashKey = entity.Account.HashKey
            } : null
        };
    }

    public async Task<Account?> GetAccountByIdAsync(int accountId)
    {
        var entity = await _context.Accounts
            .FirstOrDefaultAsync(a => a.AccountId == accountId);

        if (entity == null) return null;

        return new Account
        {
            AccountId = entity.AccountId,
            AccountNumber = entity.AccountNumber,
            Name = entity.Name,
            HashKey = entity.HashKey
        };
    }
} 