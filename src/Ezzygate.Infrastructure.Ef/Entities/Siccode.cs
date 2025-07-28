using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("SICCode", Schema = "List")]
public partial class Siccode
{
    [Key]
    [Column("SICCodeNumber")]
    public short SiccodeNumber { get; set; }

    [StringLength(80)]
    [Unicode(false)]
    public string IndustryName { get; set; } = null!;

    [InverseProperty("SiccodeNumberNavigation")]
    public virtual ICollection<TblCompany> TblCompanies { get; set; } = new List<TblCompany>();

    [InverseProperty("SiccodeNumberNavigation")]
    public virtual ICollection<TblDebitTerminal> TblDebitTerminals { get; set; } = new List<TblDebitTerminal>();
}
