using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

/// <summary>
/// Account Files uploaded by admin users
/// </summary>
[Table("RiskRuleHistory", Schema = "Log")]
public partial class RiskRuleHistory
{
    [Key]
    [Column("RiskRuleHistory_id")]
    public int RiskRuleHistoryId { get; set; }

    [Precision(0)]
    public DateTime InsertDate { get; set; }

    [Column("Account_id")]
    public int? AccountId { get; set; }

    [StringLength(50)]
    public string? RuleName { get; set; }

    [StringLength(10)]
    public string? RuleCode { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? RuleDescription { get; set; }

    [StringLength(10)]
    public string? RuleMode { get; set; }

    [StringLength(50)]
    public string? Action { get; set; }

    [StringLength(1000)]
    public string? Reason { get; set; }

    public bool? IsRuleFail { get; set; }

    [Column("TransFail_id")]
    public int? TransFailId { get; set; }

    public byte? AdminAction { get; set; }

    [Column("TransPass_id")]
    public int? TransPassId { get; set; }

    [Column("TransPreAuth_id")]
    public int? TransPreAuthId { get; set; }

    [Column("TransPending_id")]
    public int? TransPendingId { get; set; }

    [StringLength(20)]
    public string? TerminalNumber { get; set; }

    [Column("Merchant_id")]
    public int? MerchantId { get; set; }

    [StringLength(50)]
    public string? PaymentMethodDisplay { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("RiskRuleHistories")]
    public virtual Account? Account { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("RiskRuleHistories")]
    public virtual TblCompany? Merchant { get; set; }

    [ForeignKey("TransFailId")]
    [InverseProperty("RiskRuleHistories")]
    public virtual TblCompanyTransFail? TransFail { get; set; }

    [ForeignKey("TransPassId")]
    [InverseProperty("RiskRuleHistories")]
    public virtual TblCompanyTransPass? TransPass { get; set; }

    [ForeignKey("TransPendingId")]
    [InverseProperty("RiskRuleHistories")]
    public virtual TblCompanyTransPending? TransPending { get; set; }

    [ForeignKey("TransPreAuthId")]
    [InverseProperty("RiskRuleHistories")]
    public virtual TblCompanyTransApproval? TransPreAuth { get; set; }

    [ForeignKey("RiskRuleHistoryId")]
    [InverseProperty("RiskRuleHistories")]
    public virtual ICollection<AccountNote> AccountNotes { get; set; } = new List<AccountNote>();
}
