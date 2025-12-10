using Ezzygate.Infrastructure.Utilities;

namespace Ezzygate.Infrastructure.Tests.Utilities;

[TestFixture]
public class AesEncryptionTests
{
    [Test]
    public void EncryptStringAes_ThenDecryptStringAes_RoundTripSuccessful()
    {
        const string plainText = "Hello, World!";

        var encrypted = AesEncryption.EncryptStringAes(plainText);
        var decrypted = AesEncryption.DecryptStringAes(encrypted);

        Assert.Multiple(() =>
        {
            Assert.That(encrypted, Is.Not.Null);
            Assert.That(encrypted, Is.Not.EqualTo(plainText));
            Assert.That(decrypted, Is.EqualTo(plainText));
        });
    }

    [Test]
    public void EncryptStringAes_WithVariousInputs_EncryptsCorrectly()
    {
        var testCases = new[]
        {
            "Simple text",
            "Text with numbers 12345",
            "Special chars: !@#$%^&*()",
            "Unicode: 你好世界 🌍",
            "Very long text: " + new string('A', 1000),
            "Empty after trim:   ",
            "New\nLine\nText"
        };

        foreach (var plainText in testCases)
        {
            var encrypted = AesEncryption.EncryptStringAes(plainText);
            var decrypted = AesEncryption.DecryptStringAes(encrypted);

            Assert.Multiple(() =>
            {
                Assert.That(encrypted, Is.Not.Null, $"Failed for: {plainText}");
                Assert.That(decrypted, Is.EqualTo(plainText), $"Failed for: {plainText}");
            });
        }
    }

    [Test]
    public void EncryptStringAes_EncryptedOutput_IsBase64Encoded()
    {
        const string plainText = "Test";
        var encrypted = AesEncryption.EncryptStringAes(plainText);

        Assert.Multiple(() =>
        {
            Assert.That(encrypted, Is.Not.Null);
            Assert.DoesNotThrow(() => _ = Convert.FromBase64String(encrypted));
        });
    }

    [Test]
    public void EncryptStringAes_WithNullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => AesEncryption.EncryptStringAes(null!));
    }

    [Test]
    public void EncryptStringAes_WithEmptyString_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => AesEncryption.EncryptStringAes(""));
    }

    [Test]
    public void DecryptStringAes_WithNullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => AesEncryption.DecryptStringAes(null!));
    }

    [Test]
    public void DecryptStringAes_WithInvalidBase64_ThrowsFormatException()
    {
        Assert.Throws<FormatException>(() => AesEncryption.DecryptStringAes("InvalidBase64!@#"));
    }

    [Test]
    public void DecryptStringAes_WithInvalidCipherText_ReturnsKeyError()
    {
        var invalidCipherText = Convert.ToBase64String(new byte[] { 1, 2, 3, 4, 5 });

        var result = AesEncryption.DecryptStringAes(invalidCipherText);

        Assert.That(result, Is.EqualTo("keyError"));
    }

    [Test]
    public void DecryptStringAes_WithEmptyBase64String_ThrowsArgumentException()
    {
        var emptyBase64 = Convert.ToBase64String(Array.Empty<byte>());

        Assert.Throws<ArgumentException>(() => AesEncryption.DecryptStringAes(emptyBase64));
    }

    [Test]
    public void EncryptDecrypt_WithEdgeCases_HandlesCorrectly()
    {
        var edgeCases = new[]
        {
            "a", " ", "\t", "\0", new string('x', 1), new string('x', 10000)
        };

        foreach (var plainText in edgeCases)
        {
            var encrypted = AesEncryption.EncryptStringAes(plainText);
            var decrypted = AesEncryption.DecryptStringAes(encrypted);

            Assert.Multiple(() =>
            {
                Assert.That(decrypted, Is.EqualTo(plainText));
                Assert.That(encrypted, Is.Not.Null);
            });
        }
    }
}