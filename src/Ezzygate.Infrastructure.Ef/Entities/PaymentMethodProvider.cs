using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("PaymentMethodProvider", Schema = "List")]
public partial class PaymentMethodProvider
{
    [Key]
    [Column("PaymentMethodProvider_id")]
    [StringLength(16)]
    [Unicode(false)]
    public string PaymentMethodProviderId { get; set; } = null!;

    [StringLength(30)]
    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    [InverseProperty("PaymentMethodProvider")]
    public virtual ICollection<AccountPaymentMethod> AccountPaymentMethods { get; set; } = new List<AccountPaymentMethod>();

    [InverseProperty("PaymentMethodProvider")]
    public virtual ICollection<PreCreatedPaymentMethod> PreCreatedPaymentMethods { get; set; } = new List<PreCreatedPaymentMethod>();
}
