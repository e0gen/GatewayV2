using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ezzygate.Infrastructure.Cryptography.Interfaces;
using Ezzygate.Infrastructure.Cryptography.Providers;

namespace Ezzygate.Infrastructure.Cryptography;

public static class CryptographyServiceCollectionExtensions
{
    public static IServiceCollection AddCryptographyServices<TFactory>(this IServiceCollection services,
        Action<CryptographyConfiguration>? configureOptions = null)
        where TFactory : class, ICryptographyProviderFactory, new()
    {
        var config = new CryptographyConfiguration();
        configureOptions?.Invoke(config);

        var factory = new TFactory();
        CryptographyContext.Initialize(factory, config);

        services.TryAddSingleton(config);
        services.TryAddSingleton<ICryptographyProviderFactory>(factory);
        services.TryAddSingleton(CryptographyContext.DataProtection);
        services.TryAddSingleton(CryptographyContext.Storage);
        services.TryAddSingleton(CryptographyContext.FileSystem);

        return services;
    }

    public static IServiceCollection AddCrossPlatformCryptographyServices(this IServiceCollection services,
        Action<CryptographyConfiguration>? configureOptions = null)
    {
        return services.AddCryptographyServices<CrossPlatformCryptographyProviderFactory>(configureOptions);
    }
}