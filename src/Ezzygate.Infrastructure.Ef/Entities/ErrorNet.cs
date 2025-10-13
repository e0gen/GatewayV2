using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblErrorNet")]
public class ErrorNet
{
    [Key]
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public DateTime ErrorTime { get; set; }

    [Required]
    [MaxLength(25)]
    public string ProjectName { get; set; } = string.Empty;

    [Required]
    [MaxLength(25)]
    public string RemoteIP { get; set; } = string.Empty;

    [Required]
    [MaxLength(25)]
    public string LocalIP { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string RemoteUser { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string ServerName { get; set; } = string.Empty;

    [Required]
    [MaxLength(5)]
    public string ServerPort { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string ScriptName { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string RequestQueryString { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string VirtualPath { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string PhysicalPath { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string ExceptionSource { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string ExceptionMessage { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string ExceptionTargetSite { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string ExceptionStackTrace { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string ExceptionHelpLink { get; set; } = string.Empty;

    [Required]
    public int ExceptionLineNumber { get; set; }

    [Required]
    [MaxLength(100)]
    public string InnerExceptionSource { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string InnerExceptionMessage { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string InnerExceptionTargetSite { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string InnerExceptionHelpLink { get; set; } = string.Empty;

    [Required]
    public int InnerExceptionLineNumber { get; set; }

    [Required]
    public bool IsFailedSQL { get; set; }

    [Required]
    public bool IsArchive { get; set; }

    [Required]
    [Column(TypeName = "ntext")]
    public string InnerExceptionStackTrace { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string RequestForm { get; set; } = string.Empty;

    [Required]
    public bool IsHighlighted { get; set; }

    public decimal? AppVersion { get; set; }

    [MaxLength(30)]
    public string? Domain { get; set; }

    [MaxLength(3)]
    [Column("HTTP_ResponseCode")]
    public string? HttpCode { get; set; }
}

