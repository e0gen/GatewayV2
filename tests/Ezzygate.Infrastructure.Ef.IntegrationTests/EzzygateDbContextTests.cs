using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ezzygate.Infrastructure.Ef.Context;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests;

[TestFixture]
public class EzzygateDbContextTests : IDisposable
{
    private EzzygateDbContext Context { get; set; } = null!;
    private bool _disposed;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var options = new DbContextOptionsBuilder<EzzygateDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        Context = new EzzygateDbContext(options);
    }

    [SetUp]
    public async Task SetUp()
    {
        await ClearDatabaseAsync();
    }

    private static Task ClearDatabaseAsync()
    {
        return Task.CompletedTask;
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await DisposeAsync();
    }

    private async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }

    private async ValueTask DisposeAsync(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing) await Context.DisposeAsync();
            _disposed = true;
        }
    }

    public void Dispose()
    {
        DisposeAsync(false).GetAwaiter().GetResult();
    }

    public static IEnumerable<TestCaseData> DbSetTestCases
    {
        get
        {
            var dbContextType = typeof(EzzygateDbContext);
            var dbSetProperties = dbContextType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType.IsGenericType &&
                            p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

            foreach (var property in dbSetProperties)
            {
                var dbSetName = property.Name;
                var entityType = property.PropertyType.GetGenericArguments()[0];

                yield return new TestCaseData(dbSetName, entityType)
                    .SetName($"DbSet_{dbSetName}");
            }
        }
    }

    [Test]
    public void CanConnect_DatabaseIsAvailable_ReturnsTrue() =>
        Assert.That(Context.Database.CanConnect(), Is.True);

    [Test, TestCaseSource(nameof(DbSetTestCases))]
    public void DbSet_IsAccessible(string dbSetName, Type entityType)
    {
        var property = typeof(EzzygateDbContext).GetProperty(dbSetName);
        Assert.That(property, Is.Not.Null, $"DbSet {dbSetName} not found");

        var dbSet = property!.GetValue(Context);
        Assert.That(dbSet, Is.Not.Null, $"DbSet {dbSetName} is null");

        var actualEntityType = property.PropertyType.GetGenericArguments()[0];
        Assert.That(actualEntityType, Is.EqualTo(entityType),
            $"Entity type mismatch for {dbSetName}");
    }

    [Test, TestCaseSource(nameof(DbSetTestCases))]
    public Task DbSet_CanQueryData(string dbSetName, Type entityType)
    {
        var property = typeof(EzzygateDbContext).GetProperty(dbSetName);
        Assert.That(property, Is.Not.Null, $"DbSet {dbSetName} not found");

        var dbSet = property!.GetValue(Context);
        Assert.That(dbSet, Is.Not.Null, $"DbSet {dbSetName} is null");

        Assert.DoesNotThrowAsync(async () => { await TestDbSetQuery(dbSet!, entityType); },
            $"Failed to query {dbSetName} DbSet");

        return Task.CompletedTask;
    }

    private async Task TestDbSetQuery(object dbSet, Type entityType)
    {
        var method = GetType()
            .GetMethod(nameof(TestDbSetQueryGeneric), BindingFlags.NonPublic | BindingFlags.Instance)
            ?.MakeGenericMethod(entityType);

        if (method == null)
            throw new InvalidOperationException("Could not find generic test method");

        var task = method.Invoke(this, [dbSet]) as Task;
        await task!;
    }

    private async Task TestDbSetQueryGeneric<T>(object dbSet) where T : class
    {
        var typedDbSet = dbSet as IQueryable<T>;
        Assert.That(typedDbSet, Is.Not.Null, $"DbSet is not IQueryable<{typeof(T).Name}>");

        await typedDbSet!.CountAsync();
    }
}