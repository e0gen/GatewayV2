using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("ActionStatus", Schema = "List")]
public partial class ActionStatus
{
    [Key]
    [Column("ActionStatus_id")]
    public byte ActionStatusId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [InverseProperty("ActionStatus")]
    public virtual ICollection<AuthorizationBatch> AuthorizationBatches { get; set; } = new List<AuthorizationBatch>();

    [InverseProperty("ActionStatus")]
    public virtual ICollection<StatusHistory> StatusHistories { get; set; } = new List<StatusHistory>();
}
