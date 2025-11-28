using System.Text.Json.Serialization;
using Ezzygate.Integrations.Paysafe.Models;

namespace Ezzygate.Integrations.Paysafe.Api;

public class CreatePaymentHandlerResponse : PaymentHandler
{
    [JsonPropertyName("error")]
    public Error? Error { get; set; }
}