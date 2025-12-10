using Ezzygate.Domain.Interfaces;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Utilities;

namespace Ezzygate.Infrastructure.Tests.Extensions;

[TestFixture]
public class SensitiveDataExtensionsTests
{
    private class TestSensitiveData : ISensitiveData
    {
        public string? Secret { get; set; }
        public string? Phone { get; set; }

        public object Mask()
        {
            Secret = SecureUtils.MaskCvv(Secret);
            Phone = SecureUtils.MaskPhone(Phone);
            return this;
        }
    }

    [Test]
    public void MaskIfSensitive_WithISensitiveDataObject_MutatesAndReturnsSameInstance()
    {
        var sensitiveData = new TestSensitiveData
        {
            Secret = "SensitiveValue",
            Phone = "1234567"
        };

        var result = sensitiveData.MaskIfSensitive();

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.SameAs(sensitiveData));
            Assert.That(((TestSensitiveData)result).Secret, Is.EqualTo("**************"));
            Assert.That(((TestSensitiveData)result).Phone, Is.EqualTo("123*567"));
            Assert.That(sensitiveData.Secret, Is.EqualTo("**************"));
        });
    }

    [Test]
    public void MaskIfSensitive_WithNonSensitiveObject_ReturnsOriginalObject()
    {
        const string regularObject = "Not sensitive";
        var result = regularObject.MaskIfSensitive();

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(regularObject));
            Assert.That(result, Is.InstanceOf<string>());
        });
    }

    [Test]
    public void MaskIfSensitive_WithNull_ReturnsNull()
    {
        object? nullObject = null;
#pragma warning disable CS8604 // Possible null reference argument.
        var result = nullObject.MaskIfSensitive();
#pragma warning restore CS8604 // Possible null reference argument.

        Assert.That(result, Is.Null);
    }

    [Test]
    public void MaskIfSensitive_WithVariousObjectTypes_HandlesCorrectly()
    {
        var testCases = new object[]
        {
            "string", 123, 45.67, new List<int> { 1, 2, 3 }, new Dictionary<string, int> { { "key", 1 } }
        };

        foreach (var obj in testCases)
        {
            var result = obj.MaskIfSensitive();

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.SameAs(obj));
                Assert.That(result, Is.Not.Null);
            });
        }
    }

    [Test]
    public void MaskIfSensitive_WithNullProperties_HandlesCorrectly()
    {
        var sensitiveData = new TestSensitiveData
        {
            Secret = null,
            Phone = null
        };

        var result = sensitiveData.MaskIfSensitive();

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.SameAs(sensitiveData));
            Assert.That(((TestSensitiveData)result).Secret, Is.Null);
            Assert.That(((TestSensitiveData)result).Phone, Is.Null);
        });
    }

    [Test]
    public void MaskIfSensitive_WithPartiallyNullProperties_MasksOnlyNonNullValues()
    {
        var sensitiveData = new TestSensitiveData
        {
            Secret = "Secret",
            Phone = null
        };

        var result = sensitiveData.MaskIfSensitive();

        Assert.Multiple(() =>
        {
            Assert.That(((TestSensitiveData)result).Secret, Is.EqualTo("******"));
            Assert.That(((TestSensitiveData)result).Phone, Is.Null);
        });
    }

    [Test]
    public void MaskIfSensitive_CalledMultipleTimes_ResultsInSameMaskedValue()
    {
        var sensitiveData = new TestSensitiveData { Secret = "Original" };

        var result1 = sensitiveData.MaskIfSensitive();
        var result2 = sensitiveData.MaskIfSensitive();

        Assert.Multiple(() =>
        {
            Assert.That(result1, Is.SameAs(result2));
            Assert.That(((TestSensitiveData)result1).Secret, Is.EqualTo("********"));
            Assert.That(((TestSensitiveData)result2).Secret, Is.EqualTo("********"));
        });
    }
}