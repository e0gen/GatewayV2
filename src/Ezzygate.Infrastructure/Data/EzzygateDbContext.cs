using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Data;

public class EzzygateDbContext : DbContext
{
    public EzzygateDbContext(DbContextOptions<EzzygateDbContext> options) : base(options)
    {
    }

    // DbSets will be added here as we migrate entities from the legacy system
    // Example:
    // public DbSet<Account> Accounts { get; set; } = null!;
    // public DbSet<Transaction> Transactions { get; set; } = null!;
    // public DbSet<Merchant> Merchants { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Entity configurations will be added here
        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(EzzygateDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // This will be used for migrations and design-time operations
            // The actual connection string will come from DI in runtime
        }
    }
} 