using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("TransPayerInfo", Schema = "Trans")]
[Index("MerchantId", Name = "IX_TransPayerInfo_MerchantID")]
public partial class TransPayerInfo
{
    [Key]
    [Column("TransPayerInfo_id")]
    public int TransPayerInfoId { get; set; }

    [Column("TransPayerShippingDetail_id")]
    public int? TransPayerShippingDetailId { get; set; }

    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    [StringLength(50)]
    public string? InvoiceName { get; set; }

    [StringLength(50)]
    public string? PersonalNumber { get; set; }

    [StringLength(50)]
    public string? PhoneNumber { get; set; }

    [StringLength(80)]
    public string? EmailAddress { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    [StringLength(1500)]
    public string? Comment { get; set; }

    [Column("Echeck_id")]
    public int? EcheckId { get; set; }

    [Column("CreditCard_id")]
    public int? CreditCardId { get; set; }

    [Column("Merchant_id")]
    public int? MerchantId { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("TransPayerInfos")]
    public virtual TblCompany? Merchant { get; set; }

    [InverseProperty("TransPayerInfo")]
    public virtual ICollection<TblCompanyTransApproval> TblCompanyTransApprovals { get; set; } = new List<TblCompanyTransApproval>();

    [InverseProperty("TransPayerInfo")]
    public virtual ICollection<TblCompanyTransCrypto> TblCompanyTransCryptos { get; set; } = new List<TblCompanyTransCrypto>();

    [InverseProperty("TransPayerInfo")]
    public virtual ICollection<TblCompanyTransFail> TblCompanyTransFails { get; set; } = new List<TblCompanyTransFail>();

    [InverseProperty("TransPayerInfo")]
    public virtual ICollection<TblCompanyTransPass> TblCompanyTransPasses { get; set; } = new List<TblCompanyTransPass>();

    [InverseProperty("TransPayerInfo")]
    public virtual ICollection<TblCompanyTransPending> TblCompanyTransPendings { get; set; } = new List<TblCompanyTransPending>();

    [InverseProperty("TransPayerInfo")]
    public virtual ICollection<TblCompanyTransRemoved> TblCompanyTransRemoveds { get; set; } = new List<TblCompanyTransRemoved>();

    [InverseProperty("TransPayerInfo")]
    public virtual ICollection<TransPayerAdditionalInfo> TransPayerAdditionalInfos { get; set; } = new List<TransPayerAdditionalInfo>();

    [ForeignKey("TransPayerShippingDetailId")]
    [InverseProperty("TransPayerInfos")]
    public virtual TransPayerShippingDetail? TransPayerShippingDetail { get; set; }
}
