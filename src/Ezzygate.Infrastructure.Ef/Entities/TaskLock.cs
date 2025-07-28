using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("TaskLock", Schema = "System")]
public partial class TaskLock
{
    [Key]
    [StringLength(100)]
    [Unicode(false)]
    public string TaskName { get; set; } = null!;

    public bool IsTaksRunning { get; set; }

    [Precision(2)]
    public DateTime LastRunDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string MachineName { get; set; } = null!;
}
