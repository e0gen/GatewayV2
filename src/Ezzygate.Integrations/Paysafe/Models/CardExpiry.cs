using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Paysafe.Models;

public class CardExpiry
{
    [JsonPropertyName("month")]
    public int Month { get; set; }

    [JsonPropertyName("year")]
    public int Year { get; set; }
}