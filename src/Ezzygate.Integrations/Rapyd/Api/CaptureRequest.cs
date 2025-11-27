using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Rapyd.Api
{
    public class CaptureRequest
    {
        [JsonPropertyName("amount")]
        public required string Amount { get; set; }
        
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("statement_descriptor")]
        public string? StatementDescriptor { get; set; }
    }
}