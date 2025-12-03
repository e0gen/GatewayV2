using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Infrastructure.Utilities;

namespace Ezzygate.Integrations.Paysafe;

public static class PaysafeServices
{
    public static string? GetAccountNumber(string currency, bool isTestTerminal) => isTestTerminal
        ? currency switch
        {
            "USD" => "1002969520",
            "EUR" => "1002969490",
            "GBP" => "1002968160",
            _ => null
        }
        : currency switch
        {
            "EUR" => "1003102342",
            "GBP" => "1003102332",
            _ => null
        };

    public static bool VerifySignature(string content, string hmacKey, string? receivedSignature)
    {
        if (string.IsNullOrEmpty(receivedSignature))
            return false;

        try
        {
            var calculatedSignature = HashUtils.ComputeHmacSha256(content, hmacKey);
            return string.Equals(calculatedSignature, receivedSignature, StringComparison.Ordinal);
        }
        catch
        {
            return false;
        }
    }

    public static TrxType GetTrxType(TransactionContext ctx)
    {
        return ctx.TransType switch
        {
            0 => TrxType.Auth,
            3 => TrxType.Auth,
            8 => TrxType.Auth,
            1 => TrxType.PreAuth,
            2 => TrxType.PostAuth,
            4 => TrxType.Void,
            _ => throw new Exception("Paysafe invalid trans type")
        };
    }

    public enum TrxType
    {
        Auth,
        PreAuth,
        PostAuth,
        Void,
        Credit
    }
}