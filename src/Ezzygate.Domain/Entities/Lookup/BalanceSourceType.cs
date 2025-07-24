using System.ComponentModel.DataAnnotations;
using Ezzygate.Domain.Entities.Core;

namespace Ezzygate.Domain.Entities.Lookup;

public sealed class BalanceSourceType
{
    [Key]
    [StringLength(30)]
    public string BalanceSourceTypeId { get; set; } = string.Empty;

    [StringLength(50)]
    public string? Name { get; set; }

    [Required]
    public bool IsFee { get; set; }

    public ICollection<AccountBalance> AccountBalances { get; set; } = new List<AccountBalance>();
}