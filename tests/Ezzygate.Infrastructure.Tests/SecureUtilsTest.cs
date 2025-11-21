using Ezzygate.Infrastructure.Security;

namespace Ezzygate.Infrastructure.Tests;

[TestFixture]
public class SecureUtilsTest
{
    [Test]
    public void MaskNumber_WithValidCardNumber_ShouldShowOnlyLast4Digits()
    {
        var cardNumber = "1234567890123456";

        var result = SecureUtils.MaskNumber(cardNumber);

        Assert.That(result, Is.EqualTo("************3456"));
        Assert.That(result.Length, Is.EqualTo(cardNumber.Length));
    }

    [Test]
    public void MaskNumber_WithShortCardNumber_ShouldShowOnlyLast4Digits()
    {
        var cardNumber = "12345678";

        var result = SecureUtils.MaskNumber(cardNumber);

        Assert.That(result, Is.EqualTo("****5678"));
    }

    [Test]
    public void MaskNumber_WithMinimumLength_ShouldMaskCorrectly()
    {
        var cardNumber = "12345";

        var result = SecureUtils.MaskNumber(cardNumber);

        Assert.That(result, Is.EqualTo("*2345"));
    }

    [Test]
    public void MaskNumber_WithExactly4Digits_ShouldReturnUnchanged()
    {
        var cardNumber = "1234";

        var result = SecureUtils.MaskNumber(cardNumber);

        Assert.That(result, Is.EqualTo("1234"));
    }

    [Test]
    public void MaskNumber_WithLessThan4Digits_ShouldReturnUnchanged()
    {
        var cardNumber = "123";

        var result = SecureUtils.MaskNumber(cardNumber);

        Assert.That(result, Is.EqualTo("123"));
    }

    [Test]
    public void MaskNumber_WithNull_ShouldReturnNull()
    {
        var result = SecureUtils.MaskNumber(null);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void MaskNumber_WithEmptyString_ShouldReturnEmpty()
    {
        var result = SecureUtils.MaskNumber("");

        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void MaskCvv_WithValidCvv_ShouldMaskAllDigits()
    {
        var cvv = "123";

        var result = SecureUtils.MaskCvv(cvv);

        Assert.That(result, Is.EqualTo("***"));
        Assert.That(result.Length, Is.EqualTo(cvv.Length));
    }

    [Test]
    public void MaskCvv_WithFourDigitCvv_ShouldMaskAllDigits()
    {
        var cvv = "1234";

        var result = SecureUtils.MaskCvv(cvv);

        Assert.That(result, Is.EqualTo("****"));
    }

    [Test]
    public void MaskCvv_WithSingleDigit_ShouldMaskSingleDigit()
    {
        var cvv = "1";

        var result = SecureUtils.MaskCvv(cvv);

        Assert.That(result, Is.EqualTo("*"));
    }

    [Test]
    public void MaskCvv_WithNull_ShouldReturnNull()
    {
        var result = SecureUtils.MaskCvv(null);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void MaskCvv_WithEmptyString_ShouldReturnEmpty()
    {
        var result = SecureUtils.MaskCvv("");

        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void MaskEmail_WithValidEmail_ShouldMaskUsernameKeepDomain()
    {
        var email = "john.doe@example.com";

        var result = SecureUtils.MaskEmail(email);

        Assert.That(result, Is.EqualTo("j*******@example.com"));
        Assert.That(result.Length, Is.EqualTo(email.Length));
    }

    [Test]
    public void MaskEmail_WithShortUsername_ShouldMaskCorrectly()
    {
        var email = "ab@test.com";

        var result = SecureUtils.MaskEmail(email);

        Assert.That(result, Is.EqualTo("a*@test.com"));
    }

    [Test]
    public void MaskEmail_WithSingleCharUsername_ShouldReturnUnchanged()
    {
        var email = "a@test.com";

        var result = SecureUtils.MaskEmail(email);

        Assert.That(result, Is.EqualTo("a@test.com"));
    }

    [Test]
    public void MaskEmail_WithLongUsername_ShouldMaskCorrectly()
    {
        var email = "verylongusername@domain.co.uk";

        var result = SecureUtils.MaskEmail(email);

        Assert.That(result, Is.EqualTo("v***************@domain.co.uk"));
        Assert.That(result.Length, Is.EqualTo(email.Length));
    }

    [Test]
    public void MaskEmail_WithNoAtSymbol_ShouldReturnUnchanged()
    {
        var email = "notanemail";

        var result = SecureUtils.MaskEmail(email);

        Assert.That(result, Is.EqualTo("notanemail"));
    }

    [Test]
    public void MaskEmail_WithAtAtBeginning_ShouldReturnUnchanged()
    {
        var email = "@domain.com";

        var result = SecureUtils.MaskEmail(email);

        Assert.That(result, Is.EqualTo("@domain.com"));
    }

    [Test]
    public void MaskEmail_WithNull_ShouldReturnNull()
    {
        var result = SecureUtils.MaskEmail(null);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void MaskEmail_WithEmptyString_ShouldReturnEmpty()
    {
        var result = SecureUtils.MaskEmail("");

        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void MaskPhone_WithLongPhone_ShouldKeepAreaCodeAndLastDigits()
    {
        var phone = "1234567890";

        var result = SecureUtils.MaskPhone(phone);

        Assert.That(result, Is.EqualTo("123****890"));
        Assert.That(result.Length, Is.EqualTo(phone.Length));
    }

    [Test]
    public void MaskPhone_WithShortPhone_ShouldKeep2And2Digits()
    {
        var phone = "123456";

        var result = SecureUtils.MaskPhone(phone);

        Assert.That(result, Is.EqualTo("12**56"));
        Assert.That(result.Length, Is.EqualTo(phone.Length));
    }

    [Test]
    public void MaskPhone_WithVeryLongPhone_ShouldMaskCorrectly()
    {
        var phone = "12345678901234";

        var result = SecureUtils.MaskPhone(phone);

        Assert.That(result, Is.EqualTo("123********234"));
        Assert.That(result.Length, Is.EqualTo(phone.Length));
    }

    [Test]
    public void MaskPhone_WithMinimumLength_ShouldMaskCorrectly()
    {
        var phone = "12345";

        var result = SecureUtils.MaskPhone(phone);

        Assert.That(result, Is.EqualTo("12*45"));
    }

    [Test]
    public void MaskPhone_WithExactly4Digits_ShouldReturnUnchanged()
    {
        var phone = "1234";

        var result = SecureUtils.MaskPhone(phone);

        Assert.That(result, Is.EqualTo("1234"));
    }

    [Test]
    public void MaskPhone_WithLessThan4Digits_ShouldReturnUnchanged()
    {
        var phone = "123";

        var result = SecureUtils.MaskPhone(phone);

        Assert.That(result, Is.EqualTo("123"));
    }

    [Test]
    public void MaskPhone_WithNull_ShouldReturnNull()
    {
        var result = SecureUtils.MaskPhone(null);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void MaskPhone_WithEmptyString_ShouldReturnEmpty()
    {
        var result = SecureUtils.MaskPhone("");

        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void MaskName_WithNormalName_ShouldKeepFirstAndLastChar()
    {
        var name = "John";

        var result = SecureUtils.MaskName(name);

        Assert.That(result, Is.EqualTo("J**n"));
        Assert.That(result.Length, Is.EqualTo(name.Length));
    }

    [Test]
    public void MaskName_WithLongName_ShouldMaskCorrectly()
    {
        var name = "Alexander";

        var result = SecureUtils.MaskName(name);

        Assert.That(result, Is.EqualTo("A*******r"));
        Assert.That(result.Length, Is.EqualTo(name.Length));
    }

    [Test]
    public void MaskName_WithThreeCharName_ShouldMaskMiddle()
    {
        var name = "Bob";

        var result = SecureUtils.MaskName(name);

        Assert.That(result, Is.EqualTo("B*b"));
    }

    [Test]
    public void MaskName_WithTwoCharName_ShouldReturnUnchanged()
    {
        var name = "Al";

        var result = SecureUtils.MaskName(name);

        Assert.That(result, Is.EqualTo("Al"));
    }

    [Test]
    public void MaskName_WithSingleChar_ShouldReturnUnchanged()
    {
        var name = "A";

        var result = SecureUtils.MaskName(name);

        Assert.That(result, Is.EqualTo("A"));
    }

    [Test]
    public void MaskName_WithNull_ShouldReturnNull()
    {
        var result = SecureUtils.MaskName(null);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void MaskName_WithEmptyString_ShouldReturnEmpty()
    {
        var result = SecureUtils.MaskName("");

        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void MaskAddress_WithNormalAddress_ShouldKeepFirst2Chars()
    {
        var address = "123 Main Street";

        var result = SecureUtils.MaskAddress(address);

        Assert.That(result, Is.EqualTo("12*************"));
        Assert.That(result.Length, Is.EqualTo(address.Length));
    }

    [Test]
    public void MaskAddress_WithLongAddress_ShouldMaskCorrectly()
    {
        var address = "1234 Very Long Street Name Avenue";

        var result = SecureUtils.MaskAddress(address);

        Assert.That(result, Is.EqualTo("12*******************************"));
        Assert.That(result.Length, Is.EqualTo(address.Length));
    }

    [Test]
    public void MaskAddress_WithMinimumLength_ShouldMaskCorrectly()
    {
        var address = "12345";

        var result = SecureUtils.MaskAddress(address);

        Assert.That(result, Is.EqualTo("12***"));
    }

    [Test]
    public void MaskAddress_WithExactly4Chars_ShouldReturnUnchanged()
    {
        var address = "1234";

        var result = SecureUtils.MaskAddress(address);

        Assert.That(result, Is.EqualTo("1234"));
    }

    [Test]
    public void MaskAddress_WithLessThan4Chars_ShouldReturnUnchanged()
    {
        var address = "123";

        var result = SecureUtils.MaskAddress(address);

        Assert.That(result, Is.EqualTo("123"));
    }

    [Test]
    public void MaskAddress_WithNull_ShouldReturnNull()
    {
        var result = SecureUtils.MaskAddress(null);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void MaskAddress_WithEmptyString_ShouldReturnEmpty()
    {
        var result = SecureUtils.MaskAddress("");

        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void AllMaskMethods_WithRealWorldData_ShouldWorkCorrectly()
    {
        var cardNumber = "4111111111111111";
        var cvv = "123";
        var email = "customer@example.com";
        var phone = "5551234567";
        var name = "John Smith";
        var address = "123 Main Street";

        var maskedCard = SecureUtils.MaskNumber(cardNumber);
        var maskedCvv = SecureUtils.MaskCvv(cvv);
        var maskedEmail = SecureUtils.MaskEmail(email);
        var maskedPhone = SecureUtils.MaskPhone(phone);
        var maskedName = SecureUtils.MaskName(name);
        var maskedAddress = SecureUtils.MaskAddress(address);

        Assert.That(maskedCard, Is.EqualTo("************1111"));
        Assert.That(maskedCvv, Is.EqualTo("***"));
        Assert.That(maskedEmail, Is.EqualTo("c*******@example.com"));
        Assert.That(maskedPhone, Is.EqualTo("555****567"));
        Assert.That(maskedName, Is.EqualTo("J********h"));
        Assert.That(maskedAddress, Is.EqualTo("12*************"));

        Assert.That(maskedCard.Length, Is.EqualTo(cardNumber.Length));
        Assert.That(maskedCvv.Length, Is.EqualTo(cvv.Length));
        Assert.That(maskedEmail.Length, Is.EqualTo(email.Length));
        Assert.That(maskedPhone.Length, Is.EqualTo(phone.Length));
        Assert.That(maskedName.Length, Is.EqualTo(name.Length));
        Assert.That(maskedAddress.Length, Is.EqualTo(address.Length));
    }

    [Test]
    public void AllOMaskMethods_WithSpecialCharacters_ShouldHandleCorrectly()
    {
        var email = "user+tag@sub.domain.com";
        var name = "O'Connor-Smith";
        var address = "123 Main St. Apt #456";

        var maskedEmail = SecureUtils.MaskEmail(email);
        var maskedName = SecureUtils.MaskName(name);
        var maskedAddress = SecureUtils.MaskAddress(address);

        Assert.That(maskedEmail, Is.EqualTo("u*******@sub.domain.com"));
        Assert.That(maskedName, Is.EqualTo("O************h"));
        Assert.That(maskedAddress, Is.EqualTo("12*******************"));
    }

    [Test]
    public void MaskMethods_WithLargeInputs_ShouldPerformEfficiently()
    {
        var largeCardNumber = new string('1', 1000) + "2345";
        var largeName = new string('A', 500) + 'Z';
        var largeAddress = new string('1', 1000);

        var maskedCard = SecureUtils.MaskNumber(largeCardNumber);
        var maskedName = SecureUtils.MaskName(largeName);
        var maskedAddress = SecureUtils.MaskAddress(largeAddress);

        Assert.That(maskedCard, Does.EndWith("2345"));
        Assert.That(maskedName, Does.StartWith("A"));
        Assert.That(maskedName, Does.EndWith("Z"));
        Assert.That(maskedAddress, Does.StartWith("11"));

        Assert.That(maskedCard.Length, Is.EqualTo(largeCardNumber.Length));
        Assert.That(maskedName.Length, Is.EqualTo(largeName.Length));
        Assert.That(maskedAddress.Length, Is.EqualTo(largeAddress.Length));
    }
}