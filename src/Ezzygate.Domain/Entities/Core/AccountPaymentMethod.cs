using System.ComponentModel.DataAnnotations;
using Ezzygate.Domain.Entities.Lookup;

namespace Ezzygate.Domain.Entities.Core;

public sealed class AccountPaymentMethod
{
    [Key]
    public int AccountPaymentMethodId { get; set; }

    [Required]
    public int AccountId { get; set; }

    public int? AccountAddressId { get; set; }

    [Required]
    public short PaymentMethodId { get; set; }

    [Required]
    public bool IsDefault { get; set; }

    [StringLength(30)]
    public string? Title { get; set; }

    [StringLength(80)]
    public string? OwnerName { get; set; }

    [StringLength(15)]
    public string? OwnerPersonalId { get; set; }

    public DateTime? OwnerDateOfBirth { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public byte[]? Value1Encrypted { get; set; }

    public byte[]? Value2Encrypted { get; set; }

    [StringLength(20)]
    public string? PaymentMethodText { get; set; }

    [StringLength(4)]
    public string? Value1Last4Text { get; set; }

    [StringLength(6)]
    public string? Value1First6Text { get; set; }

    [StringLength(2)]
    public string? IssuerCountryIsoCode { get; set; }

    [Required]
    public byte EncryptionKey { get; set; }

    [StringLength(16)]
    public string? PaymentMethodProviderId { get; set; }

    [Required]
    public byte PaymentMethodStatusId { get; set; }

    [StringLength(32)]
    public string? ProviderReference1 { get; set; }

    public Account Account { get; set; } = null!;
    public AccountAddress? AccountAddress { get; set; }
    public PaymentMethod PaymentMethod { get; set; } = null!;
    public CountryList? IssuerCountry { get; set; }
    public PaymentMethodProvider? PaymentMethodProvider { get; set; }
}