using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewBnsTransactionsWithoutEpa
{
    public int Merchant { get; set; }

    [Column("Merchant Name")]
    [StringLength(25)]
    public string? MerchantName { get; set; }

    [StringLength(50)]
    public string Bank { get; set; } = null!;

    [Column("Terminal No")]
    [StringLength(22)]
    public string TerminalNo { get; set; } = null!;

    [Column("Contract No.")]
    [StringLength(14)]
    public string? ContractNo { get; set; }

    [Column("Trans ID")]
    [StringLength(10)]
    [Unicode(false)]
    public string? TransId { get; set; }

    [Column("Trans Date")]
    [StringLength(16)]
    [Unicode(false)]
    public string? TransDate { get; set; }

    [Column("Debit Ref")]
    [StringLength(15)]
    public string? DebitRef { get; set; }

    [Column("Auth No")]
    [StringLength(10)]
    public string? AuthNo { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string Currency { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string? Amount { get; set; }

    [Column("BIN")]
    public int Bin { get; set; }

    [Column("Credit Card")]
    [StringLength(20)]
    public string? CreditCard { get; set; }
}
