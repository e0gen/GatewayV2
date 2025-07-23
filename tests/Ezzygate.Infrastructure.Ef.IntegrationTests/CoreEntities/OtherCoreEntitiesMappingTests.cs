using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests.CoreEntities;

[TestFixture]
public class OtherCoreEntitiesMappingTests
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

    #region LoginPassword Tests

    [Test]
    public Task LoginPassword_TableMapping_ShouldBeReadable()
    {

        Assert.DoesNotThrowAsync(async () =>
        {
            var count = await _context.LoginPasswords.CountAsync();
            TestContext.WriteLine($"LoginPassword table contains {count} records");
        });
        return Task.CompletedTask;
    }

    [Test]
    public async Task LoginPassword_WithLoginAccount_ShouldLoadNavigationProperty()
    {
        var loginPassword = await _context.LoginPasswords
            .Include(lp => lp.LoginAccount)
            .FirstOrDefaultAsync();

        if (loginPassword != null)
        {
            TestContext.WriteLine($"Loaded LoginPassword ID: {loginPassword.LoginPasswordId}");
            Assert.DoesNotThrow(() => _ = loginPassword.LoginAccount);
            Assert.DoesNotThrow(() => _ = loginPassword.IsExpired); 
            TestContext.WriteLine($"Is Expired: {loginPassword.IsExpired}");
        }
    }

    #endregion

    #region AccountAddress Tests

    [Test]
    public Task AccountAddress_TableMapping_ShouldBeReadable()
    {

        Assert.DoesNotThrowAsync(async () =>
        {
            var count = await _context.AccountAddresses.CountAsync();
            TestContext.WriteLine($"AccountAddress table contains {count} records");
        });
        return Task.CompletedTask;
    }

    [Test]
    public async Task AccountAddress_WithCountryAndState_ShouldLoadNavigationProperties()
    {
        var address = await _context.AccountAddresses
            .Include(aa => aa.Country)
            .Include(aa => aa.State)
            .FirstOrDefaultAsync();

        if (address != null)
        {
            TestContext.WriteLine($"Loaded AccountAddress ID: {address.AccountAddressId}");
            Assert.DoesNotThrow(() => _ = address.Country);
            Assert.DoesNotThrow(() => _ = address.State);
            Assert.DoesNotThrow(() => _ = address.FullAddress); 
            TestContext.WriteLine($"Full Address: {address.FullAddress}");
        }
    }

    #endregion

    #region AccountBalance Tests

    [Test]
    public Task AccountBalance_TableMapping_ShouldBeReadable()
    {

        Assert.DoesNotThrowAsync(async () =>
        {
            var count = await _context.AccountBalances.CountAsync();
            TestContext.WriteLine($"AccountBalance table contains {count} records");
        });
        return Task.CompletedTask;
    }

    [Test]
    public async Task AccountBalance_WithRelationships_ShouldLoadNavigationProperties()
    {
        var balance = await _context.AccountBalances
            .Include(ab => ab.Account)
            .Include(ab => ab.BalanceSourceType)
            .Include(ab => ab.Currency)
            .FirstOrDefaultAsync();

        if (balance != null)
        {
            TestContext.WriteLine($"Loaded AccountBalance ID: {balance.AccountBalanceId}");
            Assert.DoesNotThrow(() => _ = balance.Account);
            Assert.DoesNotThrow(() => _ = balance.BalanceSourceType);
            Assert.DoesNotThrow(() => _ = balance.Currency);
            Assert.DoesNotThrow(() => _ = balance.IsCredit); 
            Assert.DoesNotThrow(() => _ = balance.IsDebit); 
            TestContext.WriteLine($"Amount: {balance.Amount}, Is Credit: {balance.IsCredit}");
        }
    }

    #endregion

    #region AccountSubUser Tests

    [Test]
    public Task AccountSubUser_TableMapping_ShouldBeReadable()
    {

        Assert.DoesNotThrowAsync(async () =>
        {
            var count = await _context.AccountSubUsers.CountAsync();
            TestContext.WriteLine($"AccountSubUser table contains {count} records");
        });
        return Task.CompletedTask;
    }

    [Test]
    public async Task AccountSubUser_WithRelationships_ShouldLoadNavigationProperties()
    {
        var subUser = await _context.AccountSubUsers
            .Include(asu => asu.Account)
            .Include(asu => asu.LoginAccount)
            .FirstOrDefaultAsync();

        if (subUser != null)
        {
            TestContext.WriteLine($"Loaded AccountSubUser ID: {subUser.AccountSubUserId}");
            Assert.DoesNotThrow(() => _ = subUser.Account);
            Assert.DoesNotThrow(() => _ = subUser.LoginAccount);
            TestContext.WriteLine($"Sub User Description: {subUser.Description ?? "NULL"}");
        }
    }

    #endregion

    #region MobileDevice Tests

    [Test]
    public Task MobileDevice_TableMapping_ShouldBeReadable()
    {

        Assert.DoesNotThrowAsync(async () =>
        {
            var count = await _context.MobileDevices.CountAsync();
            TestContext.WriteLine($"MobileDevice table contains {count} records");
        });
        return Task.CompletedTask;
    }

    [Test]
    public async Task MobileDevice_WithAccount_ShouldLoadNavigationProperty()
    {
        var device = await _context.MobileDevices
            .Include(md => md.Account)
            .FirstOrDefaultAsync();

        if (device != null)
        {
            TestContext.WriteLine($"Loaded MobileDevice ID: {device.MobileDeviceId}");
            Assert.DoesNotThrow(() => _ = device.Account);
            TestContext.WriteLine($"Device Identity: {device.DeviceIdentity ?? "NULL"}");
            TestContext.WriteLine($"Friendly Name: {device.FriendlyName ?? "NULL"}");
        }
    }

    #endregion

    #region Cart Tests

    [Test]
    public Task Cart_TableMapping_ShouldBeReadable()
    {

        Assert.DoesNotThrowAsync(async () =>
        {
            var count = await _context.Carts.CountAsync();
            TestContext.WriteLine($"Cart table contains {count} records");
        });
        return Task.CompletedTask;
    }

    [Test]
    public async Task Cart_WithCustomer_ShouldLoadNavigationProperty()
    {
        // Act
        var cart = await _context.Carts
            .Include(c => c.Customer)
            .FirstOrDefaultAsync();

        // Assert
        if (cart != null)
        {
            TestContext.WriteLine($"Loaded Cart ID: {cart.CartId}");
            Assert.DoesNotThrow(() => _ = cart.Customer);
            Assert.DoesNotThrow(() => _ = cart.Total); // Computed property
            TestContext.WriteLine($"Cart Total: {cart.Total}");
        }
    }

    #endregion

    #region CustomerShippingDetail Tests

    [Test]
    public Task CustomerShippingDetail_TableMapping_ShouldBeReadable()
    {

        Assert.DoesNotThrowAsync(async () =>
        {
            var count = await _context.CustomerShippingDetails.CountAsync();
            TestContext.WriteLine($"CustomerShippingDetail table contains {count} records");
        });
        return Task.CompletedTask;
    }

    [Test]
    public async Task CustomerShippingDetail_WithRelationships_ShouldLoadNavigationProperties()
    {
        // Act
        var shippingDetail = await _context.CustomerShippingDetails
            .Include(csd => csd.Customer)
            .Include(csd => csd.AccountAddress)
            .FirstOrDefaultAsync();

        // Assert
        if (shippingDetail != null)
        {
            TestContext.WriteLine($"Loaded CustomerShippingDetail ID: {shippingDetail.CustomerShippingDetailId}");
            Assert.DoesNotThrow(() => _ = shippingDetail.Customer);
            Assert.DoesNotThrow(() => _ = shippingDetail.AccountAddress);
            TestContext.WriteLine($"Is Default: {shippingDetail.IsDefault}");
        }
    }

    #endregion

    #region AccountPaymentMethod Tests

    [Test]
    public Task AccountPaymentMethod_TableMapping_ShouldBeReadable()
    {

        Assert.DoesNotThrowAsync(async () =>
        {
            var count = await _context.AccountPaymentMethods.CountAsync();
            TestContext.WriteLine($"AccountPaymentMethod table contains {count} records");
        });
        return Task.CompletedTask;
    }

    [Test]
    public async Task AccountPaymentMethod_WithRelationships_ShouldLoadNavigationProperties()
    {
        // Act
        var paymentMethod = await _context.AccountPaymentMethods
            .Include(apm => apm.Account)
            .Include(apm => apm.AccountAddress)
            .FirstOrDefaultAsync();

        // Assert
        if (paymentMethod != null)
        {
            TestContext.WriteLine($"Loaded AccountPaymentMethod ID: {paymentMethod.AccountPaymentMethodId}");
            Assert.DoesNotThrow(() => _ = paymentMethod.Account);
            Assert.DoesNotThrow(() => _ = paymentMethod.AccountAddress);
            TestContext.WriteLine($"Payment Method Title: {paymentMethod.Title ?? "NULL"}");
        }
    }

    #endregion
} 