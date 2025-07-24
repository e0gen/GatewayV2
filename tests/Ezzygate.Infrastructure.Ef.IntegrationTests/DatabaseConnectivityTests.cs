using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests;

[TestFixture]
[Category("DatabaseConnectivity")]
public class DatabaseConnectivityTests : DatabaseTestBase
{
    [Test]
    [Category("BasicConnectivity")]
    public void ShouldBeAbleToConnect()
    {
        var canConnect = Context.Database.CanConnect();
        TestContext.WriteLine($"Database connection status: {canConnect}");
        Assert.That(canConnect, Is.True, "Should be able to connect to the database");
    }

    [Test]
    [Category("SchemaValidation")]
    public void ShouldHaveValidDbContextConfiguration()
    {
        var model = Context.Model;
        var entityTypes = model.GetEntityTypes().ToList();

        TestContext.WriteLine($"DbContext model contains {entityTypes.Count} entity types:");
        foreach (var entityType in entityTypes)
        {
            TestContext.WriteLine($"  - {entityType.ClrType.Name} -> {entityType.GetTableName()}");
        }

        Assert.That(entityTypes.Count, Is.GreaterThan(0), "Should have entity types configured");
    }

    [Test]
    [Category("SchemaValidation")]
    public void ShouldHaveValidDatabaseSchema()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            var pendingMigrations = await Context.Database.GetPendingMigrationsAsync();
            var appliedMigrations = await Context.Database.GetAppliedMigrationsAsync();
            
            TestContext.WriteLine($"Applied migrations: {appliedMigrations.Count()}");
            TestContext.WriteLine($"Pending migrations: {pendingMigrations.Count()}");
            
            // In a production environment, there should be no pending migrations
            // For development/testing, we allow pending migrations
            Assert.That(appliedMigrations.Count(), Is.GreaterThan(0), "Should have at least one applied migration");
        });
    }
}