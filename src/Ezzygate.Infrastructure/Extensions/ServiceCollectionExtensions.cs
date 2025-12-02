using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ezzygate.Domain.Services;
using Ezzygate.Infrastructure.Configuration;
using Ezzygate.Infrastructure.Ef;
using Ezzygate.Infrastructure.Locking;
using Ezzygate.Infrastructure.Notifications;
using Ezzygate.Infrastructure.Processing;
using Ezzygate.Infrastructure.Repositories;
using Ezzygate.Infrastructure.Repositories.Interfaces;
using Ezzygate.Infrastructure.Scheduling;
using Ezzygate.Infrastructure.Services;
using Ezzygate.Infrastructure.Transactions;

namespace Ezzygate.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
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
        services.Configure<IntegrationSettings>(configuration.GetSection(IntegrationSettings.SectionName));

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.AddEzzygateDbContext(configuration, environment);
        services.AddErrorsDbContext(configuration, environment);

        if (environment.IsDevelopment())
            services.AddSingleton<IDistributedLockService, MySqlDistributedLockService>();
        else
            services.AddSingleton<IDistributedLockService, SqlServerDistributedLockService>();

        services.AddScoped<IMerchantRepository, MerchantRepository>();
        services.AddScoped<IMobileDeviceRepository, MobileDeviceRepository>();
        services.AddScoped<IRequestIdRepository, RequestIdRepository>();
        services.AddScoped<ITerminalRepository, TerminalRepository>();
        services.AddScoped<IChargeAttemptRepository, ChargeAttemptRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
        services.AddScoped<ICompanyChargeAdminRepository, CompanyChargeAdminRepository>();
        services.AddScoped<IRiskSettingsRepository, RiskSettingsRepository>();
        services.AddScoped<ITransactionContextFactory, TransactionContextFactory>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IHistoryRepository, HistoryRepository>();
        services.AddScoped<IEventPendingRepository, EventPendingRepository>();
        services.AddScoped<IIntegrationDataService, IntegrationDataService>();
        services.AddScoped<ICreditCardBinRepository, CreditCardBinRepository>();
        services.AddScoped<ICreditCardService, CreditCardService>();
        services.AddScoped<INotificationClient, NotificationClient>();
        services.AddHttpClient<ILegacyPaymentService, LegacyPaymentService>();
        services.AddMemoryCache();

        return services;
    }

    public static IServiceCollection AddDelayedTaskScheduler(this IServiceCollection services)
    {
        services.AddSingleton<DelayedTaskScheduler>();
        services.AddSingleton<IDelayedTaskScheduler>(sp => sp.GetRequiredService<DelayedTaskScheduler>());

        services.AddHostedService<DelayedTaskProcessor>();

        return services;
    }
}