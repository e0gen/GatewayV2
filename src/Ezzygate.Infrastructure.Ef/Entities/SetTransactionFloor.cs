using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("SetTransactionFloor", Schema = "Setting")]
public partial class SetTransactionFloor
{
    [Key]
    [Column("SetTransactionFloor_id")]
    public int SetTransactionFloorId { get; set; }

    [Column("SettlementType_id")]
    public byte SettlementTypeId { get; set; }

    [Column("Account_id")]
    public int? AccountId { get; set; }

    [Column("CurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string? CurrencyIsocode { get; set; }

    [StringLength(20)]
    public string? Title { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("SetTransactionFloors")]
    public virtual Account? Account { get; set; }

    [ForeignKey("CurrencyIsocode")]
    [InverseProperty("SetTransactionFloors")]
    public virtual CurrencyList? CurrencyIsocodeNavigation { get; set; }

    [InverseProperty("SetTransactionFloor")]
    public virtual ICollection<MonthlyFloorTotal> MonthlyFloorTotals { get; set; } = new List<MonthlyFloorTotal>();

    [InverseProperty("SetTransactionFloor")]
    public virtual ICollection<SetTransactionFee> SetTransactionFees { get; set; } = new List<SetTransactionFee>();

    [InverseProperty("SetTransactionFloor")]
    public virtual ICollection<SetTransactionFloorFee> SetTransactionFloorFees { get; set; } = new List<SetTransactionFloorFee>();

    [ForeignKey("SettlementTypeId")]
    [InverseProperty("SetTransactionFloors")]
    public virtual SettlemenType SettlementType { get; set; } = null!;
}
