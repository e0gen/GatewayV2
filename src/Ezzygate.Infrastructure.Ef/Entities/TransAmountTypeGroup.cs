using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("TransAmountTypeGroup", Schema = "List")]
public partial class TransAmountTypeGroup
{
    [Key]
    [Column("TransAmountTypeGroup_id")]
    public int TransAmountTypeGroupId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }

    [ForeignKey("TransAmountTypeGroupId")]
    [InverseProperty("TransAmountTypeGroups")]
    public virtual ICollection<TransAmountType> TransAmountTypes { get; set; } = new List<TransAmountType>();
}
