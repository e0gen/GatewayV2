using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewTriggersDcl
{
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(134)]
    public string? Name { get; set; }
}
