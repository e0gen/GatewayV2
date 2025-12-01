using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Paysafe.Models;

public class GatewayResponse
{
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("responseCode")]
    public string? ResponseCode { get; set; }

    [JsonPropertyName("responseCodeDescription")]
    public string? ResponseCodeDescription { get; set; }

    [JsonPropertyName("avsCode")]
    public string? AvsCode { get; set; }

    [JsonPropertyName("mid")]
    public string? Mid { get; set; }

    [JsonPropertyName("authCode")]
    public string? AuthCode { get; set; }

    [JsonPropertyName("avsResponse")]
    public string? AvsResponse { get; set; }

    [JsonPropertyName("cvvVerification")]
    public string? CvvVerification { get; set; }

    [JsonPropertyName("serializable")]
    public bool Serializable { get; set; }
}