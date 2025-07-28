using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewCustomerTran
{
    [StringLength(8)]
    [Unicode(false)]
    public string TransStatus { get; set; } = null!;

    [StringLength(200)]
    public string MerchantName { get; set; } = null!;

    [StringLength(50)]
    public string SourceName { get; set; } = null!;

    [StringLength(50)]
    public string SourceNameHeb { get; set; } = null!;

    [StringLength(550)]
    public string ReplyText { get; set; } = null!;

    [StringLength(550)]
    public string ReplyTextHeb { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    [StringLength(500)]
    public string? Comment { get; set; }

    public int? Currency { get; set; }

    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [StringLength(50)]
    public string PaymentMethodDisplay { get; set; } = null!;

    public byte Payments { get; set; }

    [StringLength(50)]
    public string ReplyCode { get; set; } = null!;

    [Column("CustomerID")]
    public int CustomerId { get; set; }
}
