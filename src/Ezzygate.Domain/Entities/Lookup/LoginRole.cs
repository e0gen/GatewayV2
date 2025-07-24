using System.ComponentModel.DataAnnotations;
using Ezzygate.Domain.Entities.Core;

namespace Ezzygate.Domain.Entities.Lookup;

public sealed class LoginRole
{
    [Key]
    public byte LoginRoleId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    public ICollection<LoginAccount> LoginAccounts { get; set; } = new List<LoginAccount>();
}