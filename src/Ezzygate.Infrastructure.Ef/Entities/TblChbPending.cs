using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblChbPending")]
public partial class TblChbPending
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("CaseID")]
    [StringLength(40)]
    public string? CaseId { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ReasonCode { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? StoredFileName { get; set; }

    [StringLength(40)]
    [Unicode(false)]
    public string? DebitRefCode { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ApprovalNumber { get; set; }

    [Column(TypeName = "money")]
    public decimal? Amount { get; set; }

    public int? Currency { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? TerminalNumber { get; set; }

    [MaxLength(200)]
    public byte[]? CardNumber256 { get; set; }

    public int Installment { get; set; }

    public bool? IsChargeback { get; set; }

    public bool? IsPhotocopy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TransDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ChargebackDate { get; set; }

    public int? TransCount { get; set; }

    [Column("TransID")]
    public int? TransId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeadlineDate { get; set; }

    [Column("DebitCompany_id")]
    public int? DebitCompanyId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? AcquirerReferenceNum { get; set; }

    [StringLength(50)]
    public string? DebitReferenceNum { get; set; }
}
