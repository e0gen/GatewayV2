using System.ComponentModel.DataAnnotations;

namespace Ezzygate.Domain.Entities.Lookup;

/// <summary>
/// Active status lookup entity for customers
/// </summary>
public sealed class ActiveStatus
{
    [Key]
    public byte ActiveStatusId { get; set; }
    
    [StringLength(50)]
    public string? Name { get; set; }

    // Collections
    public ICollection<Core.Customer> Customers { get; set; } = new List<Core.Customer>();
} 