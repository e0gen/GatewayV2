using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("MerchantSetShop", Schema = "Setting")]
[Index("SubDomainName", Name = "UIX_MerchantSetShop_SubDomainName", IsUnique = true)]
public partial class MerchantSetShop
{
    [Key]
    [Column("MerchantSetShop_id")]
    public int MerchantSetShopId { get; set; }

    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    public bool IsEnabled { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? SubDomainName { get; set; }

    [Column("UIBaseColor")]
    [StringLength(7)]
    [Unicode(false)]
    public string? UibaseColor { get; set; }

    [StringLength(30)]
    public string? LogoFileName { get; set; }

    [StringLength(30)]
    public string? BannerFileName { get; set; }

    [StringLength(150)]
    public string? BannerLinkUrl { get; set; }

    [Column("URL_Facebook")]
    [StringLength(150)]
    public string? UrlFacebook { get; set; }

    [Column("URL_Twitter")]
    [StringLength(150)]
    public string? UrlTwitter { get; set; }

    [Column("URL_Vimeo")]
    [StringLength(150)]
    public string? UrlVimeo { get; set; }

    [Column("URL_Youtube")]
    [StringLength(150)]
    public string? UrlYoutube { get; set; }

    [Column("URL_Linkedin")]
    [StringLength(150)]
    public string? UrlLinkedin { get; set; }

    [Column("URL_Pinterest")]
    [StringLength(150)]
    public string? UrlPinterest { get; set; }

    [Column("CurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string CurrencyIsocode { get; set; } = null!;

    [StringLength(300)]
    public string? StreetAddress { get; set; }

    [StringLength(50)]
    public string? ContactEmail { get; set; }

    [StringLength(25)]
    public string? ContactPhone { get; set; }

    [Column("DefaultLanguageISOCode")]
    [StringLength(2)]
    [Unicode(false)]
    public string? DefaultLanguageIsocode { get; set; }

    [Column("URL_Instagram")]
    [StringLength(150)]
    public string? UrlInstagram { get; set; }

    [Column("DiscusID")]
    [StringLength(50)]
    public string? DiscusId { get; set; }

    [StringLength(30)]
    public string? CoverFileName { get; set; }

    [ForeignKey("CurrencyIsocode")]
    [InverseProperty("MerchantSetShops")]
    public virtual CurrencyList CurrencyIsocodeNavigation { get; set; } = null!;

    [ForeignKey("DefaultLanguageIsocode")]
    [InverseProperty("MerchantSetShops")]
    public virtual LanguageList? DefaultLanguageIsocodeNavigation { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("MerchantSetShops")]
    public virtual TblCompany Merchant { get; set; } = null!;

    [InverseProperty("MerchantSetShop")]
    public virtual ICollection<MerchantSetShopInstallment> MerchantSetShopInstallments { get; set; } = new List<MerchantSetShopInstallment>();

    [InverseProperty("MerchantSetShop")]
    public virtual ICollection<MerchantSetShopToCountryRegion> MerchantSetShopToCountryRegions { get; set; } = new List<MerchantSetShopToCountryRegion>();

    [InverseProperty("MerchantSetShop")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
