using Ezzygate.Domain.Models;

namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface IChargeAttemptRepository
{
    Task<ChargeAttempt?> GetByIdAsync(int logChargeAttemptId);

    Task<bool> UpdateRedirectFlagAsync(int logChargeAttemptId, bool redirectFlag,
        CancellationToken cancellationToken = default);

    Task UpdatePendingChargeAttemptAsync(int pendingTrxId, int movedTrxId, string replyCode, string replyDescription,
        CancellationToken cancellationToken = default);

    Task UpdateInnerLogsAsync(int logChargeAttemptId, string? innerRequest, string? innerResponse,
        CancellationToken cancellationToken = default);
}