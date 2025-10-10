using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("SolutionList", Schema = "List")]
public partial class SolutionList
{
    [Key]
    [Column("SolutionList_id")]
    public byte SolutionListId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [StringLength(30)]
    public string? Subdomain { get; set; }

    [InverseProperty("SolutionList")]
    public virtual ICollection<MerchantSetText> MerchantSetTexts { get; set; } = new List<MerchantSetText>();

    [InverseProperty("SolutionList")]
    public virtual ICollection<NewSecurityObject> NewSecurityObjects { get; set; } = new List<NewSecurityObject>();

    [InverseProperty("SolutionList")]
    public virtual ICollection<SolutionBulletin> SolutionBulletins { get; set; } = new List<SolutionBulletin>();
}
