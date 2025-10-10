using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[PrimaryKey("CustomerId", "TargetCustomerId")]
[Table("CustomerRelation", Schema = "Data")]
public partial class CustomerRelation
{
    [Key]
    [Column("Customer_id")]
    public int CustomerId { get; set; }

    [Key]
    [Column("TargetCustomer_id")]
    public int TargetCustomerId { get; set; }

    [Column("PeopleRelationType_id")]
    public byte? PeopleRelationTypeId { get; set; }

    public bool? IsActive { get; set; }

    public DateOnly? ConfirmationDate { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("CustomerRelationCustomers")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("PeopleRelationTypeId")]
    [InverseProperty("CustomerRelations")]
    public virtual PeopleRelationType? PeopleRelationType { get; set; }

    [ForeignKey("TargetCustomerId")]
    [InverseProperty("CustomerRelationTargetCustomers")]
    public virtual Customer TargetCustomer { get; set; } = null!;
}
