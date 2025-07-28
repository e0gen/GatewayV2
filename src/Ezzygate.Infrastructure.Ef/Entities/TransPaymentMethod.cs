using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("TransPaymentMethod", Schema = "Trans")]
public partial class TransPaymentMethod
{
    [Key]
    [Column("TransPaymentMethod_id")]
    public int TransPaymentMethodId { get; set; }

    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    [Column("TransPaymentBillingAddress_id")]
    public int? TransPaymentBillingAddressId { get; set; }

    [Column("PaymentMethod_id")]
    public short PaymentMethodId { get; set; }

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

    [Column("CreditCard_id")]
    public int? CreditCardId { get; set; }

    [Column("Echeck_id")]
    public int? EcheckId { get; set; }

    [ForeignKey("IssuerCountryIsoCode")]
    [InverseProperty("TransPaymentMethods")]
    public virtual CountryList? IssuerCountryIsoCodeNavigation { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("TransPaymentMethods")]
    public virtual TblCompany Merchant { get; set; } = null!;

    [ForeignKey("PaymentMethodId")]
    [InverseProperty("TransPaymentMethods")]
    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    [InverseProperty("TransPaymentMethod")]
    public virtual ICollection<TblCompanyTransApproval> TblCompanyTransApprovals { get; set; } = new List<TblCompanyTransApproval>();

    [InverseProperty("TransPaymentMethod")]
    public virtual ICollection<TblCompanyTransFail> TblCompanyTransFails { get; set; } = new List<TblCompanyTransFail>();

    [InverseProperty("TransPaymentMethod")]
    public virtual ICollection<TblCompanyTransPass> TblCompanyTransPasses { get; set; } = new List<TblCompanyTransPass>();

    [InverseProperty("TransPaymentMethod")]
    public virtual ICollection<TblCompanyTransPending> TblCompanyTransPendings { get; set; } = new List<TblCompanyTransPending>();

    [InverseProperty("TransPaymentMethod")]
    public virtual ICollection<TblCompanyTransRemoved> TblCompanyTransRemoveds { get; set; } = new List<TblCompanyTransRemoved>();

    [InverseProperty("TransPaymentMethod")]
    public virtual ICollection<TblRecurringAttempt> TblRecurringAttempts { get; set; } = new List<TblRecurringAttempt>();

    [InverseProperty("TransPaymentMethod")]
    public virtual ICollection<TblRecurringCharge> TblRecurringCharges { get; set; } = new List<TblRecurringCharge>();

    [InverseProperty("TransPaymentMethod")]
    public virtual ICollection<TblRecurringSeries> TblRecurringSeries { get; set; } = new List<TblRecurringSeries>();

    [ForeignKey("TransPaymentBillingAddressId")]
    [InverseProperty("TransPaymentMethods")]
    public virtual TransPaymentBillingAddress? TransPaymentBillingAddress { get; set; }
}
