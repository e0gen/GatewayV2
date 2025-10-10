using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("ExternalServiceType", Schema = "List")]
public partial class ExternalServiceType
{
    [Key]
    [Column("ExternalServiceType_id")]
    [StringLength(16)]
    [Unicode(false)]
    public string ExternalServiceTypeId { get; set; } = null!;

    [StringLength(30)]
    public string Name { get; set; } = null!;

    [InverseProperty("ExternalServiceType")]
    public virtual ICollection<AccountExternalService> AccountExternalServices { get; set; } = new List<AccountExternalService>();

    [InverseProperty("ExternalServiceType")]
    public virtual ICollection<ExternalServiceHistory> ExternalServiceHistories { get; set; } = new List<ExternalServiceHistory>();

    [InverseProperty("ExternalServiceType")]
    public virtual ICollection<TransMatchPending> TransMatchPendings { get; set; } = new List<TransMatchPending>();
}
