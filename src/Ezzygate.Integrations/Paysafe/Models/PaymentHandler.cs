using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Paysafe.Models;

public class PaymentHandler
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("accountId")]
    public string? AccountId { get; set; }

    [JsonPropertyName("paymentType")]
    public string? PaymentType { get; set; }

    [JsonPropertyName("paymentHandleToken")]
    public string? PaymentHandleToken { get; set; }

    [JsonPropertyName("merchantRefNum")]
    public string? MerchantRefNum { get; set; }

    [JsonPropertyName("currencyCode")]
    public string? CurrencyCode { get; set; }

    [JsonPropertyName("txnTime")]
    public DateTimeOffset TxnTime { get; set; }

    [JsonPropertyName("billingDetails")]
    public BillingDetails? BillingDetails { get; set; }

    [JsonPropertyName("customerIp")]
    public string? CustomerIp { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("links")]
    public Link[]? Links { get; set; }

    [JsonPropertyName("amount")]
    public long Amount { get; set; }

    [JsonPropertyName("action")]
    public string? Action { get; set; }

    [JsonPropertyName("usage")]
    public string? Usage { get; set; }

    [JsonPropertyName("timeToLiveSeconds")]
    public long TimeToLiveSeconds { get; set; }

    [JsonPropertyName("transactionType")]
    public string? TransactionType { get; set; }

    [JsonPropertyName("executionMode")]
    public string? ExecutionMode { get; set; }

    [JsonPropertyName("profile")]
    public Profile? Profile { get; set; }

    [JsonPropertyName("card")]
    public Card? Card { get; set; }

    [JsonPropertyName("authentication")]
    public Authentication? Authentication { get; set; }

    [JsonPropertyName("returnLinks")]
    public Link[]? ReturnLinks { get; set; }

    [JsonPropertyName("skip3ds")]
    public bool Skip3Ds { get; set; }

    [JsonPropertyName("threeDs")]
    public ThreeDs? ThreeDs { get; set; }
}