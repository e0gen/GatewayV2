using System.Text.Json.Serialization;
using Ezzygate.Application.Interfaces;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Utilities;

namespace Ezzygate.Integrations.Rapyd.Models;

public class CardPaymentFields : ISensitiveData
{
    [JsonPropertyName("number")]
    public required string Number { get; set; }

    [JsonPropertyName("expiration_month")]
    public required string ExpirationMonth { get; set; }

    [JsonPropertyName("expiration_year")]
    public required string ExpirationYear { get; set; }

    [JsonPropertyName("cvv")]
    public required string Cvv { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    public object Mask()
    {
        Number = SecureUtils.MaskNumber(Number).NotNull();
        Cvv = SecureUtils.MaskCvv(Cvv).NotNull();
        Name = SecureUtils.MaskName(Name).NotNull();

        return this;
    }
}