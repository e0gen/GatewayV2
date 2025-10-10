using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("LoginHistory", Schema = "Log")]
public partial class LoginHistory
{
    [Key]
    [Column("LoginHistory_id")]
    public int LoginHistoryId { get; set; }

    [Column("LoginAccount_id")]
    public int LoginAccountId { get; set; }

    [Column("LoginResult_id")]
    public byte? LoginResultId { get; set; }

    [Precision(2)]
    public DateTime InsertDate { get; set; }

    [Column("IPAddress")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Ipaddress { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? VariableChar { get; set; }

    [ForeignKey("LoginAccountId")]
    [InverseProperty("LoginHistories")]
    public virtual LoginAccount LoginAccount { get; set; } = null!;

    [ForeignKey("LoginResultId")]
    [InverseProperty("LoginHistories")]
    public virtual LoginResult? LoginResult { get; set; }
}
