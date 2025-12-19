using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Ezzygate.Application.Configuration;

namespace Ezzygate.Infrastructure.Ef.Context;

public class ErrorsDbContextFactory : IDbContextFactory<ErrorsDbContext>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHostEnvironment _environment;

    public ErrorsDbContextFactory(IServiceProvider serviceProvider, IHostEnvironment environment)
    {
        _serviceProvider = serviceProvider;
        _environment = environment;
    }

    public ErrorsDbContext CreateDbContext()
    {
        var appConfigOptions = _serviceProvider.GetRequiredService<IOptions<ApplicationConfiguration>>();
        var appConfig = appConfigOptions.Value;

        var connectionString = appConfig.ErrorNetConnectionString;
        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("ErrorNetConnectionString not found in application configuration.");

        var optionsBuilder = new DbContextOptionsBuilder<ErrorsDbContext>();
        if (_environment.IsDevelopment())
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(9, 4, 00)));
        else
        {
            var connectionStringWithTrust = new SqlConnectionStringBuilder(connectionString) { TrustServerCertificate = true }.ConnectionString;
            optionsBuilder.UseSqlServer(connectionStringWithTrust);
        }

        return new ErrorsDbContext(optionsBuilder.Options);
    }
}