using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("ExternalServiceAction", Schema = "List")]
public partial class ExternalServiceAction
{
    [Key]
    [Column("ExternalServiceAction_id")]
    [StringLength(32)]
    [Unicode(false)]
    public string ExternalServiceActionId { get; set; } = null!;

    [StringLength(80)]
    public string Name { get; set; } = null!;

    [InverseProperty("ExternalServiceAction")]
    public virtual ICollection<ExternalServiceHistory> ExternalServiceHistories { get; set; } = new List<ExternalServiceHistory>();
}
