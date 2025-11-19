using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblLogChargeAttempts")]
public partial class TblLogChargeAttempt
{
    [Key]
    [Column("LogChargeAttempts_id")]
    public int LogChargeAttemptsId { get; set; }

    [Column("TransactionType_id")]
    public short? TransactionTypeId { get; set; }

    [Column("Lca_MerchantNumber")]
    [StringLength(7)]
    [Unicode(false)]
    public string? LcaMerchantNumber { get; set; }

    [Column("Lca_RemoteAddress")]
    [StringLength(200)]
    [Unicode(false)]
    public string? LcaRemoteAddress { get; set; }

    [Column("Lca_RequestMethod")]
    [StringLength(12)]
    public string? LcaRequestMethod { get; set; }

    [Column("Lca_PathTranslate")]
    [StringLength(4000)]
    public string? LcaPathTranslate { get; set; }

    [Column("Lca_HttpReferer")]
    [StringLength(4000)]
    public string? LcaHttpReferer { get; set; }

    [Column("Lca_QueryString")]
    [StringLength(4000)]
    public string? LcaQueryString { get; set; }

    [Column("Lca_RequestForm")]
    [StringLength(4000)]
    public string? LcaRequestForm { get; set; }

    [Column("Lca_SessionContents")]
    [StringLength(4000)]
    public string? LcaSessionContents { get; set; }

    [Column("Lca_ReplyCode")]
    [StringLength(50)]
    public string? LcaReplyCode { get; set; }

    [Column("Lca_ReplyDesc")]
    [StringLength(500)]
    public string? LcaReplyDesc { get; set; }

    [Column("Lca_TransNum")]
    public int? LcaTransNum { get; set; }

    [Column("Lca_DateStart", TypeName = "datetime")]
    public DateTime? LcaDateStart { get; set; }

    [Column("Lca_DateEnd", TypeName = "datetime")]
    public DateTime? LcaDateEnd { get; set; }

    [Column("Lca_HttpHost")]
    [StringLength(400)]
    public string? LcaHttpHost { get; set; }

    [Column("Lca_TimeString")]
    [StringLength(2000)]
    public string? LcaTimeString { get; set; }

    [Column("Lca_LocalAddr")]
    [StringLength(20)]
    [Unicode(false)]
    public string? LcaLocalAddr { get; set; }

    [Column("Lca_RequestString")]
    [StringLength(4000)]
    public string? LcaRequestString { get; set; }

    [Column("Lca_ResponseString")]
    [StringLength(4000)]
    public string? LcaResponseString { get; set; }

    [Column("Lca_IsSecure")]
    public bool? LcaIsSecure { get; set; }

    [Column("Lca_DebitCompanyID")]
    public int? LcaDebitCompanyId { get; set; }

    [Column("Lca_InnerRequest")]
    [StringLength(4000)]
    public string? LcaInnerRequest { get; set; }

    [Column("Lca_InnerResponse")]
    [StringLength(4000)]
    public string? LcaInnerResponse { get; set; }

    [StringLength(1000)]
    public string? TextValue { get; set; }

    [Column("3dsTrxId")]
    [StringLength(50)]
    public string? _3dsTrxId { get; set; }

    [Column("isRedirectApplied")]
    public bool? IsRedirectApplied { get; set; }

    [ForeignKey("LcaDebitCompanyId")]
    [InverseProperty("TblLogChargeAttempts")]
    public virtual TblDebitCompany? LcaDebitCompany { get; set; }
}
