using System.Security.Cryptography;
using System.Text;

namespace Ezzygate.Infrastructure.Extensions;

public static class HashExtensions
{
    public enum HashResultFormat
    {
        Base64,
        Hexadecimal,
        HexadecimalLowerCaseNoDash
    }

    public static string? ToMd5(this string source, HashResultFormat format = HashResultFormat.Base64)
    {
        var hash = MD5.HashData(Encoding.UTF8.GetBytes(source));
        return hash.ToString(format);
    }

    public static string? ToSha1(this string source, HashResultFormat format = HashResultFormat.Base64)
    {
        var hash = SHA1.HashData(Encoding.UTF8.GetBytes(source));
        return hash.ToString(format);
    }

    public static string? ToSha256(this string source, HashResultFormat format = HashResultFormat.Base64)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(source));
        return hash.ToString(format);
    }

    public static string? ToSha512(this string source, HashResultFormat format = HashResultFormat.Base64)
    {
        byte[] hash = SHA512.HashData(Encoding.UTF8.GetBytes(source));
        return hash.ToString(format);
    }

    public static string? ToHmacSha256(this string source, string hmacKey,
        HashResultFormat format = HashResultFormat.Base64)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(hmacKey));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(source));
        return hash.ToString(format);
    }

    private static string? ToString(this byte[] source, HashResultFormat format = HashResultFormat.Base64)
    {
        switch (format)
        {
            case HashResultFormat.Base64:
                return source.ToBase64();
            case HashResultFormat.Hexadecimal:
                return BitConverter.ToString(source);
            case HashResultFormat.HexadecimalLowerCaseNoDash:
                return BitConverter.ToString(source).ToLower().Replace("-", "");
            default:
                return null;
        }
    }

    public static byte[] FromBase64(this string source)
    {
        return Convert.FromBase64String(source);
    }

    public static byte[] FromBase64UrlString(this string source)
    {
        var originalSource = source.GetBase64StringFromUrlBase64String();
        return Convert.FromBase64String(originalSource);
    }

    public static string GetBase64StringFromUrlBase64String(this string source)
    {
        string originalBase64String = source.Replace('_', '/').Replace('-', '+');
        switch (source.Length % 4)
        {
            case 2: originalBase64String += "=="; break;
            case 3: originalBase64String += "="; break;
        }

        return originalBase64String;
    }

    public static string ToBase64(this byte[] source)
    {
        return Convert.ToBase64String(source);
    }

    public static string ToBase64UrlString(this byte[] source)
    {
        return Convert.ToBase64String(source)
            .TrimEnd(['=']).Replace('+', '-').Replace('/', '_');
    }
}