using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblImportChargebackBNS")]
public partial class TblImportChargebackBn
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("icb_IsProcessed")]
    public bool IcbIsProcessed { get; set; }

    [Column("CASE ID")]
    [StringLength(30)]
    [Unicode(false)]
    public string CaseId { get; set; } = null!;

    [Column("RSN")]
    [StringLength(30)]
    [Unicode(false)]
    public string Rsn { get; set; } = null!;

    [Column("MER-NO")]
    [StringLength(30)]
    [Unicode(false)]
    public string MerNo { get; set; } = null!;

    [Column("CARDHOLDER-NUMBER")]
    [StringLength(30)]
    [Unicode(false)]
    public string CardholderNumber { get; set; } = null!;

    [Column("TXN-DATE")]
    [StringLength(30)]
    [Unicode(false)]
    public string TxnDate { get; set; } = null!;

    [Column("AMOUNT-TXN")]
    [StringLength(30)]
    [Unicode(false)]
    public string AmountTxn { get; set; } = null!;

    [Column("CUR")]
    [StringLength(30)]
    [Unicode(false)]
    public string Cur { get; set; } = null!;

    [Column("AMOUNT-EURO")]
    [StringLength(30)]
    [Unicode(false)]
    public string AmountEuro { get; set; } = null!;

    [Column("REF-NO")]
    [StringLength(30)]
    [Unicode(false)]
    public string RefNo { get; set; } = null!;

    [Column("TICKET-NO")]
    [StringLength(30)]
    [Unicode(false)]
    public string TicketNo { get; set; } = null!;

    [Column("DEADLINE")]
    [StringLength(30)]
    [Unicode(false)]
    public string Deadline { get; set; } = null!;

    [Column("COMMENTS")]
    [StringLength(30)]
    [Unicode(false)]
    public string Comments { get; set; } = null!;

    [Column("MRF")]
    [StringLength(30)]
    [Unicode(false)]
    public string Mrf { get; set; } = null!;

    [Column("icb_User")]
    [StringLength(50)]
    [Unicode(false)]
    public string IcbUser { get; set; } = null!;

    [Column("icb_FileName")]
    [StringLength(100)]
    [Unicode(false)]
    public string IcbFileName { get; set; } = null!;

    [Column("icb_ChargebackDate", TypeName = "datetime")]
    public DateTime IcbChargebackDate { get; set; }

    [Column("icb_IsPhotocopy")]
    public bool IcbIsPhotocopy { get; set; }
}
