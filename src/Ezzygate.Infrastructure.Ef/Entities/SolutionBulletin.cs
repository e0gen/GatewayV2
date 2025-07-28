using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("SolutionBulletin", Schema = "Data")]
public partial class SolutionBulletin
{
    [Key]
    [Column("SolutionBulletin_id")]
    public int SolutionBulletinId { get; set; }

    [Column("SolutionList_id")]
    public byte? SolutionListId { get; set; }

    [StringLength(2000)]
    public string MessageText { get; set; } = null!;

    [Precision(2)]
    public DateTime MessageDate { get; set; }

    [Precision(2)]
    public DateTime? MessageExpirationDate { get; set; }

    [StringLength(50)]
    public string? InsertUserName { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string BulletinType { get; set; } = null!;

    public bool IsVisible { get; set; }

    [ForeignKey("SolutionListId")]
    [InverseProperty("SolutionBulletins")]
    public virtual SolutionList? SolutionList { get; set; }
}
