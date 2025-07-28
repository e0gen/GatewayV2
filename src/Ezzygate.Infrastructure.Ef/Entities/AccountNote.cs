using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AccountNote", Schema = "Data")]
public partial class AccountNote
{
    [Key]
    [Column("AccountNote_id")]
    public int AccountNoteId { get; set; }

    [Column("Account_id")]
    public int? AccountId { get; set; }

    [Precision(0)]
    public DateTime InsertDate { get; set; }

    [StringLength(50)]
    public string? InsertUser { get; set; }

    [StringLength(1000)]
    public string? NoteText { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("AccountNotes")]
    public virtual Account? Account { get; set; }

    [ForeignKey("AccountNoteId")]
    [InverseProperty("AccountNotes")]
    public virtual ICollection<RiskRuleHistory> RiskRuleHistories { get; set; } = new List<RiskRuleHistory>();
}
