using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

/// <summary>
/// Risk rules set for account transactions, When account is NULL it is a global rule
/// </summary>
[Table("SetRiskRule", Schema = "Setting")]
public partial class SetRiskRule
{
    [Key]
    [Column("SetRiskRule_id")]
    public int SetRiskRuleId { get; set; }

    [Column("Account_id")]
    public int? AccountId { get; set; }

    [StringLength(50)]
    public string? RuleName { get; set; }

    [Column(TypeName = "xml")]
    public string? RuleSetting { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("SetRiskRules")]
    public virtual Account? Account { get; set; }
}
