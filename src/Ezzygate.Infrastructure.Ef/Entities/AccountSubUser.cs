using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AccountSubUser", Schema = "Data")]
public partial class AccountSubUser
{
    [Key]
    [Column("AccountSubUser_id")]
    public int AccountSubUserId { get; set; }

    [Column("Account_id")]
    public int AccountId { get; set; }

    [Column("LoginAccount_id")]
    public int LoginAccountId { get; set; }

    [StringLength(50)]
    public string? Description { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("AccountSubUsers")]
    public virtual Account Account { get; set; } = null!;

    [ForeignKey("LoginAccountId")]
    [InverseProperty("AccountSubUsers")]
    public virtual LoginAccount LoginAccount { get; set; } = null!;

    [InverseProperty("AccountSubUser")]
    public virtual ICollection<MobileDevice> MobileDevices { get; set; } = new List<MobileDevice>();
}
