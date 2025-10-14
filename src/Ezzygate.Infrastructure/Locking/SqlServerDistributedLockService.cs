using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ezzygate.Infrastructure.Ef.Context;

namespace Ezzygate.Infrastructure.Locking;

public class SqlServerDistributedLockService : DatabaseDistributedLockService
{
    public SqlServerDistributedLockService(
        IDbContextFactory<EzzygateDbContext> contextFactory,
        ILogger<SqlServerDistributedLockService> logger)
        : base(contextFactory, logger)
    {
    }

    protected override string GetUpdateLockSql()
    {
        return @"
            UPDATE [System].[TaskLock] 
            SET IsTaksRunning = 1, 
                LastRunDate = GETUTCDATE(), 
                MachineName = {1}
            WHERE TaskName = {0} 
            AND (IsTaksRunning = 0 OR LastRunDate < {2})";
    }

    protected override string GetInsertLockSql()
    {
        return @"
            INSERT INTO [System].[TaskLock] (TaskName, IsTaksRunning, LastRunDate, MachineName)
            SELECT {0}, 1, GETUTCDATE(), {1}
            WHERE NOT EXISTS (SELECT 1 FROM [System].[TaskLock] WHERE TaskName = {0})";
    }

    protected override string GetReleaseLockSql()
    {
        return @"
            UPDATE [System].[TaskLock] 
            SET IsTaksRunning = 0 
            WHERE TaskName = {0} 
            AND IsTaksRunning = 1 
            AND MachineName = {1}";
    }
}