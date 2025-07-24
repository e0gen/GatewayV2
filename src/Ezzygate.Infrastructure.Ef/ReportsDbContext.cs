using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef;

public class ReportsDbContext : DbContext
{
    public ReportsDbContext(DbContextOptions<ReportsDbContext> options) : base(options)
    {
    }

    // DbSets for reports entities will be added here
    // Example:
    // public DbSet<DenormalizedTransaction> DenormalizedTransactions { get; set; } = null!;
    // public DbSet<PackageLog> PackageLogs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Reports entity configurations will be added here
    }
}