namespace Ezzygate.Infrastructure.Security;

public static class SecureUtils
{
    public static string? ObfuscateNumber(string? cardNumber)
    {
        if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length <= 4)
            return cardNumber;

        var maskLength = cardNumber.Length - 4;
        
        return string.Create(cardNumber.Length, (cardNumber, maskLength), static (span, state) =>
        {
            var (number, maskLen) = state;
            
            // Fill with asterisks for masked portion
            span[..maskLen].Fill('*');
            
            // Copy last 4 digits
            number.AsSpan(maskLen).CopyTo(span[maskLen..]);
        });
    }
    
    public static string? ObfuscateCvv(string? cvv)
    {
        if (string.IsNullOrEmpty(cvv))
            return cvv;
            
        return string.Create(cvv.Length, cvv.Length, static (span, _) =>
        {
            span.Fill('*');
        });
    }
    
    public static string? ObfuscateEmail(string? email)
    {
        if (string.IsNullOrEmpty(email))
            return email;
            
        var atIndex = email.IndexOf('@');
        if (atIndex <= 0)
            return email; // Invalid email format
            
        if (atIndex <= 1)
            return email; // Username too short to obfuscate
            
        return string.Create(email.Length, (email, atIndex), static (span, state) =>
        {
            var (emailStr, atIdx) = state;
            var emailSpan = emailStr.AsSpan();
            
            // Copy first character of username
            span[0] = emailSpan[0];
            
            // Fill username (except first char) with asterisks
            span[1..atIdx].Fill('*');
            
            // Copy domain part (@ and everything after)
            emailSpan[atIdx..].CopyTo(span[atIdx..]);
        });
    }
    
    public static string? ObfuscatePhone(string? phone)
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
            
            // Copy prefix (area code)
            phoneSpan[..prefixLen].CopyTo(span);
            
            // Fill middle with asterisks
            span[prefixLen..(prefixLen + maskLen)].Fill('*');
            
            // Copy suffix (last digits)
            phoneSpan[^suffixLen..].CopyTo(span[^suffixLen..]);
        });
    }
    
    public static string? ObfuscateName(string? name)
    {
        if (string.IsNullOrEmpty(name) || name.Length <= 2)
            return name;
            
        return string.Create(name.Length, name, static (span, nameStr) =>
        {
            var nameSpan = nameStr.AsSpan();
            
            // Copy first character
            span[0] = nameSpan[0];
            
            // Fill middle with asterisks
            span[1..^1].Fill('*');
            
            // Copy last character
            span[^1] = nameSpan[^1];
        });
    }
    
    public static string? ObfuscateAddress(string? address)
    {
        if (string.IsNullOrEmpty(address) || address.Length <= 4)
            return address;
            
        return string.Create(address.Length, address, static (span, addressStr) =>
        {
            var addressSpan = addressStr.AsSpan();
            
            // Copy first 2 characters
            addressSpan[..2].CopyTo(span);
            
            // Fill rest with asterisks
            span[2..].Fill('*');
        });
    }
}