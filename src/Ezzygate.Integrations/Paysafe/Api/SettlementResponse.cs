using System.Text.Json.Serialization;
using Ezzygate.Integrations.Paysafe.Models;

namespace Ezzygate.Integrations.Paysafe.Api;

public class SettlementResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("paymentType")]
    public string? PaymentType { get; set; }

    [JsonPropertyName("availableToRefund")]
    public string? AvailableToRefund { get; set; }

    [JsonPropertyName("childAccountNum")]
    public string? ChildAccountNum { get; set; }

    [JsonPropertyName("txnTime")]
    public DateTimeOffset TxnTime { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("gatewayReconciliationId")]
    public string? GatewayReconciliationId { get; set; }

    [JsonPropertyName("updatedTime")]
    public DateTimeOffset UpdatedTime { get; set; }

    [JsonPropertyName("statusTime")]
    public DateTimeOffset StatusTime { get; set; }

    [JsonPropertyName("liveMode")]
    public bool LiveMode { get; set; }

    [JsonPropertyName("gatewayResponse")]
    public GatewayResponse? GatewayResponse { get; set; }

    [JsonPropertyName("source")]
    public string? Source { get; set; }

    [JsonPropertyName("error")]
    public Error? Error { get; set; }

    [JsonPropertyName("riskReasonCode")]
    public int[]? RiskReasonCode { get; set; }

    [JsonPropertyName("merchantRefNum")]
    public Guid MerchantRefNum { get; set; }

    [JsonPropertyName("amount")]
    public long Amount { get; set; }

    [JsonPropertyName("dupCheck")]
    public bool DupCheck { get; set; }
}