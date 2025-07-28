using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class VwQnagroup
{
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

    public int? Questions { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? LanguageName { get; set; }
}
