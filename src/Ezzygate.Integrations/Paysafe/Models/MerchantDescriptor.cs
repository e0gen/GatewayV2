using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Paysafe.Models;

public class MerchantDescriptor
{
    [JsonPropertyName("dynamicDescriptor")]
    public string? DynamicDescriptor { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }
}