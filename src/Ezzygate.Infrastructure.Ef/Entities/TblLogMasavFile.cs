using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblLogMasavFile")]
public partial class TblLogMasavFile
{
    [Key]
    [Column("logMasavFile_id")]
    public int LogMasavFileId { get; set; }

    [Column("insertDate", TypeName = "smalldatetime")]
    public DateTime InsertDate { get; set; }

    [StringLength(20)]
    public string PayedBankCode { get; set; } = null!;

    [StringLength(100)]
    public string PayedBankDesc { get; set; } = null!;

    [StringLength(50)]
    public string LogonUser { get; set; } = null!;

    public byte Flag { get; set; }

    public double PayAmount { get; set; }

    public byte PayCurrency { get; set; }

    public short PayCount { get; set; }

    public byte DoneFlag { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string AttachedFileExt { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? FileName { get; set; }

    [Column("WireProvider_Id")]
    [StringLength(16)]
    [Unicode(false)]
    public string? WireProviderId { get; set; }

    public bool IsSendRequire { get; set; }

    [InverseProperty("LogMasavFile")]
    public virtual ICollection<TblWireMoneyLog> TblWireMoneyLogs { get; set; } = new List<TblWireMoneyLog>();
}
