using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCompany")]
[Index("ActiveStatus", Name = "IX_tblCompany_ActiveStatus")]
[Index("CustomerNumber", Name = "IX_tblCompany_CustomerNumber", IsUnique = true)]
[Index("MerchantLinkName", Name = "IX_tblCompany_MerchantLinkName")]
[Index("PayPercent", "CompanyName", "SecurityDeposit", "PayingDaysMargin", "PayingDaysMarginInitial", Name = "IX_tblCompany_PayPercent", IsDescending = new[] { true, false, false, false, false })]
[Index("IsHidePayNeed", Name = "IX_tblCompany_PayingNoNeed")]
public partial class TblCompany
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(50)]
    public string CustomerNumber { get; set; } = null!;

    [StringLength(150)]
    public string FirstName { get; set; } = null!;

    [StringLength(150)]
    public string LastName { get; set; } = null!;

    [StringLength(50)]
    public string Phone { get; set; } = null!;

    [Column("cellular")]
    [StringLength(50)]
    public string Cellular { get; set; } = null!;

    [Column("IDnumber")]
    [StringLength(15)]
    public string Idnumber { get; set; } = null!;

    [StringLength(200)]
    public string CompanyName { get; set; } = null!;

    [StringLength(200)]
    public string CompanyLegalName { get; set; } = null!;

    [StringLength(15)]
    public string CompanyLegalNumber { get; set; } = null!;

    [StringLength(50)]
    public string CompanyPhone { get; set; } = null!;

    [StringLength(50)]
    public string CompanyFax { get; set; } = null!;

    [Column("CompanyIndustry_id")]
    public short CompanyIndustryId { get; set; }

    [StringLength(200)]
    public string BillingFor { get; set; } = null!;

    [Column("comment")]
    [StringLength(150)]
    public string Comment { get; set; } = null!;

    [Column("comment_misc")]
    [StringLength(4000)]
    public string CommentMisc { get; set; } = null!;

    [StringLength(50)]
    public string UserName { get; set; } = null!;

    [StringLength(200)]
    public string Mail { get; set; } = null!;

    [Column("URL")]
    [StringLength(500)]
    public string Url { get; set; } = null!;

    [Column("merchantSupportEmail")]
    [StringLength(80)]
    public string MerchantSupportEmail { get; set; } = null!;

    [Column("merchantSupportPhoneNum")]
    [StringLength(20)]
    public string MerchantSupportPhoneNum { get; set; } = null!;

    [Column("merchantOpenningDate", TypeName = "smalldatetime")]
    public DateTime MerchantOpenningDate { get; set; }

    [Column("merchantClosingDate", TypeName = "smalldatetime")]
    public DateTime MerchantClosingDate { get; set; }

    public bool Blocked { get; set; }

    public bool Closed { get; set; }

    [StringLength(50)]
    public string IpOnReg { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column("transCcStorageShekel", TypeName = "money")]
    public decimal TransCcStorageShekel { get; set; }

    [Column("transCcStorageDollar", TypeName = "money")]
    public decimal TransCcStorageDollar { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal MakePaymentsFeeShekel { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal MakePaymentsFeeDollar { get; set; }

    [Column(TypeName = "money")]
    public decimal MinimumPayout { get; set; }

    [Column("ChargeLimit_Shekel", TypeName = "money")]
    public decimal ChargeLimitShekel { get; set; }

    [Column("ChargeLimit_Dollar", TypeName = "money")]
    public decimal ChargeLimitDollar { get; set; }

    public int? PaymentReceiveCurrency { get; set; }

    [StringLength(80)]
    public string PaymentMethod { get; set; } = null!;

    [StringLength(80)]
    public string PaymentPayeeName { get; set; } = null!;

    public int? PaymentBank { get; set; }

    [StringLength(80)]
    public string PaymentBranch { get; set; } = null!;

    [StringLength(80)]
    public string PaymentAccount { get; set; } = null!;

    [StringLength(80)]
    public string PaymentAbroadAccountName { get; set; } = null!;

    [StringLength(80)]
    public string PaymentAbroadAccountNumber { get; set; } = null!;

    [StringLength(80)]
    public string PaymentAbroadBankName { get; set; } = null!;

    [StringLength(200)]
    public string PaymentAbroadBankAddress { get; set; } = null!;

    [StringLength(80)]
    public string PaymentAbroadSwiftNumber { get; set; } = null!;

    [Column("PaymentAbroadIBAN")]
    [StringLength(80)]
    public string PaymentAbroadIban { get; set; } = null!;

    [StringLength(80)]
    public string PaymentAbroadAccountName2 { get; set; } = null!;

    [StringLength(80)]
    public string PaymentAbroadAccountNumber2 { get; set; } = null!;

    [StringLength(80)]
    public string PaymentAbroadBankName2 { get; set; } = null!;

    [StringLength(80)]
    public string PaymentAbroadBankAddress2 { get; set; } = null!;

    [StringLength(80)]
    public string PaymentAbroadSwiftNumber2 { get; set; } = null!;

    [Column("PaymentAbroadIBAN2")]
    [StringLength(80)]
    public string PaymentAbroadIban2 { get; set; } = null!;

    [Column("PaymentAbroadABA2")]
    [StringLength(80)]
    public string PaymentAbroadAba2 { get; set; } = null!;

    [StringLength(80)]
    public string PaymentAbroadSortCode2 { get; set; } = null!;

    [Column("CCardExpMM")]
    [StringLength(50)]
    public string CcardExpMm { get; set; } = null!;

    [Column("CCardExpYY")]
    [StringLength(50)]
    public string CcardExpYy { get; set; } = null!;

    [Column("CCardHolderName")]
    [StringLength(100)]
    public string CcardHolderName { get; set; } = null!;

    [Column("CCardCUI")]
    [StringLength(10)]
    public string CcardCui { get; set; } = null!;

    [Column("BillingCompanys_id")]
    public int? BillingCompanysId { get; set; }

    [Column("isChargeVAT")]
    public bool IsChargeVat { get; set; }

    public bool IsSystemPayEcheck { get; set; }

    public bool IsSystemPay { get; set; }

    [Column("IsSystemPayCVV2")]
    public bool IsSystemPayCvv2 { get; set; }

    public bool IsSystemPayPersonalNumber { get; set; }

    public bool IsSystemPayPhoneNumber { get; set; }

    public bool IsSystemPayEmail { get; set; }

    public bool IsRemoteChargeEcheck { get; set; }

    [Column("IsRemoteChargeEchCVV2")]
    public bool IsRemoteChargeEchCvv2 { get; set; }

    public bool IsRemoteChargeEchPersonalNumber { get; set; }

    public bool IsRemoteChargeEchPhoneNumber { get; set; }

    public bool IsRemoteChargeEchEmail { get; set; }

    public bool IsRemoteCharge { get; set; }

    [Column("IsRemoteChargeCVV2")]
    public bool IsRemoteChargeCvv2 { get; set; }

    public bool IsRemoteChargePersonalNumber { get; set; }

    public bool IsRemoteChargePhoneNumber { get; set; }

    public bool IsRemoteChargeEmail { get; set; }

    public bool IsCustomerPurchaseEcheck { get; set; }

    public bool IsCustomerPurchase { get; set; }

    [Column("IsCustomerPurchaseCVV2")]
    public bool IsCustomerPurchaseCvv2 { get; set; }

    public bool IsCustomerPurchasePersonalNumber { get; set; }

    public bool IsCustomerPurchasePhoneNumber { get; set; }

    public bool IsCustomerPurchaseEmail { get; set; }

    [Column("IsCustomerPurchasePayerID")]
    public bool IsCustomerPurchasePayerId { get; set; }

    public bool IsWindowChargeEwallet { get; set; }

    public bool IsWindowChargeEwalletMust { get; set; }

    public bool IsWindowChargeHiddenLogin { get; set; }

    [Column("CustomerPurchasePayerIDText")]
    [StringLength(150)]
    public string CustomerPurchasePayerIdtext { get; set; } = null!;

    public bool IsPublicPay { get; set; }

    [Column("IsPublicPayCVV2")]
    public bool IsPublicPayCvv2 { get; set; }

    public bool IsPublicPayPersonalNumber { get; set; }

    public bool IsPublicPayPhoneNumber { get; set; }

    public bool IsPublicPayEmail { get; set; }

    public bool IsMultiChargeProtection { get; set; }

    public bool IsBillingAddressMust { get; set; }

    [Column("IsCVV2")]
    public bool IsCvv2 { get; set; }

    public bool IsPersonalNumber { get; set; }

    public bool IsPhoneNumber { get; set; }

    public bool IsEmail { get; set; }

    public bool IsRefund { get; set; }

    public bool IsAskRefund { get; set; }

    public bool IsAllowRecurring { get; set; }

    public bool IsConfirmation { get; set; }

    public bool IsApprovalOnly { get; set; }

    public bool IsAutoPersonalSignup { get; set; }

    public bool IsCcStorage { get; set; }

    public bool IsCcStorageCharge { get; set; }

    public bool IsTerminalProbLog { get; set; }

    public bool IsConnectionProbLog { get; set; }

    public bool IsTransLookup { get; set; }

    public bool IsSendUserConfirmationEmail { get; set; }

    public bool IsAllowMakePayments { get; set; }

    public bool IsShowSensitiveData { get; set; }

    [Column("IsUseFraudDetection_MaxMind")]
    public bool IsUseFraudDetectionMaxMind { get; set; }

    [Column("IsUseFraudDetection_Local")]
    public bool IsUseFraudDetectionLocal { get; set; }

    [Column("MaxMind_MinScore", TypeName = "decimal(5, 2)")]
    public decimal MaxMindMinScore { get; set; }

    public int CountAdminView { get; set; }

    public byte PayingDaysMargin { get; set; }

    [Column("payingDates1")]
    [StringLength(6)]
    public string PayingDates1 { get; set; } = null!;

    [Column("payingDates2")]
    [StringLength(6)]
    public string PayingDates2 { get; set; } = null!;

    [Column("payingDates3")]
    [StringLength(6)]
    public string PayingDates3 { get; set; } = null!;

    [Column("referralID")]
    public int ReferralId { get; set; }

    [Column("referralName")]
    [StringLength(150)]
    public string ReferralName { get; set; } = null!;

    [Column("languagePreference")]
    [StringLength(3)]
    public string LanguagePreference { get; set; } = null!;

    [Column("isNetpayTerminal")]
    public bool IsNetpayTerminal { get; set; }

    [StringLength(100)]
    public string PslWalletId { get; set; } = null!;

    [StringLength(50)]
    public string PslWalletCode { get; set; } = null!;

    public bool IsAllowBatchFiles { get; set; }

    [Column("IsAllow3DTrans")]
    public bool IsAllow3Dtrans { get; set; }

    public bool IsAllowBasket { get; set; }

    [Column("isCcTransCostFailFee")]
    public bool IsCcTransCostFailFee { get; set; }

    [Column("isAllowFreeMail")]
    public bool IsAllowFreeMail { get; set; }

    [Column("descriptor")]
    [StringLength(150)]
    public string Descriptor { get; set; } = null!;

    [Column("countryBlackList")]
    [StringLength(720)]
    [Unicode(false)]
    public string CountryBlackList { get; set; } = null!;

    [Column("dailyCcMaxFailCount")]
    public byte DailyCcMaxFailCount { get; set; }

    [Column("debitCompanyExID")]
    [StringLength(20)]
    public string DebitCompanyExId { get; set; } = null!;

    [Column(TypeName = "smallmoney")]
    public decimal AffiliateFee { get; set; }

    [StringLength(12)]
    public string SecurityKey { get; set; } = null!;

    [Column("SubAffiliateID")]
    public int SubAffiliateId { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal SubAffiliateFee { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal AffiliateFeeView { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal SubAffiliateFeeView { get; set; }

    public int LastLoginCount { get; set; }

    [Column("transCcStorage", TypeName = "money")]
    public decimal TransCcStorage { get; set; }

    [Column("careOfAdminUser")]
    [StringLength(50)]
    public string CareOfAdminUser { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal SecurityDeposit { get; set; }

    [Column("PaymentAbroadABA")]
    [StringLength(80)]
    public string PaymentAbroadAba { get; set; } = null!;

    [StringLength(80)]
    public string PaymentAbroadSortCode { get; set; } = null!;

    [StringLength(100)]
    public string PaymentAbroadBankAddressSecond { get; set; } = null!;

    [StringLength(30)]
    public string PaymentAbroadBankAddressCity { get; set; } = null!;

    [StringLength(20)]
    public string PaymentAbroadBankAddressState { get; set; } = null!;

    [StringLength(20)]
    public string PaymentAbroadBankAddressZip { get; set; } = null!;

    public int PaymentAbroadBankAddressCountry { get; set; }

    [StringLength(100)]
    public string PaymentAbroadBankAddressSecond2 { get; set; } = null!;

    [StringLength(30)]
    public string PaymentAbroadBankAddressCity2 { get; set; } = null!;

    [StringLength(20)]
    public string PaymentAbroadBankAddressState2 { get; set; } = null!;

    [StringLength(20)]
    public string PaymentAbroadBankAddressZip2 { get; set; } = null!;

    public int PaymentAbroadBankAddressCountry2 { get; set; }

    public int? ParentCompany { get; set; }

    public bool IsAllowRecurringFromTransPass { get; set; }

    public string AllowedAmounts { get; set; } = null!;

    public byte ActiveStatus { get; set; }

    [StringLength(100)]
    public string ChargebackNotifyMail { get; set; } = null!;

    [StringLength(50)]
    public string CyclePeriod { get; set; } = null!;

    public byte PayingDaysMarginInitial { get; set; }

    [Column("RREnable")]
    public bool Rrenable { get; set; }

    [Column("RROnlyOnce")]
    public bool RronlyOnce { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal MakePaymentsFixFee { get; set; }

    [StringLength(512)]
    public string? ReportMail { get; set; }

    public byte? ReportMailOptions { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal HandlingFee { get; set; }

    [Column("IsUsePPWList")]
    public bool IsUsePpwlist { get; set; }

    public short SecurityPeriod { get; set; }

    [Column("WalletUB2MBFixedFee", TypeName = "smallmoney")]
    public decimal WalletUb2mbfixedFee { get; set; }

    [Column("WalletUB2MBPrcFee", TypeName = "smallmoney")]
    public decimal WalletUb2mbprcFee { get; set; }

    [StringLength(50)]
    public string UserNameAlt { get; set; } = null!;

    [StringLength(32)]
    public string HashKey { get; set; } = null!;

    [Column("MerchantIdentityID")]
    public int MerchantIdentityId { get; set; }

    [Column("isHidePayNeed")]
    public bool IsHidePayNeed { get; set; }

    public bool IsAllowRemotePull { get; set; }

    [Column("RemotePullIPs")]
    [StringLength(500)]
    public string RemotePullIps { get; set; } = null!;

    [Column("CCardNumber256")]
    [MaxLength(200)]
    public byte[]? CcardNumber256 { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal PayPercent { get; set; }

    [Column("RRAutoRet")]
    public bool? RrautoRet { get; set; }

    [StringLength(11)]
    [Unicode(false)]
    public string PaymentAbroadSepaBic { get; set; } = null!;

    [StringLength(11)]
    [Unicode(false)]
    public string PaymentAbroadSepaBic2 { get; set; } = null!;

    public bool? IsUsingNewTerminal { get; set; }

    public bool IsBillingCityOptional { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ZecureUsername { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ZecurePassword { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ZecureAccount { get; set; } = null!;

    public bool IsAskRefundRemote { get; set; }

    [Column("RemoteRefundRequestIPs")]
    [StringLength(500)]
    public string RemoteRefundRequestIps { get; set; } = null!;

    public float RiskScore { get; set; }

    public bool WalletIsEnable { get; set; }

    [Column("GroupID")]
    public int? GroupId { get; set; }

    [Column(TypeName = "money")]
    public decimal? DailyVolumeLimit { get; set; }

    public bool? IsSkipFraudDetectionInVirtualTerminal { get; set; }

    [Column("RRKeepAmount", TypeName = "money")]
    public decimal RrkeepAmount { get; set; }

    [Column("RRKeepCurrency")]
    public byte RrkeepCurrency { get; set; }

    [Column("RRComment")]
    [StringLength(255)]
    public string? Rrcomment { get; set; }

    public bool? IsCcWhiteListEnabled { get; set; }

    [Column("RRState")]
    public byte Rrstate { get; set; }

    [Column("RRPayID")]
    public int RrpayId { get; set; }

    [Column("countryWhiteList")]
    [StringLength(720)]
    [Unicode(false)]
    public string CountryWhiteList { get; set; } = null!;

    [Column("CFF_CurAmount", TypeName = "money")]
    public decimal CffCurAmount { get; set; }

    [Column("CFF_Currency")]
    public int? CffCurrency { get; set; }

    [Column("CFF_ResetDate")]
    public DateOnly CffResetDate { get; set; }

    [Column("IsBillingAddressMustIDebit")]
    public bool IsBillingAddressMustIdebit { get; set; }

    [Column("IPWhiteList")]
    [StringLength(150)]
    [Unicode(false)]
    public string IpwhiteList { get; set; } = null!;

    public int AutoCaptureHours { get; set; }

    [StringLength(100)]
    public string? ContactName { get; set; }

    [StringLength(100)]
    public string? ContactMail { get; set; }

    [StringLength(255)]
    public string? AlertEmail { get; set; }

    public bool? IsAnnualFee { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AnnualFeeDate { get; set; }

    public bool? IsAnnualFeeLowRisk { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AnnualFeeLowRiskDate { get; set; }

    public bool? IsAnnualFeeHighRisk { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AnnualFeeHighRiskDate { get; set; }

    [Column("AffiliateID")]
    public int AffiliateId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? MonthlyFeeEuroDate { get; set; }

    public bool? IsMonthlyFeeEuro { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? MonthlyFeeDollarDate { get; set; }

    public bool? IsMonthlyFeeDollar { get; set; }

    public short? MultiChargeProtectionMins { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? MonthlyFeeBankHighDate { get; set; }

    public bool? IsMonthlyFeeBankHigh { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? MonthlyFeeBankLowDate { get; set; }

    public bool? IsMonthlyFeeBankLow { get; set; }

    [Column("ForceRecurringMD5")]
    public bool? ForceRecurringMd5 { get; set; }

    [Column("ForceCCStorageMD5")]
    public bool? ForceCcstorageMd5 { get; set; }

    public bool? IsAnnualFee3dSecure { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AnnualFee3dSecureDate { get; set; }

    public bool? IsMerchantNotifiedOnPass { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string PassNotifyEmail { get; set; } = null!;

    public bool? IsAnnualFeeRegistration { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AnnualFeeRegistrationDate { get; set; }

    public byte? PreferredWireType { get; set; }

    public int RecurringLimitYears { get; set; }

    public int RecurringLimitCharges { get; set; }

    public int RecurringLimitStages { get; set; }

    [StringLength(80)]
    public string? MerchantLinkName { get; set; }

    public bool IsAllowSilentPostCcDetails { get; set; }

    public bool IsInterestedInNewsletter { get; set; }

    [Column("IsRequiredClientIP")]
    public bool IsRequiredClientIp { get; set; }

    public bool IsMerchantNotifiedOnFail { get; set; }

    [Column("MerchantDepartment_id")]
    public byte? MerchantDepartmentId { get; set; }

    [Column("Account_id")]
    public int? AccountId { get; set; }

    public byte RiskRating { get; set; }

    [Column("SICCodeNumber")]
    public short? SiccodeNumber { get; set; }

    [Column("isTipAllowed")]
    public bool IsTipAllowed { get; set; }

    [Column("3dsThresholdAmount", TypeName = "smallmoney")]
    public decimal? _3dsThresholdAmount { get; set; }

    [InverseProperty("Merchant")]
    public virtual Account? Account { get; set; }

    [ForeignKey("BillingCompanysId")]
    [InverseProperty("TblCompanies")]
    public virtual TblBillingCompany? BillingCompanys { get; set; }

    [InverseProperty("Merchant")]
    public virtual ICollection<BlacklistMerchant> BlacklistMerchants { get; set; } = new List<BlacklistMerchant>();

    [InverseProperty("Merchant")]
    public virtual ICollection<CartFailLog> CartFailLogs { get; set; } = new List<CartFailLog>();

    [InverseProperty("Merchant")]
    public virtual ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

    [InverseProperty("Merchant")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [ForeignKey("CffCurrency")]
    [InverseProperty("TblCompanyCffCurrencyNavigations")]
    public virtual TblSystemCurrency? CffCurrencyNavigation { get; set; }

    [InverseProperty("Merchant")]
    public virtual ICollection<CurrencyRate> CurrencyRates { get; set; } = new List<CurrencyRate>();

    [InverseProperty("Merchant")]
    public virtual ICollection<EventPending> EventPendings { get; set; } = new List<EventPending>();

    [ForeignKey("GroupId")]
    [InverseProperty("TblCompanies")]
    public virtual TblMerchantGroup? Group { get; set; }

    [InverseProperty("Merchant")]
    public virtual ICollection<HostedPageUrl> HostedPageUrls { get; set; } = new List<HostedPageUrl>();

    [InverseProperty("Merchant")]
    public virtual MerchantActivity? MerchantActivity { get; set; }

    [ForeignKey("MerchantDepartmentId")]
    [InverseProperty("TblCompanies")]
    public virtual MerchantDepartment? MerchantDepartment { get; set; }

    [InverseProperty("Merchant")]
    public virtual ICollection<MerchantSetCartInstallment> MerchantSetCartInstallments { get; set; } = new List<MerchantSetCartInstallment>();

    [InverseProperty("Merchant")]
    public virtual ICollection<MerchantSetPayerAdditionalInfo> MerchantSetPayerAdditionalInfos { get; set; } = new List<MerchantSetPayerAdditionalInfo>();

    [InverseProperty("Merchant")]
    public virtual ICollection<MerchantSetShop> MerchantSetShops { get; set; } = new List<MerchantSetShop>();

    [InverseProperty("Merchant")]
    public virtual ICollection<MerchantSetText> MerchantSetTexts { get; set; } = new List<MerchantSetText>();

    [ForeignKey("ParentCompany")]
    [InverseProperty("TblCompanies")]
    public virtual TblParentCompany? ParentCompanyNavigation { get; set; }

    [ForeignKey("PaymentBank")]
    [InverseProperty("TblCompanies")]
    public virtual TblSystemBankList? PaymentBankNavigation { get; set; }

    [ForeignKey("PaymentReceiveCurrency")]
    [InverseProperty("TblCompanyPaymentReceiveCurrencyNavigations")]
    public virtual TblSystemCurrency? PaymentReceiveCurrencyNavigation { get; set; }

    [InverseProperty("ProcessMerchant")]
    public virtual ICollection<PeriodicFeeType> PeriodicFeeTypes { get; set; } = new List<PeriodicFeeType>();

    [InverseProperty("Merchant")]
    public virtual ICollection<PhoneDetail> PhoneDetails { get; set; } = new List<PhoneDetail>();

    [InverseProperty("Merchant")]
    public virtual ICollection<ProcessApproved> ProcessApproveds { get; set; } = new List<ProcessApproved>();

    [InverseProperty("Merchant")]
    public virtual ICollection<ProductProperty> ProductProperties { get; set; } = new List<ProductProperty>();

    [InverseProperty("Merchant")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    [InverseProperty("Merchant")]
    public virtual ICollection<RecurringModify> RecurringModifies { get; set; } = new List<RecurringModify>();

    [InverseProperty("Merchant")]
    public virtual ICollection<RiskRuleHistory> RiskRuleHistories { get; set; } = new List<RiskRuleHistory>();

    [InverseProperty("Merchant")]
    public virtual ICollection<SetMerchantAffiliate> SetMerchantAffiliates { get; set; } = new List<SetMerchantAffiliate>();

    [InverseProperty("Merchant")]
    public virtual SetMerchantCart? SetMerchantCart { get; set; }

    [InverseProperty("Merchant")]
    public virtual ICollection<SetMerchantInstallment> SetMerchantInstallments { get; set; } = new List<SetMerchantInstallment>();

    [InverseProperty("Merchant")]
    public virtual SetMerchantInvoice? SetMerchantInvoice { get; set; }

    [InverseProperty("Merchant")]
    public virtual SetMerchantMaxmind? SetMerchantMaxmind { get; set; }

    [InverseProperty("Merchant")]
    public virtual SetMerchantMobileApp? SetMerchantMobileApp { get; set; }

    [InverseProperty("Merchant")]
    public virtual ICollection<SetMerchantPeriodicFee> SetMerchantPeriodicFees { get; set; } = new List<SetMerchantPeriodicFee>();

    [InverseProperty("Merchant")]
    public virtual SetMerchantRisk? SetMerchantRisk { get; set; }

    [InverseProperty("Merchant")]
    public virtual SetMerchantRollingReserve? SetMerchantRollingReserve { get; set; }

    [InverseProperty("Merchant")]
    public virtual ICollection<SetMerchantSettlement> SetMerchantSettlements { get; set; } = new List<SetMerchantSettlement>();

    [ForeignKey("SiccodeNumber")]
    [InverseProperty("TblCompanies")]
    public virtual Siccode? SiccodeNumberNavigation { get; set; }

    [InverseProperty("AfsCompany")]
    public virtual ICollection<TblAffiliateFeeStep> TblAffiliateFeeSteps { get; set; } = new List<TblAffiliateFeeStep>();

    [InverseProperty("BlCompany")]
    public virtual ICollection<TblBlcommon> TblBlcommons { get; set; } = new List<TblBlcommon>();

    [InverseProperty("Company")]
    public virtual ICollection<TblCcstorage> TblCcstorages { get; set; } = new List<TblCcstorage>();

    [InverseProperty("Company")]
    public virtual ICollection<TblCheckDetail> TblCheckDetails { get; set; } = new List<TblCheckDetail>();

    [InverseProperty("Company")]
    public virtual ICollection<TblCompanyBalance> TblCompanyBalances { get; set; } = new List<TblCompanyBalance>();

    [InverseProperty("Cbfcompany")]
    public virtual ICollection<TblCompanyBatchFile> TblCompanyBatchFiles { get; set; } = new List<TblCompanyBatchFile>();

    [InverseProperty("Company")]
    public virtual TblCompanyChargeAdmin? TblCompanyChargeAdmin { get; set; }

    [InverseProperty("CcfCompany")]
    public virtual ICollection<TblCompanyCreditFee> TblCompanyCreditFees { get; set; } = new List<TblCompanyCreditFee>();

    [InverseProperty("CcftCompany")]
    public virtual ICollection<TblCompanyCreditFeesTerminal> TblCompanyCreditFeesTerminals { get; set; } = new List<TblCompanyCreditFeesTerminal>();

    [InverseProperty("CffCompany")]
    public virtual ICollection<TblCompanyFeesFloor> TblCompanyFeesFloors { get; set; } = new List<TblCompanyFeesFloor>();

    [InverseProperty("Company")]
    public virtual ICollection<TblCompanyMakePaymentsProfile> TblCompanyMakePaymentsProfiles { get; set; } = new List<TblCompanyMakePaymentsProfile>();

    [InverseProperty("Company")]
    public virtual ICollection<TblCompanyMakePaymentsRequest> TblCompanyMakePaymentsRequests { get; set; } = new List<TblCompanyMakePaymentsRequest>();

    [InverseProperty("CpmCompany")]
    public virtual ICollection<TblCompanyPaymentMethod> TblCompanyPaymentMethods { get; set; } = new List<TblCompanyPaymentMethod>();

    [InverseProperty("Company")]
    public virtual TblCompanySettingsHosted? TblCompanySettingsHosted { get; set; }

    [InverseProperty("Company")]
    public virtual ICollection<TblCompanyTransApproval> TblCompanyTransApprovals { get; set; } = new List<TblCompanyTransApproval>();

    [InverseProperty("Company")]
    public virtual ICollection<TblCompanyTransCrypto> TblCompanyTransCryptos { get; set; } = new List<TblCompanyTransCrypto>();

    [InverseProperty("Company")]
    public virtual ICollection<TblCompanyTransFail> TblCompanyTransFails { get; set; } = new List<TblCompanyTransFail>();

    [InverseProperty("Company")]
    public virtual ICollection<TblCompanyTransInstallment> TblCompanyTransInstallments { get; set; } = new List<TblCompanyTransInstallment>();

    [InverseProperty("Company")]
    public virtual ICollection<TblCompanyTransPass> TblCompanyTransPasses { get; set; } = new List<TblCompanyTransPass>();

    [InverseProperty("Company")]
    public virtual ICollection<TblCompanyTransPending> TblCompanyTransPendings { get; set; } = new List<TblCompanyTransPending>();

    [InverseProperty("CcrmCompany")]
    public virtual ICollection<TblCreditCardRiskManagement> TblCreditCardRiskManagements { get; set; } = new List<TblCreditCardRiskManagement>();

    [InverseProperty("Company")]
    public virtual ICollection<TblCreditCard> TblCreditCards { get; set; } = new List<TblCreditCard>();

    [InverseProperty("Merchant")]
    public virtual ICollection<TblExternalCardCustomer> TblExternalCardCustomers { get; set; } = new List<TblExternalCardCustomer>();

    [InverseProperty("Merchant")]
    public virtual ICollection<TblExternalCardTerminalToMerchant> TblExternalCardTerminalToMerchants { get; set; } = new List<TblExternalCardTerminalToMerchant>();

    [InverseProperty("Merchant")]
    public virtual TblMerchantProcessingDatum? TblMerchantProcessingDatum { get; set; }

    [InverseProperty("Merchant")]
    public virtual TblMerchantRecurringSetting? TblMerchantRecurringSetting { get; set; }

    [InverseProperty("Merchant")]
    public virtual ICollection<TblPeriodicFee> TblPeriodicFees { get; set; } = new List<TblPeriodicFee>();

    [InverseProperty("RsCompanyNavigation")]
    public virtual ICollection<TblRecurringSeries> TblRecurringSeries { get; set; } = new List<TblRecurringSeries>();

    [InverseProperty("Company")]
    public virtual ICollection<TblRefundAsk> TblRefundAsks { get; set; } = new List<TblRefundAsk>();

    [InverseProperty("SumMerchantNavigation")]
    public virtual ICollection<TblSecurityUserMerchant> TblSecurityUserMerchants { get; set; } = new List<TblSecurityUserMerchant>();

    [InverseProperty("Merchant")]
    public virtual ICollection<TransHistory> TransHistories { get; set; } = new List<TransHistory>();

    [InverseProperty("Merchant")]
    public virtual ICollection<TransPayerInfo> TransPayerInfos { get; set; } = new List<TransPayerInfo>();

    [InverseProperty("Merchant")]
    public virtual ICollection<TransPaymentMethod> TransPaymentMethods { get; set; } = new List<TransPaymentMethod>();
}
