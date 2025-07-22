using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ezzygate.Infrastructure.Data;
using NUnit.Framework;

namespace Ezzygate.IntegrationTests.Tests.LookupEntities;

[TestFixture]
public class LookupEntitiesMappingTests
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

    #region AccountType Tests

    [Test]
    public Task AccountType_TableMapping_ShouldBeReadable()
    {

        Assert.DoesNotThrowAsync(async () =>
        {
            var count = await _context.AccountTypes.CountAsync();
            TestContext.WriteLine($"AccountType table contains {count} records");
        });
        return Task.CompletedTask;
    }

    [Test]
    public async Task AccountType_WithAccounts_ShouldLoadCollectionProperty()
    {
        var accountType = await _context.AccountTypes
            .Include(at => at.Accounts.Take(5))
            .FirstOrDefaultAsync();

        if (accountType != null)
        {
            TestContext.WriteLine($"Loaded AccountType ID: {accountType.AccountTypeId}");
            TestContext.WriteLine($"Account Type Name: {accountType.Name ?? "NULL"}");
            Assert.DoesNotThrow(() => _ = accountType.Accounts.Count);
            TestContext.WriteLine($"AccountType has {accountType.Accounts.Count} account records (max 5 loaded)");
        }
    }

    #endregion

    #region ActiveStatus Tests

    [Test]
    public Task ActiveStatus_TableMapping_ShouldBeReadable()
    {

        Assert.DoesNotThrowAsync(async () =>
        {
            var count = await _context.ActiveStatuses.CountAsync();
            TestContext.WriteLine($"ActiveStatus table contains {count} records");
        });
        return Task.CompletedTask;
    }

    [Test]
    public async Task ActiveStatus_WithCustomers_ShouldLoadCollectionProperty()
    {
        var activeStatus = await _context.ActiveStatuses
            .Include(ast => ast.Customers.Take(5))
            .FirstOrDefaultAsync();

        if (activeStatus != null)
        {
            TestContext.WriteLine($"Loaded ActiveStatus ID: {activeStatus.ActiveStatusId}");
            TestContext.WriteLine($"Active Status Name: {activeStatus.Name ?? "NULL"}");
            Assert.DoesNotThrow(() => _ = activeStatus.Customers.Count);
            TestContext.WriteLine($"ActiveStatus has {activeStatus.Customers.Count} customer records (max 5 loaded)");
        }
    }

    #endregion

    #region LoginRole Tests

    [Test]
    public Task LoginRole_TableMapping_ShouldBeReadable()
    {

        Assert.DoesNotThrowAsync(async () =>
        {
            var count = await _context.LoginRoles.CountAsync();
            TestContext.WriteLine($"LoginRole table contains {count} records");
        });
        return Task.CompletedTask;
    }

    [Test]
    public async Task LoginRole_WithLoginAccounts_ShouldLoadCollectionProperty()
    {
        var loginRole = await _context.LoginRoles
            .Include(lr => lr.LoginAccounts.Take(5))
            .FirstOrDefaultAsync();

        if (loginRole != null)
        {
            TestContext.WriteLine($"Loaded LoginRole ID: {loginRole.LoginRoleId}");
            TestContext.WriteLine($"Login Role Name: {loginRole.Name ?? "NULL"}");
            Assert.DoesNotThrow(() => _ = loginRole.LoginAccounts.Count);
            TestContext.WriteLine($"LoginRole has {loginRole.LoginAccounts.Count} login account records (max 5 loaded)");
        }
    }

    #endregion

    #region CurrencyList Tests

    [Test]
    public Task CurrencyList_TableMapping_ShouldBeReadable()
    {

        Assert.DoesNotThrowAsync(async () =>
        {
            var count = await _context.Currencies.CountAsync();
            TestContext.WriteLine($"CurrencyList table contains {count} records");
        });
        return Task.CompletedTask;
    }

    [Test]
    public async Task CurrencyList_WithAccountBalances_ShouldLoadCollectionProperty()
    {
        var currency = await _context.Currencies
            .Include(c => c.AccountBalances.Take(5))
            .FirstOrDefaultAsync();

        if (currency != null)
        {
            TestContext.WriteLine($"Loaded Currency ISO Code: {currency.CurrencyISOCode}");
            TestContext.WriteLine($"Currency Name: {currency.Name ?? "NULL"}");
            TestContext.WriteLine($"Currency Symbol: {currency.Symbol ?? "NULL"}");
            Assert.DoesNotThrow(() => _ = currency.AccountBalances.Count);
            TestContext.WriteLine($"Currency has {currency.AccountBalances.Count} balance records (max 5 loaded)");
        }
    }

    #endregion

    #region CountryList Tests

    [Test]
    public Task CountryList_TableMapping_ShouldBeReadable()
    {

        Assert.DoesNotThrowAsync(async () =>
        {
            var count = await _context.Countries.CountAsync();
            TestContext.WriteLine($"CountryList table contains {count} records");
        });
        return Task.CompletedTask;
    }

    [Test]
    public async Task CountryList_WithStatesAndAddresses_ShouldLoadCollectionProperties()
    {
        var country = await _context.Countries
            .Include(c => c.States.Take(5))
            .Include(c => c.AccountAddresses.Take(5))
            .FirstOrDefaultAsync();

        if (country != null)
        {
            TestContext.WriteLine($"Loaded Country ISO Code: {country.CountryISOCode}");
            TestContext.WriteLine($"Country Name: {country.Name ?? "NULL"}");
            Assert.DoesNotThrow(() => _ = country.States.Count);
            Assert.DoesNotThrow(() => _ = country.AccountAddresses.Count);
            TestContext.WriteLine($"Country has {country.States.Count} state records (max 5 loaded)");
            TestContext.WriteLine($"Country has {country.AccountAddresses.Count} address records (max 5 loaded)");
        }
    }

    #endregion

    #region StateList Tests

    [Test]
    public Task StateList_TableMapping_ShouldBeReadable()
    {

        Assert.DoesNotThrowAsync(async () =>
        {
            var count = await _context.States.CountAsync();
            TestContext.WriteLine($"StateList table contains {count} records");
        });
        return Task.CompletedTask;
    }

    [Test]
    public async Task StateList_WithCountryAndAddresses_ShouldLoadNavigationProperties()
    {
        var state = await _context.States
            .Include(s => s.Country)
            .Include(s => s.AccountAddresses.Take(5))
            .FirstOrDefaultAsync();

        if (state != null)
        {
            TestContext.WriteLine($"Loaded State ISO Code: {state.StateISOCode}");
            TestContext.WriteLine($"State Name: {state.Name ?? "NULL"}");
            Assert.DoesNotThrow(() => _ = state.Country);
            Assert.DoesNotThrow(() => _ = state.AccountAddresses.Count);
            TestContext.WriteLine($"Country: {state.Country?.Name ?? "NULL"}");
            TestContext.WriteLine($"State has {state.AccountAddresses.Count} address records (max 5 loaded)");
        }
    }

    #endregion

    #region BalanceSourceType Tests

    [Test]
    public Task BalanceSourceType_TableMapping_ShouldBeReadable()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            var count = await _context.BalanceSourceTypes.CountAsync();
            TestContext.WriteLine($"BalanceSourceType table contains {count} records");
        });
        return Task.CompletedTask;
    }

    [Test]
    public async Task BalanceSourceType_WithAccountBalances_ShouldLoadCollectionProperty()
    {
        var balanceSourceType = await _context.BalanceSourceTypes
            .Include(bst => bst.AccountBalances.Take(5))
            .FirstOrDefaultAsync();

        if (balanceSourceType != null)
        {
            TestContext.WriteLine($"Loaded BalanceSourceType ID: {balanceSourceType.BalanceSourceTypeId}");
            TestContext.WriteLine($"Balance Source Type Name: {balanceSourceType.Name ?? "NULL"}");
            TestContext.WriteLine($"Description: {balanceSourceType.Description ?? "NULL"}");
            Assert.DoesNotThrow(() => _ = balanceSourceType.AccountBalances.Count);
            TestContext.WriteLine($"BalanceSourceType has {balanceSourceType.AccountBalances.Count} balance records (max 5 loaded)");
        }
    }

    #endregion
} 