namespace Ezzygate.Infrastructure.Cryptography;

public class NullConfigurationDecryptor : IConfigurationDecryptor
{
    public static readonly NullConfigurationDecryptor Instance = new();

    public bool IsAvailable => false;

    public string DecryptHex(int keyIndex, string encryptedHexValue)
    {
        return encryptedHexValue;
    }
}