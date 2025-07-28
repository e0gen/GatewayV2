using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblDebitBlockLog")]
public partial class TblDebitBlockLog
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("dbl_Date", TypeName = "datetime")]
    public DateTime DblDate { get; set; }

    [Column("dbl_Type")]
    [StringLength(20)]
    public string DblType { get; set; } = null!;

    [Column("dbl_Text")]
    [StringLength(950)]
    public string DblText { get; set; } = null!;

    [Column("dbl_DebitCompany")]
    public int DblDebitCompany { get; set; }

    [Column("dbl_DebitRule")]
    public int DblDebitRule { get; set; }

    [Column("dbl_DebitTerminal")]
    public int DblDebitTerminal { get; set; }
}
