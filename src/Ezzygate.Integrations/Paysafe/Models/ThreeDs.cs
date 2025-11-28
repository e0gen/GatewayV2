using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Paysafe.Models;

public class ThreeDs
{
    [JsonPropertyName("merchantUrl")]
    public string? MerchantUrl { get; set; }

    [JsonPropertyName("deviceChannel")]
    public string? DeviceChannel { get; set; }

    [JsonPropertyName("messageCategory")]
    public string? MessageCategory { get; set; }

    [JsonPropertyName("transactionIntent")]
    public string? TransactionIntent { get; set; }

    [JsonPropertyName("authenticationPurpose")]
    public string? AuthenticationPurpose { get; set; }

    [JsonPropertyName("requestorChallengePreference")]
    public string? RequestorChallengePreference { get; set; }
}