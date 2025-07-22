using System.ComponentModel.DataAnnotations;
using Ezzygate.Domain.Entities.Lookup;

namespace Ezzygate.Domain.Entities.Core;

/// <summary>
/// Customer entity representing individual customers in the system
/// </summary>
public sealed class Customer
{
    [Key]
    public int CustomerId { get; set; }
    
    [Required]
    public byte ActiveStatusId { get; set; }
    
    public int? ApplicationIdentityId { get; set; }
    
    [Required]
    [StringLength(7)]
    public string CustomerNumber { get; set; } = string.Empty;
    
    [Required]
    public DateTime RegistrationDate { get; set; }
    
    public DateTime? RulesApproveDate { get; set; }
    
    [StringLength(50)]
    public string? FirstName { get; set; }
    
    [StringLength(50)]
    public string? LastName { get; set; }
    
    [StringLength(50)]
    public string? PersonalNumber { get; set; }
    
    [StringLength(50)]
    public string? PhoneNumber { get; set; }
    
    [StringLength(50)]
    public string? CellNumber { get; set; }
    
    public DateTime? DateOfBirth { get; set; }
    
    [StringLength(80)]
    public string? EmailAddress { get; set; }
    
    public Guid? EmailToken { get; set; }
    
    [StringLength(4)]
    public string? Pincode { get; set; }
    
    [StringLength(20)]
    public string? FacebookUserID { get; set; }
    
    public int? AccountId { get; set; }

    // Computed properties
    public string FullName => $"{FirstName} {LastName}".Trim();

    // Navigation properties
    public Account? Account { get; set; }
    public ActiveStatus? ActiveStatus { get; set; }

    // Collections
    public ICollection<Cart> Carts { get; set; } = new List<Cart>();
    public ICollection<CustomerShippingDetail> CustomerShippingDetails { get; set; } = new List<CustomerShippingDetail>();
} 