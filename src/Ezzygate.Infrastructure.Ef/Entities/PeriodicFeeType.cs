using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("PeriodicFeeType", Schema = "Data")]
public partial class PeriodicFeeType
{
    [Key]
    [Column("PeriodicFeeType_id")]
    public int PeriodicFeeTypeId { get; set; }

    [Column("AccountType_id")]
    public byte AccountTypeId { get; set; }

    [Column("CurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string? CurrencyIsocode { get; set; }

    [Column(TypeName = "decimal(19, 3)")]
    public decimal? Amount { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("TimeInterval_id")]
    public byte TimeIntervalId { get; set; }

    public bool IsActive { get; set; }

    [Column("ProcessMerchant_id")]
    public int? ProcessMerchantId { get; set; }

    [ForeignKey("AccountTypeId")]
    [InverseProperty("PeriodicFeeTypes")]
    public virtual AccountType AccountType { get; set; } = null!;

    [ForeignKey("CurrencyIsocode")]
    [InverseProperty("PeriodicFeeTypes")]
    public virtual CurrencyList? CurrencyIsocodeNavigation { get; set; }

    [ForeignKey("ProcessMerchantId")]
    [InverseProperty("PeriodicFeeTypes")]
    public virtual TblCompany? ProcessMerchant { get; set; }

    [InverseProperty("PeriodicFeeType")]
    public virtual ICollection<SetPeriodicFee> SetPeriodicFees { get; set; } = new List<SetPeriodicFee>();

    [ForeignKey("TimeIntervalId")]
    [InverseProperty("PeriodicFeeTypes")]
    public virtual TimeUnit TimeInterval { get; set; } = null!;
}
