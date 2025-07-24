using Microsoft.EntityFrameworkCore;
using Ezzygate.Domain.Entities.Core;

namespace Ezzygate.Infrastructure.Ef.Configurations;

public static class CustomerConfiguration
{
    public static void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .HasOne(c => c.ActiveStatus)
            .WithMany(aStatus => aStatus.Customers)
            .HasForeignKey(c => c.ActiveStatusId)
            .HasConstraintName("FK_Customer_ActiveStatus");

        modelBuilder.Entity<Customer>()
            .HasOne(c => c.Account)
            .WithOne()
            .HasForeignKey<Customer>(c => c.AccountId)
            .HasConstraintName("FK_Customer_Account_AccountID");
    }
}