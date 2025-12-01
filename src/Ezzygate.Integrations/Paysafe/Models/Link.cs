using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Paysafe.Models;

public class Link
{
    [JsonPropertyName("method")]
    public string? Method { get; set; }

    [JsonPropertyName("rel")]
    public string? Rel { get; set; }

    [JsonPropertyName("href")]
    public string? Href { get; set; }
}