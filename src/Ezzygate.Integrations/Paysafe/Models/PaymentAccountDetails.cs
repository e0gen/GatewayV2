using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Paysafe.Models;

public class PaymentAccountDetails
{
    [JsonPropertyName("createdRange")]
    public string? CreatedRange { get; set; }

    [JsonPropertyName("createdDate")]
    public DateTimeOffset CreatedDate { get; set; }
}