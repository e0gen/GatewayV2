using System.Security.Cryptography;
using System.Text;

namespace Ezzygate.Infrastructure.Utilities;

public static class HashUtils
{
    public static string ComputeSha256Hash(string input)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(bytes).ToLower();
    }
    
    public static string ComputeSha512Hash(string input)
    {
        var bytes = SHA512.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(bytes).ToLower();
    }
    
    public static string ComputeHmacSha256(string content, string key)
    {
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var contentBytes = Encoding.UTF8.GetBytes(content);

        using var hmac = new HMACSHA256(keyBytes);
        var hashBytes = hmac.ComputeHash(contentBytes);
        return Convert.ToBase64String(hashBytes);
    }
    
    public static string ComputeHmacSha512(string content, string key)
    {
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var contentBytes = Encoding.UTF8.GetBytes(content);

        using var hmac = new HMACSHA512(keyBytes);
        var hashBytes = hmac.ComputeHash(contentBytes);
        return Convert.ToBase64String(hashBytes);
    }

    public static string GetHashKey(int length = 10)
    {
        const string chars = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var random = new Random((int)DateTime.Now.Ticks);
        var result = new StringBuilder(length);

        for (var i = 0; i < length; i++)
        {
            var index = random.Next(1, chars.Length);
            result.Append(chars[index]);
        }

        return result.ToString();
    }
} 