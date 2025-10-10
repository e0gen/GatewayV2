using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblBankAccounts")]
public partial class TblBankAccount
{
    [Key]
    [Column("BA_ID")]
    public int BaId { get; set; }

    [Column("BA_InsDate", TypeName = "datetime")]
    public DateTime? BaInsDate { get; set; }

    [Column("BA_Update", TypeName = "datetime")]
    public DateTime? BaUpdate { get; set; }

    [Column("BA_BankName")]
    [StringLength(50)]
    public string BaBankName { get; set; } = null!;

    [Column("BA_AccountName")]
    [StringLength(50)]
    public string BaAccountName { get; set; } = null!;

    [Column("BA_AccountNumber")]
    [StringLength(50)]
    public string BaAccountNumber { get; set; } = null!;

    [Column("BA_Currency")]
    public int BaCurrency { get; set; }

    [ForeignKey("BaCurrency")]
    [InverseProperty("TblBankAccounts")]
    public virtual TblSystemCurrency BaCurrencyNavigation { get; set; } = null!;
}
