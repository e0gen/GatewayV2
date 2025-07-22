using Microsoft.EntityFrameworkCore;
using Ezzygate.Domain.Entities.Core;
using Ezzygate.Domain.Entities.Lookup;

namespace Ezzygate.Infrastructure.Data;

public class EzzygateDbContext : DbContext
{
    public EzzygateDbContext(DbContextOptions<EzzygateDbContext> options) : base(options)
    {
    }

    // Core entities
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<LoginAccount> LoginAccounts { get; set; } = null!;
    public DbSet<LoginPassword> LoginPasswords { get; set; } = null!;
    public DbSet<AccountAddress> AccountAddresses { get; set; } = null!;
    public DbSet<AccountBalance> AccountBalances { get; set; } = null!;
    public DbSet<AccountSubUser> AccountSubUsers { get; set; } = null!;
    public DbSet<MobileDevice> MobileDevices { get; set; } = null!;
    public DbSet<Cart> Carts { get; set; } = null!;
    public DbSet<CustomerShippingDetail> CustomerShippingDetails { get; set; } = null!;
    public DbSet<AccountPaymentMethod> AccountPaymentMethods { get; set; } = null!;

    // Lookup entities
    public DbSet<AccountType> AccountTypes { get; set; } = null!;
    public DbSet<ActiveStatus> ActiveStatuses { get; set; } = null!;
    public DbSet<LoginRole> LoginRoles { get; set; } = null!;
    public DbSet<CurrencyList> Currencies { get; set; } = null!;
    public DbSet<CountryList> Countries { get; set; } = null!;
    public DbSet<StateList> States { get; set; } = null!;
    public DbSet<BalanceSourceType> BalanceSourceTypes { get; set; } = null!;
    public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
    public DbSet<PaymentMethodProvider> PaymentMethodProviders { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure table names to match legacy schema
        modelBuilder.Entity<Account>().ToTable("Account", "Data");
        modelBuilder.Entity<Customer>().ToTable("Customer", "Data");
        modelBuilder.Entity<LoginAccount>().ToTable("LoginAccount", "Data");
        modelBuilder.Entity<LoginPassword>().ToTable("LoginPassword", "Data");
        modelBuilder.Entity<AccountAddress>().ToTable("AccountAddress", "Data");
        modelBuilder.Entity<AccountBalance>().ToTable("AccountBalance", "Data");
        modelBuilder.Entity<AccountSubUser>().ToTable("AccountSubUser", "Data");
        modelBuilder.Entity<MobileDevice>().ToTable("MobileDevice", "dbo");
        modelBuilder.Entity<Cart>().ToTable("Cart", "Data");
        modelBuilder.Entity<CustomerShippingDetail>().ToTable("CustomerShippingDetail", "Data");
        modelBuilder.Entity<AccountPaymentMethod>().ToTable("AccountPaymentMethod", "Data");

        // Lookup tables
        modelBuilder.Entity<AccountType>().ToTable("AccountType", "List");
        modelBuilder.Entity<ActiveStatus>().ToTable("ActiveStatus", "List");
        modelBuilder.Entity<LoginRole>().ToTable("LoginRole", "List");
        modelBuilder.Entity<CurrencyList>().ToTable("CurrencyList", "List");
        modelBuilder.Entity<CountryList>().ToTable("CountryList", "List");
        modelBuilder.Entity<StateList>().ToTable("StateList", "List");
        modelBuilder.Entity<BalanceSourceType>().ToTable("BalanceSourceType", "List");

        // Configure primary keys to match legacy naming
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

        // Configure lookup entity primary keys to match legacy naming
        modelBuilder.Entity<AccountType>().Property(e => e.AccountTypeId).HasColumnName("AccountType_id");
        modelBuilder.Entity<ActiveStatus>().Property(e => e.ActiveStatusId).HasColumnName("ActiveStatus_id");
        modelBuilder.Entity<LoginRole>().Property(e => e.LoginRoleId).HasColumnName("LoginRole_id");
        modelBuilder.Entity<CurrencyList>().Property(e => e.CurrencyISOCode).HasColumnName("CurrencyISOCode");
        modelBuilder.Entity<CountryList>().Property(e => e.CountryISOCode).HasColumnName("CountryISOCode");
        modelBuilder.Entity<StateList>().Property(e => e.StateISOCode).HasColumnName("StateISOCode");
        modelBuilder.Entity<BalanceSourceType>().Property(e => e.BalanceSourceTypeId).HasColumnName("BalanceSourceType_id");
        modelBuilder.Entity<PaymentMethod>().Property(e => e.PaymentMethodId).HasColumnName("PaymentMethod_id");
        modelBuilder.Entity<PaymentMethodProvider>().Property(e => e.PaymentMethodProviderId).HasColumnName("PaymentMethodProvider_id");

        // Configure Account foreign key columns to match legacy naming
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

        // Configure other entity foreign key columns to match legacy naming
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

        modelBuilder.Entity<AccountPaymentMethod>().Property(e => e.AccountId).HasColumnName("Account_id");
        modelBuilder.Entity<AccountPaymentMethod>().Property(e => e.AccountAddressId).HasColumnName("AccountAddress_id");

        modelBuilder.Entity<AccountAddress>().Property(e => e.StateISOCode).HasColumnName("StateISOCode");
        modelBuilder.Entity<AccountAddress>().Property(e => e.CountryISOCode).HasColumnName("CountryISOCode");

        modelBuilder.Entity<StateList>().Property(e => e.CountryISOCode).HasColumnName("CountryISOCode");

        modelBuilder.Entity<MobileDevice>().Property(e => e.AccountId).HasColumnName("Account_id");

        modelBuilder.Entity<Cart>().Property(e => e.MerchantId).HasColumnName("Merchant_id");
        modelBuilder.Entity<Cart>().Property(e => e.CustomerId).HasColumnName("Customer_id");

        modelBuilder.Entity<CustomerShippingDetail>().Property(e => e.CustomerId).HasColumnName("Customer_id");
        modelBuilder.Entity<CustomerShippingDetail>().Property(e => e.AccountAddressId).HasColumnName("AccountAddress_id");

        modelBuilder.Entity<AccountPaymentMethod>().Property(e => e.PaymentMethodId).HasColumnName("PaymentMethod_id");
        modelBuilder.Entity<AccountPaymentMethod>().Property(e => e.PaymentMethodProviderId).HasColumnName("PaymentMethodProvider_id");
        modelBuilder.Entity<AccountPaymentMethod>().Property(e => e.PaymentMethodStatusId).HasColumnName("PaymentMethodStatus_id");

        // Configure foreign key relationships
        ConfigureAccountRelationships(modelBuilder);
        ConfigureCustomerRelationships(modelBuilder);
        ConfigureLoginAccountRelationships(modelBuilder);
        ConfigureAddressRelationships(modelBuilder);
        ConfigureBalanceRelationships(modelBuilder);
        ConfigureMobileDeviceRelationships(modelBuilder);
        ConfigureAccountSubUserRelationships(modelBuilder);
        ConfigureCartRelationships(modelBuilder);
        ConfigureCustomerShippingDetailRelationships(modelBuilder);
        ConfigureAccountPaymentMethodRelationships(modelBuilder);
    }

    private static void ConfigureAccountRelationships(ModelBuilder modelBuilder)
    {
        // Account -> AccountType
        modelBuilder.Entity<Account>()
            .HasOne(a => a.AccountType)
            .WithMany(at => at.Accounts)
            .HasForeignKey(a => a.AccountTypeId)
            .HasConstraintName("FK_Account_AccountType_AccountTypeID");

        // Account -> Customer
        modelBuilder.Entity<Account>()
            .HasOne(a => a.Customer)
            .WithMany()
            .HasForeignKey(a => a.CustomerId)
            .HasConstraintName("FK_Account_Customer_CustomerID");

        // Account -> LoginAccount
        modelBuilder.Entity<Account>()
            .HasOne(a => a.LoginAccount)
            .WithMany(la => la.Accounts)
            .HasForeignKey(a => a.LoginAccountId)
            .HasConstraintName("FK_Account_LoginAccount_LoginAccountID");

        // Account -> Personal Address
        modelBuilder.Entity<Account>()
            .HasOne(a => a.PersonalAddress)
            .WithMany(aa => aa.AccountsAsPersonalAddress)
            .HasForeignKey(a => a.PersonalAddressId)
            .HasConstraintName("FK_Account_AccountAddress_PersonalAddressID");

        // Account -> Business Address
        modelBuilder.Entity<Account>()
            .HasOne(a => a.BusinessAddress)
            .WithMany(aa => aa.AccountsAsBusinessAddress)
            .HasForeignKey(a => a.BusinessAddressId)
            .HasConstraintName("FK_Account_AccountAddress_BusinessAddressID");

        // Account -> DefaultCurrency
        modelBuilder.Entity<Account>()
            .HasOne(a => a.DefaultCurrency)
            .WithMany(c => c.Accounts)
            .HasForeignKey(a => a.DefaultCurrencyISOCode)
            .HasConstraintName("FK_Account_DefaultCurrencyISOCode");
    }

    private static void ConfigureCustomerRelationships(ModelBuilder modelBuilder)
    {
        // Customer -> ActiveStatus
        modelBuilder.Entity<Customer>()
            .HasOne(c => c.ActiveStatus)
            .WithMany(as_ => as_.Customers)
            .HasForeignKey(c => c.ActiveStatusId)
            .HasConstraintName("FK_Customer_ActiveStatus");

        // Customer -> Account (one-to-one relationship)
        modelBuilder.Entity<Customer>()
            .HasOne(c => c.Account)
            .WithOne()
            .HasForeignKey<Customer>(c => c.AccountId)
            .HasConstraintName("FK_Customer_Account_AccountID");
    }

    private static void ConfigureLoginAccountRelationships(ModelBuilder modelBuilder)
    {
        // LoginAccount -> LoginRole
        modelBuilder.Entity<LoginAccount>()
            .HasOne(la => la.LoginRole)
            .WithMany(lr => lr.LoginAccounts)
            .HasForeignKey(la => la.LoginRoleId)
            .HasConstraintName("FK_LoginAccount_LoginRole_LoginRoleID");

        // LoginPassword -> LoginAccount
        modelBuilder.Entity<LoginPassword>()
            .HasOne(lp => lp.LoginAccount)
            .WithMany(la => la.LoginPasswords)
            .HasForeignKey(lp => lp.LoginAccountId)
            .HasConstraintName("FK_LoginPassword_LoginAccountID");
    }

    private static void ConfigureAddressRelationships(ModelBuilder modelBuilder)
    {
        // AccountAddress -> Country
        modelBuilder.Entity<AccountAddress>()
            .HasOne(aa => aa.Country)
            .WithMany(c => c.AccountAddresses)
            .HasForeignKey(aa => aa.CountryISOCode)
            .HasConstraintName("FK_AccountAddress_CountryList_CountryISOCode");

        // AccountAddress -> State
        modelBuilder.Entity<AccountAddress>()
            .HasOne(aa => aa.State)
            .WithMany(s => s.AccountAddresses)
            .HasForeignKey(aa => aa.StateISOCode)
            .HasConstraintName("FK_AccountAddress_StateList_StateISOCode");

        // StateList -> CountryList
        modelBuilder.Entity<StateList>()
            .HasOne(s => s.Country)
            .WithMany(c => c.States)
            .HasForeignKey(s => s.CountryISOCode)
            .HasConstraintName("FK_StateList_CountryList_CountryISOCode");
    }

    private static void ConfigureBalanceRelationships(ModelBuilder modelBuilder)
    {
        // AccountBalance -> Account
        modelBuilder.Entity<AccountBalance>()
            .HasOne(ab => ab.Account)
            .WithMany(a => a.AccountBalances)
            .HasForeignKey(ab => ab.AccountId)
            .HasConstraintName("FK_AccountBalance_AccountID");

        // AccountBalance -> BalanceSourceType
        modelBuilder.Entity<AccountBalance>()
            .HasOne(ab => ab.BalanceSourceType)
            .WithMany(bst => bst.AccountBalances)
            .HasForeignKey(ab => ab.BalanceSourceTypeId)
            .HasConstraintName("FK_AccountBalance_BalanceSourceTypeID");

        // AccountBalance -> Currency
        modelBuilder.Entity<AccountBalance>()
            .HasOne(ab => ab.Currency)
            .WithMany(c => c.AccountBalances)
            .HasForeignKey(ab => ab.CurrencyISOCode)
            .HasConstraintName("FK_AccountBalance_CurrencyISOCode");
    }

    private static void ConfigureMobileDeviceRelationships(ModelBuilder modelBuilder)
    {
        // MobileDevice -> Account
        modelBuilder.Entity<MobileDevice>()
            .HasOne(md => md.Account)
            .WithMany(a => a.MobileDevices)
            .HasForeignKey(md => md.AccountId)
            .HasConstraintName("FK_MobileDevice_Account_AccountID");
    }

    private static void ConfigureAccountSubUserRelationships(ModelBuilder modelBuilder)
    {
        // AccountSubUser -> Account
        modelBuilder.Entity<AccountSubUser>()
            .HasOne(asu => asu.Account)
            .WithMany(a => a.AccountSubUsers)
            .HasForeignKey(asu => asu.AccountId)
            .HasConstraintName("FK_AccountSubUser_Account_AccountID");

        // AccountSubUser -> LoginAccount
        modelBuilder.Entity<AccountSubUser>()
            .HasOne(asu => asu.LoginAccount)
            .WithMany(la => la.AccountSubUsers)
            .HasForeignKey(asu => asu.LoginAccountId)
            .HasConstraintName("FK_AccountSubUser_LoginAccount_LoginAccountID");
    }

    private static void ConfigureCartRelationships(ModelBuilder modelBuilder)
    {
        // Cart -> Customer
        modelBuilder.Entity<Cart>()
            .HasOne(c => c.Customer)
            .WithMany(cust => cust.Carts)
            .HasForeignKey(c => c.CustomerId)
            .HasConstraintName("FK_Cart_Customer_CustomerID");
    }

    private static void ConfigureCustomerShippingDetailRelationships(ModelBuilder modelBuilder)
    {
        // CustomerShippingDetail -> Customer
        modelBuilder.Entity<CustomerShippingDetail>()
            .HasOne(csd => csd.Customer)
            .WithMany(c => c.CustomerShippingDetails)
            .HasForeignKey(csd => csd.CustomerId)
            .HasConstraintName("FK_CustomerShippingDetail_CustomerID");

        // CustomerShippingDetail -> AccountAddress
        modelBuilder.Entity<CustomerShippingDetail>()
            .HasOne(csd => csd.AccountAddress)
            .WithMany(aa => aa.CustomerShippingDetails)
            .HasForeignKey(csd => csd.AccountAddressId)
            .HasConstraintName("FK_CustomerShippingDetail_AccountAddressID");
    }

    private static void ConfigureAccountPaymentMethodRelationships(ModelBuilder modelBuilder)
    {
        // AccountPaymentMethod -> Account
        modelBuilder.Entity<AccountPaymentMethod>()
            .HasOne(apm => apm.Account)
            .WithMany(a => a.AccountPaymentMethods)
            .HasForeignKey(apm => apm.AccountId)
            .HasConstraintName("FK_AccountPaymentMethod_AccountID");

        // AccountPaymentMethod -> AccountAddress
        modelBuilder.Entity<AccountPaymentMethod>()
            .HasOne(apm => apm.AccountAddress)
            .WithMany(aa => aa.AccountPaymentMethods)
            .HasForeignKey(apm => apm.AccountAddressId)
            .HasConstraintName("FK_AccountPaymentMethod_AccountAddress");

        // AccountPaymentMethod -> PaymentMethod
        modelBuilder.Entity<AccountPaymentMethod>()
            .HasOne(apm => apm.PaymentMethod)
            .WithMany(pm => pm.AccountPaymentMethods)
            .HasForeignKey(apm => apm.PaymentMethodId)
            .HasConstraintName("FK_AccountPaymentMethod_PaymentMethodID");

        // AccountPaymentMethod -> IssuerCountry
        modelBuilder.Entity<AccountPaymentMethod>()
            .HasOne(apm => apm.IssuerCountry)
            .WithMany(c => c.AccountPaymentMethodsAsIssuerCountry)
            .HasForeignKey(apm => apm.IssuerCountryIsoCode)
            .HasConstraintName("FK_AccountPaymentMethod_IssuerCountryIsoCode");

        // AccountPaymentMethod -> PaymentMethodProvider
        modelBuilder.Entity<AccountPaymentMethod>()
            .HasOne(apm => apm.PaymentMethodProvider)
            .WithMany(pmp => pmp.AccountPaymentMethods)
            .HasForeignKey(apm => apm.PaymentMethodProviderId)
            .HasConstraintName("FK_AccountPaymentMethod_PaymentMethodProviderID");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // This should be configured via DI, but adding fallback for development
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EzzygateDb;Trusted_Connection=true;");
        }
        
        base.OnConfiguring(optionsBuilder);
    }
} 