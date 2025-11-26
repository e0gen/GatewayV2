using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Rapyd.Models;

public class RefundData
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("amount")]
    public long Amount { get; set; }

    [JsonPropertyName("payment")]
    public string? Payment { get; set; }

    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("failure_reason")]
    public string? FailureReason { get; set; }

    [JsonPropertyName("reason")]
    public string? Reason { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("receipt_number")]
    public long ReceiptNumber { get; set; }

    [JsonPropertyName("created_at")]
    public long CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public long UpdatedAt { get; set; }

    [JsonPropertyName("merchant_reference_id")]
    public string? MerchantReferenceId { get; set; }

    [JsonPropertyName("payment_created_at")]
    public long PaymentCreatedAt { get; set; }

    [JsonPropertyName("payment_method_type")]
    public string? PaymentMethodType { get; set; }

    [JsonPropertyName("proportional_refund")]
    public bool ProportionalRefund { get; set; }
}