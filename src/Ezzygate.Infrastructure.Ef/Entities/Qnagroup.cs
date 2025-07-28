using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("QNAGroup")]
public partial class Qnagroup
{
    [Key]
    [Column("QNAGroup_id")]
    public int QnagroupId { get; set; }

    [Column("Language_id")]
    public byte LanguageId { get; set; }

    public bool IsVisible { get; set; }

    [Column("name")]
    [StringLength(80)]
    public string Name { get; set; } = null!;

    [Column("IsMerchantCP")]
    public bool IsMerchantCp { get; set; }

    public bool IsDevCenter { get; set; }

    public bool IsWallet { get; set; }

    public bool IsWebsite { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? TemplateRestriction { get; set; }

    [Column("ParentCompny_id")]
    public int? ParentCompnyId { get; set; }

    [ForeignKey("LanguageId")]
    [InverseProperty("Qnagroups")]
    public virtual LanguageList Language { get; set; } = null!;

    [InverseProperty("Qnagroup")]
    public virtual ICollection<Qna> Qnas { get; set; } = new List<Qna>();
}
