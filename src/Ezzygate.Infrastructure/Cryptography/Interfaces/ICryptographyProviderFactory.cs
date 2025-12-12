using Ezzygate.Infrastructure.FileSystem;

namespace Ezzygate.Infrastructure.Cryptography.Interfaces;

public interface ICryptographyProviderFactory
{
    IDataProtectionProvider CreateDataProtectionProvider();
    IStorageProvider CreateStorageProvider();
    IFileSystemProvider CreateFileSystemProvider();
}