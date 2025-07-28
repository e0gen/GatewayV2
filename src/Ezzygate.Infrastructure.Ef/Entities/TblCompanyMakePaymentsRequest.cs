using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCompanyMakePaymentsRequests")]
public partial class TblCompanyMakePaymentsRequest
{
    [Key]
    [Column("CompanyMakePaymentsRequests_id")]
    public int CompanyMakePaymentsRequestsId { get; set; }

    [Column("paymentType")]
    public byte PaymentType { get; set; }

    [Column("paymentDate", TypeName = "smalldatetime")]
    public DateTime PaymentDate { get; set; }

    [Column("paymentAmount")]
    public double PaymentAmount { get; set; }

    [Column("paymentExchangeRate")]
    public double PaymentExchangeRate { get; set; }

    [Column("paymentMerchantComment")]
    [StringLength(250)]
    public string PaymentMerchantComment { get; set; } = null!;

    [Column("bankIsraelInfo_PayeeName")]
    [StringLength(80)]
    public string BankIsraelInfoPayeeName { get; set; } = null!;

    [Column("bankIsraelInfo_CompanyLegalNumber")]
    [StringLength(80)]
    public string BankIsraelInfoCompanyLegalNumber { get; set; } = null!;

    [Column("bankIsraelInfo_personalIdNumber")]
    [StringLength(80)]
    public string BankIsraelInfoPersonalIdNumber { get; set; } = null!;

    [Column("bankIsraelInfo_bankBranch")]
    [StringLength(5)]
    public string BankIsraelInfoBankBranch { get; set; } = null!;

    [Column("bankIsraelInfo_AccountNumber")]
    [StringLength(80)]
    public string BankIsraelInfoAccountNumber { get; set; } = null!;

    [Column("bankIsraelInfo_PaymentMethod")]
    [StringLength(80)]
    public string BankIsraelInfoPaymentMethod { get; set; } = null!;

    [Column("bankIsraelInfo_BankCode")]
    public int BankIsraelInfoBankCode { get; set; }

    [Column("Company_id")]
    public int? CompanyId { get; set; }

    [Column("CompanyMakePaymentsProfiles_id")]
    public int? CompanyMakePaymentsProfilesId { get; set; }

    [Column("paymentCurrency")]
    public int? PaymentCurrency { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("TblCompanyMakePaymentsRequests")]
    public virtual TblCompany? Company { get; set; }

    [ForeignKey("CompanyMakePaymentsProfilesId")]
    [InverseProperty("TblCompanyMakePaymentsRequests")]
    public virtual TblCompanyMakePaymentsProfile? CompanyMakePaymentsProfiles { get; set; }

    [ForeignKey("PaymentCurrency")]
    [InverseProperty("TblCompanyMakePaymentsRequests")]
    public virtual TblSystemCurrency? PaymentCurrencyNavigation { get; set; }

    [InverseProperty("PaymentOrder")]
    public virtual ICollection<TblWireMoney> TblWireMoneys { get; set; } = new List<TblWireMoney>();
}
