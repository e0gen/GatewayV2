using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ezzygate.Infrastructure.Ef.Context;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests;

public abstract class DatabaseTestBase : IDisposable
{
    private readonly IConfiguration _configuration;
    private bool _disposed;

    protected DatabaseTestBase()
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false)
            .Build();

        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        var options = new DbContextOptionsBuilder<EzzygateDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        Context = new EzzygateDbContext(options);
    }

    protected EzzygateDbContext Context { get; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing) Context?.Dispose();
            _disposed = true;
        }
    }
}