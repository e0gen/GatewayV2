namespace Ezzygate.Infrastructure.Cryptography;

public class CryptographyConfiguration
{
    public string DefaultKeyStore { get; set; } = @"C:\GW_KEYS";
    public string SplitPartRegistryPath { get; set; } = @"HKEY_CLASSES_ROOT\Ezzygate\Crypt\_Split";
}