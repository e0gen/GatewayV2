using Ezzygate.Infrastructure.Cryptography.Interfaces;
using Ezzygate.Infrastructure.FileSystem;

namespace Ezzygate.Infrastructure.Cryptography;

public static class CryptographyContext
{
    private static ICryptographyProviderFactory? _providerFactory;
    private static CryptographyConfiguration? _config;
    private static IDataProtectionProvider? _dataProtection;
    private static IStorageProvider? _storage;
    private static IFileSystemProvider? _fileSystem;
    private static readonly Lock InitLock = new();

    public static bool IsInitialized { get; private set; }
    private const string? UnInitializedMessage = "CryptographyContext has not been initialized.";

    public static ICryptographyProviderFactory ProviderFactory =>
        _providerFactory ?? throw new InvalidOperationException(UnInitializedMessage);

    public static CryptographyConfiguration Config =>
        _config ?? throw new InvalidOperationException(UnInitializedMessage);

    public static IDataProtectionProvider DataProtection =>
        _dataProtection ?? throw new InvalidOperationException(UnInitializedMessage);

    public static IStorageProvider Storage =>
        _storage ?? throw new InvalidOperationException(UnInitializedMessage);

    public static IFileSystemProvider FileSystem =>
        _fileSystem ?? throw new InvalidOperationException(UnInitializedMessage);

    public static void Initialize(
        ICryptographyProviderFactory providerFactory,
        CryptographyConfiguration? config = null)
    {
        if (IsInitialized)
            return;

        lock (InitLock)
        {
            if (IsInitialized)
                return;

            _providerFactory = providerFactory ?? throw new ArgumentNullException(nameof(providerFactory));
            _config = config ?? new CryptographyConfiguration();

            _dataProtection = providerFactory.CreateDataProtectionProvider();
            _storage = providerFactory.CreateStorageProvider();
            _fileSystem = providerFactory.CreateFileSystemProvider();

            IsInitialized = true;
        }
    }

    public static void Reset()
    {
        lock (InitLock)
        {
            _providerFactory = null;
            _config = null;
            _dataProtection = null;
            _storage = null;
            _fileSystem = null;
            IsInitialized = false;
        }
    }
}
