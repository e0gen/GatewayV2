using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("LoginResult", Schema = "List")]
public partial class LoginResult
{
    [Key]
    [Column("LoginResult_id")]
    public byte LoginResultId { get; set; }

    [StringLength(30)]
    public string? Name { get; set; }

    [InverseProperty("LoginResult")]
    public virtual ICollection<LoginHistory> LoginHistories { get; set; } = new List<LoginHistory>();
}
