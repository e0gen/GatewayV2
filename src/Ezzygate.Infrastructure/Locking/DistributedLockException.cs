namespace Ezzygate.Infrastructure.Locking;

public class DistributedLockException : Exception
{
    public string LockKey { get; }

    public DistributedLockException(string lockKey, string message) : base(message)
    {
        LockKey = lockKey;
    }

    public DistributedLockException(string lockKey, string message, Exception innerException)
        : base(message, innerException)
    {
        LockKey = lockKey;
    }
}