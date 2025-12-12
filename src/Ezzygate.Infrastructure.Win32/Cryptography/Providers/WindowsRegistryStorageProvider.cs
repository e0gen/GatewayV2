using System.Runtime.Versioning;
using Microsoft.Win32;
using Ezzygate.Infrastructure.Cryptography.Interfaces;

namespace Ezzygate.Infrastructure.Win32.Cryptography.Providers;

[SupportedOSPlatform("windows")]
public class WindowsRegistryStorageProvider : IStorageProvider
{
    private static RegistryKey? GetRegistryKeyFromPath(string path, bool writable)
    {
        var rootEndIndex = path.IndexOfAny(['/', '\\']);
        if (rootEndIndex == -1)
            throw new ArgumentException($"Invalid registry path: {path}");

        var rootName = path[..rootEndIndex];
        var subPath = path[(rootEndIndex + 1)..];

        var hive = rootName switch
        {
            "HKEY_CLASSES_ROOT" => RegistryHive.ClassesRoot,
            "HKEY_CURRENT_USER" => RegistryHive.CurrentUser,
            "HKEY_LOCAL_MACHINE" => RegistryHive.LocalMachine,
            "HKEY_USERS" => RegistryHive.Users,
            "HKEY_CURRENT_CONFIG" => RegistryHive.CurrentConfig,
            _ => RegistryHive.CurrentUser
        };

        return RegistryKey.OpenRemoteBaseKey(hive, string.Empty)
                   .OpenSubKey(subPath, writable);
    }

    public void SaveValue(string path, string key, string value)
    {
        Registry.SetValue(path, key, value);
    }

    public void SaveValue(string path, string key, byte[] value)
    {
        Registry.SetValue(path, key, value);
    }

    public string? LoadStringValue(string path, string key)
    {
        return Registry.GetValue(path, key, null) as string;
    }

    public byte[]? LoadBinaryValue(string path, string key)
    {
        return Registry.GetValue(path, key, null) as byte[];
    }

    public void DeleteValue(string path, string key)
    {
        using var regKey = GetRegistryKeyFromPath(path, true);
        regKey?.DeleteValue(key, false);
    }

    public void DeletePath(string path)
    {
        var dirName = Path.GetDirectoryName(path);
        var fileName = Path.GetFileName(path);

        if (string.IsNullOrEmpty(dirName) || string.IsNullOrEmpty(fileName))
            throw new ArgumentException($"Invalid path: {path}");

        using var regKey = GetRegistryKeyFromPath(dirName, true);
        regKey?.DeleteSubKeyTree(fileName, false);
    }

    public bool PathExists(string path)
    {
        try
        {
            using var key = GetRegistryKeyFromPath(path, false);
            return key != null;
        }
        catch
        {
            return false;
        }
    }

    public string[]? GetSubPaths(string path)
    {
        try
        {
            using var key = GetRegistryKeyFromPath(path, false);
            return key?.GetSubKeyNames();
        }
        catch
        {
            return null;
        }
    }

    public string[]? GetValueNames(string path)
    {
        try
        {
            using var key = GetRegistryKeyFromPath(path, false);
            return key?.GetValueNames();
        }
        catch
        {
            return null;
        }
    }
}