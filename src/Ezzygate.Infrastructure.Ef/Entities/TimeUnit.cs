using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("TimeUnit", Schema = "List")]
public partial class TimeUnit
{
    [Key]
    [Column("TimeUnit_id")]
    public byte TimeUnitId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public int EstimateHours { get; set; }

    [InverseProperty("TimeInterval")]
    public virtual ICollection<PeriodicFeeType> PeriodicFeeTypes { get; set; } = new List<PeriodicFeeType>();

    [InverseProperty("AutoIntervalTimeUnitNavigation")]
    public virtual ICollection<SetMerchantSettlement> SetMerchantSettlements { get; set; } = new List<SetMerchantSettlement>();
}
