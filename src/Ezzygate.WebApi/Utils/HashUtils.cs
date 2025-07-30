using System.Security.Cryptography;
using System.Text;

namespace Ezzygate.WebApi.Utils;

public static class HashUtils
{
    public static string ComputeSha256Hash(string input)
    {
        using var sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(bytes).ToLower();
    }
} 