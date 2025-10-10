using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblRetrivalRequests")]
public partial class TblRetrivalRequest
{
    [Key]
    [Column("RetrivalRequests_ID")]
    public int RetrivalRequestsId { get; set; }

    [Column("TransPass_ID")]
    public int? TransPassId { get; set; }

    [Column("RR_InsertDate", TypeName = "datetime")]
    public DateTime? RrInsertDate { get; set; }

    [Column("RR_ReasonCode")]
    public int? RrReasonCode { get; set; }

    [Column("RR_BankCaseID")]
    [StringLength(30)]
    public string? RrBankCaseId { get; set; }

    [Column("RR_Ticket")]
    [StringLength(30)]
    public string? RrTicket { get; set; }

    [Column("RR_Deadline", TypeName = "datetime")]
    public DateTime? RrDeadline { get; set; }

    [Column("RR_Comments")]
    [StringLength(255)]
    public string? RrComments { get; set; }

    [ForeignKey("TransPassId")]
    [InverseProperty("TblRetrivalRequests")]
    public virtual TblCompanyTransPass? TransPass { get; set; }
}
