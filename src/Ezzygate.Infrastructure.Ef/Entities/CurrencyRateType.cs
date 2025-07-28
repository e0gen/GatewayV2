using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("CurrencyRateType", Schema = "List")]
public partial class CurrencyRateType
{
    [Key]
    [Column("CurrencyRateType_id")]
    [StringLength(20)]
    [Unicode(false)]
    public string CurrencyRateTypeId { get; set; } = null!;

    [StringLength(50)]
    public string? Name { get; set; }

    [InverseProperty("CurrencyRateType")]
    public virtual ICollection<CurrencyRate> CurrencyRates { get; set; } = new List<CurrencyRate>();

    [InverseProperty("CurrencyRateType")]
    public virtual ICollection<TblCompanyCreditFee> TblCompanyCreditFees { get; set; } = new List<TblCompanyCreditFee>();
}
