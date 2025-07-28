using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("TransDeniedStatus", Schema = "List")]
public partial class TransDeniedStatus
{
    [Key]
    public byte DeniedStatus { get; set; }

    [StringLength(200)]
    public string Name { get; set; } = null!;
}
