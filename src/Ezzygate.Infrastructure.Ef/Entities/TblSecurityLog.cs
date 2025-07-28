using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblSecurityLog")]
public partial class TblSecurityLog
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("sl_User")]
    public int? SlUser { get; set; }

    [Column("sl_Document")]
    public int? SlDocument { get; set; }

    [Column("sl_Date", TypeName = "datetime")]
    public DateTime SlDate { get; set; }

    [Column("sl_IP")]
    [StringLength(25)]
    [Unicode(false)]
    public string SlIp { get; set; } = null!;

    [Column("sl_IsDenied")]
    public bool SlIsDenied { get; set; }

    [Column("sl_IsReadOnly")]
    public bool SlIsReadOnly { get; set; }

    [ForeignKey("SlDocument")]
    [InverseProperty("TblSecurityLogs")]
    public virtual TblSecurityDocument? SlDocumentNavigation { get; set; }

    [ForeignKey("SlUser")]
    [InverseProperty("TblSecurityLogs")]
    public virtual TblSecurityUser? SlUserNavigation { get; set; }
}
