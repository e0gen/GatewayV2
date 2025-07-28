using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblTransactionPayFees")]
[Index("CcfCompanyId", Name = "IX_tblTransactionPayFees_CCF_CompanyID")]
[Index("CcfTransactionPayId", Name = "IX_tblTransactionPayFees_CCF_TransactionPayID")]
public partial class TblTransactionPayFee
{
    [Key]
    [Column("CCF_ID")]
    public int CcfId { get; set; }

    [Column("CCF_TransactionPayID")]
    public int CcfTransactionPayId { get; set; }

    [Column("CCF_CompanyID")]
    public int CcfCompanyId { get; set; }

    [Column("CCF_CurrencyID")]
    public int CcfCurrencyId { get; set; }

    [Column("CCF_PaymentMethod")]
    public short CcfPaymentMethod { get; set; }

    [Column("CCF_CreditTypeID")]
    public int CcfCreditTypeId { get; set; }

    [Column("CCF_ListBINs")]
    [StringLength(255)]
    public string CcfListBins { get; set; } = null!;

    [Column("CCF_ExchangeTo")]
    public short CcfExchangeTo { get; set; }

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
}
