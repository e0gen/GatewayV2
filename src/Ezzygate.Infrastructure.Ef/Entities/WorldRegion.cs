using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("WorldRegion", Schema = "List")]
public partial class WorldRegion
{
    [Key]
    [Column("WorldRegionISOCode")]
    [StringLength(2)]
    [Unicode(false)]
    public string WorldRegionIsocode { get; set; } = null!;

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("WorldRegionIsocodeNavigation")]
    public virtual ICollection<CountryList> CountryLists { get; set; } = new List<CountryList>();

    [InverseProperty("WorldRegionIsocodeNavigation")]
    public virtual ICollection<MerchantSetShopToCountryRegion> MerchantSetShopToCountryRegions { get; set; } = new List<MerchantSetShopToCountryRegion>();
}
