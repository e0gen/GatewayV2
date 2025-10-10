using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewTerminalsWithZeroFee
{
    [StringLength(200)]
    public string? Merchant { get; set; }

    [Column("Payment Method")]
    [StringLength(591)]
    public string? PaymentMethod { get; set; }

    [Column("Debit Company")]
    [StringLength(237)]
    public string? DebitCompany { get; set; }

    [Column("Terminal Name")]
    [StringLength(80)]
    public string? TerminalName { get; set; }

    [Column("Terminal No.")]
    [StringLength(20)]
    public string? TerminalNo { get; set; }

    [Column("Trans. Fee")]
    [StringLength(20)]
    [Unicode(false)]
    public string? TransFee { get; set; }

    [Column("Clearing Fee")]
    [StringLength(20)]
    [Unicode(false)]
    public string? ClearingFee { get; set; }

    [Column("Pre-Auth Fee")]
    [StringLength(20)]
    [Unicode(false)]
    public string? PreAuthFee { get; set; }

    [Column("Refund Fee")]
    [StringLength(20)]
    [Unicode(false)]
    public string? RefundFee { get; set; }

    [Column("Copy R. Fee")]
    [StringLength(20)]
    [Unicode(false)]
    public string? CopyRFee { get; set; }

    [Column("CHB Fee")]
    [StringLength(20)]
    [Unicode(false)]
    public string? ChbFee { get; set; }

    [Column("Fail Fee")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FailFee { get; set; }

    [StringLength(130)]
    [Unicode(false)]
    public string? Edit { get; set; }

    [Column("MerchantID")]
    public int? MerchantId { get; set; }

    [Column("DebitCompanyID")]
    public int? DebitCompanyId { get; set; }
}
