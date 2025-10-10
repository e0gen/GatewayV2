using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblSystemCurrencies")]
public partial class TblSystemCurrency
{
    [Key]
    [Column("CUR_ID")]
    public int CurId { get; set; }

    [Column("CUR_BaseRate", TypeName = "smallmoney")]
    public decimal CurBaseRate { get; set; }

    [Column("CUR_ISOName")]
    [StringLength(3)]
    public string CurIsoname { get; set; } = null!;

    [Column("CUR_Symbol")]
    [StringLength(25)]
    public string? CurSymbol { get; set; }

    [Column("CUR_FullName")]
    [StringLength(100)]
    public string CurFullName { get; set; } = null!;

    [Column("CUR_ExchangeFeeInd")]
    public double CurExchangeFeeInd { get; set; }

    [Column("CUR_InsertDate", TypeName = "datetime")]
    public DateTime CurInsertDate { get; set; }

    [Column("CUR_ISOCode")]
    public int CurIsocode { get; set; }

    [Column("CUR_TransactionAmountMax", TypeName = "money")]
    public decimal CurTransactionAmountMax { get; set; }

    [Column("CUR_IsSymbolBeforeAmount")]
    public bool CurIsSymbolBeforeAmount { get; set; }

    [Column("CUR_RateRequestDate", TypeName = "datetime")]
    public DateTime? CurRateRequestDate { get; set; }

    [Column("CUR_RateValueDate", TypeName = "datetime")]
    public DateTime? CurRateValueDate { get; set; }

    [InverseProperty("Currency")]
    public virtual ICollection<SetMerchantSettlement> SetMerchantSettlements { get; set; } = new List<SetMerchantSettlement>();

    [InverseProperty("AfsCurrencyNavigation")]
    public virtual ICollection<TblAffiliateFeeStep> TblAffiliateFeeSteps { get; set; } = new List<TblAffiliateFeeStep>();

    [InverseProperty("AfpPaymentCurrencyNavigation")]
    public virtual ICollection<TblAffiliatePayment> TblAffiliatePayments { get; set; } = new List<TblAffiliatePayment>();

    [InverseProperty("BaCurrencyNavigation")]
    public virtual ICollection<TblBankAccount> TblBankAccounts { get; set; } = new List<TblBankAccount>();

    [InverseProperty("CurrencyShowNavigation")]
    public virtual ICollection<TblBillingCompany> TblBillingCompanies { get; set; } = new List<TblBillingCompany>();

    [InverseProperty("CurrencyNavigation")]
    public virtual ICollection<TblCompanyBalance> TblCompanyBalances { get; set; } = new List<TblCompanyBalance>();

    [InverseProperty("CffCurrencyNavigation")]
    public virtual ICollection<TblCompany> TblCompanyCffCurrencyNavigations { get; set; } = new List<TblCompany>();

    [InverseProperty("CcfCurrency")]
    public virtual ICollection<TblCompanyCreditFee> TblCompanyCreditFees { get; set; } = new List<TblCompanyCreditFee>();

    [InverseProperty("PaymentCurrencyNavigation")]
    public virtual ICollection<TblCompanyMakePaymentsRequest> TblCompanyMakePaymentsRequests { get; set; } = new List<TblCompanyMakePaymentsRequest>();

    [InverseProperty("CpmCurrency")]
    public virtual ICollection<TblCompanyPaymentMethod> TblCompanyPaymentMethods { get; set; } = new List<TblCompanyPaymentMethod>();

    [InverseProperty("PaymentReceiveCurrencyNavigation")]
    public virtual ICollection<TblCompany> TblCompanyPaymentReceiveCurrencyNavigations { get; set; } = new List<TblCompany>();

    [InverseProperty("CurrencyNavigation")]
    public virtual ICollection<TblCompanyTransApproval> TblCompanyTransApprovals { get; set; } = new List<TblCompanyTransApproval>();

    [InverseProperty("CurrencyNavigation")]
    public virtual ICollection<TblCompanyTransFail> TblCompanyTransFails { get; set; } = new List<TblCompanyTransFail>();

    [InverseProperty("CurrencyNavigation")]
    public virtual ICollection<TblCompanyTransPass> TblCompanyTransPasses { get; set; } = new List<TblCompanyTransPass>();

    [InverseProperty("TransCurrencyNavigation")]
    public virtual ICollection<TblCompanyTransPending> TblCompanyTransPendings { get; set; } = new List<TblCompanyTransPending>();

    [InverseProperty("IdCurrencyNavigation")]
    public virtual ICollection<TblInvoiceDocument> TblInvoiceDocuments { get; set; } = new List<TblInvoiceDocument>();

    [InverseProperty("IlCurrencyNavigation")]
    public virtual ICollection<TblInvoiceLine> TblInvoiceLines { get; set; } = new List<TblInvoiceLine>();

    [InverseProperty("Currency")]
    public virtual ICollection<TblPeriodicFeeType> TblPeriodicFeeTypes { get; set; } = new List<TblPeriodicFeeType>();

    [InverseProperty("RcCurrencyNavigation")]
    public virtual ICollection<TblRecurringCharge> TblRecurringCharges { get; set; } = new List<TblRecurringCharge>();

    [InverseProperty("RsCurrencyNavigation")]
    public virtual ICollection<TblRecurringSeries> TblRecurringSeries { get; set; } = new List<TblRecurringSeries>();

    [InverseProperty("RefundAskCurrencyNavigation")]
    public virtual ICollection<TblRefundAsk> TblRefundAsks { get; set; } = new List<TblRefundAsk>();

    [InverseProperty("CurrencyNavigation")]
    public virtual ICollection<TblTransactionPay> TblTransactionPays { get; set; } = new List<TblTransactionPay>();
}
