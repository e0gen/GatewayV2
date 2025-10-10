using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("History", Schema = "Log")]
[Index("HistoryTypeId", Name = "IX_LogHistory_HistoryTypeID")]
[Index("InsertDate", Name = "IX_LogHistory_InsertDate")]
public partial class History
{
    [Key]
    [Column("History_id")]
    public int HistoryId { get; set; }

    [Column("HistoryType_id")]
    public byte HistoryTypeId { get; set; }

    [Column("Merchant_id")]
    public int? MerchantId { get; set; }

    public int? SourceIdentity { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime InsertDate { get; set; }

    [StringLength(50)]
    public string? InsertUserName { get; set; }

    [Column("InsertIPAddress")]
    [StringLength(50)]
    public string? InsertIpaddress { get; set; }

    [StringLength(4000)]
    public string? VariableChar { get; set; }

    [Column("VariableXML", TypeName = "xml")]
    public string? VariableXml { get; set; }

    [ForeignKey("HistoryTypeId")]
    [InverseProperty("Histories")]
    public virtual HistoryType HistoryType { get; set; } = null!;
}
