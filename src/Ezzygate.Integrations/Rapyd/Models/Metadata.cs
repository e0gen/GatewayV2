using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Rapyd.Models;

public class Metadata
{
    [JsonPropertyName("merchant_defined")]
    public bool MerchantDefined { get; set; }
}