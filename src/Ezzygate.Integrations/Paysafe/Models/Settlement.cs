using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Paysafe.Models;

public class Settlement
{
    [JsonPropertyName("merchantRefNum")]
    public string? MerchantRefNum { get; set; }

    [JsonPropertyName("amount")]
    public long Amount { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("availableToRefund")]
    public long AvailableToRefund { get; set; }

    [JsonPropertyName("txnTime")]
    public long TxnTime { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("gatewayReconciliationId")]
    public string? GatewayReconciliationId { get; set; }
}