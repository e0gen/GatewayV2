using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCompanyChargeAdmin")]
public partial class TblCompanyChargeAdmin
{
    [Key]
    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("isRecurringReply")]
    public bool IsRecurringReply { get; set; }

    [Column("recurringReplyUrl")]
    [StringLength(250)]
    public string RecurringReplyUrl { get; set; } = null!;

    [Column("isPendingReply")]
    public bool IsPendingReply { get; set; }

    [StringLength(250)]
    public string PendingReplyUrl { get; set; } = null!;

    [Column("WalletReplyURL")]
    [StringLength(250)]
    public string WalletReplyUrl { get; set; } = null!;

    public bool IsWalletReply { get; set; }

    public bool IsWalletReplyFail { get; set; }

    public bool UseHasKeyInWalletReply { get; set; }

    [Column("isRecurringModifyReply")]
    public bool IsRecurringModifyReply { get; set; }

    [Column("recurringModifyReplyUrl")]
    [StringLength(250)]
    public string RecurringModifyReplyUrl { get; set; } = null!;

    [Column("isNotifyRefundRequestApproved")]
    public bool IsNotifyRefundRequestApproved { get; set; }

    [StringLength(250)]
    public string? NotifyRefundRequestApprovedUrl { get; set; }

    [Column("NotifyProcessURL")]
    [StringLength(200)]
    [Unicode(false)]
    public string? NotifyProcessUrl { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("TblCompanyChargeAdmin")]
    public virtual TblCompany Company { get; set; } = null!;
}
