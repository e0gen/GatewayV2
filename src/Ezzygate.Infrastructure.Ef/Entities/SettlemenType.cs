using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("SettlemenType", Schema = "List")]
public partial class SettlemenType
{
    [Key]
    [Column("SettlementType_id")]
    public byte SettlementTypeId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [InverseProperty("SettlementType")]
    public virtual ICollection<SetTransactionFee> SetTransactionFees { get; set; } = new List<SetTransactionFee>();

    [InverseProperty("SettlementType")]
    public virtual ICollection<SetTransactionFloor> SetTransactionFloors { get; set; } = new List<SetTransactionFloor>();

    [InverseProperty("SettlementType")]
    public virtual ICollection<TransactionAmount> TransactionAmounts { get; set; } = new List<TransactionAmount>();
}
