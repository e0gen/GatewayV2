using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("ApplicationIdentityToPaymentMethod", Schema = "Data")]
public partial class ApplicationIdentityToPaymentMethod
{
    [Key]
    [Column("ApplicationIdentityToPaymentMethod_id")]
    public int ApplicationIdentityToPaymentMethodId { get; set; }

    [Column("ApplicationIdentity_id")]
    public int ApplicationIdentityId { get; set; }

    [Column("PaymentMethod_id")]
    public short PaymentMethodId { get; set; }

    public bool IsAllowCreate { get; set; }

    public bool IsAllowProcess { get; set; }

    [ForeignKey("ApplicationIdentityId")]
    [InverseProperty("ApplicationIdentityToPaymentMethods")]
    public virtual ApplicationIdentity ApplicationIdentity { get; set; } = null!;

    [ForeignKey("PaymentMethodId")]
    [InverseProperty("ApplicationIdentityToPaymentMethods")]
    public virtual PaymentMethod PaymentMethod { get; set; } = null!;
}
