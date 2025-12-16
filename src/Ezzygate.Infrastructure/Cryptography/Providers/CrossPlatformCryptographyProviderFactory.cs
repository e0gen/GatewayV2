using Ezzygate.Infrastructure.Cryptography.Interfaces;
using Ezzygate.Infrastructure.FileSystem;

namespace Ezzygate.Infrastructure.Cryptography.Providers;

public class CrossPlatformCryptographyProviderFactory : ICryptographyProviderFactory
{
    private readonly string _keyStorePath;
    private readonly string _dataStorePath;

    public CrossPlatformCryptographyProviderFactory() : this(".cryptkeys", ".cryptstore")
    {
    }

    private CrossPlatformCryptographyProviderFactory(string keyStorePath, string dataStorePath)
    {
        _keyStorePath = keyStorePath;
        _dataStorePath = dataStorePath;
    }

    public IDataProtectionProvider CreateDataProtectionProvider()
        => new CrossPlatformDataProtectionProvider(_keyStorePath);
    public IStorageProvider CreateStorageProvider()
        => new JsonStorageProvider(_dataStorePath);
    public IFileSystemProvider CreateFileSystemProvider()
        => new FileSystemProvider();
}