using System.Text;

namespace Ezzygate.Infrastructure.Utilities;

public static class SecureUtils
{
    public static string? MaskNumber(string? cardNumber)
    {
        if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length <= 4)
            return cardNumber;

        var maskLength = cardNumber.Length - 4;
        
        return string.Create(cardNumber.Length, (cardNumber, maskLength), static (span, state) =>
        {
            var (number, maskLen) = state;

            span[..maskLen].Fill('*');
            number.AsSpan(maskLen).CopyTo(span[maskLen..]);
        });
    }
    
    public static string? MaskCvv(string? cvv)
    {
        if (string.IsNullOrEmpty(cvv))
            return cvv;
            
        return string.Create(cvv.Length, cvv.Length, static (span, _) =>
        {
            span.Fill('*');
        });
    }
    
    public static string? MaskEmail(string? email)
    {
        if (string.IsNullOrEmpty(email))
            return email;
            
        var atIndex = email.IndexOf('@');
        if (atIndex <= 0)
            return email;
            
        if (atIndex <= 1)
            return email;
            
        return string.Create(email.Length, (email, atIndex), static (span, state) =>
        {
            var (emailStr, atIdx) = state;
            var emailSpan = emailStr.AsSpan();
            
            span[0] = emailSpan[0];
            span[1..atIdx].Fill('*');
            emailSpan[atIdx..].CopyTo(span[atIdx..]);
        });
    }
    
    public static string? MaskPhone(string? phone)
    {
        if (string.IsNullOrEmpty(phone) || phone.Length <= 4)
            return phone;
            
        var prefixLength = phone.Length <= 6 ? 2 : 3;
        var suffixLength = phone.Length <= 6 ? 2 : 3;
        var maskLength = phone.Length - prefixLength - suffixLength;
        
        return string.Create(phone.Length, (phone, prefixLength, suffixLength, maskLength), static (span, state) =>
        {
            var (phoneStr, prefixLen, suffixLen, maskLen) = state;
            var phoneSpan = phoneStr.AsSpan();

            phoneSpan[..prefixLen].CopyTo(span);
            span[prefixLen..(prefixLen + maskLen)].Fill('*');
            phoneSpan[^suffixLen..].CopyTo(span[^suffixLen..]);
        });
    }
    
    public static string? MaskName(string? name)
    {
        if (string.IsNullOrEmpty(name) || name.Length <= 2)
            return name;
            
        return string.Create(name.Length, name, static (span, nameStr) =>
        {
            var nameSpan = nameStr.AsSpan();

            span[0] = nameSpan[0];
            span[1..^1].Fill('*');
            span[^1] = nameSpan[^1];
        });
    }
    
    public static string? MaskAddress(string? address)
    {
        if (string.IsNullOrEmpty(address) || address.Length <= 4)
            return address;
            
        return string.Create(address.Length, address, static (span, addressStr) =>
        {
            var addressSpan = addressStr.AsSpan();

            addressSpan[..2].CopyTo(span);
            span[2..].Fill('*');
        });
    }
    
    private const string EncryptionCodeWord = "blackhorse";
    public static string? DecodeCvv(string? cvv)
    {
        if (cvv == null) return null;
        if (cvv.Length <= 3) return string.Empty;

        var result = new StringBuilder();
        for (var cvvIdx = 3; cvvIdx < cvv.Length; cvvIdx++)
        {
            var currentChar = cvv[cvvIdx];
            var codeWordIdx = EncryptionCodeWord.IndexOf(currentChar);
            if (codeWordIdx >= 0)
                result.Append(codeWordIdx);
        }

        return result.ToString();
    }
    public static string? EncodeCvv(string? cvv)
    {
        if (cvv == null) return null;

        var random = new Random();
        var paddedCvv = new StringBuilder();

        for (var i = 0; i < 3; i++)
            paddedCvv.Append(random.Next(10));

        paddedCvv.Append(cvv);

        var encoded = new StringBuilder();
        for (var i = 0; i < paddedCvv.Length; i++)
        {
            var c = paddedCvv[i];
            if (c is < '0' or > '9')
                continue;
            var digit = c - '0';
            encoded.Append(EncryptionCodeWord[digit]);
        }

        return encoded.ToString();
    }
}