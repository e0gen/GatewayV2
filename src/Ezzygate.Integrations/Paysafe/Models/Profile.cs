using System.Text.Json.Serialization;
using Ezzygate.Application.Interfaces;
using Ezzygate.Infrastructure.Utilities;

namespace Ezzygate.Integrations.Paysafe.Models;

public class Profile : ISensitiveData
{
    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string? LastName { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("merchantCustomerId")]
    public string? MerchantCustomerId { get; set; }

    public object Mask()
    {
        FirstName = SecureUtils.MaskName(FirstName);
        LastName = SecureUtils.MaskName(LastName);
        Email = SecureUtils.MaskEmail(Email);
        Phone = SecureUtils.MaskPhone(Phone);
        return this;
    }
}