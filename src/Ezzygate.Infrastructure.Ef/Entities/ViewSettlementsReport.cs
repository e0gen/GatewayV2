using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewSettlementsReport
{
    [Column(TypeName = "datetime")]
    public DateTime PayDate { get; set; }

    [Column("id")]
    public int Id { get; set; }

    [Column("transPayTotal")]
    public double TransPayTotal { get; set; }

    [Column("transChargeTotal")]
    public double TransChargeTotal { get; set; }

    [Column("tp_Note")]
    [StringLength(255)]
    public string? TpNote { get; set; }

    [Column("CompanyID")]
    public int CompanyId { get; set; }

    [Column("isShow")]
    public bool IsShow { get; set; }

    [Column("currency")]
    public int? Currency { get; set; }

    public int InvoiceNumber { get; set; }

    [Column("wireFee", TypeName = "money")]
    public decimal? WireFee { get; set; }

    public byte? WireStatus { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? WireStatusDate { get; set; }

    [StringLength(100)]
    public string? WireFileName { get; set; }

    [Column("id_BillingCompanyID")]
    public int? IdBillingCompanyId { get; set; }

    [Column("id_Type")]
    public int? IdType { get; set; }
}
