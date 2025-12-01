using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Paysafe.Api;

public class VoidAuthorizationRequest
{
    [JsonPropertyName("merchantRefNum")]
    public required string MerchantRefNum { get; set; }

    [JsonPropertyName("amount")]
    public long Amount { get; set; }
}