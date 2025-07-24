using System.ComponentModel.DataAnnotations;

namespace Ezzygate.Domain.Entities.Core;

public sealed class AccountSubUser
{
    [Key]
    public int AccountSubUserId { get; set; }

    [Required]
    public int AccountId { get; set; }

    public int? LoginAccountId { get; set; }

    [StringLength(50)]
    public string? Description { get; set; }

    public Account Account { get; set; } = null!;
    public LoginAccount? LoginAccount { get; set; }
}