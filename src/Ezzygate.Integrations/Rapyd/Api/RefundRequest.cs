using System.Text.Json.Serialization;
using Ezzygate.Integrations.Rapyd.Models;

namespace Ezzygate.Integrations.Rapyd.Api;

public class RefundRequest
{
    [JsonPropertyName("payment")]
    public required string Payment { get; set; }

    [JsonPropertyName("metadata")]
    public required Metadata Metadata { get; set; }

    [JsonPropertyName("merchant_reference_id")]
    public required string MerchantReferenceId { get; set; }

    [JsonPropertyName("reason")]
    public string? Reason { get; set; }
}