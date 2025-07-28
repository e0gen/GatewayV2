using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblLogMasavDetails")]
[Index("CompanyId", Name = "IX_tblLogMasavDetails_Company_id")]
[Index("WireMoneyId", Name = "IX_tblLogMasavDetails_WireMoney_id")]
[Index("LogMasavFileId", Name = "IX_tblLogMasavDetails_logMasavFile_id")]
public partial class TblLogMasavDetail
{
    [Key]
    [Column("logMasavDetails_id")]
    public int LogMasavDetailsId { get; set; }

    [Column("logMasavFile_id")]
    public int LogMasavFileId { get; set; }

    [Column("Company_id")]
    public int CompanyId { get; set; }

    [Column("WireMoney_id")]
    public int WireMoneyId { get; set; }

    [StringLength(200)]
    public string PayeeName { get; set; } = null!;

    [StringLength(2000)]
    public string PayeeBankDetails { get; set; } = null!;

    public double Amount { get; set; }

    public byte Currency { get; set; }

    public byte LogStatus { get; set; }

    public byte SendStatus { get; set; }

    [StringLength(255)]
    public string StatusNote { get; set; } = null!;

    [StringLength(50)]
    public string StatusUserName { get; set; } = null!;
}
