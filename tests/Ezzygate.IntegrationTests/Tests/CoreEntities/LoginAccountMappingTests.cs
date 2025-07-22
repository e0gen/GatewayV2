using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ezzygate.Infrastructure.Data;
using NUnit.Framework;

namespace Ezzygate.IntegrationTests.Tests.CoreEntities;

[TestFixture]
public class LoginAccountMappingTests
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
    public Task LoginAccount_TableMapping_ShouldBeReadable()
    {

        Assert.DoesNotThrowAsync(async () =>
        {
            var count = await _context.LoginAccounts.CountAsync();
            TestContext.WriteLine($"LoginAccount table contains {count} records");
        });
        return Task.CompletedTask;
    }

    [Test]
    public async Task LoginAccount_WithLoginRole_ShouldLoadNavigationProperty()
    {
        var loginAccount = await _context.LoginAccounts
            .Include(la => la.LoginRole)
            .FirstOrDefaultAsync();

        if (loginAccount != null)
        {
            TestContext.WriteLine($"Loaded LoginAccount ID: {loginAccount.LoginAccountId}");
            Assert.DoesNotThrow(() => _ = loginAccount.LoginRole);
            TestContext.WriteLine($"Login Role: {loginAccount.LoginRole?.Name ?? "NULL"}");
        }
        else
        {
            TestContext.WriteLine("No login accounts found in database - table mapping verified");
        }
    }

    [Test]
    public async Task LoginAccount_ComputedProperties_ShouldWork()
    {
        var loginAccount = await _context.LoginAccounts
            .FirstOrDefaultAsync();

        if (loginAccount != null)
        {
            TestContext.WriteLine($"Loaded LoginAccount ID: {loginAccount.LoginAccountId}");
            Assert.DoesNotThrow(() => _ = loginAccount.IsBlocked);
            Assert.DoesNotThrow(() => _ = loginAccount.IsLocked);
            TestContext.WriteLine($"Is Blocked: {loginAccount.IsBlocked}");
            TestContext.WriteLine($"Is Locked: {loginAccount.IsLocked}");
            TestContext.WriteLine($"Login User: {loginAccount.LoginUser ?? "NULL"}");
            TestContext.WriteLine($"Login Email: {loginAccount.LoginEmail ?? "NULL"}");
        }
    }

    [Test]
    public async Task LoginAccount_WithAccounts_ShouldLoadCollectionProperty()
    {
        var loginAccount = await _context.LoginAccounts
            .Include(la => la.Accounts.Take(5))
            .FirstOrDefaultAsync();

        if (loginAccount != null)
        {
            TestContext.WriteLine($"Loaded LoginAccount ID: {loginAccount.LoginAccountId}");
            Assert.DoesNotThrow(() => _ = loginAccount.Accounts.Count);
            TestContext.WriteLine($"LoginAccount has {loginAccount.Accounts.Count} account records (max 5 loaded)");
        }
    }

    [Test]
    public async Task LoginAccount_WithPasswords_ShouldLoadCollectionProperty()
    {
        var loginAccount = await _context.LoginAccounts
            .Include(la => la.LoginPasswords.Take(5))
            .FirstOrDefaultAsync();

        if (loginAccount != null)
        {
            TestContext.WriteLine($"Loaded LoginAccount ID: {loginAccount.LoginAccountId}");
            Assert.DoesNotThrow(() => _ = loginAccount.LoginPasswords.Count);
            TestContext.WriteLine($"LoginAccount has {loginAccount.LoginPasswords.Count} password records (max 5 loaded)");
        }
    }

    [Test]
    public async Task LoginAccount_WithSubUsers_ShouldLoadCollectionProperty()
    {
        var loginAccount = await _context.LoginAccounts
            .Include(la => la.AccountSubUsers.Take(5))
            .FirstOrDefaultAsync();

        if (loginAccount != null)
        {
            TestContext.WriteLine($"Loaded LoginAccount ID: {loginAccount.LoginAccountId}");
            Assert.DoesNotThrow(() => _ = loginAccount.AccountSubUsers.Count);
            TestContext.WriteLine($"LoginAccount has {loginAccount.AccountSubUsers.Count} sub user records (max 5 loaded)");
        }
    }
} 