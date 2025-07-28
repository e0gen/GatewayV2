using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblLogCreditCardWhitelist")]
[Index("LccwInsertDate", Name = "IX_tblLogCreditCardWhitelist_InsertDate", AllDescending = true)]
[Index("LccwMerchant", Name = "IX_tblLogCreditCardWhitelist_Merchant")]
[Index("LccwTransaction", "LccwTransactionType", Name = "IX_tblLogCreditCardWhitelist_Transaction_TransactionType", IsUnique = true)]
[Index("LccwCcwlId", Name = "IX_tblLogCreditCardWhitelist_ccwlID")]
public partial class TblLogCreditCardWhitelist
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("lccw_ccwlID")]
    public int? LccwCcwlId { get; set; }

    [Column("lccw_Merchant")]
    public int? LccwMerchant { get; set; }

    [Column("lccw_Level")]
    public byte? LccwLevel { get; set; }

    [Column("lccw_Transaction")]
    public int? LccwTransaction { get; set; }

    [Column("lccw_TransactionType")]
    public int? LccwTransactionType { get; set; }

    [Column("lccw_InsertDate", TypeName = "datetime")]
    public DateTime LccwInsertDate { get; set; }
}
