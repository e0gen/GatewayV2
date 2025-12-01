using System.Text.Json.Serialization;
using Ezzygate.Domain.Interfaces;
using Ezzygate.Integrations.Paysafe.Models;

namespace Ezzygate.Integrations.Paysafe.Api;

public class CreatePaymentHandlerRequest : ISensitiveData
{
    [JsonPropertyName("merchantRefNum")]
    public required string MerchantRefNum { get; set; }

    [JsonPropertyName("transactionType")]
    public required string TransactionType { get; set; }

    [JsonPropertyName("threeDs")]
    public ThreeDs? ThreeDs { get; set; }

    [JsonPropertyName("profile")]
    public Profile? Profile { get; set; }

    [JsonPropertyName("card")]
    public Card? Card { get; set; }

    [JsonPropertyName("accountId")]
    public string? AccountId { get; set; }

    [JsonPropertyName("paymentType")]
    public required string PaymentType { get; set; }

    [JsonPropertyName("amount")]
    public long Amount { get; set; }

    [JsonPropertyName("currencyCode")]
    public required string CurrencyCode { get; set; }

    [JsonPropertyName("billingDetails")]
    public BillingDetails? BillingDetails { get; set; }

    [JsonPropertyName("authentication")]
    public Authentication? Authentication { get; set; }

    [JsonPropertyName("returnLinks")]
    public Link[]? ReturnLinks { get; set; }

    public object Mask()
    {
        Card?.Mask();
        Profile?.Mask();
        BillingDetails?.Mask();
        return this;
    }
}