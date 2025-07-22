using Microsoft.EntityFrameworkCore;
using Ezzygate.Domain.Entities.Core;

namespace Ezzygate.Infrastructure.Ef.Configurations;

public static class LoginAccountConfiguration
{
    public static void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoginAccount>()
            .HasOne(la => la.LoginRole)
            .WithMany(lr => lr.LoginAccounts)
            .HasForeignKey(la => la.LoginRoleId)
            .HasConstraintName("FK_LoginAccount_LoginRole_LoginRoleID");

        modelBuilder.Entity<LoginPassword>()
            .HasOne(lp => lp.LoginAccount)
            .WithMany(la => la.LoginPasswords)
            .HasForeignKey(lp => lp.LoginAccountId)
            .HasConstraintName("FK_LoginPassword_LoginAccountID");
    }
}

public static class AccountAddressConfiguration
{
    public static void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountAddress>()
            .HasOne(aa => aa.Country)
            .WithMany(c => c.AccountAddresses)
            .HasForeignKey(aa => aa.CountryISOCode)
            .HasConstraintName("FK_AccountAddress_CountryList_CountryISOCode");

        modelBuilder.Entity<AccountAddress>()
            .HasOne(aa => aa.State)
            .WithMany(s => s.AccountAddresses)
            .HasForeignKey(aa => aa.StateISOCode)
            .HasConstraintName("FK_AccountAddress_StateList_StateISOCode");
    }
}

public static class AccountBalanceConfiguration
{
    public static void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountBalance>()
            .HasOne(ab => ab.Account)
            .WithMany(a => a.AccountBalances)
            .HasForeignKey(ab => ab.AccountId)
            .HasConstraintName("FK_AccountBalance_AccountID");

        modelBuilder.Entity<AccountBalance>()
            .HasOne(ab => ab.BalanceSourceType)
            .WithMany(bst => bst.AccountBalances)
            .HasForeignKey(ab => ab.BalanceSourceTypeId)
            .HasConstraintName("FK_AccountBalance_BalanceSourceTypeID");

        modelBuilder.Entity<AccountBalance>()
            .HasOne(ab => ab.Currency)
            .WithMany(c => c.AccountBalances)
            .HasForeignKey(ab => ab.CurrencyISOCode)
            .HasConstraintName("FK_AccountBalance_CurrencyISOCode");
    }
}

public static class MobileDeviceConfiguration
{
    public static void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MobileDevice>()
            .HasOne(md => md.Account)
            .WithMany(a => a.MobileDevices)
            .HasForeignKey(md => md.AccountId)
            .HasConstraintName("FK_MobileDevice_Account_AccountID");
    }
}

public static class AccountSubUserConfiguration
{
    public static void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountSubUser>()
            .HasOne(asu => asu.Account)
            .WithMany(a => a.AccountSubUsers)
            .HasForeignKey(asu => asu.AccountId)
            .HasConstraintName("FK_AccountSubUser_Account_AccountID");

        modelBuilder.Entity<AccountSubUser>()
            .HasOne(asu => asu.LoginAccount)
            .WithMany(la => la.AccountSubUsers)
            .HasForeignKey(asu => asu.LoginAccountId)
            .HasConstraintName("FK_AccountSubUser_LoginAccount_LoginAccountID");
    }
}

public static class CartConfiguration
{
    public static void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>()
            .HasOne(c => c.Customer)
            .WithMany(cust => cust.Carts)
            .HasForeignKey(c => c.CustomerId)
            .HasConstraintName("FK_Cart_Customer_CustomerID");
    }
}

public static class CustomerShippingDetailConfiguration
{
    public static void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerShippingDetail>()
            .HasOne(csd => csd.Customer)
            .WithMany(c => c.CustomerShippingDetails)
            .HasForeignKey(csd => csd.CustomerId)
            .HasConstraintName("FK_CustomerShippingDetail_CustomerID");

        modelBuilder.Entity<CustomerShippingDetail>()
            .HasOne(csd => csd.AccountAddress)
            .WithMany(aa => aa.CustomerShippingDetails)
            .HasForeignKey(csd => csd.AccountAddressId)
            .HasConstraintName("FK_CustomerShippingDetail_AccountAddressID");
    }
}

public static class AccountPaymentMethodConfiguration
{
    public static void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountPaymentMethod>()
            .HasOne(apm => apm.Account)
            .WithMany(a => a.AccountPaymentMethods)
            .HasForeignKey(apm => apm.AccountId)
            .HasConstraintName("FK_AccountPaymentMethod_AccountID");

        modelBuilder.Entity<AccountPaymentMethod>()
            .HasOne(apm => apm.AccountAddress)
            .WithMany(aa => aa.AccountPaymentMethods)
            .HasForeignKey(apm => apm.AccountAddressId)
            .HasConstraintName("FK_AccountPaymentMethod_AccountAddress");

        modelBuilder.Entity<AccountPaymentMethod>()
            .HasOne(apm => apm.PaymentMethod)
            .WithMany(pm => pm.AccountPaymentMethods)
            .HasForeignKey(apm => apm.PaymentMethodId)
            .HasConstraintName("FK_AccountPaymentMethod_PaymentMethodID");

        modelBuilder.Entity<AccountPaymentMethod>()
            .HasOne(apm => apm.IssuerCountry)
            .WithMany(c => c.AccountPaymentMethodsAsIssuerCountry)
            .HasForeignKey(apm => apm.IssuerCountryIsoCode)
            .HasConstraintName("FK_AccountPaymentMethod_IssuerCountryIsoCode");

        modelBuilder.Entity<AccountPaymentMethod>()
            .HasOne(apm => apm.PaymentMethodProvider)
            .WithMany(pmp => pmp.AccountPaymentMethods)
            .HasForeignKey(apm => apm.PaymentMethodProviderId)
            .HasConstraintName("FK_AccountPaymentMethod_PaymentMethodProviderID");
    }
} 