using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("ProductPropertyType", Schema = "List")]
public partial class ProductPropertyType
{
    [Key]
    [Column("ProductPropertyType_id")]
    public byte ProductPropertyTypeId { get; set; }

    [StringLength(20)]
    public string? Name { get; set; }

    [InverseProperty("ProductPropertyType")]
    public virtual ICollection<ProductProperty> ProductProperties { get; set; } = new List<ProductProperty>();
}
