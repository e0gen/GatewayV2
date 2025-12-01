using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Paysafe.Models;

public class ShippingDetailsUsage
{
    [JsonPropertyName("cardHolderNameMatch")]
    public bool CardHolderNameMatch { get; set; }

    [JsonPropertyName("initialUsageDate")]
    public DateTimeOffset InitialUsageDate { get; set; }

    [JsonPropertyName("initialUsageRange")]
    public string? InitialUsageRange { get; set; }
}