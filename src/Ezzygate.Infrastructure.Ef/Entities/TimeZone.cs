using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("TimeZone", Schema = "List")]
public partial class TimeZone
{
    [Key]
    public short TimeZoneOffsetMinutes { get; set; }

    [StringLength(200)]
    public string Name { get; set; } = null!;

    [InverseProperty("TimeZoneOffsetUiNavigation")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    [InverseProperty("TimeZoneOffsetUiNavigation")]
    public virtual ICollection<AdminUser> AdminUsers { get; set; } = new List<AdminUser>();
}
