using System.ComponentModel.DataAnnotations;
using Ezzygate.Domain.Entities.Core;

namespace Ezzygate.Domain.Entities.Lookup;

public sealed class ActiveStatus
{
    [Key]
    public byte ActiveStatusId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    public ICollection<Customer> Customers { get; set; } = new List<Customer>();
}