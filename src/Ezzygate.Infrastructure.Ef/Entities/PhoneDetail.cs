using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("PhoneDetail", Schema = "Trans")]
public partial class PhoneDetail
{
    [Key]
    [Column("PhoneDetail_id")]
    public int PhoneDetailId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string PhoneNumber { get; set; } = null!;

    [StringLength(50)]
    public string? FullName { get; set; }

    [StringLength(250)]
    public string? Email { get; set; }

    [Column("BillingAddress_id")]
    public int? BillingAddressId { get; set; }

    [Column("Merchant_id")]
    public int? MerchantId { get; set; }

    [Column("PhoneCarrier_id")]
    public short? PhoneCarrierId { get; set; }

    [StringLength(50)]
    public string? InvoiceName { get; set; }

    [ForeignKey("BillingAddressId")]
    [InverseProperty("PhoneDetails")]
    public virtual TblBillingAddress? BillingAddress { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("PhoneDetails")]
    public virtual TblCompany? Merchant { get; set; }

    [ForeignKey("PhoneCarrierId")]
    [InverseProperty("PhoneDetails")]
    public virtual PhoneCarrier? PhoneCarrier { get; set; }

    [InverseProperty("PhoneDetails")]
    public virtual ICollection<TblCompanyTransApproval> TblCompanyTransApprovals { get; set; } = new List<TblCompanyTransApproval>();

    [InverseProperty("PhoneDetails")]
    public virtual ICollection<TblCompanyTransFail> TblCompanyTransFails { get; set; } = new List<TblCompanyTransFail>();

    [InverseProperty("PhoneDetails")]
    public virtual ICollection<TblCompanyTransPass> TblCompanyTransPasses { get; set; } = new List<TblCompanyTransPass>();

    [InverseProperty("PhoneDetails")]
    public virtual ICollection<TblCompanyTransPending> TblCompanyTransPendings { get; set; } = new List<TblCompanyTransPending>();
}
