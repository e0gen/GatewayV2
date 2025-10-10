using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AccountCryptoWallet", Schema = "Data")]
public partial class AccountCryptoWallet
{
    [Key]
    [Column("AccountCryptoWallet_id")]
    public int AccountCryptoWalletId { get; set; }

    [Column("Account_id")]
    public int AccountId { get; set; }

    [MaxLength(100)]
    public byte[]? PrivateKey { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? PublicKey { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Address { get; set; }

    public long? LastTransHeight { get; set; }
}
