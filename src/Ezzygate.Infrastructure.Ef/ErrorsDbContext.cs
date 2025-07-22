using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef;

public class ErrorsDbContext : DbContext
{
    public ErrorsDbContext(DbContextOptions<ErrorsDbContext> options) : base(options)
    {
    }

    // DbSets for error logging entities will be added here
    // Example:
    // public DbSet<ErrorLog> ErrorLogs { get; set; } = null!;
    // public DbSet<BllLog> BllLogs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Error logging entity configurations will be added here
    }
} 