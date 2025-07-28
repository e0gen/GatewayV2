using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

/// <summary>
/// Temporary hold unassigned payment method provided by external issuer.
/// </summary>
[Table("PreCreatedPaymentMethod", Schema = "Data")]
public partial class PreCreatedPaymentMethod
{
    [Key]
    [Column("PreCreatedPaymentMethod_id")]
    public int PreCreatedPaymentMethodId { get; set; }

    [Column("PaymentMethodProvider_id")]
    [StringLength(16)]
    [Unicode(false)]
    public string? PaymentMethodProviderId { get; set; }

    [Column("PaymentMethod_id")]
    public short PaymentMethodId { get; set; }

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

    public DateOnly? ExpirationDate { get; set; }

    [StringLength(80)]
    public string? OwnerName { get; set; }

    public DateOnly? OwnerDateOfBirth { get; set; }

    [Column("OwnerPersonalID")]
    [StringLength(15)]
    [Unicode(false)]
    public string? OwnerPersonalId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? OwnerPhoneNumber { get; set; }

    public byte EncryptionKey { get; set; }

    public bool IsAssigned { get; set; }

    [ForeignKey("PaymentMethodId")]
    [InverseProperty("PreCreatedPaymentMethods")]
    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    [ForeignKey("PaymentMethodProviderId")]
    [InverseProperty("PreCreatedPaymentMethods")]
    public virtual PaymentMethodProvider? PaymentMethodProvider { get; set; }
}
