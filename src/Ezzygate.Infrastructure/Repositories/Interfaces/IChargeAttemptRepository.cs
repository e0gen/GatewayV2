using Ezzygate.Domain.Models;

namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface IChargeAttemptRepository
{
    Task<ChargeAttempt?> GetByIdAsync(int logChargeAttemptId);
}