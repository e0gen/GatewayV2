using System.Text.Json.Serialization;
using Ezzygate.Domain.Interfaces;
using Ezzygate.Infrastructure.Utilities;
using Ezzygate.Integrations.QrMoney.Models;

namespace Ezzygate.Integrations.QrMoney.Api;

public class QrMoneyPaymentRequest : ISensitiveData
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("number")]
    public string? Number { get; set; }

    [JsonPropertyName("expiration")]
    public string? Expiration { get; set; } // Format: MM/YY

    [JsonPropertyName("cvv")]
    public string? Cvv { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("phoneNumber")]
    public string? PhoneNumber { get; set; }

    [JsonPropertyName("address")]
    public string? Address { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("postalCode")]
    public string? PostalCode { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; } // ISO-3166-1 alpha-2 (e.g., "US")

    [JsonPropertyName("birthDate")]
    public string? BirthDate { get; set; } // Format: YYYYMMDD

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("unit")]
    public string? Unit { get; set; } // Currency (e.g., USD, EUR, BTC, USDT)

    [JsonPropertyName("originDomain")]
    public string? OriginDomain { get; set; }

    [JsonPropertyName("referenceId")]
    public string? ReferenceId { get; set; }

    [JsonPropertyName("notifyUrl")]
    public string? NotifyUrl { get; set; }

    [JsonPropertyName("successUrl")]
    public string? SuccessUrl { get; set; }

    [JsonPropertyName("failureUrl")]
    public string? FailureUrl { get; set; }

    [JsonPropertyName("browserInfo")]
    public BrowserInfo? BrowserInfo { get; set; }

    [JsonPropertyName("wallet")]
    public Wallet? Wallet { get; set; }

    public object Mask()
    {
        Number = SecureUtils.MaskNumber(Number);
        Cvv = SecureUtils.MaskCvv(Cvv);
        Name = SecureUtils.MaskName(Name);
        Email = SecureUtils.MaskEmail(Email);
        Address = SecureUtils.MaskAddress(Address);
        PhoneNumber = SecureUtils.MaskPhone(PhoneNumber);
        return this;
    }
}