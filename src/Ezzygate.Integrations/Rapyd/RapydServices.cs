using System.Security.Cryptography;
using System.Text;

namespace Ezzygate.Integrations.Rapyd;

public static class RapydServices
{
    public static string GetRequestHeaderSignature(string method, string urlPath, string salt, long timestamp, string body, string accessKey,
        string secretKey)
    {
        var bodyString = string.Empty;
        if (!string.IsNullOrWhiteSpace(body))
            bodyString = body == "{}" ? "" : body;

        var toSign = method.ToLower() + urlPath + salt + timestamp + accessKey + secretKey + bodyString;

        var encoding = new UTF8Encoding();
        var secretKeyBytes = encoding.GetBytes(secretKey);
        var signatureBytes = encoding.GetBytes(toSign);

        using var hmac = new HMACSHA256(secretKeyBytes);
        var signatureHash = hmac.ComputeHash(signatureBytes);
        var signatureHex = string.Concat(Array.ConvertAll(signatureHash, x => x.ToString("x2")));
        var signature = Convert.ToBase64String(encoding.GetBytes(signatureHex));

        return signature;
    }

    public static string GenerateRandomString(int size)
    {
        using var rng = new RNGCryptoServiceProvider();
        var randomBytes = new byte[size];
        rng.GetBytes(randomBytes);
        return string.Concat(Array.ConvertAll(randomBytes, x => x.ToString("x2")));
    }

    public static bool ValidateSignature(string urlPath, string salt, string timestamp, string body, string accessKey, string secretKey,
        string signature)
    {
        var toSign = urlPath + salt + timestamp + accessKey + secretKey + body;
        var encoding = Encoding.UTF8;
        var secretKeyBytes = encoding.GetBytes(secretKey);
        var toSignBytes = encoding.GetBytes(toSign);

        using var hmac = new HMACSHA256(secretKeyBytes);
        var hashBytes = hmac.ComputeHash(toSignBytes);
        var signatureHex = BitConverter
            .ToString(hashBytes)
            .Replace("-", "")
            .ToLower();
        var calculatedSignature = Convert.ToBase64String(encoding.GetBytes(signatureHex));

        return string.Equals(calculatedSignature, signature, StringComparison.Ordinal);
    }
}