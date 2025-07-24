using System.ComponentModel.DataAnnotations;
using Ezzygate.Domain.Entities.Core;

namespace Ezzygate.Domain.Entities.Lookup;

public sealed class AccountType
{
    [Key]
    public byte AccountTypeId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    public ICollection<Account> Accounts { get; set; } = new List<Account>();
}