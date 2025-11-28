using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Paysafe.Api;

public class PaymentRequest
{
    [JsonPropertyName("merchantRefNum")]
    public required string MerchantRefNum { get; set; }

    [JsonPropertyName("amount")]
    public long Amount { get; set; }

    [JsonPropertyName("currencyCode")]
    public required string CurrencyCode { get; set; }

    [JsonPropertyName("dupCheck")]
    public bool DupCheck { get; set; }

    [JsonPropertyName("preAuth")]
    public bool PreAuth { get; set; }

    [JsonPropertyName("settleWithAuth")]
    public bool SettleWithAuth { get; set; }

    [JsonPropertyName("paymentHandleToken")]
    public required string PaymentHandleToken { get; set; }

    [JsonPropertyName("customerIp")]
    public string? CustomerIp { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("keywords")]
    public string[]? Keywords { get; set; }
}