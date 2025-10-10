using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("Cart", Schema = "Data")]
public partial class Cart
{
    [Key]
    [Column("Cart_id")]
    public int CartId { get; set; }

    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    [Column("Customer_id")]
    public int? CustomerId { get; set; }

    [Column("TransPass_id")]
    public int? TransPassId { get; set; }

    [Column("TransPreAuth_id")]
    public int? TransPreAuthId { get; set; }

    [Column("TransPending_id")]
    public int? TransPendingId { get; set; }

    [Precision(0)]
    public DateTime StartDate { get; set; }

    [Precision(0)]
    public DateTime? CheckoutDate { get; set; }

    [Column("CurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string CurrencyIsocode { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalProducts { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalShipping { get; set; }

    [Column(TypeName = "decimal(11, 2)")]
    public decimal? Total { get; set; }

    public byte Installments { get; set; }

    [StringLength(30)]
    public string? RecepientName { get; set; }

    [StringLength(30)]
    public string? RecepientPhone { get; set; }

    [StringLength(50)]
    public string? RecepientMail { get; set; }

    [StringLength(200)]
    public string? Comment { get; set; }

    [StringLength(20)]
    public string? ReferenceNumber { get; set; }

    public Guid Identifier { get; set; }

    [Column("UISetting")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? Uisetting { get; set; }

    [Column("MerchantSetShop_id")]
    public int? MerchantSetShopId { get; set; }

    [InverseProperty("Cart")]
    public virtual ICollection<CartFailLog> CartFailLogs { get; set; } = new List<CartFailLog>();

    [InverseProperty("Cart")]
    public virtual ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

    [ForeignKey("CurrencyIsocode")]
    [InverseProperty("Carts")]
    public virtual CurrencyList CurrencyIsocodeNavigation { get; set; } = null!;

    [ForeignKey("CustomerId")]
    [InverseProperty("Carts")]
    public virtual Customer? Customer { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("Carts")]
    public virtual TblCompany Merchant { get; set; } = null!;

    [ForeignKey("TransPassId")]
    [InverseProperty("Carts")]
    public virtual TblCompanyTransPass? TransPass { get; set; }

    [ForeignKey("TransPendingId")]
    [InverseProperty("Carts")]
    public virtual TblCompanyTransPending? TransPending { get; set; }

    [ForeignKey("TransPreAuthId")]
    [InverseProperty("Carts")]
    public virtual TblCompanyTransApproval? TransPreAuth { get; set; }
}
