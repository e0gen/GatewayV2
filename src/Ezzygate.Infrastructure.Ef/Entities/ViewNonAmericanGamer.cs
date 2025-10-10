using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewNonAmericanGamer
{
    [Column("Email Address")]
    [StringLength(80)]
    public string EmailAddress { get; set; } = null!;

    [Column("Card Holder")]
    [StringLength(100)]
    public string CardHolder { get; set; } = null!;

    [Column("Card Type")]
    [StringLength(80)]
    public string? CardType { get; set; }

    [Column("Transaction Count")]
    public int? TransactionCount { get; set; }

    [Column("First Transaction", TypeName = "datetime")]
    public DateTime? FirstTransaction { get; set; }

    [Column("Last Transaction", TypeName = "datetime")]
    public DateTime? LastTransaction { get; set; }
}
