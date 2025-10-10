using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("ProtocolType", Schema = "List")]
public partial class ProtocolType
{
    [Key]
    [Column("ProtocolType_id")]
    [StringLength(10)]
    [Unicode(false)]
    public string ProtocolTypeId { get; set; } = null!;

    [InverseProperty("ProtocolType")]
    public virtual ICollection<AccountExternalService> AccountExternalServices { get; set; } = new List<AccountExternalService>();
}
