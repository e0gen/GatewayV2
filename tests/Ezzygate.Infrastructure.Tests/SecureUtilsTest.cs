using Ezzygate.Infrastructure.Security;

namespace Ezzygate.Infrastructure.Tests;

[TestFixture]
public class SecureUtilsTest
{
    [Test]
    public void ObfuscateNumber_WithValidCardNumber_ShouldShowOnlyLast4Digits()
    {
        var cardNumber = "1234567890123456";

        var result = SecureUtils.ObfuscateNumber(cardNumber);

        Assert.That(result, Is.EqualTo("************3456"));
        Assert.That(result.Length, Is.EqualTo(cardNumber.Length));
    }

    [Test]
    public void ObfuscateNumber_WithShortCardNumber_ShouldShowOnlyLast4Digits()
    {
        var cardNumber = "12345678";

        var result = SecureUtils.ObfuscateNumber(cardNumber);

        Assert.That(result, Is.EqualTo("****5678"));
    }

    [Test]
    public void ObfuscateNumber_WithMinimumLength_ShouldObfuscateCorrectly()
    {
        var cardNumber = "12345";

        var result = SecureUtils.ObfuscateNumber(cardNumber);

        Assert.That(result, Is.EqualTo("*2345"));
    }

    [Test]
    public void ObfuscateNumber_WithExactly4Digits_ShouldReturnUnchanged()
    {
        var cardNumber = "1234";

        var result = SecureUtils.ObfuscateNumber(cardNumber);

        Assert.That(result, Is.EqualTo("1234"));
    }

    [Test]
    public void ObfuscateNumber_WithLessThan4Digits_ShouldReturnUnchanged()
    {
        var cardNumber = "123";

        var result = SecureUtils.ObfuscateNumber(cardNumber);

        Assert.That(result, Is.EqualTo("123"));
    }

    [Test]
    public void ObfuscateNumber_WithNull_ShouldReturnNull()
    {
        var result = SecureUtils.ObfuscateNumber(null);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void ObfuscateNumber_WithEmptyString_ShouldReturnEmpty()
    {
        var result = SecureUtils.ObfuscateNumber("");

        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void ObfuscateCvv_WithValidCvv_ShouldMaskAllDigits()
    {
        var cvv = "123";

        var result = SecureUtils.ObfuscateCvv(cvv);

        Assert.That(result, Is.EqualTo("***"));
        Assert.That(result.Length, Is.EqualTo(cvv.Length));
    }

    [Test]
    public void ObfuscateCvv_WithFourDigitCvv_ShouldMaskAllDigits()
    {
        var cvv = "1234";

        var result = SecureUtils.ObfuscateCvv(cvv);

        Assert.That(result, Is.EqualTo("****"));
    }

    [Test]
    public void ObfuscateCvv_WithSingleDigit_ShouldMaskSingleDigit()
    {
        var cvv = "1";

        var result = SecureUtils.ObfuscateCvv(cvv);

        Assert.That(result, Is.EqualTo("*"));
    }

    [Test]
    public void ObfuscateCvv_WithNull_ShouldReturnNull()
    {
        var result = SecureUtils.ObfuscateCvv(null);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void ObfuscateCvv_WithEmptyString_ShouldReturnEmpty()
    {
        var result = SecureUtils.ObfuscateCvv("");

        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void ObfuscateEmail_WithValidEmail_ShouldMaskUsernameKeepDomain()
    {
        var email = "john.doe@example.com";

        var result = SecureUtils.ObfuscateEmail(email);

        Assert.That(result, Is.EqualTo("j*******@example.com"));
        Assert.That(result.Length, Is.EqualTo(email.Length));
    }

    [Test]
    public void ObfuscateEmail_WithShortUsername_ShouldMaskCorrectly()
    {
        var email = "ab@test.com";

        var result = SecureUtils.ObfuscateEmail(email);

        Assert.That(result, Is.EqualTo("a*@test.com"));
    }

    [Test]
    public void ObfuscateEmail_WithSingleCharUsername_ShouldReturnUnchanged()
    {
        var email = "a@test.com";

        var result = SecureUtils.ObfuscateEmail(email);

        Assert.That(result, Is.EqualTo("a@test.com"));
    }

    [Test]
    public void ObfuscateEmail_WithLongUsername_ShouldMaskCorrectly()
    {
        var email = "verylongusername@domain.co.uk";

        var result = SecureUtils.ObfuscateEmail(email);

        Assert.That(result, Is.EqualTo("v***************@domain.co.uk"));
        Assert.That(result.Length, Is.EqualTo(email.Length));
    }

    [Test]
    public void ObfuscateEmail_WithNoAtSymbol_ShouldReturnUnchanged()
    {
        var email = "notanemail";

        var result = SecureUtils.ObfuscateEmail(email);

        Assert.That(result, Is.EqualTo("notanemail"));
    }

    [Test]
    public void ObfuscateEmail_WithAtAtBeginning_ShouldReturnUnchanged()
    {
        var email = "@domain.com";

        var result = SecureUtils.ObfuscateEmail(email);

        Assert.That(result, Is.EqualTo("@domain.com"));
    }

    [Test]
    public void ObfuscateEmail_WithNull_ShouldReturnNull()
    {
        var result = SecureUtils.ObfuscateEmail(null);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void ObfuscateEmail_WithEmptyString_ShouldReturnEmpty()
    {
        var result = SecureUtils.ObfuscateEmail("");

        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void ObfuscatePhone_WithLongPhone_ShouldKeepAreaCodeAndLastDigits()
    {
        var phone = "1234567890";

        var result = SecureUtils.ObfuscatePhone(phone);

        Assert.That(result, Is.EqualTo("123****890"));
        Assert.That(result.Length, Is.EqualTo(phone.Length));
    }

    [Test]
    public void ObfuscatePhone_WithShortPhone_ShouldKeep2And2Digits()
    {
        var phone = "123456";

        var result = SecureUtils.ObfuscatePhone(phone);

        Assert.That(result, Is.EqualTo("12**56"));
        Assert.That(result.Length, Is.EqualTo(phone.Length));
    }

    [Test]
    public void ObfuscatePhone_WithVeryLongPhone_ShouldObfuscateCorrectly()
    {
        var phone = "12345678901234";

        var result = SecureUtils.ObfuscatePhone(phone);

        Assert.That(result, Is.EqualTo("123********234"));
        Assert.That(result.Length, Is.EqualTo(phone.Length));
    }

    [Test]
    public void ObfuscatePhone_WithMinimumLength_ShouldObfuscateCorrectly()
    {
        var phone = "12345";

        var result = SecureUtils.ObfuscatePhone(phone);

        Assert.That(result, Is.EqualTo("12*45"));
    }

    [Test]
    public void ObfuscatePhone_WithExactly4Digits_ShouldReturnUnchanged()
    {
        var phone = "1234";

        var result = SecureUtils.ObfuscatePhone(phone);

        Assert.That(result, Is.EqualTo("1234"));
    }

    [Test]
    public void ObfuscatePhone_WithLessThan4Digits_ShouldReturnUnchanged()
    {
        var phone = "123";

        var result = SecureUtils.ObfuscatePhone(phone);

        Assert.That(result, Is.EqualTo("123"));
    }

    [Test]
    public void ObfuscatePhone_WithNull_ShouldReturnNull()
    {
        var result = SecureUtils.ObfuscatePhone(null);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void ObfuscatePhone_WithEmptyString_ShouldReturnEmpty()
    {
        var result = SecureUtils.ObfuscatePhone("");

        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void ObfuscateName_WithNormalName_ShouldKeepFirstAndLastChar()
    {
        var name = "John";

        var result = SecureUtils.ObfuscateName(name);

        Assert.That(result, Is.EqualTo("J**n"));
        Assert.That(result.Length, Is.EqualTo(name.Length));
    }

    [Test]
    public void ObfuscateName_WithLongName_ShouldObfuscateCorrectly()
    {
        var name = "Alexander";

        var result = SecureUtils.ObfuscateName(name);

        Assert.That(result, Is.EqualTo("A*******r"));
        Assert.That(result.Length, Is.EqualTo(name.Length));
    }

    [Test]
    public void ObfuscateName_WithThreeCharName_ShouldObfuscateMiddle()
    {
        var name = "Bob";

        var result = SecureUtils.ObfuscateName(name);

        Assert.That(result, Is.EqualTo("B*b"));
    }

    [Test]
    public void ObfuscateName_WithTwoCharName_ShouldReturnUnchanged()
    {
        var name = "Al";

        var result = SecureUtils.ObfuscateName(name);

        Assert.That(result, Is.EqualTo("Al"));
    }

    [Test]
    public void ObfuscateName_WithSingleChar_ShouldReturnUnchanged()
    {
        var name = "A";

        var result = SecureUtils.ObfuscateName(name);

        Assert.That(result, Is.EqualTo("A"));
    }

    [Test]
    public void ObfuscateName_WithNull_ShouldReturnNull()
    {
        var result = SecureUtils.ObfuscateName(null);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void ObfuscateName_WithEmptyString_ShouldReturnEmpty()
    {
        var result = SecureUtils.ObfuscateName("");

        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void ObfuscateAddress_WithNormalAddress_ShouldKeepFirst2Chars()
    {
        var address = "123 Main Street";

        var result = SecureUtils.ObfuscateAddress(address);

        Assert.That(result, Is.EqualTo("12*************"));
        Assert.That(result.Length, Is.EqualTo(address.Length));
    }

    [Test]
    public void ObfuscateAddress_WithLongAddress_ShouldObfuscateCorrectly()
    {
        var address = "1234 Very Long Street Name Avenue";

        var result = SecureUtils.ObfuscateAddress(address);

        Assert.That(result, Is.EqualTo("12*******************************"));
        Assert.That(result.Length, Is.EqualTo(address.Length));
    }

    [Test]
    public void ObfuscateAddress_WithMinimumLength_ShouldObfuscateCorrectly()
    {
        var address = "12345";

        var result = SecureUtils.ObfuscateAddress(address);

        Assert.That(result, Is.EqualTo("12***"));
    }

    [Test]
    public void ObfuscateAddress_WithExactly4Chars_ShouldReturnUnchanged()
    {
        var address = "1234";

        var result = SecureUtils.ObfuscateAddress(address);

        Assert.That(result, Is.EqualTo("1234"));
    }

    [Test]
    public void ObfuscateAddress_WithLessThan4Chars_ShouldReturnUnchanged()
    {
        var address = "123";

        var result = SecureUtils.ObfuscateAddress(address);

        Assert.That(result, Is.EqualTo("123"));
    }

    [Test]
    public void ObfuscateAddress_WithNull_ShouldReturnNull()
    {
        var result = SecureUtils.ObfuscateAddress(null);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void ObfuscateAddress_WithEmptyString_ShouldReturnEmpty()
    {
        var result = SecureUtils.ObfuscateAddress("");

        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void AllObfuscationMethods_WithRealWorldData_ShouldWorkCorrectly()
    {
        var cardNumber = "4111111111111111";
        var cvv = "123";
        var email = "customer@example.com";
        var phone = "5551234567";
        var name = "John Smith";
        var address = "123 Main Street";

        var obfuscatedCard = SecureUtils.ObfuscateNumber(cardNumber);
        var obfuscatedCvv = SecureUtils.ObfuscateCvv(cvv);
        var obfuscatedEmail = SecureUtils.ObfuscateEmail(email);
        var obfuscatedPhone = SecureUtils.ObfuscatePhone(phone);
        var obfuscatedName = SecureUtils.ObfuscateName(name);
        var obfuscatedAddress = SecureUtils.ObfuscateAddress(address);

        Assert.That(obfuscatedCard, Is.EqualTo("************1111"));
        Assert.That(obfuscatedCvv, Is.EqualTo("***"));
        Assert.That(obfuscatedEmail, Is.EqualTo("c*******@example.com"));
        Assert.That(obfuscatedPhone, Is.EqualTo("555****567"));
        Assert.That(obfuscatedName, Is.EqualTo("J********h"));
        Assert.That(obfuscatedAddress, Is.EqualTo("12*************"));

        Assert.That(obfuscatedCard.Length, Is.EqualTo(cardNumber.Length));
        Assert.That(obfuscatedCvv.Length, Is.EqualTo(cvv.Length));
        Assert.That(obfuscatedEmail.Length, Is.EqualTo(email.Length));
        Assert.That(obfuscatedPhone.Length, Is.EqualTo(phone.Length));
        Assert.That(obfuscatedName.Length, Is.EqualTo(name.Length));
        Assert.That(obfuscatedAddress.Length, Is.EqualTo(address.Length));
    }

    [Test]
    public void AllObfuscationMethods_WithSpecialCharacters_ShouldHandleCorrectly()
    {
        var email = "user+tag@sub.domain.com";
        var name = "O'Connor-Smith";
        var address = "123 Main St. Apt #456";

        var obfuscatedEmail = SecureUtils.ObfuscateEmail(email);
        var obfuscatedName = SecureUtils.ObfuscateName(name);
        var obfuscatedAddress = SecureUtils.ObfuscateAddress(address);

        Assert.That(obfuscatedEmail, Is.EqualTo("u*******@sub.domain.com"));
        Assert.That(obfuscatedName, Is.EqualTo("O************h"));
        Assert.That(obfuscatedAddress, Is.EqualTo("12*******************"));
    }

    [Test]
    public void ObfuscationMethods_WithLargeInputs_ShouldPerformEfficiently()
    {
        var largeCardNumber = new string('1', 1000) + "2345";
        var largeName = new string('A', 500) + 'Z';
        var largeAddress = new string('1', 1000);

        var obfuscatedCard = SecureUtils.ObfuscateNumber(largeCardNumber);
        var obfuscatedName = SecureUtils.ObfuscateName(largeName);
        var obfuscatedAddress = SecureUtils.ObfuscateAddress(largeAddress);

        Assert.That(obfuscatedCard, Does.EndWith("2345"));
        Assert.That(obfuscatedName, Does.StartWith("A"));
        Assert.That(obfuscatedName, Does.EndWith("Z"));
        Assert.That(obfuscatedAddress, Does.StartWith("11"));

        Assert.That(obfuscatedCard.Length, Is.EqualTo(largeCardNumber.Length));
        Assert.That(obfuscatedName.Length, Is.EqualTo(largeName.Length));
        Assert.That(obfuscatedAddress.Length, Is.EqualTo(largeAddress.Length));
    }
}