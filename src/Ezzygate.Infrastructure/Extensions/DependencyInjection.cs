using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ezzygate.Infrastructure.Configuration;
using Ezzygate.Infrastructure.Ef;
using Ezzygate.Infrastructure.Repositories;
using Ezzygate.Infrastructure.Repositories.Interfaces;
using Ezzygate.Infrastructure.Services;

namespace Ezzygate.Infrastructure.Extensions;

public static class DependencyInjection
{
    private const string InfrastructureConfigFile = "Ezzygate.Infrastructure.json";

    public static IConfigurationBuilder AddInfrastructureConfigurationSource(
        this IConfigurationBuilder builder,
        string? basePath = null)
    {
        var configPath = string.IsNullOrEmpty(basePath)
            ? InfrastructureConfigFile
            : Path.Combine(basePath, InfrastructureConfigFile);

        if (!File.Exists(configPath) && string.IsNullOrEmpty(basePath))
        {
            var appBasePath = AppContext.BaseDirectory;
            var appConfigPath = Path.Combine(appBasePath, InfrastructureConfigFile);

            if (File.Exists(appConfigPath))
            {
                configPath = appConfigPath;
            }
        }

        builder.AddJsonFile(configPath, optional: false, reloadOnChange: true);

        return builder;
    }

    public static IServiceCollection AddInfrastructureConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ApplicationConfiguration>(configuration.GetSection(ApplicationConfiguration.SectionName));

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEzzygateDbContext(configuration);
        services.AddErrorsDbContext(configuration);

        services.AddScoped<IMerchantRepository, MerchantRepository>();
        services.AddScoped<IMobileDeviceRepository, MobileDeviceRepository>();
        services.AddScoped<IRequestIdRepository, RequestIdRepository>();
        services.AddScoped<ITerminalRepository, TerminalRepository>();
        services.AddScoped<IChargeAttemptRepository, ChargeAttemptRepository>();
        services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
        services.AddScoped<ITransactionContextFactory, TransactionContextFactory>();
        services.AddMemoryCache();

        return services;
    }
}