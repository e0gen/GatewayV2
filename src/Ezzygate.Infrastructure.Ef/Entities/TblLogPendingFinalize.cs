using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblLogPendingFinalize")]
[Index("FinalizeDate", Name = "IX_tblLogPendingFinalize_FinalizeDate", AllDescending = true)]
public partial class TblLogPendingFinalize
{
    [Key]
    [Column("PendingID")]
    public int PendingId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FinalizeDate { get; set; }

    [Column("TransPassID")]
    public int? TransPassId { get; set; }

    [Column("TransFailID")]
    public int? TransFailId { get; set; }

    [Column("TransApprovalID")]
    public int? TransApprovalId { get; set; }

    [ForeignKey("TransApprovalId")]
    [InverseProperty("TblLogPendingFinalizes")]
    public virtual TblCompanyTransApproval? TransApproval { get; set; }

    [ForeignKey("TransFailId")]
    [InverseProperty("TblLogPendingFinalizes")]
    public virtual TblCompanyTransFail? TransFail { get; set; }

    [ForeignKey("TransPassId")]
    [InverseProperty("TblLogPendingFinalizes")]
    public virtual TblCompanyTransPass? TransPass { get; set; }
}
