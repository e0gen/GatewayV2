using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ezzygate.Infrastructure.Ef.Models;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblBllLog")]
public class BllLog
{
    [Key]
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public DateTime InsertDate { get; set; }

    [Required]
    [Column("SeverityID")]
    public LogSeverity Severity { get; set; }

    [MaxLength(200)]
    public string? Tag { get; set; }

    [MaxLength(500)]
    public string? Source { get; set; }

    [Required]
    [MaxLength(500)]
    public string Message { get; set; } = string.Empty;

    [MaxLength(8192)]
    public string? LongMessage { get; set; }

    public decimal? AppVersion { get; set; }

    [MaxLength(30)]
    [Column("Domain")]
    public string? LogDomain { get; set; }

    [MaxLength(3)]
    [Column("HTTP_ResponseCode")]
    public string? HttpCode { get; set; }
}

