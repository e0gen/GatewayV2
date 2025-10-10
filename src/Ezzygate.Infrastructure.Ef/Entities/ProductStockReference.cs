using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[PrimaryKey("ProductStockId", "ProductId", "ProductPropertyId")]
[Table("ProductStockReference", Schema = "Data")]
public partial class ProductStockReference
{
    [Key]
    [Column("ProductStock_id")]
    public int ProductStockId { get; set; }

    [Key]
    [Column("Product_id")]
    public int ProductId { get; set; }

    [Key]
    [Column("ProductProperty_id")]
    public int ProductPropertyId { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductStockReferences")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("ProductPropertyId")]
    [InverseProperty("ProductStockReferences")]
    public virtual ProductProperty ProductProperty { get; set; } = null!;

    [ForeignKey("ProductStockId")]
    [InverseProperty("ProductStockReferences")]
    public virtual ProductStock ProductStock { get; set; } = null!;
}
