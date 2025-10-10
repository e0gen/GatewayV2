using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("ShippingDetail", Schema = "Data")]
public partial class ShippingDetail
{
    [Key]
    [Column("ShippingDetail_id")]
    public int ShippingDetailId { get; set; }

    [Column("TableList_id")]
    public byte TableListId { get; set; }

    [StringLength(100)]
    public string? AddressLine1 { get; set; }

    [StringLength(100)]
    public string? AddressLine2 { get; set; }

    [StringLength(60)]
    public string? AddressCity { get; set; }

    [StringLength(15)]
    public string? AddressPostalCode { get; set; }

    [Column("AddressStateISOCode")]
    [StringLength(2)]
    [Unicode(false)]
    public string? AddressStateIsocode { get; set; }

    [Column("AddressCountryISOCode")]
    [StringLength(2)]
    [Unicode(false)]
    public string? AddressCountryIsocode { get; set; }

    [StringLength(50)]
    public string? Title { get; set; }

    [ForeignKey("AddressCountryIsocode")]
    [InverseProperty("ShippingDetails")]
    public virtual CountryList? AddressCountryIsocodeNavigation { get; set; }

    [ForeignKey("AddressStateIsocode")]
    [InverseProperty("ShippingDetails")]
    public virtual StateList? AddressStateIsocodeNavigation { get; set; }

    [ForeignKey("TableListId")]
    [InverseProperty("ShippingDetails")]
    public virtual TableList TableList { get; set; } = null!;
}
