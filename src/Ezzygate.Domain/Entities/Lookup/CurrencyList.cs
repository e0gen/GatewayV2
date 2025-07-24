using System.ComponentModel.DataAnnotations;
using Ezzygate.Domain.Entities.Core;

namespace Ezzygate.Domain.Entities.Lookup;

public sealed class CurrencyList
{
    [Key]
    [StringLength(3)]
    public string CurrencyISOCode { get; set; } = string.Empty;

    [StringLength(50)]
    public string? Name { get; set; }

    [StringLength(5)]
    public string? Symbol { get; set; }

    public ICollection<Account> Accounts { get; set; } = new List<Account>();
    public ICollection<AccountBalance> AccountBalances { get; set; } = new List<AccountBalance>();
}