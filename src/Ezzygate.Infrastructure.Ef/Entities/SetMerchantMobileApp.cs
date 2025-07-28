using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("SetMerchantMobileApp", Schema = "Setting")]
[Index("MerchantId", Name = "UIX_SetMerchantMobileAppMerchant_id", IsUnique = true)]
public partial class SetMerchantMobileApp
{
    [Key]
    [Column("SetMerchantMobileApp_id")]
    public int SetMerchantMobileAppId { get; set; }

    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    public bool IsEnableMobileApp { get; set; }

    public bool IsAllowCardNotPresent { get; set; }

    public bool IsAllowRefund { get; set; }

    public bool IsAllowAuthorization { get; set; }

    public bool IsAllowInstallments { get; set; }

    public bool IsRequireEmail { get; set; }

    public bool IsRequirePersonalNumber { get; set; }

    public bool IsRequireFullName { get; set; }

    public bool IsRequirePhoneNumber { get; set; }

    [Column("IsRequireCVV")]
    public bool IsRequireCvv { get; set; }

    public bool IsRequireCardholderSignature { get; set; }

    public Guid SyncToken { get; set; }

    [Column(TypeName = "decimal(4, 2)")]
    public decimal? ValueAddedTax { get; set; }

    public bool IsAllowTaxRateChange { get; set; }

    public short MaxDeviceCount { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("SetMerchantMobileApp")]
    public virtual TblCompany Merchant { get; set; } = null!;
}
