using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("CountryGroup", Schema = "List")]
public partial class CountryGroup
{
    [Key]
    [Column("CountryGroup_id")]
    public int CountryGroupId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [ForeignKey("CountryGroupId")]
    [InverseProperty("CountryGroups")]
    public virtual ICollection<CountryList> CountryIsocodes { get; set; } = new List<CountryList>();
}
