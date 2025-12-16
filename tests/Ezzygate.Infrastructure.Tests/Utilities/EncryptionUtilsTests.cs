using System.Runtime.Versioning;
using System.Text;
using Ezzygate.Infrastructure.Cryptography;
using Ezzygate.Infrastructure.Utilities;
using Ezzygate.Infrastructure.Win32.Cryptography.Providers;

namespace Ezzygate.Infrastructure.Tests.Utilities;

[TestFixture]
[SupportedOSPlatform("windows")]
public class EncryptionUtilsTests
{
    private string _testKeysPath = null!;
    private const int TestKeyNumber = 999;

    [SetUp]
    public void SetUp()
    {
        _testKeysPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "TestKeys");
        
        if (!Directory.Exists(_testKeysPath))
            Directory.CreateDirectory(_testKeysPath);

        var factory = new WindowsCryptographyProviderFactory();
        var config = new CryptographyConfiguration
        {
            DefaultKeyStore = _testKeysPath
        };

        CryptographyContext.Reset();
        CryptographyContext.Initialize(factory, config);

        var keyFilePath = Path.Combine(_testKeysPath, $"{TestKeyNumber}.key");
        if (!File.Exists(keyFilePath))
        {
            using var key = new SymEncryptionKey();
            key.SaveToFile(keyFilePath);
        }
    }

    [TearDown]
    public void TearDown()
    {
        SymEncryption.ClearKeyCache();
        CryptographyContext.Reset();
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void EncryptHex_WithValidString_ReturnsHexString()
    {
        const string plainText = "Test encryption string";

        var encryptedHex = EncryptionUtils.EncryptHex(TestKeyNumber, plainText);

        Assert.Multiple(() =>
        {
            Assert.That(encryptedHex, Is.Not.Null);
            Assert.That(encryptedHex, Is.Not.Empty);
            Assert.That(encryptedHex, Is.Not.EqualTo(plainText));
            Assert.That(encryptedHex.Length % 2, Is.EqualTo(0));
        });
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void EncryptHex_ThenDecryptHex_RoundTripSuccessful()
    {
        const string plainText = "Round trip test value";

        var encryptedHex = EncryptionUtils.EncryptHex(TestKeyNumber, plainText);
        var decrypted = EncryptionUtils.DecryptHex(TestKeyNumber, encryptedHex);

        Assert.That(decrypted, Is.EqualTo(plainText));
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void EncryptHex_WithEmptyString_ReturnsEmptyString()
    {
        var encryptedHex = EncryptionUtils.EncryptHex(TestKeyNumber, string.Empty);

        Assert.That(encryptedHex, Is.EqualTo(string.Empty));
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void EncryptHex_WithNull_ReturnsNull()
    {
        var encryptedHex = EncryptionUtils.EncryptHex(TestKeyNumber, null);

        Assert.That(encryptedHex, Is.Null);
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void EncryptHex_WithVariousInputs_EncryptsCorrectly()
    {
        var testCases = new[]
        {
            "Simple text",
            "Text with numbers 12345",
            "Special chars: !@#$%^&*()",
            "Unicode: 你好世界 🌍",
            "Very long text: " + new string('A', 1000),
            "Connection string: Data Source=Server;Initial Catalog=DB",
            "New\nLine\nText",
            "   Whitespace   "
        };

        foreach (var plainText in testCases)
        {
            var encryptedHex = EncryptionUtils.EncryptHex(TestKeyNumber, plainText);
            var decrypted = EncryptionUtils.DecryptHex(TestKeyNumber, encryptedHex);

            Assert.Multiple(() =>
            {
                Assert.That(encryptedHex, Is.Not.Null, $"Failed for: {plainText}");
                Assert.That(decrypted, Is.EqualTo(plainText), $"Failed for: {plainText}");
            });
        }
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void DecryptHex_WithValidHexString_DecryptsCorrectly()
    {
        const string plainText = "Test decryption";
        var encryptedHex = EncryptionUtils.EncryptHex(TestKeyNumber, plainText);

        var decrypted = EncryptionUtils.DecryptHex(TestKeyNumber, encryptedHex);

        Assert.That(decrypted, Is.EqualTo(plainText));
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void DecryptHex_WithEmptyString_ReturnsEmptyString()
    {
        var decrypted = EncryptionUtils.DecryptHex(TestKeyNumber, string.Empty);

        Assert.That(decrypted, Is.EqualTo(string.Empty));
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void DecryptHex_WithNull_ReturnsNull()
    {
        var decrypted = EncryptionUtils.DecryptHex(TestKeyNumber, null!);

        Assert.That(decrypted, Is.Null);
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void DecryptHex_WithInvalidHex_ThrowsException()
    {
        const string invalidHex = "INVALID_HEX_STRING";

        Assert.Throws<FormatException>(() => EncryptionUtils.DecryptHex(TestKeyNumber, invalidHex));
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void Encrypt_WithValidString_ReturnsByteArray()
    {
        const string plainText = "Test encryption";

        var encryptedBytes = EncryptionUtils.Encrypt(TestKeyNumber, plainText);

        Assert.Multiple(() =>
        {
            Assert.That(encryptedBytes, Is.Not.Null);
            Assert.That(encryptedBytes, Is.Not.Empty);
        });
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void Encrypt_ThenDecrypt_RoundTripSuccessful()
    {
        const string plainText = "Round trip byte array test";

        var encryptedBytes = EncryptionUtils.Encrypt(TestKeyNumber, plainText);
        var decrypted = EncryptionUtils.Decrypt(TestKeyNumber, encryptedBytes);

        Assert.That(decrypted, Is.EqualTo(plainText));
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void Encrypt_WithEmptyString_ReturnsEmptyArray()
    {
        var encryptedBytes = EncryptionUtils.Encrypt(TestKeyNumber, string.Empty);

        Assert.That(encryptedBytes, Is.Empty);
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void Encrypt_WithNull_ReturnsNull()
    {
        var encryptedBytes = EncryptionUtils.Encrypt(TestKeyNumber, (string?)null);

        Assert.That(encryptedBytes, Is.Null);
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void Encrypt_WithVariousStrings_EncryptsCorrectly()
    {
        var testCases = new[]
        {
            "Simple",
            "With spaces and numbers 123",
            "Special: !@#$%",
            "Unicode: 测试",
            new string('X', 500)
        };

        foreach (var plainText in testCases)
        {
            var encryptedBytes = EncryptionUtils.Encrypt(TestKeyNumber, plainText);
            var decrypted = EncryptionUtils.Decrypt(TestKeyNumber, encryptedBytes);

            Assert.That(decrypted, Is.EqualTo(plainText), $"Failed for: {plainText}");
        }
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void Encrypt_WithValidByteArray_ReturnsEncryptedByteArray()
    {
        var plainBytes = "Test byte array encryption"u8.ToArray();

        var encryptedBytes = EncryptionUtils.Encrypt(TestKeyNumber, plainBytes);

        Assert.Multiple(() =>
        {
            Assert.That(encryptedBytes, Is.Not.Null);
            Assert.That(encryptedBytes.Length, Is.GreaterThan(0));
            Assert.That(encryptedBytes, Is.Not.EqualTo(plainBytes));
        });
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void Encrypt_WithByteArray_ThenDecrypt_RoundTripSuccessful()
    {
        var plainBytes = Encoding.UTF8.GetBytes("Round trip byte array");

        var encryptedBytes = EncryptionUtils.Encrypt(TestKeyNumber, plainBytes);
        var decrypted = EncryptionUtils.Decrypt(TestKeyNumber, encryptedBytes);
        var decryptedBytes = Encoding.UTF8.GetBytes(decrypted);

        Assert.That(decryptedBytes, Is.EqualTo(plainBytes));
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void Encrypt_WithEmptyByteArray_ReturnsEmptyArray()
    {
        var encryptedBytes = EncryptionUtils.Encrypt(TestKeyNumber, []);

        Assert.That(encryptedBytes, Is.Empty);
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void Encrypt_WithNullByteArray_ReturnsNull()
    {
        var encryptedBytes = EncryptionUtils.Encrypt(TestKeyNumber, (byte[]?)null);

        Assert.That(encryptedBytes, Is.Null);
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void Encrypt_WithVariousByteArrays_EncryptsCorrectly()
    {
        var testCases = new[]
        {
            "Simple bytes"u8.ToArray(),
            "With numbers 12345"u8.ToArray(),
            "Unicode test: 测试"u8.ToArray(),
            Encoding.UTF8.GetBytes(new string('X', 1000))
        };

        foreach (var plainBytes in testCases)
        {
            var encryptedBytes = EncryptionUtils.Encrypt(TestKeyNumber, plainBytes);
            var decrypted = EncryptionUtils.Decrypt(TestKeyNumber, encryptedBytes);
            var decryptedBytes = Encoding.UTF8.GetBytes(decrypted);

            Assert.That(decryptedBytes, Is.EqualTo(plainBytes), $"Failed for byte array of length {plainBytes.Length}");
        }
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void Decrypt_WithValidByteArray_DecryptsCorrectly()
    {
        const string plainText = "Test decryption from bytes";
        var encryptedBytes = EncryptionUtils.Encrypt(TestKeyNumber, plainText);

        var decrypted = EncryptionUtils.Decrypt(TestKeyNumber, encryptedBytes);

        Assert.That(decrypted, Is.EqualTo(plainText));
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void Decrypt_WithEmptyByteArray_ReturnsEmptyString()
    {
        var decrypted = EncryptionUtils.Decrypt(TestKeyNumber, []);

        Assert.That(decrypted, Is.EqualTo(string.Empty));
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void Decrypt_WithNullByteArray_ReturnsNull()
    {
        var decrypted = EncryptionUtils.Decrypt(TestKeyNumber, null);

        Assert.That(decrypted, Is.Null);
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void Decrypt_WithInvalidByteArray_ThrowsException()
    {
        var invalidBytes = new byte[] { 0x00, 0x01, 0x02, 0x03 };

        Assert.Throws<System.Security.Cryptography.CryptographicException>(() => EncryptionUtils.Decrypt(TestKeyNumber, invalidBytes));
    }
}