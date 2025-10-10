using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("ProductProperty", Schema = "Data")]
public partial class ProductProperty
{
    [Key]
    [Column("ProductProperty_id")]
    public int ProductPropertyId { get; set; }

    [Column("ParentID")]
    public int? ParentId { get; set; }

    [Column("Merchant_id")]
    public int? MerchantId { get; set; }

    [Column("ProductPropertyType_id")]
    public byte ProductPropertyTypeId { get; set; }

    [StringLength(20)]
    public string? Name { get; set; }

    [StringLength(20)]
    public string? Value { get; set; }

    [InverseProperty("ProductProperty")]
    public virtual ICollection<CartProductProperty> CartProductProperties { get; set; } = new List<CartProductProperty>();

    [InverseProperty("Parent")]
    public virtual ICollection<ProductProperty> InverseParent { get; set; } = new List<ProductProperty>();

    [ForeignKey("MerchantId")]
    [InverseProperty("ProductProperties")]
    public virtual TblCompany? Merchant { get; set; }

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual ProductProperty? Parent { get; set; }

    [ForeignKey("ProductPropertyTypeId")]
    [InverseProperty("ProductProperties")]
    public virtual ProductPropertyType ProductPropertyType { get; set; } = null!;

    [InverseProperty("ProductProperty")]
    public virtual ICollection<ProductStockReference> ProductStockReferences { get; set; } = new List<ProductStockReference>();
}
