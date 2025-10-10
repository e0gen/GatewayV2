using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("PeopleRelationType", Schema = "List")]
public partial class PeopleRelationType
{
    [Key]
    [Column("PeopleRelationType_id")]
    public byte PeopleRelationTypeId { get; set; }

    [StringLength(30)]
    public string? Name { get; set; }

    [StringLength(100)]
    public string? Description { get; set; }

    [InverseProperty("PeopleRelationType")]
    public virtual ICollection<CustomerRelation> CustomerRelations { get; set; } = new List<CustomerRelation>();
}
