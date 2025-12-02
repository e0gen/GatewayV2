using System.Security.Cryptography;
using System.Text;

namespace Ezzygate.Integrations.QrMoney;

public static class QrMoneyServices
{
    public static bool ValidateSignature(string body, string secretKey, string signature)
    {
        var encoding = Encoding.UTF8;
        var secretKeyBytes = encoding.GetBytes(secretKey);
        var toSignBytes = encoding.GetBytes(body);

        using var hmac = new HMACSHA512(secretKeyBytes);
        var hashBytes = hmac.ComputeHash(toSignBytes);
        var signatureHex = BitConverter
            .ToString(hashBytes)
            .Replace("-", "")
            .ToLower();
        var calculatedSignature = Convert.ToBase64String(encoding.GetBytes(signatureHex));

        return string.Equals(calculatedSignature, signature, StringComparison.Ordinal);
    }
}