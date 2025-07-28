using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("ProductCategory", Schema = "List")]
public partial class ProductCategory
{
    [Key]
    [Column("ProductCategory_id")]
    public short ProductCategoryId { get; set; }

    [Column("ParentID")]
    public short? ParentId { get; set; }

    [StringLength(20)]
    public string? Name { get; set; }

    public byte? SortOrder { get; set; }

    [InverseProperty("Parent")]
    public virtual ICollection<ProductCategory> InverseParent { get; set; } = new List<ProductCategory>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual ProductCategory? Parent { get; set; }

    [ForeignKey("ProductCategoryId")]
    [InverseProperty("ProductCategories")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
