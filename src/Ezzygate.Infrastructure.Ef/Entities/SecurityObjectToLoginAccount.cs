using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[PrimaryKey("SecurityObjectId", "LoginAccountId")]
[Table("SecurityObjectToLoginAccount", Schema = "System")]
public partial class SecurityObjectToLoginAccount
{
    [Key]
    [Column("SecurityObject_id")]
    public int SecurityObjectId { get; set; }

    [Key]
    [Column("LoginAccount_id")]
    public int LoginAccountId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Value { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? Location { get; set; }

    [ForeignKey("LoginAccountId")]
    [InverseProperty("SecurityObjectToLoginAccounts")]
    public virtual LoginAccount LoginAccount { get; set; } = null!;

    [ForeignKey("SecurityObjectId")]
    [InverseProperty("SecurityObjectToLoginAccounts")]
    public virtual SecurityObject SecurityObject { get; set; } = null!;
}
