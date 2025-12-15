using System.Text.Json;
using Ezzygate.Infrastructure.Cryptography.Interfaces;
using Ezzygate.Infrastructure.FileSystem;

namespace Ezzygate.Infrastructure.Cryptography.Providers;

public class JsonStorageProvider : IStorageProvider
{
    private readonly string _basePath;
    private readonly IFileSystemProvider _fileSystem;

    public JsonStorageProvider(string basePath = ".cryptstore", IFileSystemProvider? fileSystem = null)
    {
        _basePath = basePath;
        _fileSystem = fileSystem ?? new FileSystemProvider();

        if (!_fileSystem.DirectoryExists(_basePath))
            _fileSystem.CreateDirectory(_basePath);
    }

    private string GetPhysicalPath(string logicalPath)
    {
        var normalized = logicalPath
            .Replace("HKEY_CLASSES_ROOT\\", "")
            .Replace("HKEY_CURRENT_USER\\", "")
            .Replace("HKEY_LOCAL_MACHINE\\", "")
            .Replace("\\", Path.DirectorySeparatorChar.ToString())
            .Replace("/", Path.DirectorySeparatorChar.ToString());

        return _fileSystem.Combine(_basePath, normalized + ".json");
    }

    private Dictionary<string, object> LoadStore(string path)
    {
        var physicalPath = GetPhysicalPath(path);

        if (!_fileSystem.FileExists(physicalPath))
            return new Dictionary<string, object>();

        var json = _fileSystem.ReadAllText(physicalPath, System.Text.Encoding.UTF8);
        return JsonSerializer.Deserialize<Dictionary<string, object>>(json)
               ?? new Dictionary<string, object>();
    }

    private void SaveStore(string path, Dictionary<string, object> store)
    {
        var physicalPath = GetPhysicalPath(path);
        var directory = _fileSystem.GetDirectoryName(physicalPath);

        if (!string.IsNullOrEmpty(directory) && !_fileSystem.DirectoryExists(directory))
            _fileSystem.CreateDirectory(directory);

        var json = JsonSerializer.Serialize(store, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        _fileSystem.WriteAllText(physicalPath, json, System.Text.Encoding.UTF8);
    }

    public void SaveValue(string path, string key, string value)
    {
        var store = LoadStore(path);
        store[key] = value;
        SaveStore(path, store);
    }

    public void SaveValue(string path, string key, byte[] value)
    {
        var store = LoadStore(path);
        store[key] = Convert.ToBase64String(value);
        SaveStore(path, store);
    }

    public string? LoadStringValue(string path, string key)
    {
        var store = LoadStore(path);
        return store.TryGetValue(key, out var value) ? value.ToString() : null;
    }

    public byte[]? LoadBinaryValue(string path, string key)
    {
        var store = LoadStore(path);
        if (!store.TryGetValue(key, out var value))
            return null;

        var base64 = value.ToString();
        return string.IsNullOrEmpty(base64) ? null : Convert.FromBase64String(base64);
    }

    public void DeleteValue(string path, string key)
    {
        var store = LoadStore(path);
        store.Remove(key);
        SaveStore(path, store);
    }

    public void DeletePath(string path)
    {
        var physicalPath = GetPhysicalPath(path);
        if (_fileSystem.FileExists(physicalPath))
            _fileSystem.DeleteFile(physicalPath);
    }

    public bool PathExists(string path)
    {
        return _fileSystem.FileExists(GetPhysicalPath(path));
    }

    public string[]? GetSubPaths(string path)
    {
        var physicalPath = _fileSystem.GetDirectoryName(GetPhysicalPath(path));
        if (!_fileSystem.DirectoryExists(physicalPath))
            return null;

        return _fileSystem.GetFiles(physicalPath, "*.json")
            .Select(f => _fileSystem.GetFileNameWithoutExtension(f))
            .ToArray();
    }

    public string[] GetValueNames(string path)
    {
        var store = LoadStore(path);
        return store.Keys.ToArray();
    }
}