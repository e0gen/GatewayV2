using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Ezzygate.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Add MediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        // Add FluentValidation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Add application services (to be implemented)
        // services.AddScoped<IMerchantService, MerchantService>();
        // services.AddScoped<ITransactionService, TransactionService>();
        // services.AddScoped<IAccountService, AccountService>();

        return services;
    }
} 