using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.QrMoney.Api;

public class QrMoneyPaymentResponse
{
    [JsonPropertyName("paymentProviderId")]
    public string? PaymentProviderId { get; set; }

    [JsonPropertyName("data")]
    public string? Data { get; set; }

    [JsonPropertyName("transactionId")]
    public string? RedirectUrl { get; set; }

    [JsonPropertyName("challengeRequired")]
    public bool ChallengeRequired { get; set; }
}