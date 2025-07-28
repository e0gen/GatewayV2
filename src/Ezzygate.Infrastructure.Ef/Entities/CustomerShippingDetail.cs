using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("CustomerShippingDetail", Schema = "Data")]
public partial class CustomerShippingDetail
{
    [Key]
    [Column("CustomerShippingDetail_id")]
    public int CustomerShippingDetailId { get; set; }

    [Column("Customer_id")]
    public int CustomerId { get; set; }

    [Column("AccountAddress_id")]
    public int? AccountAddressId { get; set; }

    public bool IsDefault { get; set; }

    [StringLength(50)]
    public string? Title { get; set; }

    [StringLength(250)]
    public string? Comment { get; set; }

    [ForeignKey("AccountAddressId")]
    [InverseProperty("CustomerShippingDetails")]
    public virtual AccountAddress? AccountAddress { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("CustomerShippingDetails")]
    public virtual Customer Customer { get; set; } = null!;
}
