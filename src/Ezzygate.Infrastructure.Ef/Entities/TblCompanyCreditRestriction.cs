using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCompanyCreditRestrictions")]
public partial class TblCompanyCreditRestriction
{
    [Key]
    [Column("CCR_ID")]
    public int CcrId { get; set; }

    [Column("CCR_ParentID")]
    public int CcrParentId { get; set; }

    [Column("CCR_CPM_ID")]
    public int CcrCpmId { get; set; }

    [Column("CCR_Order")]
    public byte CcrOrder { get; set; }

    [Column("CCR_Type")]
    public byte CcrType { get; set; }

    [Column("CCR_ChildType")]
    public byte CcrChildType { get; set; }

    [Column("CCR_ListValue")]
    [StringLength(255)]
    public string CcrListValue { get; set; } = null!;

    [Column("CCR_Ratio")]
    public int CcrRatio { get; set; }

    [Column("CCR_UseCount")]
    public int CcrUseCount { get; set; }

    [Column("CCR_ExchangeTo")]
    public byte CcrExchangeTo { get; set; }

    [Column("CCR_TerminalNumber")]
    [StringLength(20)]
    public string CcrTerminalNumber { get; set; } = null!;

    [Column("CCR_MaxAmount")]
    public int CcrMaxAmount { get; set; }

    [Column("CCR_PercentFee", TypeName = "smallmoney")]
    public decimal CcrPercentFee { get; set; }

    [Column("CCR_FixedFee", TypeName = "smallmoney")]
    public decimal CcrFixedFee { get; set; }

    [Column("CCR_ApproveFixedFee", TypeName = "smallmoney")]
    public decimal CcrApproveFixedFee { get; set; }

    [Column("CCR_RefundFixedFee", TypeName = "smallmoney")]
    public decimal CcrRefundFixedFee { get; set; }

    [Column("CCR_ClarificationFee", TypeName = "smallmoney")]
    public decimal CcrClarificationFee { get; set; }

    [Column("CCR_CBFixedFee", TypeName = "smallmoney")]
    public decimal CcrCbfixedFee { get; set; }

    [Column("CCR_FailFixedFee", TypeName = "smallmoney")]
    public decimal CcrFailFixedFee { get; set; }
}
