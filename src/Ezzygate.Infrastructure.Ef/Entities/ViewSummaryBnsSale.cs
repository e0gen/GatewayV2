using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewSummaryBnsSale
{
    [StringLength(50)]
    public string ContractNo { get; set; } = null!;

    [Column("ID")]
    public int Id { get; set; }

    [StringLength(200)]
    public string CompanyName { get; set; } = null!;

    [StringLength(80)]
    public string CurrentStatus { get; set; } = null!;

    [StringLength(3)]
    [Unicode(false)]
    public string Cur { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal? SaleAmount { get; set; }

    public int? SaleCount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FirstSale { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastSale { get; set; }
}
