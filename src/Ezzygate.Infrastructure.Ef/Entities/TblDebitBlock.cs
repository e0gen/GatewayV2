using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblDebitBlock")]
public partial class TblDebitBlock
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("db_DebitRule")]
    public int DbDebitRule { get; set; }

    [Column("db_InsertDate", TypeName = "datetime")]
    public DateTime DbInsertDate { get; set; }

    [Column("db_UnblockDate", TypeName = "datetime")]
    public DateTime DbUnblockDate { get; set; }

    [Column("db_AutoEnableAttempts")]
    public int DbAutoEnableAttempts { get; set; }

    [Column("db_CountAutoEnableAttempts")]
    public int DbCountAutoEnableAttempts { get; set; }

    [Column("db_IsAutoEnable")]
    public bool DbIsAutoEnable { get; set; }

    [Column("db_IsUnblocked")]
    public bool DbIsUnblocked { get; set; }

    [Column("db_DebitCompany")]
    public int DbDebitCompany { get; set; }

    [Column("db_DebitTerminalNumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string DbDebitTerminalNumber { get; set; } = null!;
}
