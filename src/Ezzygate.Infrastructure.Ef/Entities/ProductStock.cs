using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("ProductStock", Schema = "Data")]
public partial class ProductStock
{
    [Key]
    [Column("ProductStock_id")]
    public int ProductStockId { get; set; }

    [Column("Product_id")]
    public int ProductId { get; set; }

    [Column("SKU")]
    [StringLength(20)]
    public string? Sku { get; set; }

    public short? QtyAvailable { get; set; }

    [InverseProperty("ProductStock")]
    public virtual ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

    [ForeignKey("ProductId")]
    [InverseProperty("ProductStocks")]
    public virtual Product Product { get; set; } = null!;

    [InverseProperty("ProductStock")]
    public virtual ICollection<ProductStockReference> ProductStockReferences { get; set; } = new List<ProductStockReference>();
}
