using Ezzygate.Domain.Models;

namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface IMobileDeviceRepository
{
    Task<MobileDevice?> GetByDeviceIdentityAsync(string deviceIdentity);
    Task UpdateAsync(MobileDevice device);
} 