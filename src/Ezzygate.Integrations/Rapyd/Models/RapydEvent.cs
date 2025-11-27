using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Rapyd.Models;

public class RapydEvent
{
    [JsonPropertyName("type")]
    public required string Type { get; set; }
        
    [JsonPropertyName("data")]
    public required RapydEventData Data { get; set; }
}

public class RapydEventData
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("status")]
    public required string Status { get; set; }
}