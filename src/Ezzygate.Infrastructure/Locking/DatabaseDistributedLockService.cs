using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ezzygate.Infrastructure.Ef.Context;

namespace Ezzygate.Infrastructure.Locking;

public abstract class DatabaseDistributedLockService : IDistributedLockService
{
    private readonly IDbContextFactory<EzzygateDbContext> _contextFactory;
    private readonly ILogger _logger;

    protected DatabaseDistributedLockService(
        IDbContextFactory<EzzygateDbContext> contextFactory,
        ILogger logger)
    {
        _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected abstract string GetUpdateLockSql();
    protected abstract string GetInsertLockSql();
    protected abstract string GetReleaseLockSql();

    public async Task<IDistributedLock> AcquireLockAsync(
        string lockKey,
        int estimatedExecutionMinutes = 5,
        int secondsToAbort = 10,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(lockKey);

        var endTime = DateTime.UtcNow.AddSeconds(secondsToAbort);
        var attempts = 0;

        while (DateTime.UtcNow < endTime)
        {
            attempts++;
            var distributedLock = await TryAcquireLockAsync(lockKey, estimatedExecutionMinutes, cancellationToken);

            if (distributedLock != null)
            {
                _logger.LogDebug("Lock '{LockKey}' acquired after {Attempts} attempts", lockKey, attempts);
                return distributedLock;
            }

            await Task.Delay(1000, cancellationToken);
        }

        var message = $"Unable to obtain lock for '{lockKey}' after {attempts} attempts ({secondsToAbort}s timeout)";
        throw new DistributedLockException(lockKey, message);
    }

    public async Task<IDistributedLock?> TryAcquireLockAsync(
        string lockKey,
        int estimatedExecutionMinutes = 5,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(lockKey);

        try
        {
            var acquired = await TryLockInternalAsync(lockKey, estimatedExecutionMinutes, cancellationToken);

            return acquired ? new DatabaseDistributedLock(lockKey, ReleaseLockInternalAsync) : null;
        }
        catch (Exception ex)
        {
            throw new DistributedLockException(lockKey, $"Error while trying to acquire lock '{lockKey}'", ex);
        }
    }

    private async Task<bool> TryLockInternalAsync(
        string lockKey,
        int estimatedExecutionMinutes,
        CancellationToken cancellationToken)
    {
        var machineName = TruncateString(Environment.MachineName, 50);
        lockKey = TruncateString(lockKey, 100);
        var minActiveValue = DateTime.UtcNow.AddMinutes(-estimatedExecutionMinutes);

        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var rowsUpdated = await context.Database.ExecuteSqlRawAsync(
            GetUpdateLockSql(),
            lockKey,
            machineName,
            minActiveValue);

        if (rowsUpdated > 0)
        {
            _logger.LogDebug("Lock '{LockKey}' acquired by updating existing record on machine '{MachineName}'",
                lockKey, machineName);
            return true;
        }

        try
        {
            var rowsInserted = await context.Database.ExecuteSqlRawAsync(
                GetInsertLockSql(),
                lockKey,
                machineName);

            if (rowsInserted > 0)
            {
                _logger.LogDebug("Lock '{LockKey}' acquired by inserting new record on machine '{MachineName}'",
                    lockKey, machineName);
                return true;
            }
        }
        catch (DbUpdateException)
        {
            _logger.LogDebug("Lock '{LockKey}' insert failed - likely race condition", lockKey);
            return false;
        }

        return false;
    }

    private async Task<bool> ReleaseLockInternalAsync(string lockKey, CancellationToken cancellationToken)
    {
        var machineName = TruncateString(Environment.MachineName, 50);
        lockKey = TruncateString(lockKey, 100);

        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var rowsUpdated = await context.Database.ExecuteSqlRawAsync(
            GetReleaseLockSql(),
            lockKey,
            machineName);

        if (rowsUpdated == 0)
        {
            _logger.LogWarning(
                "Task release called for unlocked/non-existent task '{LockKey}' on machine '{MachineName}'",
                lockKey, machineName);
            return false;
        }

        _logger.LogDebug("Lock '{LockKey}' released on machine '{MachineName}'", lockKey, machineName);
        return true;
    }

    private static string TruncateString(string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        return value.Length <= maxLength ? value : value[..maxLength];
    }
}