using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewImportChargebackExcel
{
    [Column("MerchantID")]
    public int MerchantId { get; set; }

    [StringLength(200)]
    public string MerchantName { get; set; } = null!;

    [Column("TransID")]
    public int TransId { get; set; }

    [StringLength(40)]
    public string TransDebitRef { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal TransAmount { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string TransCurrency { get; set; } = null!;

    [Column("RefundID")]
    public int? RefundId { get; set; }

    [StringLength(40)]
    public string? RefundDebitRef { get; set; }

    public DateOnly? RefundDate { get; set; }

    [Column(TypeName = "money")]
    public decimal? RefundAmount { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string? RefundCurrency { get; set; }

    public byte? ChargebackStatus { get; set; }

    public DateOnly? ChargebackDate { get; set; }

    [StringLength(500)]
    public string ChargebackComment { get; set; } = null!;

    [Column("MRF")]
    [StringLength(30)]
    [Unicode(false)]
    public string Mrf { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string Reason { get; set; } = null!;

    public DateOnly? FileDate { get; set; }

    [Column("CaseID")]
    [StringLength(30)]
    [Unicode(false)]
    public string CaseId { get; set; } = null!;
}
