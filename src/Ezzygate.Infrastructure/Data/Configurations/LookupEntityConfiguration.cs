using Microsoft.EntityFrameworkCore;
using Ezzygate.Domain.Entities.Lookup;

namespace Ezzygate.Infrastructure.Data.Configurations;

public static class LookupEntityConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        ConfigureTableNames(modelBuilder);
        ConfigurePrimaryKeys(modelBuilder);
        ConfigureForeignKeys(modelBuilder);
        ConfigureRelationships(modelBuilder);
    }

    private static void ConfigureTableNames(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountType>().ToTable("AccountType", "List");
        modelBuilder.Entity<ActiveStatus>().ToTable("ActiveStatus", "List");
        modelBuilder.Entity<LoginRole>().ToTable("LoginRole", "List");
        modelBuilder.Entity<CurrencyList>().ToTable("CurrencyList", "List");
        modelBuilder.Entity<CountryList>().ToTable("CountryList", "List");
        modelBuilder.Entity<StateList>().ToTable("StateList", "List");
        modelBuilder.Entity<BalanceSourceType>().ToTable("BalanceSourceType", "List");
        modelBuilder.Entity<PaymentMethod>().ToTable("PaymentMethod", "List");
        modelBuilder.Entity<PaymentMethodProvider>().ToTable("PaymentMethodProvider", "List");
    }

    private static void ConfigurePrimaryKeys(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountType>().Property(e => e.AccountTypeId).HasColumnName("AccountType_id");
        modelBuilder.Entity<ActiveStatus>().Property(e => e.ActiveStatusId).HasColumnName("ActiveStatus_id");
        modelBuilder.Entity<LoginRole>().Property(e => e.LoginRoleId).HasColumnName("LoginRole_id");
        modelBuilder.Entity<CurrencyList>().Property(e => e.CurrencyISOCode).HasColumnName("CurrencyISOCode");
        modelBuilder.Entity<CountryList>().Property(e => e.CountryISOCode).HasColumnName("CountryISOCode");
        modelBuilder.Entity<StateList>().Property(e => e.StateISOCode).HasColumnName("StateISOCode");
        modelBuilder.Entity<BalanceSourceType>().Property(e => e.BalanceSourceTypeId).HasColumnName("BalanceSourceType_id");
        modelBuilder.Entity<PaymentMethod>().Property(e => e.PaymentMethodId).HasColumnName("PaymentMethod_id");
        modelBuilder.Entity<PaymentMethodProvider>().Property(e => e.PaymentMethodProviderId).HasColumnName("PaymentMethodProvider_id");
    }

    private static void ConfigureForeignKeys(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StateList>().Property(e => e.CountryISOCode).HasColumnName("CountryISOCode");
    }

    private static void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StateList>()
            .HasOne(s => s.Country)
            .WithMany(c => c.States)
            .HasForeignKey(s => s.CountryISOCode)
            .HasConstraintName("FK_StateList_CountryList_CountryISOCode");
    }
} 