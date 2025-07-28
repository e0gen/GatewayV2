using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("LoginType", Schema = "List")]
public partial class LoginType
{
    [Key]
    [Column("LoginType_id")]
    public byte LoginTypeId { get; set; }

    [StringLength(30)]
    public string? Name { get; set; }
}
