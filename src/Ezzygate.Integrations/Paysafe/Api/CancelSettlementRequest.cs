using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Paysafe.Api;

public class CancelSettlementRequest
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = "CANCELLED";
}