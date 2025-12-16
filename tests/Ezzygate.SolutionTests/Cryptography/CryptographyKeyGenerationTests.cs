using System.Runtime.Versioning;
using Ezzygate.Infrastructure.Cryptography;
using Ezzygate.Infrastructure.Cryptography.Providers;
using Ezzygate.Infrastructure.Win32.Cryptography.Providers;

namespace Ezzygate.SolutionTests.Cryptography;

[TestFixture]
public class CryptographyKeyGenerationTests
{
    private string _testKeysPath = null!;

    [SetUp]
    public void SetUp()
    {
        _testKeysPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestKeys");
        if (!Directory.Exists(_testKeysPath))
            Directory.CreateDirectory(_testKeysPath);

        CryptographyContext.Reset();
    }

    [TearDown]
    public void TearDown()
    {
        SymEncryption.ClearKeyCache();
        CryptographyContext.Reset();
    }

    [Test]
    [Explicit("Manual test for generating Windows symmetric key")]
    [SupportedOSPlatform("windows")]
    public void GenerateWindowsSymmetricKey_CreatesKeyFile()
    {
        var factory = new WindowsCryptographyProviderFactory();
        var config = new CryptographyConfiguration
        {
            DefaultKeyStore = _testKeysPath
        };
        CryptographyContext.Initialize(factory, config);

        const int keyNumber = 999;
        var keyFilePath = Path.Combine(_testKeysPath, $"{keyNumber}.key");

        using var key = new SymEncryptionKey();
        key.SaveToFile(keyFilePath);

        Assert.That(File.Exists(keyFilePath), Is.True, "Key file should be created");

        var keyFileContent = File.ReadAllText(keyFilePath);
        TestContext.Out.WriteLine("=== Windows Symmetric Key Generated ===");
        TestContext.Out.WriteLine($"Key Number: {keyNumber}");
        TestContext.Out.WriteLine($"Key File Path: {keyFilePath}");
        TestContext.Out.WriteLine($"Key File Content:\n{keyFileContent}");
        TestContext.Out.WriteLine("========================================");
    }

    [Test]
    [Explicit("Manual test for encrypting value with Windows key")]
    [SupportedOSPlatform("windows")]
    public void EncryptValueWithWindowsKey_OutputsEncryptedValue()
    {
        var factory = new WindowsCryptographyProviderFactory();
        var config = new CryptographyConfiguration
        {
            DefaultKeyStore = _testKeysPath
        };
        CryptographyContext.Initialize(factory, config);

        const int keyNumber = 999;
        var keyFilePath = Path.Combine(_testKeysPath, $"{keyNumber}.key");

        if (!File.Exists(keyFilePath))
        {
            using var newKey = new SymEncryptionKey();
            newKey.SaveToFile(keyFilePath);
        }

        const string plainTextValue = "Data Source=EnvTestServer;Initial Catalog=EnvTestDB2";

        var encryptedHex = EncryptionUtils.EncryptHex(keyNumber, plainTextValue);
        var encryptedBytes = EncryptionUtils.Encrypt(keyNumber, plainTextValue);
        var encryptedBase64 = Convert.ToBase64String(encryptedBytes);

        TestContext.Out.WriteLine("=== Windows Encryption Result ===");
        TestContext.Out.WriteLine($"Key Number: {keyNumber}");
        TestContext.Out.WriteLine($"Plain Text: {plainTextValue}");
        TestContext.Out.WriteLine($"Encrypted (Hex): {encryptedHex}");
        TestContext.Out.WriteLine($"Encrypted (Base64): {encryptedBase64}");
        TestContext.Out.WriteLine("=================================");

        var decrypted = EncryptionUtils.DecryptHex(keyNumber, encryptedHex);
        Assert.That(decrypted, Is.EqualTo(plainTextValue), "Round-trip decryption should match original value");
    }

    [Test]
    [Explicit("Manual test for generating cross-platform symmetric key")]
    public void GenerateCrossPlatformSymmetricKey_CreatesKeyFile()
    {
        var crossPlatformKeyStore = Path.Combine(_testKeysPath, ".cryptkeys");
        var factory = new CrossPlatformCryptographyProviderFactory();
        var config = new CryptographyConfiguration
        {
            DefaultKeyStore = _testKeysPath
        };
        CryptographyContext.Initialize(factory, config);

        const int keyNumber = 998; // Use a unique key number for testing
        var keyFilePath = Path.Combine(_testKeysPath, $"{keyNumber}.key");

        using var key = new SymEncryptionKey();
        key.SaveToFile(keyFilePath);

        Assert.That(File.Exists(keyFilePath), Is.True, "Key file should be created");

        var keyFileContent = File.ReadAllText(keyFilePath);
        TestContext.Out.WriteLine("=== Cross-Platform Symmetric Key Generated ===");
        TestContext.Out.WriteLine($"Key Number: {keyNumber}");
        TestContext.Out.WriteLine($"Key File Path: {keyFilePath}");
        TestContext.Out.WriteLine($"Key File Content:\n{keyFileContent}");
        TestContext.Out.WriteLine("===============================================");
    }

    [Test]
    [Explicit("Manual test for encrypting value with cross-platform key")]
    public void EncryptValueWithCrossPlatformKey_OutputsEncryptedValue()
    {
        var factory = new CrossPlatformCryptographyProviderFactory();
        var config = new CryptographyConfiguration
        {
            DefaultKeyStore = _testKeysPath
        };
        CryptographyContext.Initialize(factory, config);

        const int keyNumber = 998;
        var keyFilePath = Path.Combine(_testKeysPath, $"{keyNumber}.key");

        if (!File.Exists(keyFilePath))
        {
            using var newKey = new SymEncryptionKey();
            newKey.SaveToFile(keyFilePath);
        }

        const string plainTextValue = "SecretValue";

        var encryptedHex = EncryptionUtils.EncryptHex(keyNumber, plainTextValue);
        var encryptedBytes = EncryptionUtils.Encrypt(keyNumber, plainTextValue);
        var encryptedBase64 = Convert.ToBase64String(encryptedBytes);

        TestContext.Out.WriteLine("=== Cross-Platform Encryption Result ===");
        TestContext.Out.WriteLine($"Key Number: {keyNumber}");
        TestContext.Out.WriteLine($"Plain Text: {plainTextValue}");
        TestContext.Out.WriteLine($"Encrypted (Hex): {encryptedHex}");
        TestContext.Out.WriteLine($"Encrypted (Base64): {encryptedBase64}");
        TestContext.Out.WriteLine("=========================================");

        var decrypted = EncryptionUtils.DecryptHex(keyNumber, encryptedHex);
        Assert.That(decrypted, Is.EqualTo(plainTextValue), "Round-trip decryption should match original value");
    }
}