using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("MerchantSetShopToCountryRegion", Schema = "Setting")]
public partial class MerchantSetShopToCountryRegion
{
    [Key]
    [Column("MerchantSetShopToCountryRegion_id")]
    public int MerchantSetShopToCountryRegionId { get; set; }

    [Column("MerchantSetShop_id")]
    public int MerchantSetShopId { get; set; }

    [Column("CountryISOCode")]
    [StringLength(2)]
    [Unicode(false)]
    public string? CountryIsocode { get; set; }

    [Column("WorldRegionISOCode")]
    [StringLength(2)]
    [Unicode(false)]
    public string? WorldRegionIsocode { get; set; }

    [ForeignKey("CountryIsocode")]
    [InverseProperty("MerchantSetShopToCountryRegions")]
    public virtual CountryList? CountryIsocodeNavigation { get; set; }

    [ForeignKey("MerchantSetShopId")]
    [InverseProperty("MerchantSetShopToCountryRegions")]
    public virtual MerchantSetShop MerchantSetShop { get; set; } = null!;

    [ForeignKey("WorldRegionIsocode")]
    [InverseProperty("MerchantSetShopToCountryRegions")]
    public virtual WorldRegion? WorldRegionIsocodeNavigation { get; set; }
}
