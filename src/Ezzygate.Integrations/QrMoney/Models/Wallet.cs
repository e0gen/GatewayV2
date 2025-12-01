using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.QrMoney.Models;

public class Wallet
{
    [JsonPropertyName("authenticationValue")]
    public string? AuthenticationValue { get; set; }

    [JsonPropertyName("walletType")]
    public string? WalletType { get; set; } // G = Google, A = Apple

    [JsonPropertyName("xid")]
    public string? Xid { get; set; }

    [JsonPropertyName("eci")]
    public string? Eci { get; set; }
}