using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("ProductType", Schema = "List")]
public partial class ProductType
{
    [Key]
    [Column("ProductType_id")]
    public byte ProductTypeId { get; set; }

    [StringLength(20)]
    public string? Name { get; set; }

    [InverseProperty("ProductType")]
    public virtual ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

    [InverseProperty("ProductType")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
