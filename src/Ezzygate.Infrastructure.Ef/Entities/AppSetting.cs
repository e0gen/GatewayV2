using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AppSetting", Schema = "System")]
public partial class AppSetting
{
    [Key]
    [StringLength(20)]
    [Unicode(false)]
    public string ValueName { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string Value { get; set; } = null!;
}
