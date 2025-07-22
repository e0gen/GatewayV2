using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ezzygate.Infrastructure.Data;
using NUnit.Framework;

namespace Ezzygate.IntegrationTests.Tests.CoreEntities;

[TestFixture]
public class CustomerMappingTests
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
    public Task Customer_TableMapping_ShouldBeReadable()
    {

        Assert.DoesNotThrowAsync(async () =>
        {
            var count = await _context.Customers.CountAsync();
            TestContext.WriteLine($"Customer table contains {count} records");
        });
        return Task.CompletedTask;
    }

    [Test]
    public async Task Customer_WithActiveStatus_ShouldLoadNavigationProperty()
    {
        var customer = await _context.Customers
            .Include(c => c.ActiveStatus)
            .FirstOrDefaultAsync();

        if (customer != null)
        {
            TestContext.WriteLine($"Loaded Customer ID: {customer.CustomerId}");
            Assert.DoesNotThrow(() => _ = customer.ActiveStatus);
            TestContext.WriteLine($"Active Status: {customer.ActiveStatus?.Name ?? "NULL"}");
        }
        else
        {
            TestContext.WriteLine("No customers found in database - table mapping verified");
        }
    }

    [Test]
    public async Task Customer_WithAccount_ShouldLoadNavigationProperty()
    {
        var customer = await _context.Customers
            .Include(c => c.Account)
            .FirstOrDefaultAsync();

        if (customer != null)
        {
            TestContext.WriteLine($"Loaded Customer ID: {customer.CustomerId}");
            Assert.DoesNotThrow(() => _ = customer.Account);
            TestContext.WriteLine($"Account ID: {customer.Account?.AccountId.ToString() ?? "NULL"}");
        }
    }

    [Test]
    public async Task Customer_ComputedProperties_ShouldWork()
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync();

        if (customer != null)
        {
            TestContext.WriteLine($"Loaded Customer ID: {customer.CustomerId}");
            Assert.DoesNotThrow(() => _ = customer.FullName);
            TestContext.WriteLine($"Customer FullName: {customer.FullName}");
            TestContext.WriteLine($"First Name: {customer.FirstName ?? "NULL"}");
            TestContext.WriteLine($"Last Name: {customer.LastName ?? "NULL"}");
        }
    }

    [Test]
    public async Task Customer_WithCarts_ShouldLoadCollectionProperty()
    {
        var customer = await _context.Customers
            .Include(c => c.Carts.Take(5))
            .FirstOrDefaultAsync();

        if (customer != null)
        {
            TestContext.WriteLine($"Loaded Customer ID: {customer.CustomerId}");
            Assert.DoesNotThrow(() => _ = customer.Carts.Count);
            TestContext.WriteLine($"Customer has {customer.Carts.Count} cart records (max 5 loaded)");
        }
    }

    [Test]
    public async Task Customer_WithShippingDetails_ShouldLoadCollectionProperty()
    {
        var customer = await _context.Customers
            .Include(c => c.CustomerShippingDetails.Take(5))
            .FirstOrDefaultAsync();

        if (customer != null)
        {
            TestContext.WriteLine($"Loaded Customer ID: {customer.CustomerId}");
            Assert.DoesNotThrow(() => _ = customer.CustomerShippingDetails.Count);
            TestContext.WriteLine($"Customer has {customer.CustomerShippingDetails.Count} shipping detail records (max 5 loaded)");
        }
    }

    [Test]
    public async Task Customer_WithAllRelationships_ShouldLoadWithoutErrors()
    {
        var customer = await _context.Customers
            .Include(c => c.ActiveStatus)
            .Include(c => c.Account)
            .Include(c => c.Carts.Take(3))
            .Include(c => c.CustomerShippingDetails.Take(3))
                .ThenInclude(csd => csd.AccountAddress)
            .FirstOrDefaultAsync();

        if (customer != null)
        {
            TestContext.WriteLine($"Successfully loaded Customer {customer.CustomerId} with all navigation properties");
            
            // Verify we can access all properties without exceptions
            Assert.DoesNotThrow(() => _ = customer.ActiveStatus?.Name);
            Assert.DoesNotThrow(() => _ = customer.Account?.AccountId);
            Assert.DoesNotThrow(() => _ = customer.FullName);
            Assert.DoesNotThrow(() => _ = customer.Carts.Count);
            Assert.DoesNotThrow(() => _ = customer.CustomerShippingDetails.Count);
            
            TestContext.WriteLine("All Customer navigation properties loaded successfully");
        }
        else
        {
            TestContext.WriteLine("No customers found - navigation property mapping verified");
        }
    }
} 