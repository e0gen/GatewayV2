using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblEpaPending")]
public partial class TblEpaPending
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [StringLength(100)]
    public string? StoredFileName { get; set; }

    [StringLength(40)]
    public string? DebitRefCode { get; set; }

    [StringLength(50)]
    public string? ApprovalNumber { get; set; }

    [Column(TypeName = "money")]
    public decimal? Amount { get; set; }

    public int? Currency { get; set; }

    [StringLength(20)]
    public string? TerminalNumber { get; set; }

    [MaxLength(200)]
    public byte[]? CardNumber256 { get; set; }

    public int Installment { get; set; }

    public bool? IsRefund { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TransDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PayoutDate { get; set; }

    public int? TransCount { get; set; }

    [Column("TransID")]
    public int? TransId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? AcquirerReferenceNum { get; set; }

    [StringLength(50)]
    public string? DebitReferenceNum { get; set; }
}
