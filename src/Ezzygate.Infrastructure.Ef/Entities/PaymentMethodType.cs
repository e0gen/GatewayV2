using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("PaymentMethodType", Schema = "List")]
public partial class PaymentMethodType
{
    [Key]
    [Column("PaymentMethodType_id")]
    public byte PaymentMethodTypeId { get; set; }

    [StringLength(20)]
    public string Name { get; set; } = null!;
}
