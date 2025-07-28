using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("PaymentMethodGroup", Schema = "List")]
[Index("ShortName", Name = "UNC_PaymentMethodGroup_ShortName", IsUnique = true)]
public partial class PaymentMethodGroup
{
    [Key]
    [Column("PaymentMethodGroup_id")]
    public byte PaymentMethodGroupId { get; set; }

    [Column("pmg_Type")]
    public int? PmgType { get; set; }

    [StringLength(80)]
    [Unicode(false)]
    public string? Name { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string? ShortName { get; set; }

    public byte? SortOrder { get; set; }

    public bool IsPopular { get; set; }

    [InverseProperty("PaymentMethodGroup")]
    public virtual ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();
}
