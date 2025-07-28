using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AccountType", Schema = "List")]
public partial class AccountType
{
    [Key]
    [Column("AccountType_id")]
    public byte AccountTypeId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [InverseProperty("AccountType")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    [InverseProperty("AccountType")]
    public virtual ICollection<PeriodicFeeType> PeriodicFeeTypes { get; set; } = new List<PeriodicFeeType>();
}
