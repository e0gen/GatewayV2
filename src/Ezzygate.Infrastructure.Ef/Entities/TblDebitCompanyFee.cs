using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblDebitCompanyFees")]
public partial class TblDebitCompanyFee
{
    [Key]
    [Column("DCF_ID")]
    public int DcfId { get; set; }

    [Column("DCF_DebitCompanyID")]
    public int DcfDebitCompanyId { get; set; }

    [Column("DCF_CurrencyID")]
    public byte DcfCurrencyId { get; set; }

    [Column("DCF_PaymentMethod")]
    public short DcfPaymentMethod { get; set; }

    [Column("DCF_TerminalNumber")]
    [StringLength(20)]
    public string DcfTerminalNumber { get; set; } = null!;

    [Column("DCF_FixedFee", TypeName = "smallmoney")]
    public decimal DcfFixedFee { get; set; }

    [Column("DCF_RefundFixedFee", TypeName = "smallmoney")]
    public decimal DcfRefundFixedFee { get; set; }

    [Column("DCF_PercentFee", TypeName = "smallmoney")]
    public decimal DcfPercentFee { get; set; }

    [Column("DCF_ApproveFixedFee", TypeName = "smallmoney")]
    public decimal DcfApproveFixedFee { get; set; }

    [Column("DCF_ClarificationFee", TypeName = "smallmoney")]
    public decimal DcfClarificationFee { get; set; }

    [Column("DCF_CBFixedFee", TypeName = "smallmoney")]
    public decimal DcfCbfixedFee { get; set; }

    [Column("DCF_FailFixedFee", TypeName = "smallmoney")]
    public decimal DcfFailFixedFee { get; set; }

    [Column("DCF_RegisterFee", TypeName = "smallmoney")]
    public decimal DcfRegisterFee { get; set; }

    [Column("DCF_YearFee", TypeName = "smallmoney")]
    public decimal DcfYearFee { get; set; }

    [Column("DCF_StatIncDays")]
    public byte DcfStatIncDays { get; set; }

    [Column("DCF_PayTransDays")]
    [StringLength(100)]
    [Unicode(false)]
    public string DcfPayTransDays { get; set; } = null!;

    [Column("DCF_PayINDays")]
    [StringLength(100)]
    [Unicode(false)]
    public string DcfPayIndays { get; set; } = null!;

    [Column("DCF_FixedCurrency")]
    public byte? DcfFixedCurrency { get; set; }

    [Column("DCF_CHBCurrency")]
    public byte? DcfChbcurrency { get; set; }

    [Column("DCF_MinPrecFee", TypeName = "smallmoney")]
    public decimal? DcfMinPrecFee { get; set; }

    [Column("DCF_MaxPrecFee", TypeName = "smallmoney")]
    public decimal? DcfMaxPrecFee { get; set; }
}
