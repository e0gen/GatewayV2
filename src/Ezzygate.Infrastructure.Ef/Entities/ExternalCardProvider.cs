using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("ExternalCardProvider", Schema = "List")]
public partial class ExternalCardProvider
{
    [Key]
    [Column("ExternalCardProvider_id")]
    public byte ExternalCardProviderId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("ExternalCardProvider")]
    public virtual ICollection<TblExternalCardTerminal> TblExternalCardTerminals { get; set; } = new List<TblExternalCardTerminal>();
}
