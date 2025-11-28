using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Paysafe.Models;

public class PaysafeEvent
{
    [JsonPropertyName("attemptNumber")]
    public string? AttemptNumber { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("eventName")]
    public string? EventName { get; set; }
}

public class PaysafeEvent<T> : PaysafeEvent
{
    [JsonPropertyName("payload")]
    public T? Payload { get; set; }
}