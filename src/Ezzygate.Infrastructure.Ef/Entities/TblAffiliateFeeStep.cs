using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblAffiliateFeeSteps")]
public partial class TblAffiliateFeeStep
{
    [Key]
    [Column("AFS_ID")]
    public int AfsId { get; set; }

    [Column("AFS_AffiliateID")]
    public int? AfsAffiliateId { get; set; }

    [Column("AFS_CompanyID")]
    public int? AfsCompanyId { get; set; }

    [Column("AFS_UpToAmount", TypeName = "money")]
    public decimal AfsUpToAmount { get; set; }

    [Column("AFS_Currency")]
    public int AfsCurrency { get; set; }

    [Column("AFS_PaymentMethod")]
    public short? AfsPaymentMethod { get; set; }

    [Column("AFS_SlicePercent", TypeName = "money")]
    public decimal? AfsSlicePercent { get; set; }

    [Column("AFS_AFPID")]
    public int? AfsAfpid { get; set; }

    [Column("AFS_PercentFee", TypeName = "smallmoney")]
    public decimal AfsPercentFee { get; set; }

    [Column("AFS_CalcMethod")]
    public byte AfsCalcMethod { get; set; }

    [Column("AFS_TransType")]
    public byte AfsTransType { get; set; }

    [ForeignKey("AfsAffiliateId")]
    [InverseProperty("TblAffiliateFeeSteps")]
    public virtual TblAffiliate? AfsAffiliate { get; set; }

    [ForeignKey("AfsAfpid")]
    [InverseProperty("TblAffiliateFeeSteps")]
    public virtual TblAffiliatePayment? AfsAfp { get; set; }

    [ForeignKey("AfsCompanyId")]
    [InverseProperty("TblAffiliateFeeSteps")]
    public virtual TblCompany? AfsCompany { get; set; }

    [ForeignKey("AfsCurrency")]
    [InverseProperty("TblAffiliateFeeSteps")]
    public virtual TblSystemCurrency AfsCurrencyNavigation { get; set; } = null!;
}
