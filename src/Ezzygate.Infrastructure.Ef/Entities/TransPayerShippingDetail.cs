using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("TransPayerShippingDetail", Schema = "Trans")]
public partial class TransPayerShippingDetail
{
    [Key]
    [Column("TransPayerShippingDetail_id")]
    public int TransPayerShippingDetailId { get; set; }

    [StringLength(50)]
    public string? Title { get; set; }

    [StringLength(100)]
    public string? Street1 { get; set; }

    [StringLength(100)]
    public string? Street2 { get; set; }

    [StringLength(60)]
    public string? City { get; set; }

    [StringLength(15)]
    public string? PostalCode { get; set; }

    [Column("StateISOCode")]
    [StringLength(2)]
    [Unicode(false)]
    public string? StateIsocode { get; set; }

    [Column("CountryISOCode")]
    [StringLength(2)]
    [Unicode(false)]
    public string? CountryIsocode { get; set; }

    [ForeignKey("CountryIsocode")]
    [InverseProperty("TransPayerShippingDetails")]
    public virtual CountryList? CountryIsocodeNavigation { get; set; }

    [ForeignKey("StateIsocode")]
    [InverseProperty("TransPayerShippingDetails")]
    public virtual StateList? StateIsocodeNavigation { get; set; }

    [InverseProperty("TransPayerShippingDetail")]
    public virtual ICollection<TransPayerInfo> TransPayerInfos { get; set; } = new List<TransPayerInfo>();
}
