using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewMerchantsMailing
{
    [Column("MerchantID")]
    public int MerchantId { get; set; }

    [StringLength(50)]
    public string MerchantNum { get; set; } = null!;

    [StringLength(200)]
    public string MerchantName { get; set; } = null!;

    [StringLength(11)]
    [Unicode(false)]
    public string Status { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string? OpenDate { get; set; }

    [StringLength(50)]
    public string AccManager { get; set; } = null!;

    [Column("isGateway")]
    public bool IsGateway { get; set; }

    [StringLength(7)]
    [Unicode(false)]
    public string Language { get; set; } = null!;

    [StringLength(504)]
    public string? ToName { get; set; }

    [StringLength(512)]
    public string? ToMail { get; set; }

    [StringLength(100)]
    public string ContactMail { get; set; } = null!;

    [StringLength(255)]
    public string AlertMail { get; set; } = null!;

    [StringLength(512)]
    public string ReportMail { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string NotifyRiskCcMail { get; set; } = null!;

    [StringLength(100)]
    public string NotifyChbMail { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string NotifyPassMail { get; set; } = null!;

    [StringLength(80)]
    public string SupportMail { get; set; } = null!;

    [Column("Login Mail")]
    [StringLength(200)]
    public string LoginMail { get; set; } = null!;

    [Column("Contact Name")]
    [StringLength(100)]
    public string ContactName { get; set; } = null!;
}
