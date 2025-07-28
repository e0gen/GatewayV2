using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[PrimaryKey("SecurityObjectId", "LoginAccountId")]
[Table("new_SecurityObjectToLoginAccount", Schema = "System")]
public partial class NewSecurityObjectToLoginAccount
{
    [Key]
    [Column("SecurityObject_id")]
    public int SecurityObjectId { get; set; }

    [Key]
    [Column("LoginAccount_id")]
    public int LoginAccountId { get; set; }

    public bool Value { get; set; }

    [ForeignKey("LoginAccountId")]
    [InverseProperty("NewSecurityObjectToLoginAccounts")]
    public virtual LoginAccount LoginAccount { get; set; } = null!;

    [ForeignKey("SecurityObjectId")]
    [InverseProperty("NewSecurityObjectToLoginAccounts")]
    public virtual NewSecurityObject SecurityObject { get; set; } = null!;
}
