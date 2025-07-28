using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("SetMerchantRollingReserve", Schema = "Setting")]
[Index("MerchantId", Name = "UIX_SetMerchantRollingReserveMerchant_id", IsUnique = true)]
public partial class SetMerchantRollingReserve
{
    [Key]
    [Column("SetMerchantRollingReserve_id")]
    public int SetMerchantRollingReserveId { get; set; }

    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    public byte ActionType { get; set; }

    public bool IsAutoReserve { get; set; }

    public bool IsAutoReturn { get; set; }

    [Column(TypeName = "money")]
    public decimal? FixHoldAmount { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal? ReservePercentage { get; set; }

    public short? ReservePeriod { get; set; }

    [StringLength(250)]
    public string? Comment { get; set; }

    [Column("FixHoldCurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string? FixHoldCurrencyIsocode { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("SetMerchantRollingReserve")]
    public virtual TblCompany Merchant { get; set; } = null!;
}
