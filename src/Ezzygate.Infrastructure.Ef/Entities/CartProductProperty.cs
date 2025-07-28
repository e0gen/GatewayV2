using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("CartProductProperty", Schema = "Data")]
public partial class CartProductProperty
{
    [Key]
    [Column("CartProductProperty_id")]
    public int CartProductPropertyId { get; set; }

    [Column("CartProduct_id")]
    public int CartProductId { get; set; }

    [Column("ProductProperty_id")]
    public int? ProductPropertyId { get; set; }

    [StringLength(50)]
    public string PropertyName { get; set; } = null!;

    [StringLength(50)]
    public string PropertyValue { get; set; } = null!;

    [ForeignKey("CartProductId")]
    [InverseProperty("CartProductProperties")]
    public virtual CartProduct CartProduct { get; set; } = null!;

    [ForeignKey("ProductPropertyId")]
    [InverseProperty("CartProductProperties")]
    public virtual ProductProperty? ProductProperty { get; set; }
}
