using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblSettlementAmount")]
public partial class TblSettlementAmount
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public int? ItemCount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReleaseDate { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    public int? ReferenceNumber { get; set; }

    [InverseProperty("SettlementAmount")]
    public virtual ICollection<TblTransactionAmount> TblTransactionAmounts { get; set; } = new List<TblTransactionAmount>();
}
