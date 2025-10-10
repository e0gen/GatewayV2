using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("BalanceSourceType", Schema = "List")]
public partial class BalanceSourceType
{
    [Key]
    [Column("BalanceSourceType_id")]
    [StringLength(30)]
    [Unicode(false)]
    public string BalanceSourceTypeId { get; set; } = null!;

    [StringLength(50)]
    public string? Name { get; set; }

    public bool IsFee { get; set; }

    [InverseProperty("BalanceSourceType")]
    public virtual ICollection<AccountBalance> AccountBalances { get; set; } = new List<AccountBalance>();
}
