using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("TransAmountType", Schema = "List")]
public partial class TransAmountType
{
    [Key]
    [Column("TransAmountType_id")]
    public int TransAmountTypeId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }

    [ForeignKey("TransAmountTypeId")]
    [InverseProperty("TransAmountTypes")]
    public virtual ICollection<TblDebitCompany> DebitCompanies { get; set; } = new List<TblDebitCompany>();

    [ForeignKey("TransAmountTypeId")]
    [InverseProperty("TransAmountTypes")]
    public virtual ICollection<TransAmountTypeGroup> TransAmountTypeGroups { get; set; } = new List<TransAmountTypeGroup>();
}
