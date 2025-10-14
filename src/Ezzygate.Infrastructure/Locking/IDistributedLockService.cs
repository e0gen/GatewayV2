namespace Ezzygate.Infrastructure.Locking;

public interface IDistributedLockService
{
    Task<IDistributedLock> AcquireLockAsync(
        string lockKey,
        int estimatedExecutionMinutes = 5,
        int secondsToAbort = 10,
        CancellationToken cancellationToken = default);

    Task<IDistributedLock?> TryAcquireLockAsync(
        string lockKey,
        int estimatedExecutionMinutes = 5,
        CancellationToken cancellationToken = default);
}