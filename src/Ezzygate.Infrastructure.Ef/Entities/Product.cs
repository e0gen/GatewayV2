using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("Product", Schema = "Data")]
public partial class Product
{
    [Key]
    [Column("Product_id")]
    public int ProductId { get; set; }

    [Column("ProductType_id")]
    public byte ProductTypeId { get; set; }

    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    [Column("CurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string CurrencyIsocode { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    public byte TransType { get; set; }

    public byte Installments { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? RecurringString { get; set; }

    public short QtyStart { get; set; }

    public short QtyEnd { get; set; }

    public short QtyStep { get; set; }

    public short? QtyAvailable { get; set; }

    [Column("SKU")]
    [StringLength(20)]
    public string? Sku { get; set; }

    public byte? SortOrder { get; set; }

    [Column("isActive")]
    public bool IsActive { get; set; }

    [Column("isDynamicPrice")]
    public bool IsDynamicPrice { get; set; }

    [StringLength(80)]
    public string? ImageFileName { get; set; }

    [StringLength(80)]
    public string? DownloadFileName { get; set; }

    [Column("OLDProductID")]
    public int? OldproductId { get; set; }

    [Column("MerchantSetShop_id")]
    public int MerchantSetShopId { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

    [ForeignKey("CurrencyIsocode")]
    [InverseProperty("Products")]
    public virtual CurrencyList CurrencyIsocodeNavigation { get; set; } = null!;

    [ForeignKey("MerchantId")]
    [InverseProperty("Products")]
    public virtual TblCompany Merchant { get; set; } = null!;

    [ForeignKey("MerchantSetShopId")]
    [InverseProperty("Products")]
    public virtual MerchantSetShop MerchantSetShop { get; set; } = null!;

    [InverseProperty("Product")]
    public virtual ICollection<ProductStockReference> ProductStockReferences { get; set; } = new List<ProductStockReference>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductStock> ProductStocks { get; set; } = new List<ProductStock>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductText> ProductTexts { get; set; } = new List<ProductText>();

    [ForeignKey("ProductTypeId")]
    [InverseProperty("Products")]
    public virtual ProductType ProductType { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("Products")]
    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
}
