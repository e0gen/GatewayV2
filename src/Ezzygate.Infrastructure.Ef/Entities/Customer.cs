using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("Customer", Schema = "Data")]
public partial class Customer
{
    [Key]
    [Column("Customer_id")]
    public int CustomerId { get; set; }

    [Column("ActiveStatus_id")]
    public byte ActiveStatusId { get; set; }

    [Column("ApplicationIdentity_id")]
    public int? ApplicationIdentityId { get; set; }

    [StringLength(7)]
    [Unicode(false)]
    public string CustomerNumber { get; set; } = null!;

    [Precision(0)]
    public DateTime RegistrationDate { get; set; }

    [Precision(0)]
    public DateTime? RulesApproveDate { get; set; }

    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    [StringLength(50)]
    public string? PersonalNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? PhoneNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CellNumber { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    [StringLength(80)]
    public string? EmailAddress { get; set; }

    public Guid? EmailToken { get; set; }

    [StringLength(4)]
    [Unicode(false)]
    public string? Pincode { get; set; }

    [Column("FacebookUserID")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FacebookUserId { get; set; }

    [Column("Account_id")]
    public int? AccountId { get; set; }

    [InverseProperty("Customer")]
    public virtual Account? Account { get; set; }

    [ForeignKey("ActiveStatusId")]
    [InverseProperty("Customers")]
    public virtual ActiveStatus ActiveStatus { get; set; } = null!;

    [ForeignKey("ApplicationIdentityId")]
    [InverseProperty("Customers")]
    public virtual ApplicationIdentity? ApplicationIdentity { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [InverseProperty("Customer")]
    public virtual ICollection<CustomerRelation> CustomerRelationCustomers { get; set; } = new List<CustomerRelation>();

    [InverseProperty("TargetCustomer")]
    public virtual ICollection<CustomerRelation> CustomerRelationTargetCustomers { get; set; } = new List<CustomerRelation>();

    [InverseProperty("Customer")]
    public virtual ICollection<CustomerShippingDetail> CustomerShippingDetails { get; set; } = new List<CustomerShippingDetail>();

    [InverseProperty("Customer")]
    public virtual ICollection<EventPending> EventPendings { get; set; } = new List<EventPending>();
}
