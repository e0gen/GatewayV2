using System.Text.Json.Serialization;
using Ezzygate.Domain.Interfaces;

namespace Ezzygate.Integrations.Rapyd.Models;

public class CardPaymentMethod : ISensitiveData
{
    [JsonPropertyName("type")]
    public required string Type { get; set; }

    [JsonPropertyName("fields")]
    public required CardPaymentFields PaymentFields { get; set; }

    public object Mask()
    {
        PaymentFields.Mask();

        return this;
    }
}