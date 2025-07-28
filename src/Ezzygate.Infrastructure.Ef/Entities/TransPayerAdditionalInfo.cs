using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

/// <summary>
/// Transaction payer extra info as requested by the merchant
/// </summary>
[Table("TransPayerAdditionalInfo", Schema = "Trans")]
public partial class TransPayerAdditionalInfo
{
    [Key]
    [Column("TransPayerAdditionalInfo_id")]
    public int TransPayerAdditionalInfoId { get; set; }

    [StringLength(25)]
    public string FieldLabel { get; set; } = null!;

    [StringLength(50)]
    public string FieldValue { get; set; } = null!;

    [Column("TransPayerInfo_id")]
    public int TransPayerInfoId { get; set; }

    [ForeignKey("TransPayerInfoId")]
    [InverseProperty("TransPayerAdditionalInfos")]
    public virtual TransPayerInfo TransPayerInfo { get; set; } = null!;
}
