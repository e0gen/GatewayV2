using Ezzygate.Domain.Models;

namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface IMerchantRepository
{
    Task<Merchant?> GetByCustomerNumberAsync(string customerNumber);
    Task<Merchant?> GetByIdAsync(int merchantId);
    Task<Account?> GetAccountByIdAsync(int accountId);
} 