using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("RecurringModify", Schema = "Log")]
public partial class RecurringModify
{
    [Key]
    [Column("RecurringModify_id")]
    public int RecurringModifyId { get; set; }

    [Column("Merchant_id")]
    public int? MerchantId { get; set; }

    [Column("RecurringSeries_id")]
    public int? RecurringSeriesId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column("IPAddress")]
    [StringLength(20)]
    public string? Ipaddress { get; set; }

    [StringLength(10)]
    public string? Action { get; set; }

    [StringLength(1000)]
    public string? RequestString { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("RecurringModifies")]
    public virtual TblCompany? Merchant { get; set; }
}
