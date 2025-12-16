namespace Ezzygate.Infrastructure.Cryptography;

public static class SymEncryption
{
    private static readonly Dictionary<int, SymEncryptionKey> CachedKeys = new();

    private static SymEncryptionKey FromFile(string fileName, string? password = null)
    {
        var key = new SymEncryptionKey();
        key.LoadFromFile(fileName, password);
        return key;
    }

    private static SymEncryptionKey FromKeyIndex(int keyIndex)
    {
        var fileName = MapFileName(keyIndex);
        return FromFile(fileName);
    }

    public static SymEncryptionKey GetKey(int index)
    {
        if (CachedKeys.TryGetValue(index, out var key))
            return key;

        key = FromKeyIndex(index);
        CachedKeys[index] = key;
        return key;
    }

    public static void ClearKeyCache()
    {
        foreach (var key in CachedKeys.Values)
            key.Dispose();
        CachedKeys.Clear();
    }

    public static int[] EnumAvailableKeyIndexes(string? storePath = null)
    {
        var config = CryptographyContext.Config;
        var storage = CryptographyContext.Storage;
        var fileSystem = CryptographyContext.FileSystem;

        storePath ??= config.DefaultKeyStore;

        var isRegistry = storePath.StartsWith("Registry://");
        if (isRegistry)
            storePath = storePath.Substring("Registry://".Length);

        var result = new List<int>();
        string[]? names;

        if (isRegistry)
        {
            names = storage.GetSubPaths(storePath);
        }
        else
        {
            if (!fileSystem.DirectoryExists(storePath))
                return [];
            names = fileSystem.GetFiles(storePath, "*.key");
        }

        if (names != null)
        {
            foreach (var name in names)
            {
                var fileName = fileSystem.GetFileNameWithoutExtension(name);
                if (int.TryParse(fileName, out int index))
                    result.Add(index);
            }
        }

        return result.ToArray();
    }

    private static string MapFileName(int keyIndex)
    {
        var config = CryptographyContext.Config;
        var storePath = config.DefaultKeyStore.Replace("Registry://", "");
        return Path.Combine(storePath, $"{keyIndex}.key");
    }
}