using Ezzygate.Infrastructure.Configuration;
using Ezzygate.Infrastructure.Cryptography;

namespace Ezzygate.WebApi.Extensions;

public static class CryptographyExtensions
{
    public static IConfigurationDecryptor CreatePlatformConfigurationDecryptor()
    {
        return OperatingSystem.IsWindows()
            ? Infrastructure.Win32.ServiceCollectionExtensions.CreateWindowsConfigurationDecryptor()
            : NullConfigurationDecryptor.Instance;
    }
}