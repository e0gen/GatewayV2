using System.Text.Json.Serialization;
using Ezzygate.Application.Interfaces;

namespace Ezzygate.Integrations.Rapyd.Api;

public class PaymentRequest : ISensitiveData
{
    [JsonPropertyName("amount")]
    public required string Amount { get; set; }

    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("payment_method")]
    public required object PaymentMethod { get; set; }

    [JsonPropertyName("capture")]
    public bool Capture { get; set; }

    [JsonPropertyName("complete_payment_url")]
    public required string CompletePaymentUrl { get; set; }

    [JsonPropertyName("error_payment_url")]
    public required string ErrorPaymentUrl { get; set; }

    public object Mask()
    {
        if (PaymentMethod is ISensitiveData paymentMethod)
            paymentMethod.Mask();

        return this;
    }
}