using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblLog_NoConnection")]
[Index("LncInsertDate", Name = "IX_tblLog_NoConnection_InsertDate")]
public partial class TblLogNoConnection
{
    [Key]
    [Column("LogNoConnection_id")]
    public int LogNoConnectionId { get; set; }

    [Column("Lnc_InsertDate", TypeName = "datetime")]
    public DateTime LncInsertDate { get; set; }

    [Column("Lnc_DebitReturnCode")]
    [StringLength(10)]
    public string LncDebitReturnCode { get; set; } = null!;

    [Column("Lnc_DebitReferenceCode")]
    [StringLength(30)]
    public string LncDebitReferenceCode { get; set; } = null!;

    [Column("Lnc_companyName")]
    [StringLength(500)]
    public string LncCompanyName { get; set; } = null!;

    [Column("Lnc_CompanyID")]
    public int LncCompanyId { get; set; }

    [Column("Lnc_TransactionTypeID")]
    public int LncTransactionTypeId { get; set; }

    [Column("Lnc_Amount", TypeName = "money")]
    public decimal LncAmount { get; set; }

    [Column("Lnc_Currency")]
    public byte LncCurrency { get; set; }

    [Column("Lnc_TerminalNumber")]
    [StringLength(20)]
    public string LncTerminalNumber { get; set; } = null!;

    [Column("Lnc_IpAddress")]
    [StringLength(50)]
    public string LncIpAddress { get; set; } = null!;

    [Column("Lnc_DebitCompany")]
    public byte LncDebitCompany { get; set; }

    [Column("lnc_TransactionFailID")]
    public int LncTransactionFailId { get; set; }

    [Column("Lnc_HTTP_Error")]
    public string LncHttpError { get; set; } = null!;

    [Column("lnc_AutoRefundStatus")]
    public int LncAutoRefundStatus { get; set; }

    [Column("lnc_AutoRefundDate", TypeName = "datetime")]
    public DateTime LncAutoRefundDate { get; set; }

    [Column("lnc_AutoRefundReply")]
    [StringLength(20)]
    public string LncAutoRefundReply { get; set; } = null!;
}
