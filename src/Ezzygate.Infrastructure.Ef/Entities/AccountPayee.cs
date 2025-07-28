using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AccountPayee", Schema = "Data")]
public partial class AccountPayee
{
    [Key]
    [Column("AccountPayee_id")]
    public int AccountPayeeId { get; set; }

    [Column("Account_id")]
    public int AccountId { get; set; }

    [Column("AccountBankAccount_id")]
    public int? AccountBankAccountId { get; set; }

    [StringLength(10)]
    public string? SearchTag { get; set; }

    [StringLength(10)]
    public string? LegalNumber { get; set; }

    [StringLength(50)]
    public string? CompanyName { get; set; }

    [StringLength(50)]
    public string? ContactName { get; set; }

    [StringLength(25)]
    public string? PhoneNumber { get; set; }

    [Column("faxNumber")]
    [StringLength(25)]
    public string? FaxNumber { get; set; }

    [StringLength(50)]
    public string? EmailAddress { get; set; }

    [StringLength(100)]
    public string? StreetAddress { get; set; }

    [StringLength(100)]
    public string? Comment { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("AccountPayees")]
    public virtual Account Account { get; set; } = null!;

    [ForeignKey("AccountBankAccountId")]
    [InverseProperty("AccountPayees")]
    public virtual AccountBankAccount? AccountBankAccount { get; set; }

    [InverseProperty("AccountPayee")]
    public virtual ICollection<Wire> Wires { get; set; } = new List<Wire>();
}
