using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCompanyPaymentMethods")]
public partial class TblCompanyPaymentMethod
{
    [Key]
    [Column("CPM_ID")]
    public int CpmId { get; set; }

    [Column("CPM_CreditTypeID")]
    public byte CpmCreditTypeId { get; set; }

    [Column("CPM_IsDisabled")]
    public bool CpmIsDisabled { get; set; }

    [Column("CPM_PercentFee", TypeName = "smallmoney")]
    public decimal CpmPercentFee { get; set; }

    [Column("CPM_FixedFee", TypeName = "smallmoney")]
    public decimal CpmFixedFee { get; set; }

    [Column("CPM_ApproveFixedFee", TypeName = "smallmoney")]
    public decimal CpmApproveFixedFee { get; set; }

    [Column("CPM_RefundFixedFee", TypeName = "smallmoney")]
    public decimal CpmRefundFixedFee { get; set; }

    [Column("CPM_ClarificationFee", TypeName = "smallmoney")]
    public decimal CpmClarificationFee { get; set; }

    [Column("CPM_CBFixedFee", TypeName = "smallmoney")]
    public decimal CpmCbfixedFee { get; set; }

    [Column("CPM_FailFixedFee", TypeName = "smallmoney")]
    public decimal CpmFailFixedFee { get; set; }

    [Column("CPM_CompanyID")]
    public int? CpmCompanyId { get; set; }

    [Column("CPM_CurrencyID")]
    public int? CpmCurrencyId { get; set; }

    [Column("CPM_PaymentMethod")]
    public short? CpmPaymentMethod { get; set; }

    [ForeignKey("CpmCompanyId")]
    [InverseProperty("TblCompanyPaymentMethods")]
    public virtual TblCompany? CpmCompany { get; set; }

    [ForeignKey("CpmCurrencyId")]
    [InverseProperty("TblCompanyPaymentMethods")]
    public virtual TblSystemCurrency? CpmCurrency { get; set; }

    [ForeignKey("CpmPaymentMethod")]
    [InverseProperty("TblCompanyPaymentMethods")]
    public virtual PaymentMethod? CpmPaymentMethodNavigation { get; set; }
}
