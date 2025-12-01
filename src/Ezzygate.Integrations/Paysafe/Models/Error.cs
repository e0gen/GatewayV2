using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Paysafe.Models;

public class Error
{
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("details")]
    public string[]? Details { get; set; }

    [JsonPropertyName("fieldErrors")]
    public FieldError[]? FieldErrors { get; set; }

    public string ToMessage()
    {
        var result = Message ?? string.Empty;
        if (FieldErrors is { Length: > 0 })
        {
            result += $": {string.Join(", ", FieldErrors.Select(field => field.ToMessage()))}";
        }

        return result;
    }
}

public class FieldError
{
    [JsonPropertyName("field")]
    public string? Field { get; set; }

    [JsonPropertyName("error")]
    public string? Error { get; set; }

    public string ToMessage()
    {
        return $"{Field}: {Error}";
    }
}