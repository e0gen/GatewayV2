using Microsoft.EntityFrameworkCore;
using Ezzygate.Infrastructure.Ef.Entities;

namespace Ezzygate.Infrastructure.Ef.Context;

public class ErrorsDbContext : DbContext
{
    public ErrorsDbContext(DbContextOptions<ErrorsDbContext> options)
        : base(options)
    {
    }

    public DbSet<BllLog> BllLogs { get; set; }
    public DbSet<ErrorNet> ErrorNets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BllLog>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Severity).HasConversion<byte>();
        });

        modelBuilder.Entity<ErrorNet>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ErrorTime).HasDefaultValueSql("(getdate())");
        });
    }
}