using System.Text.Json.Serialization;
using Ezzygate.Integrations.Rapyd.Models;

namespace Ezzygate.Integrations.Rapyd.Api
{
    public class RapydResponse<TData> where TData : class
    {
        [JsonPropertyName("status")]
        public RapydStatus Status { get; set; }

        [JsonPropertyName("data")]
        public TData Data { get; set; }
    }
}