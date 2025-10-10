using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewCurrency
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("ISO")]
    [StringLength(80)]
    public string Iso { get; set; } = null!;

    [StringLength(80)]
    public string NameHeb { get; set; } = null!;
}
