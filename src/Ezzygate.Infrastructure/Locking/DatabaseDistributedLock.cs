namespace Ezzygate.Infrastructure.Locking;

internal sealed class DatabaseDistributedLock : IDistributedLock
{
    private readonly Func<string, CancellationToken, Task<bool>> _releaseAction;
    private bool _disposed;

    public string LockKey { get; }
    public bool IsAcquired { get; private set; }

    public DatabaseDistributedLock(string lockKey, Func<string, CancellationToken, Task<bool>> releaseAction)
    {
        LockKey = lockKey ?? throw new ArgumentNullException(nameof(lockKey));
        _releaseAction = releaseAction ?? throw new ArgumentNullException(nameof(releaseAction));
        IsAcquired = true;
    }

    public async Task ReleaseAsync(CancellationToken cancellationToken = default)
    {
        if (!IsAcquired || _disposed) return;

        try
        {
            await _releaseAction(LockKey, cancellationToken);
            IsAcquired = false;
        }
        catch (Exception ex)
        {
            throw new DistributedLockException(LockKey, $"Failed to release lock '{LockKey}'", ex);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;

        await ReleaseAsync();
        _disposed = true;
    }
}

