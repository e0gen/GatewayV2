using Microsoft.EntityFrameworkCore;
using Ezzygate.Domain.Entities.Core;

namespace Ezzygate.Infrastructure.Data.Configurations;

public static class AccountConfiguration
{
    public static void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .HasOne(a => a.AccountType)
            .WithMany(at => at.Accounts)
            .HasForeignKey(a => a.AccountTypeId)
            .HasConstraintName("FK_Account_AccountType_AccountTypeID");

        modelBuilder.Entity<Account>()
            .HasOne(a => a.Customer)
            .WithMany()
            .HasForeignKey(a => a.CustomerId)
            .HasConstraintName("FK_Account_Customer_CustomerID");

        modelBuilder.Entity<Account>()
            .HasOne(a => a.LoginAccount)
            .WithMany(la => la.Accounts)
            .HasForeignKey(a => a.LoginAccountId)
            .HasConstraintName("FK_Account_LoginAccount_LoginAccountID");

        modelBuilder.Entity<Account>()
            .HasOne(a => a.PersonalAddress)
            .WithMany(aa => aa.AccountsAsPersonalAddress)
            .HasForeignKey(a => a.PersonalAddressId)
            .HasConstraintName("FK_Account_AccountAddress_PersonalAddressID");

        modelBuilder.Entity<Account>()
            .HasOne(a => a.BusinessAddress)
            .WithMany(aa => aa.AccountsAsBusinessAddress)
            .HasForeignKey(a => a.BusinessAddressId)
            .HasConstraintName("FK_Account_AccountAddress_BusinessAddressID");

        modelBuilder.Entity<Account>()
            .HasOne(a => a.DefaultCurrency)
            .WithMany(c => c.Accounts)
            .HasForeignKey(a => a.DefaultCurrencyISOCode)
            .HasConstraintName("FK_Account_DefaultCurrencyISOCode");
    }
} 