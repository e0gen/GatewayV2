using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("LanguageList", Schema = "List")]
[Index("LanguageIsocode", Name = "UIX_LanguageList_LanguageISOCode", IsUnique = true)]
public partial class LanguageList
{
    [Key]
    [Column("Language_id")]
    public byte LanguageId { get; set; }

    [Column("ISO2")]
    [StringLength(2)]
    [Unicode(false)]
    public string? Iso2 { get; set; }

    [Column("ISO3")]
    [StringLength(3)]
    [Unicode(false)]
    public string? Iso3 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Name { get; set; }

    [StringLength(50)]
    public string? NativeName { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? Culture { get; set; }

    [Column("IsRTL")]
    public bool IsRtl { get; set; }

    [Column("LanguageISOCode")]
    [StringLength(2)]
    [Unicode(false)]
    public string LanguageIsocode { get; set; } = null!;

    [InverseProperty("DefaultLanguageIsocodeNavigation")]
    public virtual ICollection<MerchantSetShop> MerchantSetShops { get; set; } = new List<MerchantSetShop>();

    [InverseProperty("Language")]
    public virtual ICollection<MerchantSetText> MerchantSetTexts { get; set; } = new List<MerchantSetText>();

    [InverseProperty("LanguageIsocodeNavigation")]
    public virtual ICollection<ProductText> ProductTexts { get; set; } = new List<ProductText>();

    [InverseProperty("Language")]
    public virtual ICollection<Qnagroup> Qnagroups { get; set; } = new List<Qnagroup>();

    [InverseProperty("MerchantTextDefaultLanguageNavigation")]
    public virtual ICollection<TblCompanySettingsHosted> TblCompanySettingsHosteds { get; set; } = new List<TblCompanySettingsHosted>();
}
