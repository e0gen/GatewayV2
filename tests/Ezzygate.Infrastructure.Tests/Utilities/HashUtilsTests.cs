using Ezzygate.Infrastructure.Utilities;

namespace Ezzygate.Infrastructure.Tests.Utilities;

[TestFixture]
public class HashUtilsTests
{
    [Test]
    public void ComputeSha256Hash_WithValidInput_ProducesCorrectHash()
    {
        const string input = "Hello, World!";
        var hash = HashUtils.ComputeSha256Hash(input);

        Assert.Multiple(() =>
        {
            Assert.That(hash, Is.Not.Null);
            Assert.That(hash, Has.Length.EqualTo(64));
            Assert.That(hash, Is.EqualTo(hash.ToLower()));
        });
    }

    [Test]
    public void ComputeSha256Hash_WithVariousInputs_ProducesConsistentHashes()
    {
        var testCases = new[]
        {
            "Hello", "World", "", "1234567890"
        };

        foreach (var input in testCases)
        {
            var hash1 = HashUtils.ComputeSha256Hash(input);
            var hash2 = HashUtils.ComputeSha256Hash(input);

            Assert.Multiple(() =>
            {
                Assert.That(hash1, Is.EqualTo(hash2));
                Assert.That(hash1, Has.Length.EqualTo(64));
            });
        }
    }

    [Test]
    public void ComputeSha256Hash_WithNullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => HashUtils.ComputeSha256Hash(null!));
    }

    [Test]
    public void ComputeSha256Hash_OutputIsLowercaseHex()
    {
        const string input = "Test Input";
        var hash = HashUtils.ComputeSha256Hash(input);

        Assert.Multiple(() =>
        {
            Assert.That(hash, Is.EqualTo(hash.ToLower()));
            Assert.That(hash, Does.Match("^[0-9a-f]{64}$"));
        });
    }

    [Test]
    public void ComputeSha512Hash_WithValidInput_ProducesCorrectHash()
    {
        const string input = "Hello, World!";
        var hash = HashUtils.ComputeSha512Hash(input);

        Assert.Multiple(() =>
        {
            Assert.That(hash, Is.Not.Null);
            Assert.That(hash, Has.Length.EqualTo(128));
            Assert.That(hash, Is.EqualTo(hash.ToLower()));
        });
    }

    [Test]
    public void ComputeSha512Hash_WithVariousInputs_ProducesConsistentHashes()
    {
        var testCases = new[]
        {
            "Hello", "", "1234567890"
        };

        foreach (var input in testCases)
        {
            var hash1 = HashUtils.ComputeSha512Hash(input);
            var hash2 = HashUtils.ComputeSha512Hash(input);

            Assert.Multiple(() =>
            {
                Assert.That(hash1, Is.EqualTo(hash2));
                Assert.That(hash1, Has.Length.EqualTo(128));
            });
        }
    }

    [Test]
    public void ComputeSha512Hash_WithNullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => HashUtils.ComputeSha512Hash(null!));
    }

    [Test]
    public void ComputeSha512Hash_OutputIsLowercaseHex()
    {
        const string input = "Test Input";
        var hash = HashUtils.ComputeSha512Hash(input);

        Assert.Multiple(() =>
        {
            Assert.That(hash, Is.EqualTo(hash.ToLower()));
            Assert.That(hash, Does.Match("^[0-9a-f]{128}$"),
                "Hash should be 128 lowercase hexadecimal characters");
        });
    }

    [Test]
    public void ComputeHmacSha256_WithValidInput_ProducesCorrectHmac()
    {
        const string content = "Hello, World!";
        const string key = "secret-key";
        var hmac = HashUtils.ComputeHmacSha256(content, key);

        Assert.Multiple(() =>
        {
            Assert.That(hmac, Is.Not.Null);
            Assert.That(hmac, Is.Not.Empty);
            Assert.DoesNotThrow(() => _ = Convert.FromBase64String(hmac));
        });
    }

    [Test]
    public void ComputeHmacSha256_WithDifferentKeys_ProducesDifferentCodes()
    {
        const string content = "Hello, World!";
        var hmac1 = HashUtils.ComputeHmacSha256(content, "key1");
        var hmac2 = HashUtils.ComputeHmacSha256(content, "key2");

        Assert.That(hmac1, Is.Not.EqualTo(hmac2),
            "Different keys should produce different HMACs");
    }

    [Test]
    public void ComputeHmacSha256_WithSameInputAndKey_ProducesSameHmac()
    {
        const string content = "Hello, World!";
        const string key = "secret-key";
        var hmac1 = HashUtils.ComputeHmacSha256(content, key);
        var hmac2 = HashUtils.ComputeHmacSha256(content, key);

        Assert.That(hmac1, Is.EqualTo(hmac2));
    }

    [Test]
    public void ComputeHmacSha256_WithNullContent_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            HashUtils.ComputeHmacSha256(null!, "key"));
    }

    [Test]
    public void ComputeHmacSha256_WithNullKey_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            HashUtils.ComputeHmacSha256("content", null!));
    }

    [Test]
    public void ComputeHmacSha256_OutputIsBase64Encoded()
    {
        const string content = "Test content";
        const string key = "test-key";
        var hmac = HashUtils.ComputeHmacSha256(content, key);

        Assert.DoesNotThrow(() => _ = Convert.FromBase64String(hmac));
    }

    [Test]
    public void ComputeHmacSha512_WithValidInput_ProducesCorrectHmac()
    {
        const string content = "Hello, World!";
        const string key = "secret-key";
        var hmac = HashUtils.ComputeHmacSha512(content, key);

        Assert.Multiple(() =>
        {
            Assert.That(hmac, Is.Not.Null);
            Assert.That(hmac, Is.Not.Empty);
            Assert.DoesNotThrow(() => _ = Convert.FromBase64String(hmac));
        });
    }

    [Test]
    public void ComputeHmacSha512_WithDifferentKeys_ProducesDifferentCodes()
    {
        const string content = "Hello, World!";
        var hmac1 = HashUtils.ComputeHmacSha512(content, "key1");
        var hmac2 = HashUtils.ComputeHmacSha512(content, "key2");

        Assert.That(hmac1, Is.Not.EqualTo(hmac2));
    }

    [Test]
    public void ComputeHmacSha512_WithSameInputAndKey_ProducesSameHmac()
    {
        const string content = "Hello, World!";
        const string key = "secret-key";
        var hmac1 = HashUtils.ComputeHmacSha512(content, key);
        var hmac2 = HashUtils.ComputeHmacSha512(content, key);

        Assert.That(hmac1, Is.EqualTo(hmac2));
    }

    [Test]
    public void ComputeHmacSha512_WithNullContent_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            HashUtils.ComputeHmacSha512(null!, "key"));
    }

    [Test]
    public void ComputeHmacSha512_WithNullKey_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            HashUtils.ComputeHmacSha512("content", null!));
    }

    [Test]
    public void ComputeHmacSha512_OutputIsBase64Encoded()
    {
        const string content = "Test content";
        const string key = "test-key";
        var hmac = HashUtils.ComputeHmacSha512(content, key);

        Assert.DoesNotThrow(() => _ = Convert.FromBase64String(hmac));
    }

    [Test]
    public void AllHashMethods_WithUnicodeInput_HandleCorrectly()
    {
        const string unicodeInput = "你好世界 🌍";

        var sha256 = HashUtils.ComputeSha256Hash(unicodeInput);
        var sha512 = HashUtils.ComputeSha512Hash(unicodeInput);
        var hmac256 = HashUtils.ComputeHmacSha256(unicodeInput, "key");
        var hmac512 = HashUtils.ComputeHmacSha512(unicodeInput, "key");

        Assert.Multiple(() =>
        {
            Assert.That(sha256, Is.Not.Null);
            Assert.That(sha512, Is.Not.Null);
            Assert.That(hmac256, Is.Not.Null);
            Assert.That(hmac512, Is.Not.Null);
        });
    }

    [Test]
    public void Sha256AndSha512_WithSameInput_ProduceDifferentHashes()
    {
        const string input = "Test input";
        var sha256 = HashUtils.ComputeSha256Hash(input);
        var sha512 = HashUtils.ComputeSha512Hash(input);

        Assert.Multiple(() =>
        {
            Assert.That(sha256, Has.Length.EqualTo(64));
            Assert.That(sha512, Has.Length.EqualTo(128));
            Assert.That(sha256, Is.Not.EqualTo(sha512));
        });
    }
}