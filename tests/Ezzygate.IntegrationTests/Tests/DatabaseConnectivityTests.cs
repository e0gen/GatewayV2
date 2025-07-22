using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ezzygate.Infrastructure.Data;
using NUnit.Framework;

namespace Ezzygate.IntegrationTests.Tests;

[TestFixture]
public class DatabaseConnectivityTests
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
    public void DatabaseConnection_ShouldBeEstablished()
    {

        Assert.DoesNotThrow(() =>
        {
            var canConnect = _context.Database.CanConnect();
            TestContext.WriteLine($"Database connection status: {canConnect}");
            Assert.That(canConnect, Is.True, "Should be able to connect to the database");
        });
    }

    [Test]
    public async Task AllDbSets_ShouldBeAccessible()
    {
 - Verify all DbSets are accessible
        var entityCounts = new Dictionary<string, int>();

        Assert.DoesNotThrowAsync(async () =>
        {
            // Core entities
            entityCounts["Accounts"] = await _context.Accounts.CountAsync();
            entityCounts["Customers"] = await _context.Customers.CountAsync();
            entityCounts["LoginAccounts"] = await _context.LoginAccounts.CountAsync();
            entityCounts["LoginPasswords"] = await _context.LoginPasswords.CountAsync();
            entityCounts["AccountAddresses"] = await _context.AccountAddresses.CountAsync();
            entityCounts["AccountBalances"] = await _context.AccountBalances.CountAsync();
            entityCounts["AccountSubUsers"] = await _context.AccountSubUsers.CountAsync();
            entityCounts["MobileDevices"] = await _context.MobileDevices.CountAsync();
            entityCounts["Carts"] = await _context.Carts.CountAsync();
            entityCounts["CustomerShippingDetails"] = await _context.CustomerShippingDetails.CountAsync();
            entityCounts["AccountPaymentMethods"] = await _context.AccountPaymentMethods.CountAsync();
            
            // Lookup entities
            entityCounts["AccountTypes"] = await _context.AccountTypes.CountAsync();
            entityCounts["ActiveStatuses"] = await _context.ActiveStatuses.CountAsync();
            entityCounts["LoginRoles"] = await _context.LoginRoles.CountAsync();
            entityCounts["Currencies"] = await _context.Currencies.CountAsync();
            entityCounts["Countries"] = await _context.Countries.CountAsync();
            entityCounts["States"] = await _context.States.CountAsync();
            entityCounts["BalanceSourceTypes"] = await _context.BalanceSourceTypes.CountAsync();
        });

        // Log all counts
        TestContext.WriteLine("=== DATABASE ENTITY COUNTS ===");
        TestContext.WriteLine("Core Entities:");
        foreach (var kvp in entityCounts.Where(x => !IsLookupEntity(x.Key)))
        {
            TestContext.WriteLine($"  {kvp.Key}: {kvp.Value} records");
        }

        TestContext.WriteLine("Lookup Entities:");
        foreach (var kvp in entityCounts.Where(x => IsLookupEntity(x.Key)))
        {
            TestContext.WriteLine($"  {kvp.Key}: {kvp.Value} records");
        }

        TestContext.WriteLine($"Successfully accessed {entityCounts.Count} entity tables");
    }

    [Test]
    public void DbContextConfiguration_ShouldBeValid()
    {

        Assert.DoesNotThrow(() =>
        {
            var model = _context.Model;
            var entityTypes = model.GetEntityTypes().ToList();
            
            TestContext.WriteLine($"DbContext model contains {entityTypes.Count} entity types:");
            foreach (var entityType in entityTypes)
            {
                TestContext.WriteLine($"  - {entityType.ClrType.Name} -> {entityType.GetTableName()}");
            }
            
            Assert.That(entityTypes.Count, Is.GreaterThan(0), "Should have entity types configured");
        });
    }

    private static bool IsLookupEntity(string entityName)
    {
        return entityName.Contains("Type") || 
               entityName.Contains("Status") || 
               entityName.Contains("Role") || 
               entityName.Contains("Currenc") || 
               entityName.Contains("Countr") || 
               entityName.Contains("State") || 
               entityName.Contains("BalanceSource");
    }
} 