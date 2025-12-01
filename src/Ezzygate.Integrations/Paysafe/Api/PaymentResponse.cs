using System.Text.Json.Serialization;
using Ezzygate.Integrations.Paysafe.Models;

namespace Ezzygate.Integrations.Paysafe.Api;

public class PaymentResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("paymentType")]
    public string? PaymentType { get; set; }

    [JsonPropertyName("paymentHandleToken")]
    public string? PaymentHandleToken { get; set; }

    [JsonPropertyName("merchantRefNum")]
    public Guid MerchantRefNum { get; set; }

    [JsonPropertyName("currencyCode")]
    public string? CurrencyCode { get; set; }

    [JsonPropertyName("settleWithAuth")]
    public bool SettleWithAuth { get; set; }

    [JsonPropertyName("txnTime")]
    public DateTimeOffset TxnTime { get; set; }

    [JsonPropertyName("billingDetails")]
    public BillingDetails? BillingDetails { get; set; }

    [JsonPropertyName("customerIp")]
    public string? CustomerIp { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("amount")]
    public long Amount { get; set; }

    [JsonPropertyName("preAuth")]
    public bool PreAuth { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("availableToSettle")]
    public long AvailableToSettle { get; set; }

    [JsonPropertyName("gatewayResponse")]
    public GatewayResponse? GatewayResponse { get; set; }

    [JsonPropertyName("merchantDescriptor")]
    public MerchantDescriptor? MerchantDescriptor { get; set; }

    [JsonPropertyName("settlements")]
    public Settlement[]? Settlements { get; set; }

    [JsonPropertyName("authentication")]
    public Authentication? Authentication { get; set; }

    [JsonPropertyName("profile")]
    public Profile? Profile { get; set; }

    [JsonPropertyName("card")]
    public Card? Card { get; set; }

    [JsonPropertyName("cardSchemeTransactionId")]
    public string? CardSchemeTransactionId { get; set; }

    [JsonPropertyName("error")]
    public Error? Error { get; set; }
}