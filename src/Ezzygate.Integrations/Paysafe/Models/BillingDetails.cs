using System.Text.Json.Serialization;
using Ezzygate.Domain.Interfaces;
using Ezzygate.Infrastructure.Security;

namespace Ezzygate.Integrations.Paysafe.Models;

public class BillingDetails : ISensitiveData
{
    [JsonPropertyName("nickName")]
    public string? NickName { get; set; }

    [JsonPropertyName("street")]
    public string? Street { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("zip")]
    public string? Zip { get; set; }

    public object Mask()
    {
        NickName = SecureUtils.MaskName(NickName);
        Street = SecureUtils.MaskAddress(Street);
        return this;
    }
}