using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCompanyCreditFees")]
[Index("CcfCompanyId", Name = "IX_tblCompanyCreditFees_CCF_CompanyID")]
[Index("CcfCreditTypeId", Name = "IX_tblCompanyCreditFees_CCF_CreditTypeID")]
[Index("CcfCurrencyId", Name = "IX_tblCompanyCreditFees_CCF_CurrencyID")]
public partial class TblCompanyCreditFee
{
    [Key]
    [Column("CCF_ID")]
    public int CcfId { get; set; }

    [Column("CCF_CompanyID")]
    public int CcfCompanyId { get; set; }

    [Column("CCF_CurrencyID")]
    public int CcfCurrencyId { get; set; }

    [Column("CCF_PaymentMethod")]
    public short? CcfPaymentMethod { get; set; }

    [Column("CCF_CreditTypeID")]
    public int CcfCreditTypeId { get; set; }

    [Column("CCF_IsDisabled")]
    public bool CcfIsDisabled { get; set; }

    [Column("CCF_ListBINs")]
    [StringLength(255)]
    public string CcfListBins { get; set; } = null!;

    [Column("CCF_ExchangeTo")]
    public short CcfExchangeTo { get; set; }

    [Column("CCF_TerminalNumber")]
    [StringLength(20)]
    public string CcfTerminalNumber { get; set; } = null!;

    [Column("CCF_MaxAmount", TypeName = "money")]
    public decimal CcfMaxAmount { get; set; }

    [Column("CCF_PercentFee", TypeName = "smallmoney")]
    public decimal CcfPercentFee { get; set; }

    [Column("CCF_FixedFee", TypeName = "smallmoney")]
    public decimal CcfFixedFee { get; set; }

    [Column("CCF_ApproveFixedFee", TypeName = "smallmoney")]
    public decimal CcfApproveFixedFee { get; set; }

    [Column("CCF_RefundFixedFee", TypeName = "smallmoney")]
    public decimal CcfRefundFixedFee { get; set; }

    [Column("CCF_ClarificationFee", TypeName = "smallmoney")]
    public decimal CcfClarificationFee { get; set; }

    [Column("CCF_CBFixedFee", TypeName = "smallmoney")]
    public decimal CcfCbfixedFee { get; set; }

    [Column("CCF_FailFixedFee", TypeName = "smallmoney")]
    public decimal CcfFailFixedFee { get; set; }

    [Column("CCF_TSelMode")]
    public byte CcfTselMode { get; set; }

    [Column("CCF_PartialRefundFixedFee", TypeName = "smallmoney")]
    public decimal CcfPartialRefundFixedFee { get; set; }

    [Column("CCF_FraudRefundFixedFee", TypeName = "smallmoney")]
    public decimal CcfFraudRefundFixedFee { get; set; }

    [Column("CCF_PercentCashback", TypeName = "decimal(4, 2)")]
    public decimal CcfPercentCashback { get; set; }

    [Column("CurrencyRateType_id")]
    [StringLength(20)]
    [Unicode(false)]
    public string? CurrencyRateTypeId { get; set; }

    [ForeignKey("CcfCompanyId")]
    [InverseProperty("TblCompanyCreditFees")]
    public virtual TblCompany CcfCompany { get; set; } = null!;

    [ForeignKey("CcfCurrencyId")]
    [InverseProperty("TblCompanyCreditFees")]
    public virtual TblSystemCurrency CcfCurrency { get; set; } = null!;

    [ForeignKey("CcfPaymentMethod")]
    [InverseProperty("TblCompanyCreditFees")]
    public virtual PaymentMethod? CcfPaymentMethodNavigation { get; set; }

    [ForeignKey("CurrencyRateTypeId")]
    [InverseProperty("TblCompanyCreditFees")]
    public virtual CurrencyRateType? CurrencyRateType { get; set; }

    [InverseProperty("CcftCcf")]
    public virtual ICollection<TblCompanyCreditFeesTerminal> TblCompanyCreditFeesTerminals { get; set; } = new List<TblCompanyCreditFeesTerminal>();
}
