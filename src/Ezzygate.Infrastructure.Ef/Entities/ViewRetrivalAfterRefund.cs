using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewRetrivalAfterRefund
{
    public byte? DebitCompany { get; set; }

    [Column("dt_ContractNumber")]
    [StringLength(50)]
    public string? DtContractNumber { get; set; }

    [Column("dt_BankExternalID")]
    [StringLength(30)]
    public string? DtBankExternalId { get; set; }

    [Column("TRM_ID")]
    public int? TrmId { get; set; }

    [Column("TRM_Number")]
    [StringLength(20)]
    public string? TrmNumber { get; set; }

    [Column("RR_BankCaseID")]
    [StringLength(30)]
    public string? RrBankCaseId { get; set; }

    [StringLength(66)]
    public string? CardDispaly { get; set; }

    [StringLength(100)]
    public string? CardOwner { get; set; }

    [Column("ORG_ID")]
    public int? OrgId { get; set; }

    [Column("ORG_Date", TypeName = "datetime")]
    public DateTime? OrgDate { get; set; }

    [Column("ORG_Approval")]
    [StringLength(50)]
    public string? OrgApproval { get; set; }

    [Column("ORG_Amount", TypeName = "money")]
    public decimal? OrgAmount { get; set; }

    [Column("ORG_Currency")]
    public int? OrgCurrency { get; set; }

    [Column("ORG_RefCode")]
    [StringLength(40)]
    public string? OrgRefCode { get; set; }

    [Column("REF_ID")]
    public int? RefId { get; set; }

    [Column("REF_Date", TypeName = "datetime")]
    public DateTime? RefDate { get; set; }

    [Column("REF_Approval")]
    [StringLength(50)]
    public string? RefApproval { get; set; }

    [Column("REF_Amount", TypeName = "money")]
    public decimal? RefAmount { get; set; }

    [Column("REF_Currency")]
    public int? RefCurrency { get; set; }

    [Column("REF_RefCode")]
    [StringLength(40)]
    public string? RefRefCode { get; set; }

    [Column("CompanyID")]
    public int? CompanyId { get; set; }

    [Column("CMP_Url")]
    [StringLength(500)]
    public string? CmpUrl { get; set; }

    [Column("CMP_Support")]
    [StringLength(101)]
    public string? CmpSupport { get; set; }
}
