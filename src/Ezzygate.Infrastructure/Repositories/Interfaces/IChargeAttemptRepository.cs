using System.Linq.Expressions;
using Ezzygate.Domain.Models;

namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface IChargeAttemptRepository
{
    Task<ChargeAttempt?> GetByIdAsync(int logChargeAttemptId);

    Task<ChargeAttempt?> GetByTransactionIdAsync(int transactionId);

    Task<bool> UpdateAsync(
        Expression<Func<int, bool>> byId,
        Action<ChargeAttemptUpdate> configure,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateByTransactionAsync(
        Expression<Func<int?, string?, bool>> byTransaction,
        Action<ChargeAttemptUpdate> configure,
        CancellationToken cancellationToken = default);
}