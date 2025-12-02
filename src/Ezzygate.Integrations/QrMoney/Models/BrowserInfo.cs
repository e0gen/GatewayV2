using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.QrMoney.Models;

public class BrowserInfo
{
    [JsonPropertyName("browserAcceptHeader")]
    public string? BrowserAcceptHeader { get; set; }

    [JsonPropertyName("browserColorDepth")]
    public string? BrowserColorDepth { get; set; }

    [JsonPropertyName("browserIP")]
    public string? BrowserIp { get; set; }

    [JsonPropertyName("browserJavaEnabled")]
    public bool? BrowserJavaEnabled { get; set; }

    [JsonPropertyName("browserJavascriptEnabled")]
    public bool? BrowserJavascriptEnabled { get; set; }

    [JsonPropertyName("browserLanguage")]
    public string? BrowserLanguage { get; set; }

    [JsonPropertyName("browserScreenHeight")]
    public string? BrowserScreenHeight { get; set; }

    [JsonPropertyName("browserScreenWidth")]
    public string? BrowserScreenWidth { get; set; }

    [JsonPropertyName("browserTZ")]
    public string? BrowserTz { get; set; }

    [JsonPropertyName("browserUserAgent")]
    public string? BrowserUserAgent { get; set; }
}