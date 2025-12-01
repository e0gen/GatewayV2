using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Paysafe.Models;

public class UserLogin
{
    [JsonPropertyName("authenticationMethod")]
    public string? AuthenticationMethod { get; set; }

    [JsonPropertyName("data")]
    public string? Data { get; set; }

    [JsonPropertyName("time")]
    public DateTimeOffset Time { get; set; }
}