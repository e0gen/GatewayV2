using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ezzygate.Application.Interfaces;

namespace Ezzygate.Infrastructure.Ef.Context;

public class EzzygateDbContextFactory : IDbContextFactory<EzzygateDbContext>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHostEnvironment _environment;

    public EzzygateDbContextFactory(IServiceProvider serviceProvider, IHostEnvironment environment)
    {
        _serviceProvider = serviceProvider;
        _environment = environment;
    }

    public EzzygateDbContext CreateDbContext()
    {
        var domainConfigProvider = _serviceProvider.GetRequiredService<IDomainConfigurationProvider>();
        var domainConfig = domainConfigProvider.GetCurrentDomainConfiguration();
        var connectionString = domainConfig.Sql1ConnectionString;

        if (string.IsNullOrEmpty(connectionString))
        {
            var defaultDomain = domainConfigProvider.GetDomainConfiguration("default");
            connectionString = defaultDomain.Sql1ConnectionString;
        }

        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException($"Sql1ConnectionString not found for domain '{domainConfig.Host}' or default domain.");

        var optionsBuilder = new DbContextOptionsBuilder<EzzygateDbContext>();
        if (_environment.IsDevelopment())
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(9, 4, 00)));
        else
            optionsBuilder.UseSqlServer(connectionString);

        return new EzzygateDbContext(optionsBuilder.Options);
    }
}