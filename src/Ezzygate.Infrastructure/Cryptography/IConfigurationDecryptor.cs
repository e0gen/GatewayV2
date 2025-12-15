namespace Ezzygate.Infrastructure.Cryptography;

public interface IConfigurationDecryptor
{
    string DecryptHex(int keyIndex, string encryptedHexValue);

    bool IsAvailable { get; }
}

