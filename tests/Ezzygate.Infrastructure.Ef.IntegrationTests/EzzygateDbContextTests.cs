using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ezzygate.Infrastructure.Ef.Context;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests;

public class EzzygateDbContextTests : IDisposable
{
    private EzzygateDbContext Context { get; }

    public EzzygateDbContextTests()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var options = new DbContextOptionsBuilder<EzzygateDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        Context = new EzzygateDbContext(options);
    }

    public void Dispose()
    {
        Context?.Dispose();
    }

    [Test]
    public void DbContext_CanConnect()
    {
        var canConnect = Context.Database.CanConnect();
        Assert.That(canConnect, Is.True);
    }

    [Test]
    public void AccountAddresses_Readable()
    {
        Assert.DoesNotThrowAsync(async () => { await Context.AccountAddresses.CountAsync(); });
    }

    [Test]
    public void AccountBalances_Readable()
    {
        Assert.DoesNotThrowAsync(async () => { await Context.AccountBalances.CountAsync(); });
    }
}