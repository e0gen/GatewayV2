using System.Runtime.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Ezzygate.Infrastructure.Configuration;
using Ezzygate.Infrastructure.Cryptography;
using Ezzygate.Infrastructure.Win32.Cryptography.Providers;

namespace Ezzygate.Infrastructure.Win32;

public static class ServiceCollectionExtensions
{
    [SupportedOSPlatform("windows")]
    public static IServiceCollection AddWindowsCryptographyServices(
        this IServiceCollection services, Action<CryptographyConfiguration>? configureOptions = null)
    {
        return services.AddCryptographyServices<WindowsCryptographyProviderFactory>(configureOptions);
    }

    [SupportedOSPlatform("windows")]
    public static IConfigurationDecryptor CreateWindowsConfigurationDecryptor(CryptographyConfiguration? config = null)
    {
        return CryptographyServiceCollectionExtensions.CreateConfigurationDecryptor(
            new WindowsCryptographyProviderFactory(), config);
    }
}
