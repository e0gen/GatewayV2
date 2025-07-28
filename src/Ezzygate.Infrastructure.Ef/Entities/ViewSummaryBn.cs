using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewSummaryBn
{
    [StringLength(50)]
    public string? ContractNo { get; set; }

    [Column("ID")]
    public int? Id { get; set; }

    [StringLength(200)]
    public string? CompanyName { get; set; }

    [StringLength(80)]
    public string? CurrentStatus { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string? Cur { get; set; }

    [Column(TypeName = "money")]
    public decimal SaleAmount { get; set; }

    public int SaleCount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FirstSale { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastSale { get; set; }

    [Column(TypeName = "money")]
    public decimal DeclineAmount { get; set; }

    public int DeclineCount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FirstDecline { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastDecline { get; set; }
}
