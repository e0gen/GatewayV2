using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("SetTransactionFee", Schema = "Setting")]
public partial class SetTransactionFee
{
    [Key]
    [Column("SetTransactionFee_id")]
    public int SetTransactionFeeId { get; set; }

    [Column("FeeCalcMethod_id")]
    public byte FeeCalcMethodId { get; set; }

    [Column("AmountType_id")]
    public byte AmountTypeId { get; set; }

    [Column("SettlementType_id")]
    public byte SettlementTypeId { get; set; }

    [Column("Account_id")]
    public int? AccountId { get; set; }

    [Column("SetTransactionFloor_id")]
    public int? SetTransactionFloorId { get; set; }

    [Column(TypeName = "decimal(7, 4)")]
    public decimal? PercentValue { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? FixedAmount { get; set; }

    [Column(TypeName = "decimal(7, 4)")]
    public decimal? SettlementPercentValue { get; set; }

    [Column("CurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string? CurrencyIsocode { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("SetTransactionFees")]
    public virtual Account? Account { get; set; }

    [ForeignKey("AmountTypeId")]
    [InverseProperty("SetTransactionFees")]
    public virtual AmountType AmountType { get; set; } = null!;

    [ForeignKey("CurrencyIsocode")]
    [InverseProperty("SetTransactionFees")]
    public virtual CurrencyList? CurrencyIsocodeNavigation { get; set; }

    [ForeignKey("FeeCalcMethodId")]
    [InverseProperty("SetTransactionFees")]
    public virtual FeeCalcMethod FeeCalcMethod { get; set; } = null!;

    [ForeignKey("SetTransactionFloorId")]
    [InverseProperty("SetTransactionFees")]
    public virtual SetTransactionFloor? SetTransactionFloor { get; set; }

    [ForeignKey("SettlementTypeId")]
    [InverseProperty("SetTransactionFees")]
    public virtual SettlemenType SettlementType { get; set; } = null!;
}
