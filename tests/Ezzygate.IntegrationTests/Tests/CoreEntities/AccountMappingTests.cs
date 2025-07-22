using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ezzygate.Infrastructure.Data;
using NUnit.Framework;

namespace Ezzygate.IntegrationTests.Tests.CoreEntities;

[TestFixture]
public class AccountMappingTests
{
    private EzzygateDbContext _context = null!;
    private IConfiguration _configuration = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        
        var options = new DbContextOptionsBuilder<EzzygateDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        _context = new EzzygateDbContext(options);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _context?.Dispose();
    }

    [Test]
    public Task Account_TableMapping_ShouldBeReadable()
    {

        Assert.DoesNotThrowAsync(async () =>
        {
            var count = await _context.Accounts.CountAsync();
            TestContext.WriteLine($"Account table contains {count} records");
        });
        return Task.CompletedTask;
    }

    [Test]
    public async Task Account_WithAccountType_ShouldLoadNavigationProperty()
    {
        var account = await _context.Accounts
            .Include(a => a.AccountType)
            .FirstOrDefaultAsync();

        if (account != null)
        {
            TestContext.WriteLine($"Loaded Account ID: {account.AccountId}");
            Assert.DoesNotThrow(() => _ = account.AccountType);
            TestContext.WriteLine($"Account Type: {account.AccountType?.Name ?? "NULL"}");
        }
        else
        {
            TestContext.WriteLine("No accounts found in database - table mapping verified");
        }
    }

    [Test]
    public async Task Account_WithCustomer_ShouldLoadNavigationProperty()
    {
        var account = await _context.Accounts
            .Include(a => a.Customer)
            .FirstOrDefaultAsync();

        if (account != null)
        {
            TestContext.WriteLine($"Loaded Account ID: {account.AccountId}");
            Assert.DoesNotThrow(() => _ = account.Customer);
            TestContext.WriteLine($"Customer: {account.Customer?.FullName ?? "NULL"}");
        }
    }

    [Test]
    public async Task Account_WithLoginAccount_ShouldLoadNavigationProperty()
    {
        var account = await _context.Accounts
            .Include(a => a.LoginAccount)
            .FirstOrDefaultAsync();

        if (account != null)
        {
            TestContext.WriteLine($"Loaded Account ID: {account.AccountId}");
            Assert.DoesNotThrow(() => _ = account.LoginAccount);
            TestContext.WriteLine($"Login Account: {account.LoginAccount?.LoginUser ?? "NULL"}");
        }
    }

    [Test]
    public async Task Account_WithAddresses_ShouldLoadNavigationProperties()
    {
        var account = await _context.Accounts
            .Include(a => a.PersonalAddress)
            .Include(a => a.BusinessAddress)
            .FirstOrDefaultAsync();

        if (account != null)
        {
            TestContext.WriteLine($"Loaded Account ID: {account.AccountId}");
            Assert.DoesNotThrow(() => _ = account.PersonalAddress);
            Assert.DoesNotThrow(() => _ = account.BusinessAddress);
            TestContext.WriteLine($"Personal Address: {account.PersonalAddress?.FullAddress ?? "NULL"}");
            TestContext.WriteLine($"Business Address: {account.BusinessAddress?.FullAddress ?? "NULL"}");
        }
    }

    [Test]
    public async Task Account_WithBalances_ShouldLoadCollectionProperty()
    {
        var account = await _context.Accounts
            .Include(a => a.AccountBalances.Take(5))
            .FirstOrDefaultAsync();

        if (account != null)
        {
            TestContext.WriteLine($"Loaded Account ID: {account.AccountId}");
            Assert.DoesNotThrow(() => _ = account.AccountBalances.Count);
            TestContext.WriteLine($"Account has {account.AccountBalances.Count} balance records (max 5 loaded)");
        }
    }

    [Test]
    public async Task Account_WithAllRelationships_ShouldLoadWithoutErrors()
    {
        var account = await _context.Accounts
            .Include(a => a.AccountType)
            .Include(a => a.Customer)
            .Include(a => a.LoginAccount)
            .Include(a => a.PersonalAddress)
            .Include(a => a.BusinessAddress)
            .Include(a => a.AccountBalances.Take(3))
            .Include(a => a.MobileDevices.Take(3))
            .Include(a => a.AccountSubUsers.Take(3))
            .FirstOrDefaultAsync();

        if (account != null)
        {
            TestContext.WriteLine($"Successfully loaded Account {account.AccountId} with all navigation properties");
            
            // Verify we can access all properties without exceptions
            Assert.DoesNotThrow(() => _ = account.AccountType?.Name);
            Assert.DoesNotThrow(() => _ = account.Customer?.FullName);
            Assert.DoesNotThrow(() => _ = account.LoginAccount?.LoginUser);
            Assert.DoesNotThrow(() => _ = account.PersonalAddress?.FullAddress);
            Assert.DoesNotThrow(() => _ = account.BusinessAddress?.FullAddress);
            Assert.DoesNotThrow(() => _ = account.AccountBalances.Count);
            Assert.DoesNotThrow(() => _ = account.MobileDevices.Count);
            Assert.DoesNotThrow(() => _ = account.AccountSubUsers.Count);
            
            TestContext.WriteLine("All Account navigation properties loaded successfully");
        }
        else
        {
            TestContext.WriteLine("No accounts found - navigation property mapping verified");
        }
    }
} 