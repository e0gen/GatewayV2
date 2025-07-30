using Ezzygate.Domain.Models;

namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface IMerchantRepository
{
    Task<Merchant?> GetByMerchantNumberAsync(string customerNumber);
    Task<Merchant?> GetByIdAsync(int merchantId);
    Task<Account?> GetAccountByIdAsync(int accountId);
} 