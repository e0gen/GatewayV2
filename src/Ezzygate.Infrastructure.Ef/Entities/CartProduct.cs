using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("CartProduct", Schema = "Data")]
public partial class CartProduct
{
    [Key]
    [Column("CartProduct_id")]
    public int CartProductId { get; set; }

    [Column("Cart_id")]
    public int CartId { get; set; }

    [Column("Product_id")]
    public int? ProductId { get; set; }

    [Column("ProductType_id")]
    public byte ProductTypeId { get; set; }

    [Column("ProductStock_id")]
    public int? ProductStockId { get; set; }

    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    [Precision(0)]
    public DateTime InsertDate { get; set; }

    [StringLength(200)]
    public string Name { get; set; } = null!;

    public short Quantity { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(8, 2)")]
    public decimal ShippingFee { get; set; }

    [Column("VATPercent", TypeName = "decimal(5, 2)")]
    public decimal Vatpercent { get; set; }

    [Column("CurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string CurrencyIsocode { get; set; } = null!;

    [Column("CurrencyFXRate", TypeName = "decimal(8, 5)")]
    public decimal CurrencyFxrate { get; set; }

    [Column(TypeName = "decimal(23, 4)")]
    public decimal? TotalProduct { get; set; }

    [Column(TypeName = "decimal(21, 4)")]
    public decimal? TotalShipping { get; set; }

    [Column(TypeName = "decimal(24, 4)")]
    public decimal? Total { get; set; }

    [Column("UISetting")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Uisetting { get; set; }

    [ForeignKey("CartId")]
    [InverseProperty("CartProducts")]
    public virtual Cart Cart { get; set; } = null!;

    [InverseProperty("CartProduct")]
    public virtual ICollection<CartProductProperty> CartProductProperties { get; set; } = new List<CartProductProperty>();

    [ForeignKey("CurrencyIsocode")]
    [InverseProperty("CartProducts")]
    public virtual CurrencyList CurrencyIsocodeNavigation { get; set; } = null!;

    [ForeignKey("MerchantId")]
    [InverseProperty("CartProducts")]
    public virtual TblCompany Merchant { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("CartProducts")]
    public virtual Product? Product { get; set; }

    [ForeignKey("ProductStockId")]
    [InverseProperty("CartProducts")]
    public virtual ProductStock? ProductStock { get; set; }

    [ForeignKey("ProductTypeId")]
    [InverseProperty("CartProducts")]
    public virtual ProductType ProductType { get; set; } = null!;
}
