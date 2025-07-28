using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("EventPendingType", Schema = "List")]
public partial class EventPendingType
{
    [Key]
    [Column("EventPendingType_id")]
    public short EventPendingTypeId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    public byte? MinutesForWarning { get; set; }

    [InverseProperty("EventPendingType")]
    public virtual ICollection<EventPending> EventPendings { get; set; } = new List<EventPending>();
}
