using Microsoft.EntityFrameworkCore;
using Ezzygate.Domain.Entities.Core;
using Ezzygate.Domain.Entities.Lookup;
using Ezzygate.Infrastructure.Ef.Configurations;

namespace Ezzygate.Infrastructure.Ef;

public class EzzygateDbContext : DbContext
{
    public EzzygateDbContext(DbContextOptions<EzzygateDbContext> options) : base(options)
    {
    }

    // Data Schema
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<LoginAccount> LoginAccounts { get; set; } = null!;
    public DbSet<LoginPassword> LoginPasswords { get; set; } = null!;
    public DbSet<AccountAddress> AccountAddresses { get; set; } = null!;
    public DbSet<AccountBalance> AccountBalances { get; set; } = null!;
    public DbSet<AccountSubUser> AccountSubUsers { get; set; } = null!;
    public DbSet<Cart> Carts { get; set; } = null!;
    public DbSet<CustomerShippingDetail> CustomerShippingDetails { get; set; } = null!;
    public DbSet<AccountPaymentMethod> AccountPaymentMethods { get; set; } = null!;

    // dbo Schema
    public DbSet<MobileDevice> MobileDevices { get; set; } = null!;

    // List Schema
    public DbSet<AccountType> AccountTypes { get; set; } = null!;
    public DbSet<ActiveStatus> ActiveStatuses { get; set; } = null!;
    public DbSet<LoginRole> LoginRoles { get; set; } = null!;
    public DbSet<CurrencyList> Currencies { get; set; } = null!;
    public DbSet<CountryList> Countries { get; set; } = null!;
    public DbSet<StateList> States { get; set; } = null!;
    public DbSet<BalanceSourceType> BalanceSourceTypes { get; set; } = null!;
    public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
    public DbSet<PaymentMethodProvider> PaymentMethodProviders { get; set; } = null!;

    // Trans Schema
    // TODO: Add transaction entities when needed
    // public DbSet<Transaction> Transactions { get; set; } = null!;
    // public DbSet<TransPaymentMethod> TransPaymentMethods { get; set; } = null!;

    // Merchant Entities
    // TODO: Add merchant entities when needed
    // public DbSet<Merchant> Merchants { get; set; } = null!;

    // Wire and Finance Entities
    // TODO: Add wire and finance entities when needed
    // public DbSet<Wire> Wires { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        CoreEntityConfiguration.Configure(modelBuilder);
        LookupEntityConfiguration.Configure(modelBuilder);

        // TransactionEntityConfiguration.Configure(modelBuilder);
        // MerchantEntityConfiguration.Configure(modelBuilder);
        // WireEntityConfiguration.Configure(modelBuilder);
        // RiskManagementConfiguration.Configure(modelBuilder);
        // ReportingConfiguration.Configure(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            // This should be configured via DI, but adding fallback for development
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EzzygateDb;Trusted_Connection=true;");

        base.OnConfiguring(optionsBuilder);
    }
}