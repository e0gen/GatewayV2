namespace Ezzygate.Infrastructure.Locking;

public interface IDistributedLock : IAsyncDisposable
{
    string LockKey { get; }
    bool IsAcquired { get; }
    Task ReleaseAsync(CancellationToken cancellationToken = default);
}