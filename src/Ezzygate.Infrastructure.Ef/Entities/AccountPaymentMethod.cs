using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

/// <summary>
/// Hold the personal + gateway provided payment method of an account
/// </summary>
[Table("AccountPaymentMethod", Schema = "Data")]
[Index("Value1Encrypted", Name = "IX_AccountPaymentMethod_Value1Encrypted")]
public partial class AccountPaymentMethod
{
    [Key]
    [Column("AccountPaymentMethod_id")]
    public int AccountPaymentMethodId { get; set; }

    [Column("Account_id")]
    public int AccountId { get; set; }

    [Column("AccountAddress_id")]
    public int? AccountAddressId { get; set; }

    [Column("PaymentMethod_id")]
    public short PaymentMethodId { get; set; }

    public bool IsDefault { get; set; }

    [StringLength(30)]
    public string? Title { get; set; }

    [StringLength(80)]
    public string? OwnerName { get; set; }

    [Column("OwnerPersonalID")]
    [StringLength(15)]
    [Unicode(false)]
    public string? OwnerPersonalId { get; set; }

    public DateOnly? OwnerDateOfBirth { get; set; }

    public DateOnly? ExpirationDate { get; set; }

    [MaxLength(40)]
    public byte[]? Value1Encrypted { get; set; }

    [MaxLength(40)]
    public byte[]? Value2Encrypted { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? PaymentMethodText { get; set; }

    [StringLength(4)]
    [Unicode(false)]
    public string? Value1Last4Text { get; set; }

    [StringLength(6)]
    [Unicode(false)]
    public string? Value1First6Text { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? IssuerCountryIsoCode { get; set; }

    public byte EncryptionKey { get; set; }

    [Column("PaymentMethodProvider_id")]
    [StringLength(16)]
    [Unicode(false)]
    public string? PaymentMethodProviderId { get; set; }

    [Column("PaymentMethodStatus_id")]
    public byte PaymentMethodStatusId { get; set; }

    [StringLength(32)]
    [Unicode(false)]
    public string? ProviderReference1 { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("AccountPaymentMethods")]
    public virtual Account Account { get; set; } = null!;

    [ForeignKey("AccountAddressId")]
    [InverseProperty("AccountPaymentMethods")]
    public virtual AccountAddress? AccountAddress { get; set; }

    [ForeignKey("IssuerCountryIsoCode")]
    [InverseProperty("AccountPaymentMethods")]
    public virtual CountryList? IssuerCountryIsoCodeNavigation { get; set; }

    [ForeignKey("PaymentMethodId")]
    [InverseProperty("AccountPaymentMethods")]
    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    [ForeignKey("PaymentMethodProviderId")]
    [InverseProperty("AccountPaymentMethods")]
    public virtual PaymentMethodProvider? PaymentMethodProvider { get; set; }

    [InverseProperty("AccountPaymentMethod")]
    public virtual ICollection<SetPeriodicFee> SetPeriodicFees { get; set; } = new List<SetPeriodicFee>();
}
