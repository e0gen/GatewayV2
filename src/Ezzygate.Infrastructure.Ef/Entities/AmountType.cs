using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AmountType", Schema = "List")]
public partial class AmountType
{
    [Key]
    [Column("AmountType_id")]
    public byte AmountTypeId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    public byte? SortOrder { get; set; }

    [InverseProperty("AmountType")]
    public virtual ICollection<SetTransactionFee> SetTransactionFees { get; set; } = new List<SetTransactionFee>();

    [InverseProperty("AmountType")]
    public virtual ICollection<TransactionAmount> TransactionAmounts { get; set; } = new List<TransactionAmount>();
}
