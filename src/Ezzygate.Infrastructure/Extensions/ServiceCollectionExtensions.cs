using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ezzygate.Application.Configuration;
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
    public static IServiceCollection AddInfrastructureConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ApplicationConfiguration>(configuration.GetSection(ApplicationConfiguration.SectionName));

        services.AddScoped<IDomainConfigurationProvider, DomainConfigurationProvider>();
        services.AddScoped(sp => sp.GetRequiredService<IDomainConfigurationProvider>().GetCurrentDomainConfiguration());

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.AddEzzygateDbContext();
        services.AddErrorsDbContext();

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
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IHistoryRepository, HistoryRepository>();
        services.AddScoped<IEventPendingRepository, EventPendingRepository>();
        services.AddScoped<IRecurringRepository, RecurringRepository>();
        services.AddScoped<IIntegrationDataService, IntegrationDataService>();
        services.AddScoped<ICreditCardBinRepository, CreditCardBinRepository>();
        services.AddScoped<ICreditCardService, CreditCardService>();
        services.AddScoped<IPaymentPageSettingsRepository, PaymentPageSettingsRepository>();
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