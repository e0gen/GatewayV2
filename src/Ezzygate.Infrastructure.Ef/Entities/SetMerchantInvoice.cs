using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("SetMerchantInvoice", Schema = "Setting")]
[Index("MerchantId", Name = "UIX_SetMerchantInvoiceMerchant_id", IsUnique = true)]
public partial class SetMerchantInvoice
{
    [Key]
    [Column("SetMerchantInvoice_id")]
    public int SetMerchantInvoiceId { get; set; }

    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    [Column("ExternalProviderID")]
    public byte ExternalProviderId { get; set; }

    [StringLength(50)]
    public string? ExternalUseName { get; set; }

    [Column("ExternalUserID")]
    [StringLength(50)]
    public string? ExternalUserId { get; set; }

    [StringLength(50)]
    public string? ExternalPassword { get; set; }

    [StringLength(100)]
    public string? ItemText { get; set; }

    public bool IsEnable { get; set; }

    [Column("IsAutoGenerateILS")]
    public bool IsAutoGenerateIls { get; set; }

    public bool IsAutoGenerateOther { get; set; }

    public bool IsAutoGenerateRefund { get; set; }

    public bool IsIncludeTax { get; set; }

    public bool IsCreateInvoice { get; set; }

    public bool IsCreateReceipt { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("SetMerchantInvoice")]
    public virtual TblCompany Merchant { get; set; } = null!;
}
