using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Rapyd.Models;

public class RapydStatus
{
    [JsonPropertyName("error_code")]
    public string? ErrorCode { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("response_code")]
    public string? ResponseCode { get; set; }

    [JsonPropertyName("operation_id")]
    public string? OperationId { get; set; }
}