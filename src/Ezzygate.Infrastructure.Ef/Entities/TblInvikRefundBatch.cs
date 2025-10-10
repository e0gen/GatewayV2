using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblInvikRefundBatch")]
[Index("IrbRefundRequest", Name = "IX_tblInvikRefundBatch_RefundRequest", IsUnique = true, AllDescending = true)]
public partial class TblInvikRefundBatch
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("irb_RefundRequest")]
    public int IrbRefundRequest { get; set; }

    [Column("irb_InsertDate", TypeName = "datetime")]
    public DateTime IrbInsertDate { get; set; }

    [Column("irb_User")]
    [StringLength(50)]
    [Unicode(false)]
    public string IrbUser { get; set; } = null!;

    [Column("irb_IsDownloaded")]
    public bool IrbIsDownloaded { get; set; }

    [Column("irb_DownloadDate", TypeName = "datetime")]
    public DateTime IrbDownloadDate { get; set; }

    [Column("irb_DownloadFileNumber")]
    public int IrbDownloadFileNumber { get; set; }
}
