using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.QrMoney.Api;

public class QrMoneyError
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("errors")]
    public object? Errors { get; set; }

    [JsonPropertyName("traceId")]
    public string? TraceId { get; set; }
}