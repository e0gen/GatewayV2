using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Paysafe.Models;

public class Authentication
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("eci")]
    public string? Eci { get; set; }

    [JsonPropertyName("cavv")]
    public string? Cavv { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("threeDResult")]
    public string? ThreeDResult { get; set; }

    [JsonPropertyName("threeDSecureVersion")]
    public string? ThreeDSecureVersion { get; set; }

    [JsonPropertyName("directoryServerTransactionId")]
    public string? DirectoryServerTransactionId { get; set; }

    [JsonPropertyName("exemptionIndicator")]
    public string? ExemptionIndicator { get; set; }

    [JsonPropertyName("error")]
    public Error? Error { get; set; }
}