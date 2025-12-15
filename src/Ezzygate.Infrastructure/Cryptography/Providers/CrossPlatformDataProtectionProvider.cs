using System.Security.Cryptography;
using Ezzygate.Infrastructure.Cryptography.Enums;
using Ezzygate.Infrastructure.Cryptography.Interfaces;

namespace Ezzygate.Infrastructure.Cryptography.Providers;

public class CrossPlatformDataProtectionProvider : IDataProtectionProvider
{
    private readonly string _keyStorePath;
    private readonly Dictionary<ProtectScope, byte[]> _keys = new();

    public CrossPlatformDataProtectionProvider(string keyStorePath = ".cryptkeys")
    {
        _keyStorePath = keyStorePath;
        EnsureKeysExist();
    }

    private void EnsureKeysExist()
    {
        if (!Directory.Exists(_keyStorePath))
            Directory.CreateDirectory(_keyStorePath);

        LoadOrCreateKey(ProtectScope.CurrentUser, "user.key");
        LoadOrCreateKey(ProtectScope.LocalMachine, "machine.key");
    }

    private void LoadOrCreateKey(ProtectScope scope, string fileName)
    {
        var keyPath = Path.Combine(_keyStorePath, fileName);

        if (File.Exists(keyPath))
        {
            _keys[scope] = File.ReadAllBytes(keyPath);
        }
        else
        {
            _keys[scope] = RandomNumberGenerator.GetBytes(32);
            File.WriteAllBytes(keyPath, _keys[scope]);

            // On Unix systems, restrict permissions
            if (!OperatingSystem.IsWindows())
            {
                try
                {
                    File.SetUnixFileMode(keyPath, UnixFileMode.UserRead | UnixFileMode.UserWrite);
                }
                catch
                {
                    /* Ignore if not supported */
                }
            }
        }
    }

    public byte[] Protect(byte[] data, byte[]? optionalEntropy, ProtectScope scope)
    {
        if (scope == ProtectScope.None)
            return data;

        using var aes = Aes.Create();
        aes.Key = _keys[scope];
        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor();
        var encrypted = encryptor.TransformFinalBlock(data, 0, data.Length);

        // Combine IV and encrypted data
        var result = new byte[aes.IV.Length + encrypted.Length];
        Array.Copy(aes.IV, 0, result, 0, aes.IV.Length);
        Array.Copy(encrypted, 0, result, aes.IV.Length, encrypted.Length);

        return result;
    }

    public byte[] Unprotect(byte[] data, byte[]? optionalEntropy, ProtectScope scope)
    {
        if (scope == ProtectScope.None)
            return data;

        using var aes = Aes.Create();
        aes.Key = _keys[scope];

        // Extract IV and encrypted data
        var iv = new byte[aes.IV.Length];
        var encrypted = new byte[data.Length - iv.Length];
        Array.Copy(data, 0, iv, 0, iv.Length);
        Array.Copy(data, iv.Length, encrypted, 0, encrypted.Length);

        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor();
        return decryptor.TransformFinalBlock(encrypted, 0, encrypted.Length);
    }
}