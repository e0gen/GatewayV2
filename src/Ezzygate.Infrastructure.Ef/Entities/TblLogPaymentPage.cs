using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblLogPaymentPage")]
[Index("LppDateStart", Name = "IX_tblLogPaymentPage_Lpp_DateStart")]
public partial class TblLogPaymentPage
{
    [Key]
    [Column("LogPaymentPage_id")]
    public int LogPaymentPageId { get; set; }

    [Column("Lpp_MerchantNumber")]
    [StringLength(7)]
    [Unicode(false)]
    public string? LppMerchantNumber { get; set; }

    [Column("Lpp_RemoteAddress")]
    [StringLength(20)]
    [Unicode(false)]
    public string? LppRemoteAddress { get; set; }

    [Column("Lpp_RequestMethod")]
    [StringLength(12)]
    public string? LppRequestMethod { get; set; }

    [Column("Lpp_PathTranslate")]
    [StringLength(4000)]
    public string? LppPathTranslate { get; set; }

    [Column("Lpp_HttpHost")]
    [StringLength(400)]
    public string? LppHttpHost { get; set; }

    [Column("Lpp_HttpReferer")]
    [StringLength(4000)]
    public string? LppHttpReferer { get; set; }

    [Column("Lpp_QueryString")]
    [StringLength(4000)]
    public string? LppQueryString { get; set; }

    [Column("Lpp_RequestForm")]
    [StringLength(4000)]
    public string? LppRequestForm { get; set; }

    [Column("Lpp_SessionContents")]
    [StringLength(4000)]
    public string? LppSessionContents { get; set; }

    [Column("Lpp_ReplyCode")]
    [StringLength(50)]
    public string? LppReplyCode { get; set; }

    [Column("Lpp_ReplyDesc")]
    [StringLength(500)]
    public string? LppReplyDesc { get; set; }

    [Column("Lpp_TransNum")]
    public int? LppTransNum { get; set; }

    [Column("Lpp_DateStart", TypeName = "datetime")]
    public DateTime? LppDateStart { get; set; }

    [Column("Lpp_DateEnd", TypeName = "datetime")]
    public DateTime? LppDateEnd { get; set; }

    [Column("Lpp_LocalAddr")]
    [StringLength(20)]
    [Unicode(false)]
    public string? LppLocalAddr { get; set; }

    [Column("Lpp_IsSecure")]
    public bool? LppIsSecure { get; set; }

    [InverseProperty("LpptLogPaymentPage")]
    public virtual ICollection<TblLogPaymentPageTran> TblLogPaymentPageTrans { get; set; } = new List<TblLogPaymentPageTran>();
}
