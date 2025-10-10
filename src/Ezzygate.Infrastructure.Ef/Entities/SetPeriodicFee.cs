using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("SetPeriodicFee", Schema = "Setting")]
public partial class SetPeriodicFee
{
    [Key]
    [Column("SetPeriodicFee_id")]
    public int SetPeriodicFeeId { get; set; }

    [Column("PeriodicFeeType_id")]
    public int PeriodicFeeTypeId { get; set; }

    [Column("Account_id")]
    public int AccountId { get; set; }

    [Column("AccountPaymentMethod_id")]
    public int? AccountPaymentMethodId { get; set; }

    [Column("CurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string CurrencyIsocode { get; set; } = null!;

    [Column(TypeName = "decimal(19, 3)")]
    public decimal Amount { get; set; }

    public bool IsActive { get; set; }

    public DateOnly DateNextCharge { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("SetPeriodicFees")]
    public virtual Account Account { get; set; } = null!;

    [ForeignKey("AccountPaymentMethodId")]
    [InverseProperty("SetPeriodicFees")]
    public virtual AccountPaymentMethod? AccountPaymentMethod { get; set; }

    [ForeignKey("CurrencyIsocode")]
    [InverseProperty("SetPeriodicFees")]
    public virtual CurrencyList CurrencyIsocodeNavigation { get; set; } = null!;

    [ForeignKey("PeriodicFeeTypeId")]
    [InverseProperty("SetPeriodicFees")]
    public virtual PeriodicFeeType PeriodicFeeType { get; set; } = null!;
}
