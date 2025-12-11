using System.Text.Json.Serialization;
using Ezzygate.Application.Interfaces;
using Ezzygate.Infrastructure.Utilities;

namespace Ezzygate.Integrations.Paysafe.Models;

public class Card : ISensitiveData
{
    [JsonPropertyName("cardNum")]
    public string? CardNum { get; set; }

    [JsonPropertyName("cardExpiry")]
    public CardExpiry? CardExpiry { get; set; }

    [JsonPropertyName("cvv")]
    public string? Cvv { get; set; }

    [JsonPropertyName("holderName")]
    public string? HolderName { get; set; }

    [JsonPropertyName("issuingCountry")]
    public string? IssuingCountry { get; set; }

    public object Mask()
    {
        CardNum = SecureUtils.MaskNumber(CardNum);
        Cvv = SecureUtils.MaskCvv(Cvv);
        HolderName = SecureUtils.MaskName(HolderName);
        return this;
    }
}