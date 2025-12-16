using System.Runtime.Versioning;
using Ezzygate.Infrastructure.Cryptography.Interfaces;
using Ezzygate.Infrastructure.FileSystem;

namespace Ezzygate.Infrastructure.Win32.Cryptography.Providers;

[SupportedOSPlatform("windows")]
public class WindowsCryptographyProviderFactory : ICryptographyProviderFactory
{
    public IDataProtectionProvider CreateDataProtectionProvider() 
        => new WindowsDataProtectionProvider();

    public IStorageProvider CreateStorageProvider() 
        => new WindowsRegistryStorageProvider();

    public IFileSystemProvider CreateFileSystemProvider() 
        => new FileSystemProvider();
}