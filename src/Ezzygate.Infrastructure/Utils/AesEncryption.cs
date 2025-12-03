using System.Security.Cryptography;

namespace Ezzygate.Infrastructure.Utils;

public static class AesEncryption
{
    private static readonly byte[] Key = "8080808080808080"u8.ToArray();
    private static readonly byte[] Iv = "8080808080808080"u8.ToArray();

    public static string EncryptStringAes(string plainText)
    {
        var encrypted = EncryptStringToBytes(plainText, Key, Iv);
        return Convert.ToBase64String(encrypted);
    }

    public static string DecryptStringAes(string cipherText)
    {
        var encrypted = Convert.FromBase64String(cipherText);
        return DecryptStringFromBytes(encrypted, Key, Iv);
    }

    private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
    {
        ArgumentNullException.ThrowIfNull(plainText);
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(iv);

        if (plainText.Length == 0)
            throw new ArgumentException("Plain text cannot be empty", nameof(plainText));
        if (key.Length == 0)
            throw new ArgumentException("Key cannot be empty", nameof(key));
        if (iv.Length == 0)
            throw new ArgumentException("IV cannot be empty", nameof(iv));

        using var aes = Aes.Create();
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = key;
        aes.IV = iv;

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var msEncrypt = new MemoryStream();
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
            using var swEncrypt = new StreamWriter(csEncrypt);
            swEncrypt.Write(plainText);
        }
        return msEncrypt.ToArray();
    }

    private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
    {
        ArgumentNullException.ThrowIfNull(cipherText);
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(iv);

        if (cipherText.Length == 0)
            throw new ArgumentException("Cipher text cannot be empty", nameof(cipherText));
        if (key.Length == 0)
            throw new ArgumentException("Key cannot be empty", nameof(key));
        if (iv.Length == 0)
            throw new ArgumentException("IV cannot be empty", nameof(iv));

        using var aes = Aes.Create();
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = key;
        aes.IV = iv;

        try
        {
            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var msDecrypt = new MemoryStream(cipherText);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);
            return srDecrypt.ReadToEnd();
        }
        catch
        {
            return "keyError";
        }
    }
}