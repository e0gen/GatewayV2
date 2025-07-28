using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCompanyBatchFiles")]
public partial class TblCompanyBatchFile
{
    [Key]
    [Column("CompanyBatchFiles_id")]
    public int CompanyBatchFilesId { get; set; }

    [Column("CBFCompany_id")]
    public int CbfcompanyId { get; set; }

    [Column("CBFFileName")]
    [StringLength(255)]
    public string CbffileName { get; set; } = null!;

    [Column("CBFFileType")]
    public byte CbffileType { get; set; }

    [Column("CBFInsDate", TypeName = "datetime")]
    public DateTime CbfinsDate { get; set; }

    [Column("CBFParseDate", TypeName = "datetime")]
    public DateTime CbfparseDate { get; set; }

    [Column("CBFStatus")]
    public byte Cbfstatus { get; set; }

    [Column("CBFTotalRows")]
    public int CbftotalRows { get; set; }

    [Column("CBFRows")]
    public int Cbfrows { get; set; }

    [ForeignKey("CbfcompanyId")]
    [InverseProperty("TblCompanyBatchFiles")]
    public virtual TblCompany Cbfcompany { get; set; } = null!;
}
