using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCreditCardRiskManagement")]
[Index("CcrmCompanyId", Name = "IX_tblCreditCardRiskManagement_CCRM_CompanyID")]
public partial class TblCreditCardRiskManagement
{
    [Key]
    [Column("CCRM_ID")]
    public int CcrmId { get; set; }

    [Column("CCRM_InsDate", TypeName = "datetime")]
    public DateTime CcrmInsDate { get; set; }

    [Column("CCRM_CompanyID")]
    public int? CcrmCompanyId { get; set; }

    [Column("CCRM_PaymentMethod")]
    public short? CcrmPaymentMethod { get; set; }

    [Column("CCRM_CreditType")]
    public byte CcrmCreditType { get; set; }

    [Column("CCRM_Currency")]
    public short CcrmCurrency { get; set; }

    [Column("CCRM_Amount", TypeName = "money")]
    public decimal CcrmAmount { get; set; }

    [Column("CCRM_MaxTrans")]
    public int CcrmMaxTrans { get; set; }

    [Column("CCRM_Act")]
    public byte CcrmAct { get; set; }

    [Column("CCRM_ReplySource")]
    public int CcrmReplySource { get; set; }

    [Column("CCRM_Hours")]
    public int CcrmHours { get; set; }

    [Column("CCRM_IsActive")]
    public bool CcrmIsActive { get; set; }

    [Column("CCRM_BlockHours")]
    public int CcrmBlockHours { get; set; }

    [Column("ccrm_days")]
    public int? CcrmDays { get; set; }

    [Column("ccrm_blockdays")]
    public int? CcrmBlockdays { get; set; }

    [Column("CCRM_ReplyAmount")]
    [StringLength(20)]
    [Unicode(false)]
    public string CcrmReplyAmount { get; set; } = null!;

    [Column("CCRM_ReplyMaxTrans")]
    [StringLength(20)]
    [Unicode(false)]
    public string CcrmReplyMaxTrans { get; set; } = null!;

    [Column("CCRM_WhitelistLevel")]
    public int? CcrmWhitelistLevel { get; set; }

    [Column("CCRM_ApplyVT")]
    public bool CcrmApplyVt { get; set; }

    [ForeignKey("CcrmCompanyId")]
    [InverseProperty("TblCreditCardRiskManagements")]
    public virtual TblCompany? CcrmCompany { get; set; }

    [ForeignKey("CcrmPaymentMethod")]
    [InverseProperty("TblCreditCardRiskManagements")]
    public virtual PaymentMethod? CcrmPaymentMethodNavigation { get; set; }
}
