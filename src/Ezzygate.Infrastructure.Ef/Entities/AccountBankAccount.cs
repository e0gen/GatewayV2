using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AccountBankAccount", Schema = "Data")]
public partial class AccountBankAccount
{
    [Key]
    [Column("AccountBankAccount_id")]
    public int AccountBankAccountId { get; set; }

    [Column("Account_id")]
    public int AccountId { get; set; }

    [Column("CurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string? CurrencyIsocode { get; set; }

    [Column("RefAccountBankAccount_id")]
    public int? RefAccountBankAccountId { get; set; }

    public bool IsDefault { get; set; }

    [StringLength(80)]
    public string? AccountName { get; set; }

    [StringLength(40)]
    [Unicode(false)]
    public string? AccountNumber { get; set; }

    [Column("IBAN")]
    [StringLength(40)]
    public string? Iban { get; set; }

    [StringLength(20)]
    public string? SwiftNumber { get; set; }

    [StringLength(50)]
    public string? BankName { get; set; }

    [StringLength(100)]
    public string? BankStreet1 { get; set; }

    [StringLength(100)]
    public string? BankStreet2 { get; set; }

    [StringLength(60)]
    public string? BankCity { get; set; }

    [StringLength(20)]
    public string? BankPostalCode { get; set; }

    [Column("BankStateISOCode")]
    [StringLength(2)]
    [Unicode(false)]
    public string? BankStateIsocode { get; set; }

    [Column("BankCountryISOCode")]
    [StringLength(2)]
    [Unicode(false)]
    public string? BankCountryIsocode { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? BankBranch { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? BankCode { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("AccountBankAccounts")]
    public virtual Account Account { get; set; } = null!;

    [InverseProperty("AccountBankAccount")]
    public virtual ICollection<AccountPayee> AccountPayees { get; set; } = new List<AccountPayee>();

    [ForeignKey("BankCountryIsocode")]
    [InverseProperty("AccountBankAccounts")]
    public virtual CountryList? BankCountryIsocodeNavigation { get; set; }

    [ForeignKey("BankStateIsocode")]
    [InverseProperty("AccountBankAccounts")]
    public virtual StateList? BankStateIsocodeNavigation { get; set; }

    [ForeignKey("CurrencyIsocode")]
    [InverseProperty("AccountBankAccounts")]
    public virtual CurrencyList? CurrencyIsocodeNavigation { get; set; }

    [InverseProperty("RefAccountBankAccount")]
    public virtual ICollection<AccountBankAccount> InverseRefAccountBankAccount { get; set; } = new List<AccountBankAccount>();

    [ForeignKey("RefAccountBankAccountId")]
    [InverseProperty("InverseRefAccountBankAccount")]
    public virtual AccountBankAccount? RefAccountBankAccount { get; set; }
}
