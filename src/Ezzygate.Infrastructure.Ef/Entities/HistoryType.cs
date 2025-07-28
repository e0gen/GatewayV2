using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("HistoryType", Schema = "List")]
public partial class HistoryType
{
    [Key]
    [Column("HistoryType_id")]
    public byte HistoryTypeId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    public short? RetentionPeriodDays { get; set; }

    [InverseProperty("HistoryType")]
    public virtual ICollection<History> Histories { get; set; } = new List<History>();
}
