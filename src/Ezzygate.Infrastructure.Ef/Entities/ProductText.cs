using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[PrimaryKey("ProductId", "LanguageIsocode")]
[Table("ProductText", Schema = "Data")]
public partial class ProductText
{
    [Key]
    [Column("Product_id")]
    public int ProductId { get; set; }

    [Key]
    [Column("LanguageISOCode")]
    [StringLength(2)]
    [Unicode(false)]
    public string LanguageIsocode { get; set; } = null!;

    public bool IsDefaultLanguage { get; set; }

    [StringLength(200)]
    public string Name { get; set; } = null!;

    [StringLength(1000)]
    public string? Description { get; set; }

    [StringLength(600)]
    public string? ReceiptText { get; set; }

    [StringLength(600)]
    public string? ReceiptLink { get; set; }

    [StringLength(100)]
    public string? MetaTitle { get; set; }

    [StringLength(250)]
    public string? MetaDescription { get; set; }

    [StringLength(250)]
    public string? MetaKeyword { get; set; }

    [ForeignKey("LanguageIsocode")]
    [InverseProperty("ProductTexts")]
    public virtual LanguageList LanguageIsocodeNavigation { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("ProductTexts")]
    public virtual Product Product { get; set; } = null!;
}
