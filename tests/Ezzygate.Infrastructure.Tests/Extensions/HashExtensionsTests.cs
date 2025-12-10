using Ezzygate.Infrastructure.Extensions;

namespace Ezzygate.Infrastructure.Tests.Extensions;

[TestFixture]
public class HashExtensionsTests
{
    private const string Input = "Hello, World!";

    [Test]
    public void ToMd5_WithBase64Format_ProducesBase64Hash()
    {
        
        var hash = Input.ToMd5();

        Assert.Multiple(() =>
        {
            Assert.That(hash, Is.Not.Null);
            Assert.DoesNotThrow(() => _ = Convert.FromBase64String(hash));
        });
    }

    [Test]
    public void ToMd5_WithHexadecimalFormat_ProducesHexHash()
    {
        var hash = Input.ToMd5(HashExtensions.HashResultFormat.Hexadecimal);

        Assert.Multiple(() =>
        {
            Assert.That(hash, Is.Not.Null);
            Assert.That(hash, Does.Match("^[0-9A-F]{2}(-[0-9A-F]{2}){15}$"));
        });
    }

    [Test]
    public void ToMd5_WithHexadecimalLowerCaseNoDashFormat_ProducesLowercaseHexHash()
    {
        var hash = Input.ToMd5(HashExtensions.HashResultFormat.HexadecimalLowerCaseNoDash);

        Assert.Multiple(() =>
        {
            Assert.That(hash, Is.Not.Null);
            Assert.That(hash, Does.Match("^[0-9a-f]{32}$"));
            Assert.That(hash, Is.EqualTo(hash.ToLower()));
        });
    }

    [Test]
    public void ToMd5_WithDefaultFormat_UsesBase64()
    {
        const string input = "Test";
        var hashDefault = input.ToMd5();
        var hashBase64 = input.ToMd5();

        Assert.That(hashDefault, Is.EqualTo(hashBase64));
    }

    [Test]
    public void ToSha1_WithAllFormats_ProducesCorrectHashes()
    {
        var base64 = Input.ToSha1();
        var hex = Input.ToSha1(HashExtensions.HashResultFormat.Hexadecimal);
        var hexLower = Input.ToSha1(HashExtensions.HashResultFormat.HexadecimalLowerCaseNoDash);

        Assert.Multiple(() =>
        {
            Assert.DoesNotThrow(() => _ = Convert.FromBase64String(base64));
            Assert.That(hex, Does.Match("^[0-9A-F]{2}(-[0-9A-F]{2}){19}$"));
            Assert.That(hexLower, Does.Match("^[0-9a-f]{40}$"));
        });
    }

    [Test]
    public void ToSha256_WithAllFormats_ProducesCorrectHashes()
    {
        var base64 = Input.ToSha256();
        var hex = Input.ToSha256(HashExtensions.HashResultFormat.Hexadecimal);
        var hexLower = Input.ToSha256(HashExtensions.HashResultFormat.HexadecimalLowerCaseNoDash);

        Assert.Multiple(() =>
        {
            Assert.DoesNotThrow(() => _ = Convert.FromBase64String(base64));
            Assert.That(hex, Does.Match("^[0-9A-F]{2}(-[0-9A-F]{2}){31}$"));
            Assert.That(hexLower, Does.Match("^[0-9a-f]{64}$"));
        });
    }

    [Test]
    public void ToSha512_WithAllFormats_ProducesCorrectHashes()
    {
        var base64 = Input.ToSha512();
        var hex = Input.ToSha512(HashExtensions.HashResultFormat.Hexadecimal);
        var hexLower = Input.ToSha512(HashExtensions.HashResultFormat.HexadecimalLowerCaseNoDash);

        Assert.Multiple(() =>
        {
            Assert.DoesNotThrow(() => _ = Convert.FromBase64String(base64));
            Assert.That(hex, Does.Match("^[0-9A-F]{2}(-[0-9A-F]{2}){63}$"));
            Assert.That(hexLower, Does.Match("^[0-9a-f]{128}$"));
        });
    }

    [Test]
    public void ToHmacSha256_WithAllFormats_ProducesCorrectCodes()
    {
        const string key = "secret-key";
        
        var base64 = Input.ToHmacSha256(key);
        var hex = Input.ToHmacSha256(key, HashExtensions.HashResultFormat.Hexadecimal);
        var hexLower = Input.ToHmacSha256(key, HashExtensions.HashResultFormat.HexadecimalLowerCaseNoDash);

        Assert.Multiple(() =>
        {
            Assert.DoesNotThrow(() => _ = Convert.FromBase64String(base64));
            Assert.That(hex, Does.Match("^[0-9A-F]{2}(-[0-9A-F]{2}){31}$"));
            Assert.That(hexLower, Does.Match("^[0-9a-f]{64}$"));
        });
    }

    [Test]
    public void ToHmacSha256_WithDifferentKeys_ProducesDifferentCodes()
    {
        var hmac1 = Input.ToHmacSha256("key1");
        var hmac2 = Input.ToHmacSha256("key2");

        Assert.That(hmac1, Is.Not.EqualTo(hmac2));
    }

    [Test]
    public void ToHmacSha256_WithDefaultFormat_UsesBase64()
    {
        const string key = "test-key";
        var hmacDefault = Input.ToHmacSha256(key);
        var hmacBase64 = Input.ToHmacSha256(key);

        Assert.That(hmacDefault, Is.EqualTo(hmacBase64));
    }

    [Test]
    public void FromBase64_WithValidBase64_ConvertsToBytes()
    {
        var originalBytes = new byte[] { 1, 2, 3, 4, 5 };
        var base64 = Convert.ToBase64String(originalBytes);

        var result = base64.FromBase64();

        Assert.That(result, Is.EqualTo(originalBytes));
    }

    [Test]
    public void FromBase64_WithInvalidBase64_ThrowsFormatException()
    {
        Assert.Throws<FormatException>(() => "InvalidBase64!@#".FromBase64());
    }

    [Test]
    public void FromBase64UrlString_WithValidUrlBase64_ConvertsToBytes()
    {
        var originalBytes = new byte[] { 1, 2, 3, 4, 5 };
        var base64Url = Convert.ToBase64String(originalBytes)
            .TrimEnd('=').Replace('+', '-').Replace('/', '_');

        var result = base64Url.FromBase64UrlString();

        Assert.That(result, Is.EqualTo(originalBytes));
    }

    [Test]
    public void GetBase64StringFromUrlBase64String_WithValidUrlBase64_ConvertsCorrectly()
    {
        var originalBase64 = "SGVsbG8gV29ybGQ=";
        var urlBase64 = originalBase64.Replace('+', '-').Replace('/', '_').TrimEnd('=');

        var result = urlBase64.GetBase64StringFromUrlBase64String();

        Assert.That(result, Is.EqualTo(originalBase64));
    }

    [Test]
    public void GetBase64StringFromUrlBase64String_WithPadding_HandlesCorrectly()
    {
        var testCases = new[]
        {
            "Hello"u8.ToArray(), "Hell"u8.ToArray(), "Hel"u8.ToArray(), "Hello "u8.ToArray()
        };

        foreach (var bytes in testCases)
        {
            var base64 = Convert.ToBase64String(bytes);
            var urlBase64 = base64.TrimEnd('=').Replace('+', '-').Replace('/', '_');

            var result = urlBase64.GetBase64StringFromUrlBase64String();
            var resultBytes = Convert.FromBase64String(result);

            Assert.Multiple(() =>
            {
                Assert.That(resultBytes, Is.EqualTo(bytes));
                Assert.That(result.Length % 4, Is.EqualTo(0));
            });
        }
    }

    [Test]
    public void ToBase64_WithValidBytes_ConvertsToBase64()
    {
        var bytes = new byte[] { 1, 2, 3, 4, 5 };
        var expectedBase64 = Convert.ToBase64String(bytes);

        var result = bytes.ToBase64();

        Assert.That(result, Is.EqualTo(expectedBase64));
    }

    [Test]
    public void ToBase64UrlString_WithPlusSign_ReplacesWithDash()
    {
        var bytes = new byte[] { 0xFB, 0xFF, 0xBF };
    
        var defaultResult = bytes.ToBase64();
        var result = bytes.ToBase64UrlString();
    
        Assert.Multiple(() =>
        {
            Assert.That(defaultResult, Is.EqualTo("+/+/"));
            Assert.That(result, Does.Not.Contain("+"));
            Assert.That(result, Does.Contain("-"));
        });
    }

    [Test]
    public void ToBase64UrlString_WithSlash_ReplacesWithUnderscore()
    {
        var bytes = new byte[] { 0xFF, 0xEF };
    
        var defaultResult = bytes.ToBase64();
        var result = bytes.ToBase64UrlString();
    
        Assert.Multiple(() =>
        {
            Assert.That(defaultResult, Is.EqualTo("/+8="));
            Assert.That(result, Does.Not.Contain("/"));
            Assert.That(result, Does.Contain("_"));
        });
    }

    [Test]
    public void ToBase64UrlString_WithTwoBytes_RemovesPadding()
    {
        var bytes = new byte[] { 1, 2 };

        var defaultResult = bytes.ToBase64();
        var result = bytes.ToBase64UrlString();
        Assert.Multiple(() =>
        {
            Assert.That(defaultResult, Is.EqualTo("AQI="));
            Assert.That(result, Is.EqualTo("AQI"));
        });
    }

    [Test]
    public void AllHashMethods_WithNullInput_ThrowsArgumentNullException()
    {
        string? nullInput = null;

        Assert.Multiple(() =>
        {
            Assert.Throws<ArgumentNullException>(() => nullInput!.ToMd5());
            Assert.Throws<ArgumentNullException>(() => nullInput!.ToSha1());
            Assert.Throws<ArgumentNullException>(() => nullInput!.ToSha256());
            Assert.Throws<ArgumentNullException>(() => nullInput!.ToSha512());
            Assert.Throws<ArgumentNullException>(() => nullInput!.ToHmacSha256("key"));
        });
    }

    [Test]
    public void AllHashMethods_WithEmptyString_ProducesValidHash()
    {
        const string empty = "";

        Assert.Multiple(() =>
        {
            Assert.DoesNotThrow(() => empty.ToMd5());
            Assert.DoesNotThrow(() => empty.ToSha1());
            Assert.DoesNotThrow(() => empty.ToSha256());
            Assert.DoesNotThrow(() => empty.ToSha512());
            Assert.DoesNotThrow(() => empty.ToHmacSha256("key"));
        });
    }

    [Test]
    public void HashMethods_WithUnicodeInput_HandleCorrectly()
    {
        const string unicodeInput = "你好世界 🌍";

        Assert.Multiple(() =>
        {
            Assert.DoesNotThrow(() => unicodeInput.ToMd5());
            Assert.DoesNotThrow(() => unicodeInput.ToSha1());
            Assert.DoesNotThrow(() => unicodeInput.ToSha256());
            Assert.DoesNotThrow(() => unicodeInput.ToSha512());
            Assert.DoesNotThrow(() => unicodeInput.ToHmacSha256("key"));
        });
    }

    [Test]
    public void DifferentHashAlgorithms_WithSameInput_ProduceDifferentHashes()
    {
        var md5 = Input.ToMd5(HashExtensions.HashResultFormat.HexadecimalLowerCaseNoDash);
        var sha1 = Input.ToSha1(HashExtensions.HashResultFormat.HexadecimalLowerCaseNoDash);
        var sha256 = Input.ToSha256(HashExtensions.HashResultFormat.HexadecimalLowerCaseNoDash);
        var sha512 = Input.ToSha512(HashExtensions.HashResultFormat.HexadecimalLowerCaseNoDash);

        Assert.Multiple(() =>
        {
            Assert.That(md5, Has.Length.EqualTo(32));
            Assert.That(sha1, Has.Length.EqualTo(40));
            Assert.That(sha256, Has.Length.EqualTo(64));
            Assert.That(sha512, Has.Length.EqualTo(128));
            Assert.That(md5, Is.Not.EqualTo(sha1));
            Assert.That(sha1, Is.Not.EqualTo(sha256));
            Assert.That(sha256, Is.Not.EqualTo(sha512));
        });
    }
}