using Ezzygate.Infrastructure.Cryptography;
using Ezzygate.Infrastructure.Win32;

namespace Ezzygate.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPlatformCryptographyServices(this IServiceCollection services)
    {
        if (OperatingSystem.IsWindows())
            services.AddWindowsCryptographyServices();
        else
            services.AddCrossPlatformCryptographyServices();

        return services;
    }
}