using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewCustomerTransPass
{
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

    [Column("id")]
    public int Id { get; set; }

    [StringLength(500)]
    public string? Comment { get; set; }

    [StringLength(50)]
    public string ReplyCode { get; set; } = null!;

    public byte Payments { get; set; }

    [StringLength(50)]
    public string PaymentMethodDisplay { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    public int? Currency { get; set; }

    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    [Column("CustomerID")]
    public int CustomerId { get; set; }
}
