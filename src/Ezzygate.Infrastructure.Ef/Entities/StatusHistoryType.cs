using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("StatusHistoryType", Schema = "List")]
public partial class StatusHistoryType
{
    [Key]
    [Column("StatusHistoryType_id")]
    [StringLength(16)]
    [Unicode(false)]
    public string StatusHistoryTypeId { get; set; } = null!;

    [StringLength(50)]
    public string? Name { get; set; }

    [InverseProperty("StatusHistoryType")]
    public virtual ICollection<StatusHistory> StatusHistories { get; set; } = new List<StatusHistory>();
}
