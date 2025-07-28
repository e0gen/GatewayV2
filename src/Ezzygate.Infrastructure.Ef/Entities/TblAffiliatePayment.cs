using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblAffiliatePayments")]
public partial class TblAffiliatePayment
{
    [Key]
    [Column("AFP_ID")]
    public int AfpId { get; set; }

    [Column("AFP_InsertDate", TypeName = "datetime")]
    public DateTime AfpInsertDate { get; set; }

    [Column("AFP_Affiliate_ID")]
    public int AfpAffiliateId { get; set; }

    [Column("AFP_TransPaymentID")]
    public int AfpTransPaymentId { get; set; }

    [Column("AFP_FeeRatio", TypeName = "money")]
    public decimal AfpFeeRatio { get; set; }

    [Column("AFP_PaymentAmount", TypeName = "money")]
    public decimal AfpPaymentAmount { get; set; }

    [Column("AFP_PaymentNote")]
    [StringLength(50)]
    public string AfpPaymentNote { get; set; } = null!;

    [Column("AFP_CompanyID")]
    public int AfpCompanyId { get; set; }

    [Column("AFP_PaymentCurrency")]
    public int AfpPaymentCurrency { get; set; }

    [ForeignKey("AfpAffiliateId")]
    [InverseProperty("TblAffiliatePayments")]
    public virtual TblAffiliate AfpAffiliate { get; set; } = null!;

    [ForeignKey("AfpPaymentCurrency")]
    [InverseProperty("TblAffiliatePayments")]
    public virtual TblSystemCurrency AfpPaymentCurrencyNavigation { get; set; } = null!;

    [InverseProperty("AfsAfp")]
    public virtual ICollection<TblAffiliateFeeStep> TblAffiliateFeeSteps { get; set; } = new List<TblAffiliateFeeStep>();

    [InverseProperty("AfplAfp")]
    public virtual ICollection<TblAffiliatePaymentsLine> TblAffiliatePaymentsLines { get; set; } = new List<TblAffiliatePaymentsLine>();

    [InverseProperty("AffiliatePayments")]
    public virtual ICollection<TblWireMoney> TblWireMoneys { get; set; } = new List<TblWireMoney>();

    [InverseProperty("AffiliateSettlement")]
    public virtual ICollection<Wire> Wires { get; set; } = new List<Wire>();
}
