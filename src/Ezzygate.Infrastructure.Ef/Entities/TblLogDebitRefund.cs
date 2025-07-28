using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblLogDebitRefund")]
public partial class TblLogDebitRefund
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ldr_InsertDate", TypeName = "datetime")]
    public DateTime LdrInsertDate { get; set; }

    [Column("ldr_TransFail")]
    public int LdrTransFail { get; set; }

    [Column("ldr_ReplyCode")]
    [StringLength(20)]
    [Unicode(false)]
    public string LdrReplyCode { get; set; } = null!;

    [Column("ldr_LogNoConnection")]
    public int LdrLogNoConnection { get; set; }

    [Column("ldr_Answer")]
    [StringLength(1000)]
    public string LdrAnswer { get; set; } = null!;
}
