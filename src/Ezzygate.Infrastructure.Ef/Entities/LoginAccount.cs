using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("LoginAccount", Schema = "Data")]
public partial class LoginAccount
{
    [Key]
    [Column("LoginAccount_id")]
    public int LoginAccountId { get; set; }

    [Column("LoginRole_id")]
    public byte LoginRoleId { get; set; }

    public bool IsActive { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? LoginUser { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? LoginEmail { get; set; }

    public byte? FailCount { get; set; }

    [Precision(2)]
    public DateTime? LastFailTime { get; set; }

    [Precision(2)]
    public DateTime? LastSuccessTime { get; set; }

    [Precision(2)]
    public DateTime? BlockEndTime { get; set; }

    [InverseProperty("LoginAccount")]
    public virtual ICollection<AccountSubUser> AccountSubUsers { get; set; } = new List<AccountSubUser>();

    [InverseProperty("LoginAccount")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    [InverseProperty("LoginAccount")]
    public virtual ICollection<AdminUser> AdminUsers { get; set; } = new List<AdminUser>();

    [InverseProperty("LoginAccount")]
    public virtual ICollection<LoginHistory> LoginHistories { get; set; } = new List<LoginHistory>();

    [InverseProperty("LoginAccount")]
    public virtual ICollection<LoginPassword> LoginPasswords { get; set; } = new List<LoginPassword>();

    [ForeignKey("LoginRoleId")]
    [InverseProperty("LoginAccounts")]
    public virtual LoginRole LoginRole { get; set; } = null!;

    [InverseProperty("LoginAccount")]
    public virtual ICollection<NewSecurityObjectToLoginAccount> NewSecurityObjectToLoginAccounts { get; set; } = new List<NewSecurityObjectToLoginAccount>();

    [InverseProperty("LoginAccount")]
    public virtual ICollection<SecurityObjectToLoginAccount> SecurityObjectToLoginAccounts { get; set; } = new List<SecurityObjectToLoginAccount>();
}
