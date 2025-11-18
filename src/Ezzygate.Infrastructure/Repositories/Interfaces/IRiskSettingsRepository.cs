using Ezzygate.Domain.Models;

namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface IRiskSettingsRepository
{
    Task<RiskSettings?> GetByMerchantIdAsync(int merchantId);
}