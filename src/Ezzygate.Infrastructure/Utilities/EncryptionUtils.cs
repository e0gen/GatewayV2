
namespace Ezzygate.Infrastructure.Cryptography;

public static class EncryptionUtils
{
    public static string DecryptHex(int keyIndex, string encryptedHexValue)
    {
        if (string.IsNullOrEmpty(encryptedHexValue))
            return encryptedHexValue;

        var key = SymEncryption.GetKey(keyIndex);
        return key.Decrypt(new Blob { Hex = encryptedHexValue }).Text;
    }

    public static string Decrypt(int keyIndex, byte[]? source)
    {
        if (source == null) return null!;
        if (source.Length == 0) return string.Empty;

        var key = SymEncryption.GetKey(keyIndex);
        return key.Decrypt(new Blob(source)).Text;
    }

    public static byte[] Encrypt(int keyIndex, string? source)
    {
        if (source == null) return null!;
        if (source.Length == 0) return [];

        var key = SymEncryption.GetKey(keyIndex);
        return key.Encrypt(new Blob(source)).Bytes;
    }

    public static byte[] Encrypt(int keyIndex, byte[]? source)
    {
        if (source == null) return null!;
        if (source.Length == 0) return [];

        var key = SymEncryption.GetKey(keyIndex);
        return key.Encrypt(new Blob(source)).Bytes;
    }

    public static string EncryptHex(int keyIndex, string? source)
    {
        if (source == null) return null!;
        if (source.Length == 0) return string.Empty;

        var key = SymEncryption.GetKey(keyIndex);
        return key.Encrypt(new Blob(source)).Hex;
    }
}