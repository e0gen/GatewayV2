using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Paysafe.Api;

public class VoidAuthorizationResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("txnTime")]
    public DateTimeOffset TxnTime { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("merchantRefNum")]
    public Guid MerchantRefNum { get; set; }

    [JsonPropertyName("amount")]
    public long Amount { get; set; }
}