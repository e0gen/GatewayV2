using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblLog_TerminalJump")]
[Index("LtjGroupId", Name = "IX_tblLog_TerminalJump_ltj_GroupID")]
[Index("LtjInsertDate", Name = "IX_tblLog_TerminalJump_ltj_InsertDate")]
[Index("LtjTransId", Name = "IX_tblLog_TerminalJump_ltj_TransID")]
public partial class TblLogTerminalJump
{
    [Key]
    [Column("ltj_ID")]
    public int LtjId { get; set; }

    [Column("ltj_GroupID")]
    public int LtjGroupId { get; set; }

    [Column("ltj_InsertDate", TypeName = "datetime")]
    public DateTime LtjInsertDate { get; set; }

    [Column("ltj_CompanyID")]
    public int LtjCompanyId { get; set; }

    [Column("ltj_TransID")]
    public int LtjTransId { get; set; }

    [Column("ltj_TransReply")]
    [StringLength(50)]
    public string LtjTransReply { get; set; } = null!;

    [Column("ltj_DebitCompanyID")]
    public int LtjDebitCompanyId { get; set; }

    [Column("ltj_TerminalNumber")]
    [StringLength(20)]
    public string LtjTerminalNumber { get; set; } = null!;

    [Column("ltj_JumpIndex")]
    public int LtjJumpIndex { get; set; }
}
