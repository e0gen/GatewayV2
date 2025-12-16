namespace Ezzygate.Infrastructure.Cryptography.Interfaces;

public interface IStorageProvider
{
    void SaveValue(string path, string key, string value);
    void SaveValue(string path, string key, byte[] value);
    string? LoadStringValue(string path, string key);
    byte[]? LoadBinaryValue(string path, string key);
    void DeleteValue(string path, string key);
    void DeletePath(string path);
    bool PathExists(string path);
    string[]? GetSubPaths(string path);
    string[]? GetValueNames(string path);
}