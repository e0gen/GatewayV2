using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblPeriodicFeeType")]
public partial class TblPeriodicFeeType
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    [Column("CurrencyID")]
    public int CurrencyId { get; set; }

    public bool IsAnnual { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "money")]
    public decimal? FeeLimit { get; set; }

    public int Behavior { get; set; }

    [ForeignKey("CurrencyId")]
    [InverseProperty("TblPeriodicFeeTypes")]
    public virtual TblSystemCurrency Currency { get; set; } = null!;

    [InverseProperty("Type")]
    public virtual ICollection<TblPeriodicFee> TblPeriodicFees { get; set; } = new List<TblPeriodicFee>();
}
