using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Rapyd.Models;

public class PaymentData
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
        
    [JsonPropertyName("amount")]
    public long Amount { get; set; }

    [JsonPropertyName("original_amount")]
    public long OriginalAmount { get; set; }

    [JsonPropertyName("is_partial")]
    public bool IsPartial { get; set; }

    [JsonPropertyName("currency_code")]
    public string? CurrencyCode { get; set; }

    [JsonPropertyName("country_code")]
    public string? CountryCode { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("merchant_reference_id")]
    public string? MerchantReferenceId { get; set; }

    [JsonPropertyName("customer_token")]
    public string? CustomerToken { get; set; }

    [JsonPropertyName("payment_method")]
    public string? PaymentMethod { get; set; }

    [JsonPropertyName("auth_code")]
    public object? AuthCode { get; set; }

    [JsonPropertyName("expiration")]
    public long Expiration { get; set; }

    [JsonPropertyName("captured")]
    public bool Captured { get; set; }

    [JsonPropertyName("refunded")]
    public bool Refunded { get; set; }

    [JsonPropertyName("refunded_amount")]
    public long RefundedAmount { get; set; }

    [JsonPropertyName("receipt_email")]
    public string? ReceiptEmail { get; set; }

    [JsonPropertyName("redirect_url")]
    public string? RedirectUrl { get; set; }

    [JsonPropertyName("complete_payment_url")]
    public string? CompletePaymentUrl { get; set; }

    [JsonPropertyName("error_payment_url")]
    public string? ErrorPaymentUrl { get; set; }

    [JsonPropertyName("metadata")]
    public Metadata? Metadata { get; set; }

    [JsonPropertyName("failure_code")]
    public string? FailureCode { get; set; }

    [JsonPropertyName("failure_message")]
    public string? FailureMessage { get; set; }

    [JsonPropertyName("next_action")]
    public string? NextAction { get; set; }

    [JsonPropertyName("error_code")]
    public string? ErrorCode { get; set; }
}