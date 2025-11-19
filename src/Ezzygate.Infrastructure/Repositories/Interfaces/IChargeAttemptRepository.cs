using System.Threading;
using Ezzygate.Domain.Models;

namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface IChargeAttemptRepository
{
    Task<ChargeAttempt?> GetByIdAsync(int logChargeAttemptId);
    Task<bool> UpdateRedirectFlagAsync(int logChargeAttemptId, bool redirectFlag, CancellationToken cancellationToken = default);
}