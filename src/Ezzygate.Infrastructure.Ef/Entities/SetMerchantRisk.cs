using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("SetMerchantRisk", Schema = "Setting")]
public partial class SetMerchantRisk
{
    [Key]
    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    public byte? LimitCcForEmailAllowedCount { get; set; }

    public bool? IsLimitCcForEmailBlockNewCc { get; set; }

    public bool? IsLimitCcForEmailBlockAllCc { get; set; }

    public bool? IsLimitCcForEmailBlockEmail { get; set; }

    public bool? IsLimitCcForEmailDeclineTrans { get; set; }

    public bool? IsLimitCcForEmailNotifyByEmail { get; set; }

    [StringLength(200)]
    public string? LimitCcForEmailNotifyByEmailList { get; set; }

    public byte? LimitEmailForCcAllowedCount { get; set; }

    public bool? IsLimitEmailForCcBlockNewEmail { get; set; }

    public bool? IsLimitEmailForCcBlockAllEmails { get; set; }

    public bool? IsLimitEmailForCcBlockCc { get; set; }

    public bool? IsLimitEmailForCcDeclineTrans { get; set; }

    public bool? IsLimitEmailForCcNotifyByEmail { get; set; }

    [StringLength(200)]
    public string? LimitEmailForCcNotifyByEmailList { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? WhitelistCountry { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? WhitelistState { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? BlacklistCountry { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? BlacklistState { get; set; }

    public bool? IsEnableCcWhiteList { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? AmountAllowedList { get; set; }

    [Column("BlacklistIPCountry")]
    [StringLength(500)]
    [Unicode(false)]
    public string? BlacklistIpcountry { get; set; }

    [Column("PH3A_IsEnabled")]
    public bool? Ph3aIsEnabled { get; set; }

    [Column("PH3A_MinScoreAllowed")]
    public byte? Ph3aMinScoreAllowed { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("SetMerchantRisk")]
    public virtual TblCompany Merchant { get; set; } = null!;
}
