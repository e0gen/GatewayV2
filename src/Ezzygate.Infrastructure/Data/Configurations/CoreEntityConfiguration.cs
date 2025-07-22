using Microsoft.EntityFrameworkCore;
using Ezzygate.Domain.Entities.Core;

namespace Ezzygate.Infrastructure.Data.Configurations;

public static class CoreEntityConfiguration
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
        modelBuilder.Entity<Account>().ToTable("Account", "Data");
        modelBuilder.Entity<Customer>().ToTable("Customer", "Data");
        modelBuilder.Entity<LoginAccount>().ToTable("LoginAccount", "Data");
        modelBuilder.Entity<LoginPassword>().ToTable("LoginPassword", "Data");
        modelBuilder.Entity<AccountAddress>().ToTable("AccountAddress", "Data");
        modelBuilder.Entity<AccountBalance>().ToTable("AccountBalance", "Data");
        modelBuilder.Entity<AccountSubUser>().ToTable("AccountSubUser", "Data");
        modelBuilder.Entity<Cart>().ToTable("Cart", "Data");
        modelBuilder.Entity<CustomerShippingDetail>().ToTable("CustomerShippingDetail", "Data");
        modelBuilder.Entity<AccountPaymentMethod>().ToTable("AccountPaymentMethod", "Data");

        modelBuilder.Entity<MobileDevice>().ToTable("MobileDevice", "dbo");
    }

    private static void ConfigurePrimaryKeys(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>().Property(e => e.AccountId).HasColumnName("Account_id");
        modelBuilder.Entity<Customer>().Property(e => e.CustomerId).HasColumnName("Customer_id");
        modelBuilder.Entity<LoginAccount>().Property(e => e.LoginAccountId).HasColumnName("LoginAccount_id");
        modelBuilder.Entity<LoginPassword>().Property(e => e.LoginPasswordId).HasColumnName("LoginPassword_id");
        modelBuilder.Entity<AccountAddress>().Property(e => e.AccountAddressId).HasColumnName("AccountAddress_id");
        modelBuilder.Entity<AccountBalance>().Property(e => e.AccountBalanceId).HasColumnName("AccountBalance_id");
        modelBuilder.Entity<AccountSubUser>().Property(e => e.AccountSubUserId).HasColumnName("AccountSubUser_id");
        modelBuilder.Entity<MobileDevice>().Property(e => e.MobileDeviceId).HasColumnName("MobileDevice_id");
        modelBuilder.Entity<Cart>().Property(e => e.CartId).HasColumnName("Cart_id");
        modelBuilder.Entity<CustomerShippingDetail>().Property(e => e.CustomerShippingDetailId).HasColumnName("CustomerShippingDetail_id");
        modelBuilder.Entity<AccountPaymentMethod>().Property(e => e.AccountPaymentMethodId).HasColumnName("AccountPaymentMethod_id");
    }

    private static void ConfigureForeignKeys(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>().Property(e => e.AccountTypeId).HasColumnName("AccountType_id");
        modelBuilder.Entity<Account>().Property(e => e.MerchantId).HasColumnName("Merchant_id");
        modelBuilder.Entity<Account>().Property(e => e.CustomerId).HasColumnName("Customer_id");
        modelBuilder.Entity<Account>().Property(e => e.AffiliateId).HasColumnName("Affiliate_id");
        modelBuilder.Entity<Account>().Property(e => e.DebitCompanyId).HasColumnName("DebitCompany_id");
        modelBuilder.Entity<Account>().Property(e => e.PersonalAddressId).HasColumnName("PersonalAddress_id");
        modelBuilder.Entity<Account>().Property(e => e.BusinessAddressId).HasColumnName("BusinessAddress_id");
        modelBuilder.Entity<Account>().Property(e => e.PreferredWireProviderId).HasColumnName("PreferredWireProvider_id");
        modelBuilder.Entity<Account>().Property(e => e.LoginAccountId).HasColumnName("LoginAccount_id");
        modelBuilder.Entity<Account>().Property(e => e.DefaultCurrencyISOCode).HasColumnName("DefaultCurrencyISOCode");

        modelBuilder.Entity<AccountBalance>().Property(e => e.AccountId).HasColumnName("Account_id");
        modelBuilder.Entity<AccountBalance>().Property(e => e.BalanceSourceTypeId).HasColumnName("BalanceSourceType_id");
        modelBuilder.Entity<AccountBalance>().Property(e => e.CurrencyISOCode).HasColumnName("CurrencyISOCode");

        modelBuilder.Entity<Customer>().Property(e => e.ActiveStatusId).HasColumnName("ActiveStatus_id");
        modelBuilder.Entity<Customer>().Property(e => e.AccountId).HasColumnName("Account_id");
        modelBuilder.Entity<Customer>().Property(e => e.ApplicationIdentityId).HasColumnName("ApplicationIdentity_id");

        modelBuilder.Entity<LoginAccount>().Property(e => e.LoginRoleId).HasColumnName("LoginRole_id");
        modelBuilder.Entity<LoginPassword>().Property(e => e.LoginAccountId).HasColumnName("LoginAccount_id");

        modelBuilder.Entity<AccountSubUser>().Property(e => e.AccountId).HasColumnName("Account_id");
        modelBuilder.Entity<AccountSubUser>().Property(e => e.LoginAccountId).HasColumnName("LoginAccount_id");

        modelBuilder.Entity<AccountAddress>().Property(e => e.StateISOCode).HasColumnName("StateISOCode");
        modelBuilder.Entity<AccountAddress>().Property(e => e.CountryISOCode).HasColumnName("CountryISOCode");

        modelBuilder.Entity<MobileDevice>().Property(e => e.AccountId).HasColumnName("Account_id");

        modelBuilder.Entity<Cart>().Property(e => e.MerchantId).HasColumnName("Merchant_id");
        modelBuilder.Entity<Cart>().Property(e => e.CustomerId).HasColumnName("Customer_id");

        modelBuilder.Entity<CustomerShippingDetail>().Property(e => e.CustomerId).HasColumnName("Customer_id");
        modelBuilder.Entity<CustomerShippingDetail>().Property(e => e.AccountAddressId).HasColumnName("AccountAddress_id");

        modelBuilder.Entity<AccountPaymentMethod>().Property(e => e.AccountId).HasColumnName("Account_id");
        modelBuilder.Entity<AccountPaymentMethod>().Property(e => e.AccountAddressId).HasColumnName("AccountAddress_id");
        modelBuilder.Entity<AccountPaymentMethod>().Property(e => e.PaymentMethodId).HasColumnName("PaymentMethod_id");
        modelBuilder.Entity<AccountPaymentMethod>().Property(e => e.PaymentMethodProviderId).HasColumnName("PaymentMethodProvider_id");
        modelBuilder.Entity<AccountPaymentMethod>().Property(e => e.PaymentMethodStatusId).HasColumnName("PaymentMethodStatus_id");
    }

    private static void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        AccountConfiguration.ConfigureRelationships(modelBuilder);
        CustomerConfiguration.ConfigureRelationships(modelBuilder);
        LoginAccountConfiguration.ConfigureRelationships(modelBuilder);
        AccountAddressConfiguration.ConfigureRelationships(modelBuilder);
        AccountBalanceConfiguration.ConfigureRelationships(modelBuilder);
        MobileDeviceConfiguration.ConfigureRelationships(modelBuilder);
        AccountSubUserConfiguration.ConfigureRelationships(modelBuilder);
        CartConfiguration.ConfigureRelationships(modelBuilder);
        CustomerShippingDetailConfiguration.ConfigureRelationships(modelBuilder);
        AccountPaymentMethodConfiguration.ConfigureRelationships(modelBuilder);
    }
} 