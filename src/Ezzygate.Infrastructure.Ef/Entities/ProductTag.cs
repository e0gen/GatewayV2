using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("ProductTag", Schema = "Data")]
public partial class ProductTag
{
    [Key]
    [Column("ProductTag_id")]
    public int ProductTagId { get; set; }

    [Column("Product_id")]
    public int ProductId { get; set; }

    [StringLength(50)]
    public string Tag { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("ProductTags")]
    public virtual Product Product { get; set; } = null!;
}
