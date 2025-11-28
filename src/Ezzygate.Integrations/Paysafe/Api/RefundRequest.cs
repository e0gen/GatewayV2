using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Paysafe.Api;

public class RefundRequest
{
    [JsonPropertyName("merchantRefNum")]
    public required string MerchantRefNum { get; set; }

    [JsonPropertyName("amount")]
    public long Amount { get; set; }

    [JsonPropertyName("dupCheck")]
    public bool DupCheck { get; set; }
}