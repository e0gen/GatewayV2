using Microsoft.EntityFrameworkCore;
using Ezzygate.Infrastructure.Ef.Entities;
using TimeZone = Ezzygate.Infrastructure.Ef.Entities.TimeZone;

namespace Ezzygate.Infrastructure.Ef.Context;

public partial class EzzygateDbContext : DbContext
{
    public EzzygateDbContext()
    {
    }

    public EzzygateDbContext(DbContextOptions<EzzygateDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountAddress> AccountAddresses { get; set; }

    public virtual DbSet<AccountBalance> AccountBalances { get; set; }

    public virtual DbSet<AccountBalanceMoneyRequest> AccountBalanceMoneyRequests { get; set; }

    public virtual DbSet<AccountBankAccount> AccountBankAccounts { get; set; }

    public virtual DbSet<AccountCryptoWallet> AccountCryptoWallets { get; set; }

    public virtual DbSet<AccountExternalService> AccountExternalServices { get; set; }

    public virtual DbSet<AccountFile> AccountFiles { get; set; }

    public virtual DbSet<AccountMsg> AccountMsgs { get; set; }

    public virtual DbSet<AccountMsgInbox> AccountMsgInboxes { get; set; }

    public virtual DbSet<AccountNote> AccountNotes { get; set; }

    public virtual DbSet<AccountPayee> AccountPayees { get; set; }

    public virtual DbSet<AccountPaymentMethod> AccountPaymentMethods { get; set; }

    public virtual DbSet<AccountSubUser> AccountSubUsers { get; set; }

    public virtual DbSet<AccountType> AccountTypes { get; set; }

    public virtual DbSet<ActionStatus> ActionStatuses { get; set; }

    public virtual DbSet<ActiveStatus> ActiveStatuses { get; set; }

    public virtual DbSet<AdminGroup> AdminGroups { get; set; }

    public virtual DbSet<AdminUser> AdminUsers { get; set; }

    public virtual DbSet<AdminUserToMailbox> AdminUserToMailboxes { get; set; }

    public virtual DbSet<AmountType> AmountTypes { get; set; }

    public virtual DbSet<AppModule> AppModules { get; set; }

    public virtual DbSet<AppModuleAccountSetting> AppModuleAccountSettings { get; set; }

    public virtual DbSet<AppModuleSetting> AppModuleSettings { get; set; }

    public virtual DbSet<AppSetting> AppSettings { get; set; }

    public virtual DbSet<ApplicationIdentity> ApplicationIdentities { get; set; }

    public virtual DbSet<ApplicationIdentityToCurrency> ApplicationIdentityToCurrencies { get; set; }

    public virtual DbSet<ApplicationIdentityToMerchantGroup> ApplicationIdentityToMerchantGroups { get; set; }

    public virtual DbSet<ApplicationIdentityToPaymentMethod> ApplicationIdentityToPaymentMethods { get; set; }

    public virtual DbSet<ApplicationIdentityToken> ApplicationIdentityTokens { get; set; }

    public virtual DbSet<AuthorizationBatch> AuthorizationBatches { get; set; }

    public virtual DbSet<AuthorizationTransDatum> AuthorizationTransData { get; set; }

    public virtual DbSet<BalanceSourceType> BalanceSourceTypes { get; set; }

    public virtual DbSet<BlacklistMerchant> BlacklistMerchants { get; set; }

    public virtual DbSet<BlockLevel> BlockLevels { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartFailLog> CartFailLogs { get; set; }

    public virtual DbSet<CartProduct> CartProducts { get; set; }

    public virtual DbSet<CartProductProperty> CartProductProperties { get; set; }

    public virtual DbSet<CcMailUsage> CcMailUsages { get; set; }

    public virtual DbSet<ChangeAudit> ChangeAudits { get; set; }

    public virtual DbSet<ChbReasonCode> ChbReasonCodes { get; set; }

    public virtual DbSet<CountryGroup> CountryGroups { get; set; }

    public virtual DbSet<CountryList> CountryLists { get; set; }

    public virtual DbSet<CurrencyList> CurrencyLists { get; set; }

    public virtual DbSet<CurrencyRate> CurrencyRates { get; set; }

    public virtual DbSet<CurrencyRateType> CurrencyRateTypes { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerRelation> CustomerRelations { get; set; }

    public virtual DbSet<CustomerShippingDetail> CustomerShippingDetails { get; set; }

    public virtual DbSet<Dbversion> Dbversions { get; set; }

    public virtual DbSet<EventPending> EventPendings { get; set; }

    public virtual DbSet<EventPendingType> EventPendingTypes { get; set; }

    public virtual DbSet<ExternalCardProvider> ExternalCardProviders { get; set; }

    public virtual DbSet<ExternalServiceAction> ExternalServiceActions { get; set; }

    public virtual DbSet<ExternalServiceHistory> ExternalServiceHistories { get; set; }

    public virtual DbSet<ExternalServiceType> ExternalServiceTypes { get; set; }

    public virtual DbSet<FacebookPageToMerchant> FacebookPageToMerchants { get; set; }

    public virtual DbSet<FeeCalcMethod> FeeCalcMethods { get; set; }

    public virtual DbSet<FileItemType> FileItemTypes { get; set; }

    public virtual DbSet<FraudDetection> FraudDetections { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<HistoryType> HistoryTypes { get; set; }

    public virtual DbSet<HostedPageUrl> HostedPageUrls { get; set; }

    public virtual DbSet<InvoiceIssuerCurrency> InvoiceIssuerCurrencies { get; set; }

    public virtual DbSet<InvoiceProvider> InvoiceProviders { get; set; }

    public virtual DbSet<InvoiceType> InvoiceTypes { get; set; }

    public virtual DbSet<LanguageList> LanguageLists { get; set; }

    public virtual DbSet<LoginAccount> LoginAccounts { get; set; }

    public virtual DbSet<LoginHistory> LoginHistories { get; set; }

    public virtual DbSet<LoginPassword> LoginPasswords { get; set; }

    public virtual DbSet<LoginResult> LoginResults { get; set; }

    public virtual DbSet<LoginRole> LoginRoles { get; set; }

    public virtual DbSet<LoginType> LoginTypes { get; set; }

    public virtual DbSet<MerchantActivity> MerchantActivities { get; set; }

    public virtual DbSet<MerchantDepartment> MerchantDepartments { get; set; }

    public virtual DbSet<MerchantSetCartInstallment> MerchantSetCartInstallments { get; set; }

    public virtual DbSet<MerchantSetPayerAdditionalInfo> MerchantSetPayerAdditionalInfos { get; set; }

    public virtual DbSet<MerchantSetShop> MerchantSetShops { get; set; }

    public virtual DbSet<MerchantSetShopInstallment> MerchantSetShopInstallments { get; set; }

    public virtual DbSet<MerchantSetShopToCountryRegion> MerchantSetShopToCountryRegions { get; set; }

    public virtual DbSet<MerchantSetText> MerchantSetTexts { get; set; }

    public virtual DbSet<MobileDevice> MobileDevices { get; set; }

    public virtual DbSet<MonthlyFloorTotal> MonthlyFloorTotals { get; set; }

    public virtual DbSet<NewAppModule> NewAppModules { get; set; }

    public virtual DbSet<NewSecurityObject> NewSecurityObjects { get; set; }

    public virtual DbSet<NewSecurityObjectToAdminGroup> NewSecurityObjectToAdminGroups { get; set; }

    public virtual DbSet<NewSecurityObjectToLoginAccount> NewSecurityObjectToLoginAccounts { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<PaymentMethodGroup> PaymentMethodGroups { get; set; }

    public virtual DbSet<PaymentMethodProvider> PaymentMethodProviders { get; set; }

    public virtual DbSet<PaymentMethodToCountryCurrency> PaymentMethodToCountryCurrencies { get; set; }

    public virtual DbSet<PaymentMethodType> PaymentMethodTypes { get; set; }

    public virtual DbSet<PeopleRelationType> PeopleRelationTypes { get; set; }

    public virtual DbSet<PeriodicFeeType> PeriodicFeeTypes { get; set; }

    public virtual DbSet<PhoneCarrier> PhoneCarriers { get; set; }

    public virtual DbSet<PhoneDetail> PhoneDetails { get; set; }

    public virtual DbSet<PreCreatedPaymentMethod> PreCreatedPaymentMethods { get; set; }

    public virtual DbSet<ProcessApproved> ProcessApproveds { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductProperty> ProductProperties { get; set; }

    public virtual DbSet<ProductPropertyType> ProductPropertyTypes { get; set; }

    public virtual DbSet<ProductStock> ProductStocks { get; set; }

    public virtual DbSet<ProductStockReference> ProductStockReferences { get; set; }

    public virtual DbSet<ProductTag> ProductTags { get; set; }

    public virtual DbSet<ProductText> ProductTexts { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<ProtocolType> ProtocolTypes { get; set; }

    public virtual DbSet<Qna> Qnas { get; set; }

    public virtual DbSet<Qnagroup> Qnagroups { get; set; }

    public virtual DbSet<RecurringModify> RecurringModifies { get; set; }

    public virtual DbSet<RiskRuleHistory> RiskRuleHistories { get; set; }

    public virtual DbSet<RunningProcess> RunningProcesses { get; set; }

    public virtual DbSet<SecurityObject> SecurityObjects { get; set; }

    public virtual DbSet<SecurityObjectToAdminGroup> SecurityObjectToAdminGroups { get; set; }

    public virtual DbSet<SecurityObjectToLoginAccount> SecurityObjectToLoginAccounts { get; set; }

    public virtual DbSet<SetMerchantAffiliate> SetMerchantAffiliates { get; set; }

    public virtual DbSet<SetMerchantCart> SetMerchantCarts { get; set; }

    public virtual DbSet<SetMerchantInstallment> SetMerchantInstallments { get; set; }

    public virtual DbSet<SetMerchantInvoice> SetMerchantInvoices { get; set; }

    public virtual DbSet<SetMerchantMaxmind> SetMerchantMaxminds { get; set; }

    public virtual DbSet<SetMerchantMobileApp> SetMerchantMobileApps { get; set; }

    public virtual DbSet<SetMerchantPeriodicFee> SetMerchantPeriodicFees { get; set; }

    public virtual DbSet<SetMerchantRisk> SetMerchantRisks { get; set; }

    public virtual DbSet<SetMerchantRollingReserve> SetMerchantRollingReserves { get; set; }

    public virtual DbSet<SetMerchantSettlement> SetMerchantSettlements { get; set; }

    public virtual DbSet<SetPeriodicFee> SetPeriodicFees { get; set; }

    public virtual DbSet<SetRiskRule> SetRiskRules { get; set; }

    public virtual DbSet<SetTransactionFee> SetTransactionFees { get; set; }

    public virtual DbSet<SetTransactionFloor> SetTransactionFloors { get; set; }

    public virtual DbSet<SetTransactionFloorFee> SetTransactionFloorFees { get; set; }

    public virtual DbSet<SettlemenType> SettlemenTypes { get; set; }

    public virtual DbSet<ShippingDetail> ShippingDetails { get; set; }

    public virtual DbSet<Siccode> Siccodes { get; set; }

    public virtual DbSet<SolutionBulletin> SolutionBulletins { get; set; }

    public virtual DbSet<SolutionList> SolutionLists { get; set; }

    public virtual DbSet<StateList> StateLists { get; set; }

    public virtual DbSet<StatusHistory> StatusHistories { get; set; }

    public virtual DbSet<StatusHistoryType> StatusHistoryTypes { get; set; }

    public virtual DbSet<SysErrorCode> SysErrorCodes { get; set; }

    public virtual DbSet<SystemUserType> SystemUserTypes { get; set; }

    public virtual DbSet<TableList> TableLists { get; set; }

    public virtual DbSet<TaskLock> TaskLocks { get; set; }

    public virtual DbSet<TblAffiliate> TblAffiliates { get; set; }

    public virtual DbSet<TblAffiliateBankAccount> TblAffiliateBankAccounts { get; set; }

    public virtual DbSet<TblAffiliateFeeStep> TblAffiliateFeeSteps { get; set; }

    public virtual DbSet<TblAffiliatePayment> TblAffiliatePayments { get; set; }

    public virtual DbSet<TblAffiliatePaymentsLine> TblAffiliatePaymentsLines { get; set; }

    public virtual DbSet<TblAffiliatesCount> TblAffiliatesCounts { get; set; }

    public virtual DbSet<TblAutoCapture> TblAutoCaptures { get; set; }

    public virtual DbSet<TblBankAccount> TblBankAccounts { get; set; }

    public virtual DbSet<TblBillingAddress> TblBillingAddresses { get; set; }

    public virtual DbSet<TblBillingCompany> TblBillingCompanys { get; set; }

    public virtual DbSet<TblBlackListBin> TblBlackListBins { get; set; }

    public virtual DbSet<TblBlcommon> TblBlcommons { get; set; }

    public virtual DbSet<TblBnsStoredCard> TblBnsStoredCards { get; set; }

    public virtual DbSet<TblCcstorage> TblCcstorages { get; set; }

    public virtual DbSet<TblChargebackReason> TblChargebackReasons { get; set; }

    public virtual DbSet<TblChbFileLog> TblChbFileLogs { get; set; }

    public virtual DbSet<TblChbPending> TblChbPendings { get; set; }

    public virtual DbSet<TblCheckDetail> TblCheckDetails { get; set; }

    public virtual DbSet<TblCompany> TblCompanies { get; set; }

    public virtual DbSet<TblCompanyBalance> TblCompanyBalances { get; set; }

    public virtual DbSet<TblCompanyBatchFile> TblCompanyBatchFiles { get; set; }

    public virtual DbSet<TblCompanyChargeAdmin> TblCompanyChargeAdmins { get; set; }

    public virtual DbSet<TblCompanyCreditFee> TblCompanyCreditFees { get; set; }

    public virtual DbSet<TblCompanyCreditFeesTerminal> TblCompanyCreditFeesTerminals { get; set; }

    public virtual DbSet<TblCompanyCreditRestriction> TblCompanyCreditRestrictions { get; set; }

    public virtual DbSet<TblCompanyFeesFloor> TblCompanyFeesFloors { get; set; }

    public virtual DbSet<TblCompanyMakePaymentsProfile> TblCompanyMakePaymentsProfiles { get; set; }

    public virtual DbSet<TblCompanyMakePaymentsRequest> TblCompanyMakePaymentsRequests { get; set; }

    public virtual DbSet<TblCompanyPaymentMethod> TblCompanyPaymentMethods { get; set; }

    public virtual DbSet<TblCompanySettingsHosted> TblCompanySettingsHosteds { get; set; }

    public virtual DbSet<TblCompanyTransApproval> TblCompanyTransApprovals { get; set; }

    public virtual DbSet<TblCompanyTransCrypto> TblCompanyTransCryptos { get; set; }

    public virtual DbSet<TblCompanyTransFail> TblCompanyTransFails { get; set; }

    public virtual DbSet<TblCompanyTransInstallment> TblCompanyTransInstallments { get; set; }

    public virtual DbSet<TblCompanyTransPass> TblCompanyTransPasses { get; set; }

    public virtual DbSet<TblCompanyTransPending> TblCompanyTransPendings { get; set; }

    public virtual DbSet<TblCompanyTransRemoved> TblCompanyTransRemoveds { get; set; }

    public virtual DbSet<TblCreditCard> TblCreditCards { get; set; }

    public virtual DbSet<TblCreditCardBin> TblCreditCardBins { get; set; }

    public virtual DbSet<TblCreditCardRiskManagement> TblCreditCardRiskManagements { get; set; }

    public virtual DbSet<TblCreditCardType> TblCreditCardTypes { get; set; }

    public virtual DbSet<TblCreditCardWhitelist> TblCreditCardWhitelists { get; set; }

    public virtual DbSet<TblDebitBlock> TblDebitBlocks { get; set; }

    public virtual DbSet<TblDebitBlockLog> TblDebitBlockLogs { get; set; }

    public virtual DbSet<TblDebitCompany> TblDebitCompanies { get; set; }

    public virtual DbSet<TblDebitCompanyCode> TblDebitCompanyCodes { get; set; }

    public virtual DbSet<TblDebitCompanyFee> TblDebitCompanyFees { get; set; }

    public virtual DbSet<TblDebitCompanyLoginDatum> TblDebitCompanyLoginData { get; set; }

    public virtual DbSet<TblDebitCompanyPaymentTokenization> TblDebitCompanyPaymentTokenizations { get; set; }

    public virtual DbSet<TblDebitRule> TblDebitRules { get; set; }

    public virtual DbSet<TblDebitTerminal> TblDebitTerminals { get; set; }

    public virtual DbSet<TblEpaActionType> TblEpaActionTypes { get; set; }

    public virtual DbSet<TblEpaFileLog> TblEpaFileLogs { get; set; }

    public virtual DbSet<TblEpaPending> TblEpaPendings { get; set; }

    public virtual DbSet<TblExternalCardCustomer> TblExternalCardCustomers { get; set; }

    public virtual DbSet<TblExternalCardCustomerPayment> TblExternalCardCustomerPayments { get; set; }

    public virtual DbSet<TblExternalCardTerminal> TblExternalCardTerminals { get; set; }

    public virtual DbSet<TblExternalCardTerminalToMerchant> TblExternalCardTerminalToMerchants { get; set; }

    public virtual DbSet<TblFraudCcBlackList> TblFraudCcBlackLists { get; set; }

    public virtual DbSet<TblGeoIp> TblGeoIps { get; set; }

    public virtual DbSet<TblGlobalDatum> TblGlobalData { get; set; }

    public virtual DbSet<TblGlobalValue> TblGlobalValues { get; set; }

    public virtual DbSet<TblImportChargebackBn> TblImportChargebackBns { get; set; }

    public virtual DbSet<TblImportChargebackJcc> TblImportChargebackJccs { get; set; }

    public virtual DbSet<TblInvikRefundBatch> TblInvikRefundBatches { get; set; }

    public virtual DbSet<TblInvoiceDocument> TblInvoiceDocuments { get; set; }

    public virtual DbSet<TblInvoiceLine> TblInvoiceLines { get; set; }

    public virtual DbSet<TblLogChargeAttempt> TblLogChargeAttempts { get; set; }

    public virtual DbSet<TblLogCreditCardWhitelist> TblLogCreditCardWhitelists { get; set; }

    public virtual DbSet<TblLogDebitRefund> TblLogDebitRefunds { get; set; }

    public virtual DbSet<TblLogImportEpa> TblLogImportEpas { get; set; }

    public virtual DbSet<TblLogMasavDetail> TblLogMasavDetails { get; set; }

    public virtual DbSet<TblLogMasavFile> TblLogMasavFiles { get; set; }

    public virtual DbSet<TblLogNoConnection> TblLogNoConnections { get; set; }

    public virtual DbSet<TblLogPaymentPage> TblLogPaymentPages { get; set; }

    public virtual DbSet<TblLogPaymentPageTran> TblLogPaymentPageTrans { get; set; }

    public virtual DbSet<TblLogPendingFinalize> TblLogPendingFinalizes { get; set; }

    public virtual DbSet<TblLogTerminalJump> TblLogTerminalJumps { get; set; }

    public virtual DbSet<TblMerchantBankAccount> TblMerchantBankAccounts { get; set; }

    public virtual DbSet<TblMerchantGroup> TblMerchantGroups { get; set; }

    public virtual DbSet<TblMerchantProcessingDatum> TblMerchantProcessingData { get; set; }

    public virtual DbSet<TblMerchantRecurringSetting> TblMerchantRecurringSettings { get; set; }

    public virtual DbSet<TblParentCompany> TblParentCompanies { get; set; }

    public virtual DbSet<TblPasswordHistory> TblPasswordHistories { get; set; }

    public virtual DbSet<TblPeriodicFee> TblPeriodicFees { get; set; }

    public virtual DbSet<TblPeriodicFeeType> TblPeriodicFeeTypes { get; set; }

    public virtual DbSet<TblRecurringAttempt> TblRecurringAttempts { get; set; }

    public virtual DbSet<TblRecurringCharge> TblRecurringCharges { get; set; }

    public virtual DbSet<TblRecurringSeries> TblRecurringSeries { get; set; }

    public virtual DbSet<TblRefundAsk> TblRefundAsks { get; set; }

    public virtual DbSet<TblRefundAskLog> TblRefundAskLogs { get; set; }

    public virtual DbSet<TblRetrivalRequest> TblRetrivalRequests { get; set; }

    public virtual DbSet<TblSecurityDocument> TblSecurityDocuments { get; set; }

    public virtual DbSet<TblSecurityDocumentGroup> TblSecurityDocumentGroups { get; set; }

    public virtual DbSet<TblSecurityGroup> TblSecurityGroups { get; set; }

    public virtual DbSet<TblSecurityLog> TblSecurityLogs { get; set; }

    public virtual DbSet<TblSecurityUser> TblSecurityUsers { get; set; }

    public virtual DbSet<TblSecurityUserDebitCompany> TblSecurityUserDebitCompanies { get; set; }

    public virtual DbSet<TblSecurityUserGroup> TblSecurityUserGroups { get; set; }

    public virtual DbSet<TblSecurityUserMerchant> TblSecurityUserMerchants { get; set; }

    public virtual DbSet<TblSettlementAmount> TblSettlementAmounts { get; set; }

    public virtual DbSet<TblSystemBankList> TblSystemBankLists { get; set; }

    public virtual DbSet<TblSystemCurrency> TblSystemCurrencies { get; set; }

    public virtual DbSet<TblTransactionAmount> TblTransactionAmounts { get; set; }

    public virtual DbSet<TblTransactionPay> TblTransactionPays { get; set; }

    public virtual DbSet<TblTransactionPayFee> TblTransactionPayFees { get; set; }

    public virtual DbSet<TblWalletIdentity> TblWalletIdentities { get; set; }

    public virtual DbSet<TblWhiteListBin> TblWhiteListBins { get; set; }

    public virtual DbSet<TblWireMoney> TblWireMoneys { get; set; }

    public virtual DbSet<TblWireMoneyFile> TblWireMoneyFiles { get; set; }

    public virtual DbSet<TblWireMoneyLog> TblWireMoneyLogs { get; set; }

    public virtual DbSet<TimeUnit> TimeUnits { get; set; }

    public virtual DbSet<TimeZone> TimeZones { get; set; }

    public virtual DbSet<TransAmountType> TransAmountTypes { get; set; }

    public virtual DbSet<TransAmountTypeGroup> TransAmountTypeGroups { get; set; }

    public virtual DbSet<TransCreditType> TransCreditTypes { get; set; }

    public virtual DbSet<TransDeniedStatus> TransDeniedStatuses { get; set; }

    public virtual DbSet<TransHistory> TransHistories { get; set; }

    public virtual DbSet<TransHistoryType> TransHistoryTypes { get; set; }

    public virtual DbSet<TransMatchPending> TransMatchPendings { get; set; }

    public virtual DbSet<TransPayerAdditionalInfo> TransPayerAdditionalInfos { get; set; }

    public virtual DbSet<TransPayerInfo> TransPayerInfos { get; set; }

    public virtual DbSet<TransPayerShippingDetail> TransPayerShippingDetails { get; set; }

    public virtual DbSet<TransPaymentBillingAddress> TransPaymentBillingAddresses { get; set; }

    public virtual DbSet<TransPaymentMethod> TransPaymentMethods { get; set; }

    public virtual DbSet<TransSource> TransSources { get; set; }

    public virtual DbSet<TransactionAmount> TransactionAmounts { get; set; }

    public virtual DbSet<ViewBin> ViewBins { get; set; }

    public virtual DbSet<ViewBnsTransactionsWithoutEpa> ViewBnsTransactionsWithoutEpas { get; set; }

    public virtual DbSet<ViewCurrency> ViewCurrencies { get; set; }

    public virtual DbSet<ViewCustomerTran> ViewCustomerTrans { get; set; }

    public virtual DbSet<ViewCustomerTransFail> ViewCustomerTransFails { get; set; }

    public virtual DbSet<ViewCustomerTransPass> ViewCustomerTransPasses { get; set; }

    public virtual DbSet<ViewDebitTerminalMonthlyLimit> ViewDebitTerminalMonthlyLimits { get; set; }

    public virtual DbSet<ViewGetNewid> ViewGetNewids { get; set; }

    public virtual DbSet<ViewImportChargeback> ViewImportChargebacks { get; set; }

    public virtual DbSet<ViewImportChargebackBn> ViewImportChargebackBns { get; set; }

    public virtual DbSet<ViewImportChargebackExcel> ViewImportChargebackExcels { get; set; }

    public virtual DbSet<ViewImportChargebackJcc> ViewImportChargebackJccs { get; set; }

    public virtual DbSet<ViewInvoice> ViewInvoices { get; set; }

    public virtual DbSet<ViewMerchantMailingList> ViewMerchantMailingLists { get; set; }

    public virtual DbSet<ViewMerchantTerminal> ViewMerchantTerminals { get; set; }

    public virtual DbSet<ViewMerchantsInactive> ViewMerchantsInactives { get; set; }

    public virtual DbSet<ViewMerchantsIsracard> ViewMerchantsIsracards { get; set; }

    public virtual DbSet<ViewMerchantsMailing> ViewMerchantsMailings { get; set; }

    public virtual DbSet<ViewEzzygate014> ViewEzzygate014s { get; set; }

    public virtual DbSet<ViewNonAmericanGamer> ViewNonAmericanGamers { get; set; }

    public virtual DbSet<ViewPaymentsInfo> ViewPaymentsInfos { get; set; }

    public virtual DbSet<ViewRandNumber> ViewRandNumbers { get; set; }

    public virtual DbSet<ViewRecurringActiveCharge> ViewRecurringActiveCharges { get; set; }

    public virtual DbSet<ViewRecurringAdmin> ViewRecurringAdmins { get; set; }

    public virtual DbSet<ViewRecurringToKeepPaymentInfo> ViewRecurringToKeepPaymentInfos { get; set; }

    public virtual DbSet<ViewRecurringUnprocessed> ViewRecurringUnprocesseds { get; set; }

    public virtual DbSet<ViewRetrivalAfterRefund> ViewRetrivalAfterRefunds { get; set; }

    public virtual DbSet<ViewSettlementsReport> ViewSettlementsReports { get; set; }

    public virtual DbSet<ViewSummaryBn> ViewSummaryBns { get; set; }

    public virtual DbSet<ViewSummaryBnsDecline> ViewSummaryBnsDeclines { get; set; }

    public virtual DbSet<ViewSummaryBnsDeclinesArchive> ViewSummaryBnsDeclinesArchives { get; set; }

    public virtual DbSet<ViewSummaryBnsSale> ViewSummaryBnsSales { get; set; }

    public virtual DbSet<ViewTerminalsWithZeroFee> ViewTerminalsWithZeroFees { get; set; }

    public virtual DbSet<ViewTriggersDcl> ViewTriggersDcls { get; set; }

    public virtual DbSet<VwQnagroup> VwQnagroups { get; set; }

    public virtual DbSet<Wire> Wires { get; set; }

    public virtual DbSet<WireAccount> WireAccounts { get; set; }

    public virtual DbSet<WireBatch> WireBatches { get; set; }

    public virtual DbSet<WireHistory> WireHistories { get; set; }

    public virtual DbSet<WireProvider> WireProviders { get; set; }

    public virtual DbSet<WorldRegion> WorldRegions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Hebrew_CI_AS");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account", "Data", tb => tb.HasTrigger("Account_UpdateTargetID"));

            entity.HasIndex(e => new { e.AccountNumber, e.AccountTypeId }, "IX_Account_AccountNumber")
                .IsUnique()
                .HasFilter("([AccountNumber] IS NOT NULL)")
                .HasFillFactor(90);

            entity.HasIndex(e => e.AffiliateId, "IX_Account_Affiliate_id")
                .IsUnique()
                .HasFilter("([Affiliate_id] IS NOT NULL)")
                .HasFillFactor(100);

            entity.HasIndex(e => e.CustomerId, "IX_Account_Customer_id")
                .IsUnique()
                .HasFilter("([Customer_id] IS NOT NULL)")
                .HasFillFactor(100);

            entity.HasIndex(e => e.DebitCompanyId, "IX_Account_DebitCompany_id")
                .IsUnique()
                .HasFilter("([DebitCompany_id] IS NOT NULL)")
                .HasFillFactor(100);

            entity.HasIndex(e => e.MerchantId, "IX_Account_Merchant_id")
                .IsUnique()
                .HasFilter("([Merchant_id] IS NOT NULL)")
                .HasFillFactor(100);

            entity.Property(e => e.AccountNumber).IsFixedLength();
            entity.Property(e => e.DefaultCurrencyIsocode).IsFixedLength();

            entity.HasOne(d => d.AccountType).WithMany(p => p.Accounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Account_AccountType_AccountTypeID");

            entity.HasOne(d => d.Affiliate).WithOne(p => p.Account).HasConstraintName("FK_Account_tblAffiliates_AffiliateID");

            entity.HasOne(d => d.BusinessAddress).WithMany(p => p.AccountBusinessAddresses).HasConstraintName("FK_Account_AccountAddress_BusinessAddressID");

            entity.HasOne(d => d.Customer).WithOne(p => p.Account).HasConstraintName("FK_Account_Customer_CustomerID");

            entity.HasOne(d => d.DebitCompany).WithOne(p => p.Account).HasConstraintName("FK_Account_tblDebitCompany_DebitCompanyID");

            entity.HasOne(d => d.DefaultCurrencyIsocodeNavigation).WithMany(p => p.Accounts).HasConstraintName("FK_Account_DefaultCurrencyISOCode");

            entity.HasOne(d => d.LoginAccount).WithMany(p => p.Accounts)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Account_LoginAccount_LoginAccountID");

            entity.HasOne(d => d.Merchant).WithOne(p => p.Account).HasConstraintName("FK_Account_tblCompany_MerchantID");

            entity.HasOne(d => d.PersonalAddress).WithMany(p => p.AccountPersonalAddresses)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Account_AccountAddress_PersonalAddressID");

            entity.HasOne(d => d.PreferredWireProvider).WithMany(p => p.Accounts).HasConstraintName("FK_Account_WireProvider_WireProviderID");
        });

        modelBuilder.Entity<AccountAddress>(entity =>
        {
            entity.Property(e => e.CountryIsocode).IsFixedLength();
            entity.Property(e => e.StateIsocode).IsFixedLength();

            entity.HasOne(d => d.CountryIsocodeNavigation).WithMany(p => p.AccountAddresses).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<AccountBalance>(entity =>
        {
            entity.ToTable("AccountBalance", "Data", tb => tb.HasTrigger("trAccountBalance_UpdateTotalBalance_Ins"));

            entity.Property(e => e.CurrencyIsocode).IsFixedLength();
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountBalances)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountBalance_AccountID");

            entity.HasOne(d => d.BalanceSourceType).WithMany(p => p.AccountBalances)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountBalance_BalanceSourceTypeID");

            entity.HasOne(d => d.CurrencyIsocodeNavigation).WithMany(p => p.AccountBalances)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountBalance_CurrencyISOCode");
        });

        modelBuilder.Entity<AccountBalanceMoneyRequest>(entity =>
        {
            entity.HasKey(e => e.AccountBalanceMoneyRequestId).HasFillFactor(100);

            entity.Property(e => e.CurrencyIsocode).IsFixedLength();
            entity.Property(e => e.RequestDate).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.CurrencyIsocodeNavigation).WithMany(p => p.AccountBalanceMoneyRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountBalanceMoneyRequest_CurrencyISOCode");

            entity.HasOne(d => d.SourceAccountBalance).WithMany(p => p.AccountBalanceMoneyRequestSourceAccountBalances).HasConstraintName("FK_AccountBalanceMoneyRequest_SourceBalanceID");

            entity.HasOne(d => d.SourceAccount).WithMany(p => p.AccountBalanceMoneyRequestSourceAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountBalanceMoneyRequest_SourceAccountID");

            entity.HasOne(d => d.TargetAccountBalance).WithMany(p => p.AccountBalanceMoneyRequestTargetAccountBalances).HasConstraintName("FK_AccountBalanceMoneyRequest_TargetBalanceID");

            entity.HasOne(d => d.TargetAccount).WithMany(p => p.AccountBalanceMoneyRequestTargetAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountBalanceMoneyRequest_TargetAccountID");
        });

        modelBuilder.Entity<AccountBankAccount>(entity =>
        {
            entity.Property(e => e.BankCountryIsocode).IsFixedLength();
            entity.Property(e => e.BankStateIsocode).IsFixedLength();
            entity.Property(e => e.CurrencyIsocode).IsFixedLength();

            entity.HasOne(d => d.Account).WithMany(p => p.AccountBankAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountBankAccount_AccountID");

            entity.HasOne(d => d.BankCountryIsocodeNavigation).WithMany(p => p.AccountBankAccounts).HasConstraintName("FK_AccountBankAccount_CountryList_CountryISOCode");

            entity.HasOne(d => d.BankStateIsocodeNavigation).WithMany(p => p.AccountBankAccounts).HasConstraintName("FK_AccountBankAccount_StateList_StateISOCode");

            entity.HasOne(d => d.CurrencyIsocodeNavigation).WithMany(p => p.AccountBankAccounts).HasConstraintName("FK_AccountBankAccount_CurrencyISOCode");

            entity.HasOne(d => d.RefAccountBankAccount).WithMany(p => p.InverseRefAccountBankAccount).HasConstraintName("FK_AccountBankAccount_RefAccountBankAccountID");
        });

        modelBuilder.Entity<AccountExternalService>(entity =>
        {
            entity.HasOne(d => d.Account).WithMany(p => p.AccountExternalServices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountExternalService_AccountID");

            entity.HasOne(d => d.ExternalServiceType).WithMany(p => p.AccountExternalServices).HasConstraintName("FK_ExternalServiceType_ExternalServiceTypeID");

            entity.HasOne(d => d.ProtocolType).WithMany(p => p.AccountExternalServices).HasConstraintName("FK_ExternalServiceType_ProtocolTypeID");
        });

        modelBuilder.Entity<AccountFile>(entity =>
        {
            entity.HasKey(e => e.AccountFileId).HasFillFactor(100);

            entity.Property(e => e.InsertDate).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountFiles).HasConstraintName("FK_AccountFile_AccountID");

            entity.HasOne(d => d.FileItemType).WithMany(p => p.AccountFiles).HasConstraintName("FK_AccountFile_FileItemType_FileItemTypeID");
        });

        modelBuilder.Entity<AccountMsg>(entity =>
        {
            entity.HasKey(e => e.AccountMsgId).HasFillFactor(100);

            entity.Property(e => e.InsertDate).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsAdminMsg).HasDefaultValue(true);

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("FK_AccountMsg_ParentID");
        });

        modelBuilder.Entity<AccountMsgInbox>(entity =>
        {
            entity.HasKey(e => e.AccountMsgInboxId).HasFillFactor(100);

            entity.Property(e => e.InsertDate).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountMsgInboxes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountMsgInbox_AccountID");

            entity.HasOne(d => d.AccountMsg).WithMany(p => p.AccountMsgInboxes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountMsgInbox_AccountMsgID");
        });

        modelBuilder.Entity<AccountNote>(entity =>
        {
            entity.HasKey(e => e.AccountNoteId).HasFillFactor(100);

            entity.Property(e => e.InsertDate).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountNotes).HasConstraintName("FK_AccountNote_AccountID");
        });

        modelBuilder.Entity<AccountPayee>(entity =>
        {
            entity.HasKey(e => e.AccountPayeeId).HasFillFactor(100);

            entity.HasOne(d => d.AccountBankAccount).WithMany(p => p.AccountPayees).HasConstraintName("FK_AccountPayee_AccountBankAccount_AccountBankAccountID");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountPayees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountPayee_Account_AccountID");
        });

        modelBuilder.Entity<AccountPaymentMethod>(entity =>
        {
            entity.ToTable("AccountPaymentMethod", "Data", tb => tb.HasComment("Hold the personal + gateway provided payment method of an account"));

            entity.Property(e => e.IssuerCountryIsoCode).IsFixedLength();
            entity.Property(e => e.Value1First6Text).IsFixedLength();
            entity.Property(e => e.Value1Last4Text).IsFixedLength();

            entity.HasOne(d => d.AccountAddress).WithMany(p => p.AccountPaymentMethods)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_AccountPaymentMethod_AccountAddress");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountPaymentMethods)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountPaymentMethod_AccountID");

            entity.HasOne(d => d.IssuerCountryIsoCodeNavigation).WithMany(p => p.AccountPaymentMethods).HasConstraintName("FK_AccountPaymentMethod_IssuerCountryIsoCode");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.AccountPaymentMethods)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountPaymentMethod_PaymentMethodID");

            entity.HasOne(d => d.PaymentMethodProvider).WithMany(p => p.AccountPaymentMethods).HasConstraintName("FK_AccountPaymentMethod_PaymentMethodProviderID");
        });

        modelBuilder.Entity<AccountSubUser>(entity =>
        {
            entity.HasKey(e => e.AccountSubUserId).HasName("PK_SecurityObjectToLoginAccount");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountSubUsers).HasConstraintName("FK_AccountSubUser_Account_AccountID");

            entity.HasOne(d => d.LoginAccount).WithMany(p => p.AccountSubUsers).HasConstraintName("FK_AccountSubUser_LoginAccount_LoginAccountID");
        });

        modelBuilder.Entity<AdminUser>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.StartDate).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.LoginAccount).WithMany(p => p.AdminUsers).HasConstraintName("FK_AdminUser_LoginAccount_LoginAccountID");

            entity.HasMany(d => d.Accounts).WithMany(p => p.AdminUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "AdminUserToAccount",
                    r => r.HasOne<Account>().WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AdminUserToAccount_Account_AccountID"),
                    l => l.HasOne<AdminUser>().WithMany()
                        .HasForeignKey("AdminUserId")
                        .HasConstraintName("FK_AdminUserToAccount_AdminUser_AdminUserID"),
                    j =>
                    {
                        j.HasKey("AdminUserId", "AccountId");
                        j.ToTable("AdminUserToAccount", "System");
                        j.IndexerProperty<short>("AdminUserId").HasColumnName("AdminUser_id");
                        j.IndexerProperty<int>("AccountId").HasColumnName("Account_id");
                    });

            entity.HasMany(d => d.AdminGroups).WithMany(p => p.AdminUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "AdminUserToAdminGroup",
                    r => r.HasOne<AdminGroup>().WithMany()
                        .HasForeignKey("AdminGroupId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AdminUserToAdminGroup_AdminGroup_AdminGroupID"),
                    l => l.HasOne<AdminUser>().WithMany()
                        .HasForeignKey("AdminUserId")
                        .HasConstraintName("FK_AdminUserToAdminGroup_AdminUser_AdminUserID"),
                    j =>
                    {
                        j.HasKey("AdminUserId", "AdminGroupId").HasName("PK_AdminUserToRole");
                        j.ToTable("AdminUserToAdminGroup", "System");
                        j.IndexerProperty<short>("AdminUserId").HasColumnName("AdminUser_id");
                        j.IndexerProperty<short>("AdminGroupId").HasColumnName("AdminGroup_id");
                    });
        });

        modelBuilder.Entity<AdminUserToMailbox>(entity =>
        {
            entity.HasOne(d => d.AdminUser).WithMany(p => p.AdminUserToMailboxes).HasConstraintName("FK_AdminUserToMailbox_AdminUser_AdminUserID");
        });

        modelBuilder.Entity<AppModule>(entity =>
        {
            entity.HasKey(e => e.AppModuleId).HasName("PK_ModuleID");

            entity.Property(e => e.InstallDate).HasDefaultValueSql("(sysdatetime())");
        });

        modelBuilder.Entity<AppModuleAccountSetting>(entity =>
        {
            entity.HasOne(d => d.Account).WithMany(p => p.AppModuleAccountSettings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppModuleAccountSetting_AccountID");

            entity.HasOne(d => d.AppModule).WithMany(p => p.AppModuleAccountSettings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppModuleAccountSetting_AppModuleID");
        });

        modelBuilder.Entity<AppModuleSetting>(entity =>
        {
            entity.HasOne(d => d.AppModule).WithMany(p => p.AppModuleSettings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AppModuleSetting_AppModule_AppModuleID");
        });

        modelBuilder.Entity<ApplicationIdentity>(entity =>
        {
            entity.HasOne(d => d.WireAccount).WithMany(p => p.ApplicationIdentities)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_ApplicationIdentity_WireAccount");
        });

        modelBuilder.Entity<ApplicationIdentityToCurrency>(entity =>
        {
            entity.HasKey(e => e.ApplicationIdentityToCurrencyId).IsClustered(false);

            entity.Property(e => e.CurrencyIsocode).IsFixedLength();

            entity.HasOne(d => d.ApplicationIdentity).WithMany(p => p.ApplicationIdentityToCurrencies).HasConstraintName("FK_ApplicationIdentityToCurrency_ApplicationIdentityID");

            entity.HasOne(d => d.CurrencyIsocodeNavigation).WithMany(p => p.ApplicationIdentityToCurrencies).HasConstraintName("FK_ApplicationIdentityToCurrency_CurrencyISOCode");
        });

        modelBuilder.Entity<ApplicationIdentityToMerchantGroup>(entity =>
        {
            entity.HasKey(e => e.ApplicationIdentityToMerchantGroupId).IsClustered(false);

            entity.HasOne(d => d.ApplicationIdentity).WithMany(p => p.ApplicationIdentityToMerchantGroups).HasConstraintName("FK_ApplicationIdentityToMerchantGroup_ApplicationIdentity");

            entity.HasOne(d => d.MerchantGroup).WithMany(p => p.ApplicationIdentityToMerchantGroups).HasConstraintName("FK_ApplicationIdentityToMerchantGroup_MerchantGroup");
        });

        modelBuilder.Entity<ApplicationIdentityToPaymentMethod>(entity =>
        {
            entity.HasKey(e => e.ApplicationIdentityToPaymentMethodId).IsClustered(false);

            entity.HasIndex(e => new { e.ApplicationIdentityId, e.PaymentMethodId }, "UIX_ApplicationIdentityToPaymentMethod").IsClustered();

            entity.HasOne(d => d.ApplicationIdentity).WithMany(p => p.ApplicationIdentityToPaymentMethods).HasConstraintName("FK_ApplicationIdentityToPaymentMethod_ApplicationIdentity");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.ApplicationIdentityToPaymentMethods).HasConstraintName("FK_ApplicationIdentityToPaymentMethod_PaymentMethod");
        });

        modelBuilder.Entity<ApplicationIdentityToken>(entity =>
        {
            entity.HasKey(e => e.ApplicationIdentityTokenId).IsClustered(false);

            entity.Property(e => e.Token).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.ApplicationIdentity).WithMany(p => p.ApplicationIdentityTokens).HasConstraintName("FK_ApplicationIdentityToken_ApplicationIdentity");
        });

        modelBuilder.Entity<AuthorizationBatch>(entity =>
        {
            entity.HasKey(e => e.AuthorizationBatchId).IsClustered(false);

            entity.HasIndex(e => new { e.BatchInternalKey, e.TransTerminalNumber }, "IX_AuthorizationBatch_InternalKey_TerminalNumber").IsClustered();

            entity.Property(e => e.BatchDate).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.ActionStatus).WithMany(p => p.AuthorizationBatches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuthorizationBatch_ActionStatusID");
        });

        modelBuilder.Entity<AuthorizationTransDatum>(entity =>
        {
            entity.HasKey(e => e.AuthorizationTransDataId).IsClustered(false);

            entity.HasOne(d => d.TransPass).WithMany(p => p.AuthorizationTransData)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_AuthorizationTransData_tblCompanyTransPass_TransPassID");

            entity.HasOne(d => d.TransPending).WithMany(p => p.AuthorizationTransData)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_AuthorizationTransData_tblCompanyTransPending_TransPendingID");

            entity.HasOne(d => d.TransPreAuth).WithMany(p => p.AuthorizationTransData)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_AuthorizationTransData_tblCompanyTransApproval_TransApprovalID");
        });

        modelBuilder.Entity<BlacklistMerchant>(entity =>
        {
            entity.Property(e => e.DeleteButton).HasComputedColumnSql("(('<input type=\"button\" class=\"buttonWhite buttonDelete\" value=\"Delete\" onclick=\"ConfirmAndDeleteRecord('+str([BlacklistMerchant_id]))+');\" />')", false);
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.SearchLink).HasComputedColumnSql("(('<a href=\"#\" onclick=\"OpenMerchantSearch('+str([BlacklistMerchant_id]))+');return false;\">Merchants</a>')", false);

            entity.HasOne(d => d.Merchant).WithMany(p => p.BlacklistMerchants)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_BlacklistMerchant_tblCompany");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasIndex(e => e.TransPassId, "IX_Cart_PassID")
                .HasFilter("([TransPass_id] IS NOT NULL)")
                .HasFillFactor(90);

            entity.HasIndex(e => e.TransPendingId, "IX_Cart_PendingID")
                .HasFilter("([TransPending_id] IS NOT NULL)")
                .HasFillFactor(90);

            entity.HasIndex(e => e.TransPreAuthId, "IX_Cart_PreAuthID")
                .HasFilter("([TransPreAuth_id] IS NOT NULL)")
                .HasFillFactor(90);

            entity.Property(e => e.CurrencyIsocode).IsFixedLength();
            entity.Property(e => e.StartDate).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Total).HasComputedColumnSql("([TotalProducts]+[TotalShipping])", true);

            entity.HasOne(d => d.CurrencyIsocodeNavigation).WithMany(p => p.Carts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cart_Currency");

            entity.HasOne(d => d.Customer).WithMany(p => p.Carts).HasConstraintName("FK_Cart_Customer_CustomerID");

            entity.HasOne(d => d.Merchant).WithMany(p => p.Carts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cart_tblCompany_MerchantID");

            entity.HasOne(d => d.TransPass).WithMany(p => p.Carts).HasConstraintName("FK_Cart_tblCompanyTransPass_TransPassID");

            entity.HasOne(d => d.TransPending).WithMany(p => p.Carts).HasConstraintName("FK_Cart_tblCompanyTransPending_TransPendingID");

            entity.HasOne(d => d.TransPreAuth).WithMany(p => p.Carts).HasConstraintName("FK_Cart_tblCompanyTransApproval_TransApprovalID");
        });

        modelBuilder.Entity<CartFailLog>(entity =>
        {
            entity.HasIndex(e => e.TransFailId, "IX_CartFailLog_FailID")
                .HasFilter("([TransFail_id] IS NOT NULL)")
                .HasFillFactor(90);

            entity.Property(e => e.InsertDate).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartFailLogs).HasConstraintName("FK_CartFailLog_Cart_CartID");

            entity.HasOne(d => d.Merchant).WithMany(p => p.CartFailLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartFailLog_tblCompany_MerchantID");

            entity.HasOne(d => d.TransFail).WithMany(p => p.CartFailLogs)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CartFailLog_tblCompanyTransFail_TransFailID");
        });

        modelBuilder.Entity<CartProduct>(entity =>
        {
            entity.Property(e => e.CurrencyIsocode).IsFixedLength();
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Total).HasComputedColumnSql("((([Price]+[ShippingFee])*[Quantity])*((1)+[VATPercent]))", true);
            entity.Property(e => e.TotalProduct).HasComputedColumnSql("(([Price]*[Quantity])*((1)+[VATPercent]))", true);
            entity.Property(e => e.TotalShipping).HasComputedColumnSql("(([ShippingFee]*[Quantity])*((1)+[VATPercent]))", true);

            entity.HasOne(d => d.Cart).WithMany(p => p.CartProducts).HasConstraintName("FK_CartProduct_Cart_CartID");

            entity.HasOne(d => d.CurrencyIsocodeNavigation).WithMany(p => p.CartProducts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartProduct_Currency");

            entity.HasOne(d => d.Merchant).WithMany(p => p.CartProducts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartProduct_tblCompany_MerchantID");

            entity.HasOne(d => d.Product).WithMany(p => p.CartProducts).HasConstraintName("FK_CartProduct_Product_ProductID");

            entity.HasOne(d => d.ProductStock).WithMany(p => p.CartProducts).HasConstraintName("FK_CartProduct_ProductStock_ProductStockID");

            entity.HasOne(d => d.ProductType).WithMany(p => p.CartProducts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartProduct_ProductTypeID");
        });

        modelBuilder.Entity<CartProductProperty>(entity =>
        {
            entity.HasOne(d => d.CartProduct).WithMany(p => p.CartProductProperties).HasConstraintName("FK_CartProductProperty_CartProduct_CartProductID");

            entity.HasOne(d => d.ProductProperty).WithMany(p => p.CartProductProperties)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CartProductProperty_ProductProperty_ProductPropertyID");
        });

        modelBuilder.Entity<ChangeAudit>(entity =>
        {
            entity.HasKey(e => e.ChangeAuditId).HasFillFactor(100);

            entity.Property(e => e.InsertDate).HasDefaultValueSql("(sysdatetime())");
        });

        modelBuilder.Entity<ChbReasonCode>(entity =>
        {
            entity.ToTable("ChbReasonCode", "List", tb => tb.HasComment("Creditcard's issuer CHB reason list"));
        });

        modelBuilder.Entity<CountryList>(entity =>
        {
            entity.HasKey(e => e.CountryIsocode).HasFillFactor(100);

            entity.Property(e => e.CountryIsocode).IsFixedLength();
            entity.Property(e => e.Isocode3).IsFixedLength();
            entity.Property(e => e.Isonumber).IsFixedLength();
            entity.Property(e => e.WorldRegionIsocode).IsFixedLength();

            entity.HasMany(d => d.CountryGroups).WithMany(p => p.CountryIsocodes)
                .UsingEntity<Dictionary<string, object>>(
                    "CountryListToCountryGroup",
                    r => r.HasOne<CountryGroup>().WithMany().HasForeignKey("CountryGroupId"),
                    l => l.HasOne<CountryList>().WithMany().HasForeignKey("CountryIsocode"),
                    j =>
                    {
                        j.HasKey("CountryIsocode", "CountryGroupId");
                        j.ToTable("CountryListToCountryGroup", "List");
                        j.IndexerProperty<string>("CountryIsocode")
                            .HasMaxLength(2)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("CountryISOCode");
                        j.IndexerProperty<int>("CountryGroupId").HasColumnName("CountryGroup_id");
                    });
        });

        modelBuilder.Entity<CurrencyList>(entity =>
        {
            entity.Property(e => e.CurrencyIsocode).IsFixedLength();
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsSymbolBeforeAmount).HasDefaultValue(true);
            entity.Property(e => e.Isonumber).IsFixedLength();
        });

        modelBuilder.Entity<CurrencyRate>(entity =>
        {
            entity.Property(e => e.BaseCurrencyIsocode).IsFixedLength();
            entity.Property(e => e.CurrencyIsocode).IsFixedLength();
            entity.Property(e => e.CurrencyRateTypeId).IsFixedLength();

            entity.HasOne(d => d.CurrencyIsocodeNavigation).WithMany(p => p.CurrencyRates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CurrencyRates_CurrencyISOCode");

            entity.HasOne(d => d.CurrencyRateType).WithMany(p => p.CurrencyRates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CurrencyRates_CurrencyRateTypeID");

            entity.HasOne(d => d.Merchant).WithMany(p => p.CurrencyRates).HasConstraintName("FK_CurrencyRates_MerchantID");
        });

        modelBuilder.Entity<CurrencyRateType>(entity =>
        {
            entity.Property(e => e.CurrencyRateTypeId).IsFixedLength();
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK_tblCustomer");

            entity.ToTable("Customer", "Data", tb => tb.HasTrigger("Customer_UpdateAccountID"));

            entity.Property(e => e.CustomerNumber).IsFixedLength();
            entity.Property(e => e.Pincode).IsFixedLength();
            entity.Property(e => e.RegistrationDate).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.ActiveStatus).WithMany(p => p.Customers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_ActiveStatus");

            entity.HasOne(d => d.ApplicationIdentity).WithMany(p => p.Customers)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Customer_ApplicationIdentityID");
        });

        modelBuilder.Entity<CustomerRelation>(entity =>
        {
            entity.HasKey(e => new { e.CustomerId, e.TargetCustomerId }).HasFillFactor(100);

            entity.Property(e => e.IsActive).HasDefaultValue(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerRelationCustomers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerRelation_CustomerID");

            entity.HasOne(d => d.PeopleRelationType).WithMany(p => p.CustomerRelations)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_CustomerRelation_PeopleRelationTypeID");

            entity.HasOne(d => d.TargetCustomer).WithMany(p => p.CustomerRelationTargetCustomers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerRelation_TargetCustomerID");
        });

        modelBuilder.Entity<CustomerShippingDetail>(entity =>
        {
            entity.HasOne(d => d.AccountAddress).WithMany(p => p.CustomerShippingDetails)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_CustomerShippingDetail_AccountAddress");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerShippingDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerShippingDetail_CustomerID");
        });

        modelBuilder.Entity<Dbversion>(entity =>
        {
            entity.HasKey(e => e.DbversionId).HasFillFactor(100);

            entity.Property(e => e.DeployDate).HasDefaultValueSql("(sysdatetime())");
        });

        modelBuilder.Entity<EventPending>(entity =>
        {
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.TryCount).HasDefaultValue((byte)1);

            entity.HasOne(d => d.AdminUser).WithMany(p => p.EventPendings)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_EventPending_SystemAdminUser_AdminUserID");

            entity.HasOne(d => d.Affiliate).WithMany(p => p.EventPendings)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_EventPending_tblAffiliates_AffiliateID");

            entity.HasOne(d => d.Customer).WithMany(p => p.EventPendings).HasConstraintName("FK_EventPending_Customer");

            entity.HasOne(d => d.EventPendingType).WithMany(p => p.EventPendings).HasConstraintName("FK_EventPending_EventPendingType");

            entity.HasOne(d => d.Merchant).WithMany(p => p.EventPendings).HasConstraintName("FK_EventPending_Merchant");

            entity.HasOne(d => d.TransFail).WithMany(p => p.EventPendings)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_EventPending_tblCompanyTransFail");

            entity.HasOne(d => d.TransPass).WithMany(p => p.EventPendings)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_EventPending_tblCompanyTransPass");

            entity.HasOne(d => d.TransPending).WithMany(p => p.EventPendings).HasConstraintName("FK_EventPending_tblCompanyTransPending");

            entity.HasOne(d => d.TransPreAuth).WithMany(p => p.EventPendings)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_EventPending_tblCompanyTransApproval");
        });

        modelBuilder.Entity<EventPendingType>(entity =>
        {
            entity.Property(e => e.EventPendingTypeId).ValueGeneratedNever();
        });

        modelBuilder.Entity<ExternalServiceHistory>(entity =>
        {
            entity.HasOne(d => d.Account).WithMany(p => p.ExternalServiceHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExternalServiceHistory_AccountID");

            entity.HasOne(d => d.ExternalServiceAction).WithMany(p => p.ExternalServiceHistories).HasConstraintName("FK_ExternalServiceHistory_ExternalServiceActionID");

            entity.HasOne(d => d.ExternalServiceType).WithMany(p => p.ExternalServiceHistories).HasConstraintName("FK_ExternalServiceHistory_ExternalServiceTypeID");
        });

        modelBuilder.Entity<FraudDetection>(entity =>
        {
            entity.HasKey(e => e.FraudDetectionId).IsClustered(false);

            entity.HasIndex(e => e.InsertDate, "IX_FraudDetection_InsertDate").IsClustered();

            entity.Property(e => e.BinCountry).IsFixedLength();
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ReturnQueriesRemaining).IsFixedLength();
            entity.Property(e => e.SendingBin).IsFixedLength();
            entity.Property(e => e.SendingCountry).IsFixedLength();
            entity.Property(e => e.SendingIp).IsFixedLength();
            entity.Property(e => e.SendingRegion).IsFixedLength();
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.HistoryType).WithMany(p => p.Histories).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<HostedPageUrl>(entity =>
        {
            entity.Property(e => e.IsEnabled).HasDefaultValue(true);

            entity.HasOne(d => d.Merchant).WithMany(p => p.HostedPageUrls)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HostedPageURL_MerchantID");
        });

        modelBuilder.Entity<LanguageList>(entity =>
        {
            entity.Property(e => e.LanguageId).ValueGeneratedOnAdd();
            entity.Property(e => e.Iso2).IsFixedLength();
            entity.Property(e => e.Iso3).IsFixedLength();
            entity.Property(e => e.LanguageIsocode).IsFixedLength();
        });

        modelBuilder.Entity<LoginAccount>(entity =>
        {
            entity.HasOne(d => d.LoginRole).WithMany(p => p.LoginAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LoginAccount_LoginRole_LoginRoleID");
        });

        modelBuilder.Entity<LoginHistory>(entity =>
        {
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.LoginAccount).WithMany(p => p.LoginHistories).HasConstraintName("FK_LoginHistory_LoginAccountID");

            entity.HasOne(d => d.LoginResult).WithMany(p => p.LoginHistories).HasConstraintName("FK_LoginHistory_LoginResultID");
        });

        modelBuilder.Entity<LoginPassword>(entity =>
        {
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.LoginAccount).WithMany(p => p.LoginPasswords).HasConstraintName("FK_LoginPassword_LoginAccountID");
        });

        modelBuilder.Entity<MerchantActivity>(entity =>
        {
            entity.Property(e => e.MerchantId).ValueGeneratedNever();
        });

        modelBuilder.Entity<MerchantDepartment>(entity =>
        {
            entity.Property(e => e.MerchantDepartmentId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<MerchantSetCartInstallment>(entity =>
        {
            entity.HasOne(d => d.Merchant).WithMany(p => p.MerchantSetCartInstallments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantSetCartInstallments_tblCompany_MerchantID");
        });

        modelBuilder.Entity<MerchantSetPayerAdditionalInfo>(entity =>
        {
            entity.ToTable("MerchantSetPayerAdditionalInfo", "Setting", tb => tb.HasComment("Merchant set extra fields for getting more info from client"));

            entity.HasOne(d => d.Merchant).WithMany(p => p.MerchantSetPayerAdditionalInfos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantSetPayerAdditionalInfo_MerchantID");
        });

        modelBuilder.Entity<MerchantSetShop>(entity =>
        {
            entity.Property(e => e.CurrencyIsocode).IsFixedLength();
            entity.Property(e => e.DefaultLanguageIsocode).IsFixedLength();

            entity.HasOne(d => d.DefaultLanguageIsocodeNavigation).WithMany(p => p.MerchantSetShops)
                .HasPrincipalKey(p => p.LanguageIsocode)
                .HasForeignKey(d => d.DefaultLanguageIsocode)
                .HasConstraintName("FK_MerchantSetShop_DefaultLanguageISOCode");

            entity.HasOne(d => d.Merchant).WithMany(p => p.MerchantSetShops)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MerchantSetShop_tblCompany_MerchantID");
        });

        modelBuilder.Entity<MerchantSetShopInstallment>(entity =>
        {
            entity.HasOne(d => d.MerchantSetShop).WithMany(p => p.MerchantSetShopInstallments).HasConstraintName("FK_MerchantSetShopInstallments_MerchantSetShop_MerchantSetShopID");
        });

        modelBuilder.Entity<MerchantSetShopToCountryRegion>(entity =>
        {
            entity.Property(e => e.CountryIsocode).IsFixedLength();
            entity.Property(e => e.WorldRegionIsocode).IsFixedLength();

            entity.HasOne(d => d.MerchantSetShop).WithMany(p => p.MerchantSetShopToCountryRegions).HasConstraintName("FK_MerchantSetShopToCountryRegion_MerchantSetShop_MerchantSetShopID");
        });

        modelBuilder.Entity<MobileDevice>(entity =>
        {
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Account).WithMany(p => p.MobileDevices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MobileDevice_Account_AccountID");

            entity.HasOne(d => d.AccountSubUser).WithMany(p => p.MobileDevices).HasConstraintName("FK_MobileDevice_AccountSubUser_AccountSubUserID");
        });

        modelBuilder.Entity<MonthlyFloorTotal>(entity =>
        {
            entity.Property(e => e.DateInFocus).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.SetTransactionFloor).WithMany(p => p.MonthlyFloorTotals).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<NewAppModule>(entity =>
        {
            entity.HasKey(e => e.AppModuleId).HasName("PK_new_ModuleID");
        });

        modelBuilder.Entity<NewSecurityObject>(entity =>
        {
            entity.HasOne(d => d.AppModule).WithMany(p => p.NewSecurityObjects).HasConstraintName("FK_new_SecurityObject_new_AppModule_AppModuleID");

            entity.HasOne(d => d.SolutionList).WithMany(p => p.NewSecurityObjects)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SecurityObject_SolutionListID");
        });

        modelBuilder.Entity<NewSecurityObjectToAdminGroup>(entity =>
        {
            entity.HasOne(d => d.AdminGroup).WithMany(p => p.NewSecurityObjectToAdminGroups).HasConstraintName("FK_new_SecurityObjectToAdminGroup_new_AdminGroup_AdminGroupID");

            entity.HasOne(d => d.SecurityObject).WithMany(p => p.NewSecurityObjectToAdminGroups).HasConstraintName("FK_new_SecurityObjectToAdminGroup_new_SecurityObject_SecurityObjectID");
        });

        modelBuilder.Entity<NewSecurityObjectToLoginAccount>(entity =>
        {
            entity.HasOne(d => d.LoginAccount).WithMany(p => p.NewSecurityObjectToLoginAccounts).HasConstraintName("FK_new_SecurityObjectToLoginAccount_new_LoginAccount_LoginAccountID");

            entity.HasOne(d => d.SecurityObject).WithMany(p => p.NewSecurityObjectToLoginAccounts).HasConstraintName("FK_new_SecurityObjectToLoginAccount_new_SecurityObject_SecurityObjectID");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.Property(e => e.PaymentMethodId).ValueGeneratedNever();
        });

        modelBuilder.Entity<PeopleRelationType>(entity =>
        {
            entity.HasKey(e => e.PeopleRelationTypeId).HasFillFactor(100);
        });

        modelBuilder.Entity<PeriodicFeeType>(entity =>
        {
            entity.Property(e => e.CurrencyIsocode).IsFixedLength();

            entity.HasOne(d => d.AccountType).WithMany(p => p.PeriodicFeeTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PeriodicFeeType_AccountTypeID");

            entity.HasOne(d => d.CurrencyIsocodeNavigation).WithMany(p => p.PeriodicFeeTypes).HasConstraintName("FK_PeriodicFeeType_CurrencyISOCode");

            entity.HasOne(d => d.ProcessMerchant).WithMany(p => p.PeriodicFeeTypes).HasConstraintName("FK_PeriodicFeeType_ProcessMerchantID");

            entity.HasOne(d => d.TimeInterval).WithMany(p => p.PeriodicFeeTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PeriodicFeeType_TimeUnitID");
        });

        modelBuilder.Entity<PhoneCarrier>(entity =>
        {
            entity.Property(e => e.PhoneCarrierId).ValueGeneratedNever();
        });

        modelBuilder.Entity<PhoneDetail>(entity =>
        {
            entity.HasIndex(e => e.BillingAddressId, "IX_TransPhoneDetail_BillingAddress_id").HasFilter("([BillingAddress_id] IS NOT NULL)");

            entity.HasOne(d => d.BillingAddress).WithMany(p => p.PhoneDetails).OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<PreCreatedPaymentMethod>(entity =>
        {
            entity.ToTable("PreCreatedPaymentMethod", "Data", tb => tb.HasComment("Temporary hold unassigned payment method provided by external issuer."));

            entity.Property(e => e.Value1First6Text).IsFixedLength();
            entity.Property(e => e.Value1Last4Text).IsFixedLength();

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.PreCreatedPaymentMethods)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PreCreatedPaymentMethod_PaymentMethodID");

            entity.HasOne(d => d.PaymentMethodProvider).WithMany(p => p.PreCreatedPaymentMethods).HasConstraintName("FK_PreCreatedPaymentMethod_PaymentMethodProviderID");
        });

        modelBuilder.Entity<ProcessApproved>(entity =>
        {
            entity.HasKey(e => e.ProcessApprovedId).HasName("PK_TrackProcessApproved");

            entity.HasIndex(e => e.PaymentMethodStamp, "IX_ProcessApproved_PaymentMethodStamp").HasFillFactor(90);

            entity.Property(e => e.TransDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Merchant).WithMany(p => p.ProcessApproveds).HasConstraintName("FK_ProcessApproved_tblCompany");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.ProcessApproveds).HasConstraintName("FK_ProcessApproved_PaymentMethod");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.CurrencyIsocode).IsFixedLength();

            entity.HasOne(d => d.CurrencyIsocodeNavigation).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Currency");

            entity.HasOne(d => d.Merchant).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_tblCompany_CompanyID");

            entity.HasOne(d => d.MerchantSetShop).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_MerchantSetShop_MerchantSetShopID");

            entity.HasOne(d => d.ProductType).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_ProductTypeID");

            entity.HasMany(d => d.ProductCategories).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductToProductCategory",
                    r => r.HasOne<ProductCategory>().WithMany()
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ProductToProductCategory_ProductCategory"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_ProductToProductCategory_Product"),
                    j =>
                    {
                        j.HasKey("ProductId", "ProductCategoryId");
                        j.ToTable("ProductToProductCategory", "Data");
                        j.IndexerProperty<int>("ProductId").HasColumnName("Product_id");
                        j.IndexerProperty<short>("ProductCategoryId").HasColumnName("ProductCategory_id");
                    });
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("FK_ProductProperty_ProductCategory_ParentID");
        });

        modelBuilder.Entity<ProductProperty>(entity =>
        {
            entity.HasOne(d => d.Merchant).WithMany(p => p.ProductProperties).HasConstraintName("FK_ProductProperty_tblCompany_CompanyID");

            entity.HasOne(d => d.ProductPropertyType).WithMany(p => p.ProductProperties)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductProperty_ProductPropertyTypeID");
        });

        modelBuilder.Entity<ProductStock>(entity =>
        {
            entity.HasOne(d => d.Product).WithMany(p => p.ProductStocks).HasConstraintName("FK_ProductStock_Product");
        });

        modelBuilder.Entity<ProductStockReference>(entity =>
        {
            entity.HasOne(d => d.Product).WithMany(p => p.ProductStockReferences).HasConstraintName("FK_ProductStockReference_Product");

            entity.HasOne(d => d.ProductProperty).WithMany(p => p.ProductStockReferences)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductStockReference_ProductProperty");

            entity.HasOne(d => d.ProductStock).WithMany(p => p.ProductStockReferences)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductStockReference_ProductStock");
        });

        modelBuilder.Entity<ProductTag>(entity =>
        {
            entity.HasOne(d => d.Product).WithMany(p => p.ProductTags).HasConstraintName("FK_ProductTag_Product");
        });

        modelBuilder.Entity<ProductText>(entity =>
        {
            entity.Property(e => e.LanguageIsocode).IsFixedLength();

            entity.HasOne(d => d.LanguageIsocodeNavigation).WithMany(p => p.ProductTexts)
                .HasPrincipalKey(p => p.LanguageIsocode)
                .HasForeignKey(d => d.LanguageIsocode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductText_LanguageISOCode");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductTexts).HasConstraintName("FK_ProductText_Product");
        });

        modelBuilder.Entity<Qnagroup>(entity =>
        {
            entity.HasKey(e => e.QnagroupId).HasFillFactor(90);

            entity.Property(e => e.QnagroupId).ValueGeneratedNever();

            entity.HasOne(d => d.Language).WithMany(p => p.Qnagroups).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<RecurringModify>(entity =>
        {
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Merchant).WithMany(p => p.RecurringModifies)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_RecurringModify_tblCompany");
        });

        modelBuilder.Entity<RiskRuleHistory>(entity =>
        {
            entity.HasKey(e => e.RiskRuleHistoryId).HasFillFactor(100);

            entity.ToTable("RiskRuleHistory", "Log", tb =>
                {
                    tb.HasComment("Account Files uploaded by admin users");
                    tb.HasTrigger("trRiskRuleHistory_SetMerchantID_Ins");
                });

            entity.Property(e => e.InsertDate).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Account).WithMany(p => p.RiskRuleHistories).HasConstraintName("FK_RiskRuleHistory_AccountID");

            entity.HasOne(d => d.Merchant).WithMany(p => p.RiskRuleHistories).HasConstraintName("FK_RiskRuleHistory_tblCompany_MerchantID");

            entity.HasOne(d => d.TransFail).WithMany(p => p.RiskRuleHistories).HasConstraintName("FK_RiskRuleHistory_TransFailID");

            entity.HasOne(d => d.TransPass).WithMany(p => p.RiskRuleHistories).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.TransPending).WithMany(p => p.RiskRuleHistories).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.TransPreAuth).WithMany(p => p.RiskRuleHistories).OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(d => d.AccountNotes).WithMany(p => p.RiskRuleHistories)
                .UsingEntity<Dictionary<string, object>>(
                    "RiskRuleHistoryToAccountNote",
                    r => r.HasOne<AccountNote>().WithMany()
                        .HasForeignKey("AccountNoteId")
                        .HasConstraintName("FK_RiskRuleHistoryToAccountNote_AccountNoteID"),
                    l => l.HasOne<RiskRuleHistory>().WithMany()
                        .HasForeignKey("RiskRuleHistoryId")
                        .HasConstraintName("FK_RiskRuleHistoryToAccountNote_RiskRuleHistoryID"),
                    j =>
                    {
                        j.HasKey("RiskRuleHistoryId", "AccountNoteId");
                        j.ToTable("RiskRuleHistoryToAccountNote", "Log");
                        j.IndexerProperty<int>("RiskRuleHistoryId").HasColumnName("RiskRuleHistory_id");
                        j.IndexerProperty<int>("AccountNoteId").HasColumnName("AccountNote_id");
                    });
        });

        modelBuilder.Entity<RunningProcess>(entity =>
        {
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(sysdatetime())");
        });

        modelBuilder.Entity<SecurityObject>(entity =>
        {
            entity.HasKey(e => e.SecurityObjectId).HasName("PK_AppModuleObject");

            entity.HasOne(d => d.AppModule).WithMany(p => p.SecurityObjects).HasConstraintName("FK_SecurityObject_AppModule_AppModuleID");
        });

        modelBuilder.Entity<SecurityObjectToAdminGroup>(entity =>
        {
            entity.HasOne(d => d.AdminGroup).WithMany(p => p.SecurityObjectToAdminGroups).HasConstraintName("FK_SecurityObjectToAdminGroup_AdminGroup_AdminGroupID");

            entity.HasOne(d => d.SecurityObject).WithMany(p => p.SecurityObjectToAdminGroups).HasConstraintName("FK_SecurityObjectToAdminGroup_SecurityObject_SecurityObjectID");
        });

        modelBuilder.Entity<SecurityObjectToLoginAccount>(entity =>
        {
            entity.HasOne(d => d.LoginAccount).WithMany(p => p.SecurityObjectToLoginAccounts).HasConstraintName("FK_SecurityObjectToLoginAccount_LoginAccount_LoginAccountID");

            entity.HasOne(d => d.SecurityObject).WithMany(p => p.SecurityObjectToLoginAccounts).HasConstraintName("FK_SecurityObjectToLoginAccount_SecurityObject_SecurityObjectID");
        });

        modelBuilder.Entity<SetMerchantAffiliate>(entity =>
        {
            entity.HasOne(d => d.Affiliate).WithMany(p => p.SetMerchantAffiliates).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Merchant).WithMany(p => p.SetMerchantAffiliates).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SetMerchantCart>(entity =>
        {
            entity.Property(e => e.MerchantId).ValueGeneratedNever();

            entity.HasOne(d => d.Merchant).WithOne(p => p.SetMerchantCart)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SetMerchantCart_tblCompany_MerchantID");
        });

        modelBuilder.Entity<SetMerchantInstallment>(entity =>
        {
            entity.HasOne(d => d.Merchant).WithMany(p => p.SetMerchantInstallments).HasConstraintName("FK_SetMerchantInstallments_MerchantID");
        });

        modelBuilder.Entity<SetMerchantInvoice>(entity =>
        {
            entity.Property(e => e.ExternalProviderId).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Merchant).WithOne(p => p.SetMerchantInvoice).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SetMerchantMaxmind>(entity =>
        {
            entity.Property(e => e.MerchantId).ValueGeneratedNever();

            entity.HasOne(d => d.Merchant).WithOne(p => p.SetMerchantMaxmind).HasConstraintName("FK_SetMerchantMaxmind_tblCompany_MerchantID");
        });

        modelBuilder.Entity<SetMerchantMobileApp>(entity =>
        {
            entity.Property(e => e.IsAllowTaxRateChange).HasDefaultValue(true);
            entity.Property(e => e.SyncToken).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<SetMerchantPeriodicFee>(entity =>
        {
            entity.Property(e => e.CurrencyIsocode).IsFixedLength();

            entity.HasOne(d => d.CurrencyIsocodeNavigation).WithMany(p => p.SetMerchantPeriodicFees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SetMerchantPeriodicFee_CurrencyISOCode");

            entity.HasOne(d => d.Merchant).WithMany(p => p.SetMerchantPeriodicFees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SetMerchantPeriodicFee_MerchantID");
        });

        modelBuilder.Entity<SetMerchantRisk>(entity =>
        {
            entity.Property(e => e.MerchantId).ValueGeneratedNever();
            entity.Property(e => e.Ph3aIsEnabled).HasDefaultValue(false);
            entity.Property(e => e.Ph3aMinScoreAllowed).HasDefaultValue((byte)0);
        });

        modelBuilder.Entity<SetMerchantRollingReserve>(entity =>
        {
            entity.Property(e => e.FixHoldCurrencyIsocode).IsFixedLength();

            entity.HasOne(d => d.Merchant).WithOne(p => p.SetMerchantRollingReserve).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SetMerchantSettlement>(entity =>
        {
            entity.ToTable("SetMerchantSettlement", "Setting", tb => tb.HasTrigger("trSetMerchantSettlement_RemoveNonSetRecords_Upd"));

            entity.Property(e => e.PayPercent).HasDefaultValue(100m);

            entity.HasOne(d => d.AutoIntervalTimeUnitNavigation).WithMany(p => p.SetMerchantSettlements).HasConstraintName("FK_SetMerchantSettlement_TimeUnitID");

            entity.HasOne(d => d.Currency).WithMany(p => p.SetMerchantSettlements).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Merchant).WithMany(p => p.SetMerchantSettlements).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SetPeriodicFee>(entity =>
        {
            entity.Property(e => e.CurrencyIsocode).IsFixedLength();

            entity.HasOne(d => d.Account).WithMany(p => p.SetPeriodicFees).HasConstraintName("FK_SetPeriodicFee_AccountID");

            entity.HasOne(d => d.AccountPaymentMethod).WithMany(p => p.SetPeriodicFees)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SetPeriodicFee_AccountPaymentMethodID");

            entity.HasOne(d => d.CurrencyIsocodeNavigation).WithMany(p => p.SetPeriodicFees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SetPeriodicFee_CurrencyISOCode");

            entity.HasOne(d => d.PeriodicFeeType).WithMany(p => p.SetPeriodicFees).HasConstraintName("FK_SetPeriodicFee_PeriodicFeeTypeID");
        });

        modelBuilder.Entity<SetRiskRule>(entity =>
        {
            entity.HasKey(e => e.SetRiskRuleId).HasFillFactor(100);

            entity.ToTable("SetRiskRule", "Setting", tb => tb.HasComment("Risk rules set for account transactions, When account is NULL it is a global rule"));

            entity.HasOne(d => d.Account).WithMany(p => p.SetRiskRules).HasConstraintName("FK_SetRiskRule_AccountID");
        });

        modelBuilder.Entity<SetTransactionFee>(entity =>
        {
            entity.HasKey(e => e.SetTransactionFeeId).HasFillFactor(100);

            entity.Property(e => e.CurrencyIsocode).IsFixedLength();

            entity.HasOne(d => d.Account).WithMany(p => p.SetTransactionFees)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SetTransactionFee_AccountID");

            entity.HasOne(d => d.AmountType).WithMany(p => p.SetTransactionFees).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.FeeCalcMethod).WithMany(p => p.SetTransactionFees).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SettlementType).WithMany(p => p.SetTransactionFees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SetTransactionFee_SettlementType_SettlementType_id");
        });

        modelBuilder.Entity<SetTransactionFloor>(entity =>
        {
            entity.Property(e => e.CurrencyIsocode).IsFixedLength();

            entity.HasOne(d => d.Account).WithMany(p => p.SetTransactionFloors)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SetTransactionFloor_AccountID");

            entity.HasOne(d => d.CurrencyIsocodeNavigation).WithMany(p => p.SetTransactionFloors).HasConstraintName("FK_SetTransactionFloore_CurrencyList_CurrencyISOCode");

            entity.HasOne(d => d.SettlementType).WithMany(p => p.SetTransactionFloors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SetTransactionFloor_SettlementType_SettlementType_id");
        });

        modelBuilder.Entity<SetTransactionFloorFee>(entity =>
        {
            entity.HasOne(d => d.SetTransactionFloor).WithMany(p => p.SetTransactionFloorFees).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SettlemenType>(entity =>
        {
            entity.HasKey(e => e.SettlementTypeId).HasName("PK_SettlementType");
        });

        modelBuilder.Entity<ShippingDetail>(entity =>
        {
            entity.HasKey(e => e.ShippingDetailId).HasName("PK_tblBillingAddress");

            entity.Property(e => e.AddressCountryIsocode).IsFixedLength();
            entity.Property(e => e.AddressStateIsocode).IsFixedLength();

            entity.HasOne(d => d.AddressCountryIsocodeNavigation).WithMany(p => p.ShippingDetails).HasConstraintName("FK_ShippingDetail_AddressCountryISOCode");

            entity.HasOne(d => d.AddressStateIsocodeNavigation).WithMany(p => p.ShippingDetails).HasConstraintName("FK_ShippingDetail_AddressStateISOCode");

            entity.HasOne(d => d.TableList).WithMany(p => p.ShippingDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShippingDetail_TableList");
        });

        modelBuilder.Entity<Siccode>(entity =>
        {
            entity.Property(e => e.SiccodeNumber).ValueGeneratedNever();
        });

        modelBuilder.Entity<SolutionBulletin>(entity =>
        {
            entity.HasOne(d => d.SolutionList).WithMany(p => p.SolutionBulletins).HasConstraintName("FK_SolutionBulletin_SolutionListID");
        });

        modelBuilder.Entity<StateList>(entity =>
        {
            entity.HasKey(e => e.StateIsocode).HasFillFactor(100);

            entity.Property(e => e.StateIsocode).IsFixedLength();
            entity.Property(e => e.CountryIsocode).IsFixedLength();

            entity.HasOne(d => d.CountryIsocodeNavigation).WithMany(p => p.StateLists).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<StatusHistory>(entity =>
        {
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.ActionStatus).WithMany(p => p.StatusHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StatusHistory_ActionStatusID");

            entity.HasOne(d => d.StatusHistoryType).WithMany(p => p.StatusHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StatusHistory_StatusHistoryTypeID");
        });

        modelBuilder.Entity<SysErrorCode>(entity =>
        {
            entity.HasKey(e => e.SysErrorCodeId).HasFillFactor(100);

            entity.Property(e => e.LanguageIsocode).IsFixedLength();
        });

        modelBuilder.Entity<SystemUserType>(entity =>
        {
            entity.HasKey(e => e.UserTypeId).HasName("PK_UserType");
        });

        modelBuilder.Entity<TaskLock>(entity =>
        {
            entity.HasKey(e => e.TaskName).HasFillFactor(100);

            entity.Property(e => e.LastRunDate).HasDefaultValueSql("(sysdatetime())");
        });

        modelBuilder.Entity<TblAffiliate>(entity =>
        {
            entity.HasKey(e => e.AffiliatesId).HasFillFactor(90);

            entity.ToTable("tblAffiliates", tb => tb.HasTrigger("Affiliates_UpdateAccountID"));

            entity.Property(e => e.AfcompanyId).HasDefaultValue(0);
            entity.Property(e => e.AflinkRefId).IsFixedLength();
            entity.Property(e => e.Afsettlements).HasDefaultValue(true);
            entity.Property(e => e.Comments).HasDefaultValue("");
            entity.Property(e => e.ControlPanelUsername).HasDefaultValue("");
            entity.Property(e => e.HeaderImageFileName).HasDefaultValue("");
            entity.Property(e => e.HeaderSmallImageFileName).HasDefaultValue("");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadAba).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadAba2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadAccountName).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadAccountName2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadAccountNumber).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadAccountNumber2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankAddress).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankAddress2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankAddressCity).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankAddressCity2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankAddressCountry).HasDefaultValue(0);
            entity.Property(e => e.PaymentAbroadBankAddressCountry2).HasDefaultValue(0);
            entity.Property(e => e.PaymentAbroadBankAddressSecond).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankAddressSecond2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankAddressState).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankAddressState2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankAddressZip).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankAddressZip2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankName).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankName2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadIban).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadIban2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadSepaBic).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadSepaBic2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadSortCode).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadSortCode2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadSwiftNumber).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadSwiftNumber2).HasDefaultValue("");

            entity.HasMany(d => d.ApplicationIdentities).WithMany(p => p.Affiliates)
                .UsingEntity<Dictionary<string, object>>(
                    "TblAffiliatesToApplicationIdentity",
                    r => r.HasOne<ApplicationIdentity>().WithMany()
                        .HasForeignKey("ApplicationIdentityId")
                        .HasConstraintName("FK_AffiliatesToApplicationIdentity_ApplicationIdentityID"),
                    l => l.HasOne<TblAffiliate>().WithMany()
                        .HasForeignKey("AffiliatesId")
                        .HasConstraintName("FK_AffiliatesToApplicationIdentity_AffiliatesID"),
                    j =>
                    {
                        j.HasKey("AffiliatesId", "ApplicationIdentityId").HasName("PK_AffiliatesToApplicationIdentity");
                        j.ToTable("tblAffiliatesToApplicationIdentity");
                        j.IndexerProperty<int>("AffiliatesId").HasColumnName("Affiliates_id");
                        j.IndexerProperty<int>("ApplicationIdentityId").HasColumnName("ApplicationIdentity_id");
                    });
        });

        modelBuilder.Entity<TblAffiliateBankAccount>(entity =>
        {
            entity.Property(e => e.AbaAba).HasDefaultValue("");
            entity.Property(e => e.AbaAba2).HasDefaultValue("");
            entity.Property(e => e.AbaAccountName).HasDefaultValue("");
            entity.Property(e => e.AbaAccountName2).HasDefaultValue("");
            entity.Property(e => e.AbaAccountNumber).HasDefaultValue("");
            entity.Property(e => e.AbaAccountNumber2).HasDefaultValue("");
            entity.Property(e => e.AbaBankAddress).HasDefaultValue("");
            entity.Property(e => e.AbaBankAddress2).HasDefaultValue("");
            entity.Property(e => e.AbaBankAddressCity).HasDefaultValue("");
            entity.Property(e => e.AbaBankAddressCity2).HasDefaultValue("");
            entity.Property(e => e.AbaBankAddressSecond).HasDefaultValue("");
            entity.Property(e => e.AbaBankAddressSecond2).HasDefaultValue("");
            entity.Property(e => e.AbaBankAddressState).HasDefaultValue("");
            entity.Property(e => e.AbaBankAddressState2).HasDefaultValue("");
            entity.Property(e => e.AbaBankAddressZip).HasDefaultValue("");
            entity.Property(e => e.AbaBankAddressZip2).HasDefaultValue("");
            entity.Property(e => e.AbaBankName).HasDefaultValue("");
            entity.Property(e => e.AbaBankName2).HasDefaultValue("");
            entity.Property(e => e.AbaIban).HasDefaultValue("");
            entity.Property(e => e.AbaIban2).HasDefaultValue("");
            entity.Property(e => e.AbaSepaBic).HasDefaultValue("");
            entity.Property(e => e.AbaSepaBic2).HasDefaultValue("");
            entity.Property(e => e.AbaSortCode).HasDefaultValue("");
            entity.Property(e => e.AbaSortCode2).HasDefaultValue("");
            entity.Property(e => e.AbaSwiftNumber).HasDefaultValue("");
            entity.Property(e => e.AbaSwiftNumber2).HasDefaultValue("");
        });

        modelBuilder.Entity<TblAffiliateFeeStep>(entity =>
        {
            entity.Property(e => e.AfsSlicePercent).HasDefaultValue(0m);

            entity.HasOne(d => d.AfsCurrencyNavigation).WithMany(p => p.TblAffiliateFeeSteps).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<TblAffiliatePayment>(entity =>
        {
            entity.Property(e => e.AfpInsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.AfpPaymentNote).HasDefaultValue("");
        });

        modelBuilder.Entity<TblAffiliatePaymentsLine>(entity =>
        {
            entity.HasOne(d => d.AfplAfp).WithMany(p => p.TblAffiliatePaymentsLines).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TblAffiliatesCount>(entity =>
        {
            entity.Property(e => e.Afcdate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Afcaff).WithMany(p => p.TblAffiliatesCounts).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TblAutoCapture>(entity =>
        {
            entity.Property(e => e.AuthorizedTransactionId).ValueGeneratedNever();

            entity.HasOne(d => d.CaptureTransaction).WithMany(p => p.TblAutoCaptures).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.DeclineTransaction).WithMany(p => p.TblAutoCaptures).OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<TblBankAccount>(entity =>
        {
            entity.Property(e => e.BaInsDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.BaUpdate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TblBillingAddress>(entity =>
        {
            entity.Property(e => e.Address1).HasDefaultValue("");
            entity.Property(e => e.Address2).HasDefaultValue("");
            entity.Property(e => e.City).HasDefaultValue("");
            entity.Property(e => e.CountryIso).HasDefaultValue("");
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.StateIso).HasDefaultValue("");
            entity.Property(e => e.ZipCode).HasDefaultValue("");
        });

        modelBuilder.Entity<TblBillingCompany>(entity =>
        {
            entity.Property(e => e.Address).HasDefaultValue("");
            entity.Property(e => e.Email).HasDefaultValue("");
            entity.Property(e => e.IsShow).HasDefaultValue(true);
            entity.Property(e => e.LanguageShow).HasDefaultValue("");
            entity.Property(e => e.Name).HasDefaultValue("");
            entity.Property(e => e.Number).HasDefaultValue("");

            entity.HasOne(d => d.CurrencyShowNavigation).WithMany(p => p.TblBillingCompanies).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<TblBlackListBin>(entity =>
        {
            entity.Property(e => e.Bin).HasDefaultValue("");
            entity.Property(e => e.PrimaryId).HasDefaultValue(0);
        });

        modelBuilder.Entity<TblBlcommon>(entity =>
        {
            entity.Property(e => e.BlComment).HasDefaultValue("");
            entity.Property(e => e.BlInsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.BlUser).HasDefaultValueSql("(user_name())");

            entity.HasOne(d => d.BlBlockLevelNavigation).WithMany(p => p.TblBlcommons).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.BlCompany).WithMany(p => p.TblBlcommons).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TblBnsStoredCard>(entity =>
        {
            entity.HasIndex(e => e.Identifier256, "IX_tblBnsStoredCard_Identifier256")
                .IsUnique()
                .HasFillFactor(80);
        });

        modelBuilder.Entity<TblCcstorage>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.HasIndex(e => new { e.CompanyId, e.CcardNumber256, e.IsDeleted }, "IX_StoreCreditCard_companyID_CCard_number256_isDeleted")
                .IsDescending(true, false, false)
                .HasFillFactor(80);

            entity.Property(e => e.Bincountry).IsFixedLength();
            entity.Property(e => e.CcCui).HasDefaultValue("");
            entity.Property(e => e.CcardDisplay).HasDefaultValue("");
            entity.Property(e => e.Chemail).HasDefaultValue("");
            entity.Property(e => e.ChfullName).HasDefaultValue("");
            entity.Property(e => e.ChpersonalNum).HasDefaultValue("");
            entity.Property(e => e.ChphoneNumber).HasDefaultValue("");
            entity.Property(e => e.Comment).HasDefaultValue("");
            entity.Property(e => e.ExpMm)
                .HasDefaultValue("")
                .IsFixedLength();
            entity.Property(e => e.ExpYy)
                .HasDefaultValue("")
                .IsFixedLength();
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Ipaddress).HasDefaultValue("");

            entity.HasOne(d => d.Company).WithMany(p => p.TblCcstorages).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Country).WithMany(p => p.TblCcstorages)
                .HasPrincipalKey(p => p.CountryId)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_tblCCStorage_CountryList_CountryId");

            entity.HasOne(d => d.PaymentMethodNavigation).WithMany(p => p.TblCcstorages).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.State).WithMany(p => p.TblCcstorages)
                .HasPrincipalKey(p => p.StateId)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_tblCCStorage_StateList_StateId");
        });

        modelBuilder.Entity<TblChbFileLog>(entity =>
        {
            entity.Property(e => e.ActionDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.ActionTypeNavigation).WithMany(p => p.TblChbFileLogs).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<TblChbPending>(entity =>
        {
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TblCheckDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.HasIndex(e => e.BillingAddressId, "IX_tblCheckDetails_BillingAddressId").HasFilter("([BillingAddressId] IS NOT NULL)");

            entity.HasIndex(e => e.CompanyId, "IX_tblCheckDetails_companyId").HasFillFactor(90);

            entity.Property(e => e.AccountName).HasDefaultValue("");
            entity.Property(e => e.BankCity).HasDefaultValue("");
            entity.Property(e => e.BankCountry).HasDefaultValue("US");
            entity.Property(e => e.BankName).HasDefaultValue("");
            entity.Property(e => e.BankPhone).HasDefaultValue("");
            entity.Property(e => e.BankState).HasDefaultValue("");
            entity.Property(e => e.BillingAddressId).HasDefaultValue(0);
            entity.Property(e => e.BirthDate).HasDefaultValue("");
            entity.Property(e => e.Comment).HasDefaultValue("");
            entity.Property(e => e.CustomerId)
                .HasDefaultValue(0)
                .HasComment("");
            entity.Property(e => e.Email).HasDefaultValue("");
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PersonalNumber).HasDefaultValue("");
            entity.Property(e => e.PhoneNumber).HasDefaultValue("");

            entity.HasOne(d => d.BillingAddress).WithMany(p => p.TblCheckDetails).OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<TblCompany>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.ToTable("tblCompany", tb =>
                {
                    tb.HasTrigger("Company_UpdateAccountID");
                    tb.HasTrigger("trCompany_MerchantActivity_InsDel");
                    tb.HasTrigger("trgCompanyAddMerchantProcessingData");
                    tb.HasTrigger("trgCompanyAddPeriodicFee");
                    tb.HasTrigger("trgCompanyAddSetCustomerNumber");
                    tb.HasTrigger("trgCompanyAddSetDefaultBillingCompany");
                    tb.HasTrigger("trgCompanyAddSetParentCompany");
                    tb.HasTrigger("trgCompanyUpdateCutRecurringTests");
                });

            entity.Property(e => e.ActiveStatus).HasDefaultValue((byte)1);
            entity.Property(e => e.AffiliateFeeView).HasDefaultValue(100m);
            entity.Property(e => e.AllowedAmounts).HasDefaultValue("");
            entity.Property(e => e.BillingFor).HasDefaultValue("");
            entity.Property(e => e.Blocked).HasDefaultValue(true);
            entity.Property(e => e.CareOfAdminUser).HasDefaultValue("");
            entity.Property(e => e.CcardCui).HasDefaultValue("");
            entity.Property(e => e.CcardExpMm).HasDefaultValue("");
            entity.Property(e => e.CcardExpYy).HasDefaultValue("");
            entity.Property(e => e.CcardHolderName).HasDefaultValue("");
            entity.Property(e => e.Cellular).HasDefaultValue("");
            entity.Property(e => e.CffResetDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ChargeLimitDollar).HasDefaultValue(50000.00m);
            entity.Property(e => e.ChargeLimitShekel).HasDefaultValue(50000.00m);
            entity.Property(e => e.ChargebackNotifyMail).HasDefaultValue("");
            entity.Property(e => e.Comment).HasDefaultValue("");
            entity.Property(e => e.CommentMisc).HasDefaultValue("");
            entity.Property(e => e.CompanyFax).HasDefaultValue("");
            entity.Property(e => e.CompanyLegalName).HasDefaultValue("");
            entity.Property(e => e.CompanyLegalNumber).HasDefaultValue("");
            entity.Property(e => e.CompanyName).HasDefaultValue("");
            entity.Property(e => e.CompanyPhone).HasDefaultValue("");
            entity.Property(e => e.CountryBlackList).HasDefaultValue("");
            entity.Property(e => e.CountryWhiteList).HasDefaultValue("");
            entity.Property(e => e.CustomerNumber).HasDefaultValue("");
            entity.Property(e => e.CustomerPurchasePayerIdtext).HasDefaultValue("");
            entity.Property(e => e.CyclePeriod).HasDefaultValue("");
            entity.Property(e => e.DebitCompanyExId).HasDefaultValue("");
            entity.Property(e => e.Descriptor).HasDefaultValue("");
            entity.Property(e => e.FirstName).HasDefaultValue("");
            entity.Property(e => e.HashKey).HasDefaultValue("");
            entity.Property(e => e.Idnumber).HasDefaultValue("");
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IpOnReg).HasDefaultValue("0.0.0.0");
            entity.Property(e => e.IpwhiteList).HasDefaultValue("");
            entity.Property(e => e.IsAllowSilentPostCcDetails).HasDefaultValue(true);
            entity.Property(e => e.IsCustomerPurchaseCvv2).HasDefaultValue(true);
            entity.Property(e => e.IsCustomerPurchaseEmail).HasDefaultValue(true);
            entity.Property(e => e.IsCustomerPurchasePersonalNumber).HasDefaultValue(true);
            entity.Property(e => e.IsCustomerPurchasePhoneNumber).HasDefaultValue(true);
            entity.Property(e => e.IsCvv2).HasDefaultValue(true);
            entity.Property(e => e.IsEmail).HasDefaultValue(true);
            entity.Property(e => e.IsHidePayNeed).HasDefaultValue(true);
            entity.Property(e => e.IsInterestedInNewsletter).HasDefaultValue(true);
            entity.Property(e => e.IsMultiChargeProtection).HasDefaultValue(true);
            entity.Property(e => e.IsEzzygateTerminal).HasDefaultValue(true);
            entity.Property(e => e.IsPersonalNumber).HasDefaultValue(true);
            entity.Property(e => e.IsPhoneNumber).HasDefaultValue(true);
            entity.Property(e => e.IsPublicPayCvv2).HasDefaultValue(true);
            entity.Property(e => e.IsPublicPayEmail).HasDefaultValue(true);
            entity.Property(e => e.IsPublicPayPersonalNumber).HasDefaultValue(true);
            entity.Property(e => e.IsPublicPayPhoneNumber).HasDefaultValue(true);
            entity.Property(e => e.IsRemoteChargeCvv2).HasDefaultValue(true);
            entity.Property(e => e.IsRemoteChargeEchCvv2).HasDefaultValue(true);
            entity.Property(e => e.IsRemoteChargeEchEmail).HasDefaultValue(true);
            entity.Property(e => e.IsRemoteChargeEchPersonalNumber).HasDefaultValue(true);
            entity.Property(e => e.IsRemoteChargeEchPhoneNumber).HasDefaultValue(true);
            entity.Property(e => e.IsRemoteChargeEmail).HasDefaultValue(true);
            entity.Property(e => e.IsRemoteChargePersonalNumber).HasDefaultValue(true);
            entity.Property(e => e.IsRemoteChargePhoneNumber).HasDefaultValue(true);
            entity.Property(e => e.IsSystemPayCvv2).HasDefaultValue(true);
            entity.Property(e => e.IsSystemPayEmail).HasDefaultValue(true);
            entity.Property(e => e.IsSystemPayPersonalNumber).HasDefaultValue(true);
            entity.Property(e => e.IsSystemPayPhoneNumber).HasDefaultValue(true);
            entity.Property(e => e.IsUsingNewTerminal).HasDefaultValue(false);
            entity.Property(e => e.LanguagePreference).HasDefaultValue("eng");
            entity.Property(e => e.LastName).HasDefaultValue("");
            entity.Property(e => e.Mail).HasDefaultValue("");
            entity.Property(e => e.MaxMindMinScore).HasDefaultValue(5m);
            entity.Property(e => e.MerchantClosingDate).HasDefaultValueSql("('')");
            entity.Property(e => e.MerchantOpenningDate).HasDefaultValueSql("('')");
            entity.Property(e => e.MerchantSupportEmail).HasDefaultValue("");
            entity.Property(e => e.MerchantSupportPhoneNum).HasDefaultValue("");
            entity.Property(e => e.MultiChargeProtectionMins).HasDefaultValue((short)5);
            entity.Property(e => e.PassNotifyEmail).HasDefaultValue("");
            entity.Property(e => e.PayPercent).HasDefaultValue(100m);
            entity.Property(e => e.PayingDates1).HasDefaultValue("");
            entity.Property(e => e.PayingDates2).HasDefaultValue("");
            entity.Property(e => e.PayingDates3).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadAba).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadAba2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadAccountName).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadAccountName2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadAccountNumber).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadAccountNumber2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankAddress).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankAddress2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankAddressCity).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankAddressCity2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankAddressSecond).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankAddressSecond2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankAddressState).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankAddressState2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankAddressZip).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankAddressZip2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankName).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadBankName2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadIban).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadIban2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadSepaBic).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadSepaBic2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadSortCode).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadSortCode2).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadSwiftNumber).HasDefaultValue("");
            entity.Property(e => e.PaymentAbroadSwiftNumber2).HasDefaultValue("");
            entity.Property(e => e.PaymentAccount).HasDefaultValue("");
            entity.Property(e => e.PaymentBranch).HasDefaultValue("");
            entity.Property(e => e.PaymentMethod).HasDefaultValue("");
            entity.Property(e => e.PaymentPayeeName).HasDefaultValue("");
            entity.Property(e => e.Phone).HasDefaultValue("");
            entity.Property(e => e.PslWalletCode).HasDefaultValue("");
            entity.Property(e => e.PslWalletId).HasDefaultValue("");
            entity.Property(e => e.RecurringLimitCharges).HasDefaultValue(400);
            entity.Property(e => e.RecurringLimitStages).HasDefaultValue(4);
            entity.Property(e => e.RecurringLimitYears).HasDefaultValue(3);
            entity.Property(e => e.ReferralName).HasDefaultValue("");
            entity.Property(e => e.RemotePullIps).HasDefaultValue("");
            entity.Property(e => e.RemoteRefundRequestIps).HasDefaultValue("");
            entity.Property(e => e.ReportMail).HasDefaultValue("");
            entity.Property(e => e.ReportMailOptions).HasDefaultValue((byte)0);
            entity.Property(e => e.RiskRating).HasDefaultValue((byte)50);
            entity.Property(e => e.RiskScore).HasDefaultValue(-1f);
            entity.Property(e => e.RrautoRet).HasDefaultValue(false);
            entity.Property(e => e.SecurityDeposit).HasDefaultValue(10m);
            entity.Property(e => e.SecurityKey).HasDefaultValue("");
            entity.Property(e => e.SecurityPeriod).HasDefaultValue((short)6);
            entity.Property(e => e.SubAffiliateFeeView).HasDefaultValue(100m);
            entity.Property(e => e.Url).HasDefaultValue("");
            entity.Property(e => e.UserName).HasDefaultValue("");
            entity.Property(e => e.UserNameAlt).HasDefaultValue("");
            entity.Property(e => e.WalletIsEnable).HasDefaultValue(true);
            entity.Property(e => e.ZecureAccount).HasDefaultValue("");
            entity.Property(e => e.ZecurePassword).HasDefaultValue("");
            entity.Property(e => e.ZecureUsername).HasDefaultValue("");

            entity.HasOne(d => d.BillingCompanys).WithMany(p => p.TblCompanies).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.Group).WithMany(p => p.TblCompanies).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.ParentCompanyNavigation).WithMany(p => p.TblCompanies).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.PaymentBankNavigation).WithMany(p => p.TblCompanies).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.SiccodeNumberNavigation).WithMany(p => p.TblCompanies)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_tblCompany_SICCodeNumber");
        });

        modelBuilder.Entity<TblCompanyBalance>(entity =>
        {
            entity.ToTable("tblCompanyBalance", tb => tb.HasTrigger("trgCompanyBalanceRecalcBalance"));

            entity.Property(e => e.Comment).HasDefaultValue("");
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.SourceInfo).HasDefaultValue("");

            entity.HasOne(d => d.Company).WithMany(p => p.TblCompanyBalances).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.CurrencyNavigation).WithMany(p => p.TblCompanyBalances).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TblCompanyBatchFile>(entity =>
        {
            entity.HasKey(e => e.CompanyBatchFilesId).HasFillFactor(90);

            entity.Property(e => e.CbfinsDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CbfparseDate).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<TblCompanyChargeAdmin>(entity =>
        {
            entity.Property(e => e.PendingReplyUrl).HasDefaultValue("");
            entity.Property(e => e.RecurringModifyReplyUrl).HasDefaultValue("");
            entity.Property(e => e.RecurringReplyUrl).HasDefaultValue("");
            entity.Property(e => e.WalletReplyUrl).HasDefaultValue("");
        });

        modelBuilder.Entity<TblCompanyCreditFee>(entity =>
        {
            entity.Property(e => e.CcfExchangeTo).HasDefaultValue((short)-1);
            entity.Property(e => e.CcfListBins).HasDefaultValue("");
            entity.Property(e => e.CcfTerminalNumber).HasDefaultValue("");
            entity.Property(e => e.CurrencyRateTypeId).IsFixedLength();

            entity.HasOne(d => d.CurrencyRateType).WithMany(p => p.TblCompanyCreditFees).HasConstraintName("FK_CompanyCreditFees_CurrencyRateTypeID");
        });

        modelBuilder.Entity<TblCompanyCreditFeesTerminal>(entity =>
        {
            entity.Property(e => e.CcftTerminal).HasDefaultValue("");

            entity.HasOne(d => d.CcftCcf).WithMany(p => p.TblCompanyCreditFeesTerminals).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TblCompanyCreditRestriction>(entity =>
        {
            entity.Property(e => e.CcrListValue).HasDefaultValue("");
            entity.Property(e => e.CcrRatio).HasDefaultValue(1);
            entity.Property(e => e.CcrTerminalNumber).HasDefaultValue("");
        });

        modelBuilder.Entity<TblCompanyMakePaymentsProfile>(entity =>
        {
            entity.HasKey(e => e.CompanyMakePaymentsProfilesId).HasFillFactor(90);

            entity.Property(e => e.BankAbroadAba).HasDefaultValue("");
            entity.Property(e => e.BankAbroadAba2).HasDefaultValue("");
            entity.Property(e => e.BankAbroadAccountName).HasDefaultValue("");
            entity.Property(e => e.BankAbroadAccountName2).HasDefaultValue("");
            entity.Property(e => e.BankAbroadAccountNumber).HasDefaultValue("");
            entity.Property(e => e.BankAbroadAccountNumber2).HasDefaultValue("");
            entity.Property(e => e.BankAbroadBankAddress).HasDefaultValue("");
            entity.Property(e => e.BankAbroadBankAddress2).HasDefaultValue("");
            entity.Property(e => e.BankAbroadBankAddressCity).HasDefaultValue("");
            entity.Property(e => e.BankAbroadBankAddressCity2).HasDefaultValue("");
            entity.Property(e => e.BankAbroadBankAddressSecond).HasDefaultValue("");
            entity.Property(e => e.BankAbroadBankAddressSecond2).HasDefaultValue("");
            entity.Property(e => e.BankAbroadBankAddressState).HasDefaultValue("");
            entity.Property(e => e.BankAbroadBankAddressState2).HasDefaultValue("");
            entity.Property(e => e.BankAbroadBankAddressZip).HasDefaultValue("");
            entity.Property(e => e.BankAbroadBankAddressZip2).HasDefaultValue("");
            entity.Property(e => e.BankAbroadBankName).HasDefaultValue("");
            entity.Property(e => e.BankAbroadBankName2).HasDefaultValue("");
            entity.Property(e => e.BankAbroadIban).HasDefaultValue("");
            entity.Property(e => e.BankAbroadIban2).HasDefaultValue("");
            entity.Property(e => e.BankAbroadSepaBic).HasDefaultValue("");
            entity.Property(e => e.BankAbroadSepaBic2).HasDefaultValue("");
            entity.Property(e => e.BankAbroadSortCode).HasDefaultValue("");
            entity.Property(e => e.BankAbroadSortCode2).HasDefaultValue("");
            entity.Property(e => e.BankAbroadSwiftNumber).HasDefaultValue("");
            entity.Property(e => e.BankAbroadSwiftNumber2).HasDefaultValue("");
            entity.Property(e => e.BankIsraelInfoAccountNumber).HasDefaultValue("");
            entity.Property(e => e.BankIsraelInfoBankBranch).HasDefaultValue("");
            entity.Property(e => e.BankIsraelInfoBankCode).HasDefaultValue("0");
            entity.Property(e => e.BankIsraelInfoCompanyLegalNumber).HasDefaultValue("");
            entity.Property(e => e.BankIsraelInfoPayeeName).HasDefaultValue("");
            entity.Property(e => e.BankIsraelInfoPaymentMethod).HasDefaultValue("");
            entity.Property(e => e.BankIsraelInfoPersonalIdNumber).HasDefaultValue("");
            entity.Property(e => e.BasicInfoAddress).HasDefaultValue("");
            entity.Property(e => e.BasicInfoComment).HasDefaultValue("");
            entity.Property(e => e.BasicInfoContactPersonName).HasDefaultValue("");
            entity.Property(e => e.BasicInfoCostumerName).HasDefaultValue("");
            entity.Property(e => e.BasicInfoCostumerNumber).HasDefaultValue("");
            entity.Property(e => e.BasicInfoEmail).HasDefaultValue("");
            entity.Property(e => e.BasicInfoFaxNumber).HasDefaultValue("");
            entity.Property(e => e.BasicInfoPhoneNumber).HasDefaultValue("");
            entity.Property(e => e.ProfileType).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Company).WithMany(p => p.TblCompanyMakePaymentsProfiles).OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<TblCompanyMakePaymentsRequest>(entity =>
        {
            entity.HasKey(e => e.CompanyMakePaymentsRequestsId).HasFillFactor(90);

            entity.Property(e => e.BankIsraelInfoAccountNumber).HasDefaultValue("");
            entity.Property(e => e.BankIsraelInfoBankBranch).HasDefaultValue("");
            entity.Property(e => e.BankIsraelInfoCompanyLegalNumber).HasDefaultValue("");
            entity.Property(e => e.BankIsraelInfoPayeeName).HasDefaultValue("");
            entity.Property(e => e.BankIsraelInfoPaymentMethod).HasDefaultValue("");
            entity.Property(e => e.BankIsraelInfoPersonalIdNumber).HasDefaultValue("");
            entity.Property(e => e.PaymentDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PaymentMerchantComment).HasDefaultValue("");

            entity.HasOne(d => d.Company).WithMany(p => p.TblCompanyMakePaymentsRequests).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.CompanyMakePaymentsProfiles).WithMany(p => p.TblCompanyMakePaymentsRequests).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.PaymentCurrencyNavigation).WithMany(p => p.TblCompanyMakePaymentsRequests).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TblCompanyPaymentMethod>(entity =>
        {
            entity.HasOne(d => d.CpmCompany).WithMany(p => p.TblCompanyPaymentMethods).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.CpmCurrency).WithMany(p => p.TblCompanyPaymentMethods).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TblCompanySettingsHosted>(entity =>
        {
            entity.ToTable("tblCompanySettingsHosted", tb => tb.HasTrigger("trgCompanySettingsHosted_SetIsEnabled_InsUpd"));

            entity.Property(e => e.CompanyId).ValueGeneratedNever();
            entity.Property(e => e.IsShowAddress1).HasDefaultValue(true);
            entity.Property(e => e.IsShowAddress2).HasDefaultValue(true);
            entity.Property(e => e.IsShowAuthorizeCheckbox).HasDefaultValue(true);
            entity.Property(e => e.IsShowCity).HasDefaultValue(true);
            entity.Property(e => e.IsShowConfirmationPage).HasDefaultValue(true);
            entity.Property(e => e.IsShowCountry).HasDefaultValue(true);
            entity.Property(e => e.IsShowEmail).HasDefaultValue(true);
            entity.Property(e => e.IsShowMerchantDetails).HasDefaultValue(true);
            entity.Property(e => e.IsShowPersonalId).HasDefaultValue(true);
            entity.Property(e => e.IsShowPhone).HasDefaultValue(true);
            entity.Property(e => e.IsShowState).HasDefaultValue(true);
            entity.Property(e => e.IsShowZipCode).HasDefaultValue(true);
            entity.Property(e => e.IsWhiteLabel).HasDefaultValue(false);
            entity.Property(e => e.Uiversion).HasDefaultValue((byte)2);
        });

        modelBuilder.Entity<TblCompanyTransApproval>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.ToTable("tblCompanyTransApproval", tb =>
                {
                    tb.HasTrigger("trTransApproval_SetOldTransSource_InsUpd");
                    tb.HasTrigger("trTransApproval_SetPMDetailID_InsUpd");
                    tb.HasTrigger("trTransPreAuth_TrackMerchantActivity_Ins");
                });

            entity.HasIndex(e => e.CustomerId, "IX_tblCompanyTransApproval_CustomerID").HasFilter("([CustomerID]>(0))");

            entity.HasIndex(e => new { e.InsertDate, e.CompanyId, e.Currency }, "IX_tblCompanyTransApproval_InsertDate_CompanyID_Currency").HasFillFactor(90);

            entity.HasIndex(e => e.TransAnswerId, "IX_tblCompanyTransApproval_TransAnswerID")
                .IsUnique()
                .HasFilter("([TransAnswerID] IS NOT NULL)");

            entity.HasIndex(e => e.CompanyId, "IX_tblCompanyTransApproval_companyID")
                .IsDescending()
                .HasFillFactor(85);

            entity.Property(e => e.ApprovalNumber).HasDefaultValue("");
            entity.Property(e => e.Comment).HasDefaultValue("");
            entity.Property(e => e.DebitFee).HasDefaultValue(0m);
            entity.Property(e => e.DebitReferenceCode).HasDefaultValue("");
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Ipaddress).HasDefaultValue("");
            entity.Property(e => e.Is3Dsecure).HasDefaultValue(false);
            entity.Property(e => e.OrderNumber).HasDefaultValue("");
            entity.Property(e => e.PaymentMethodDisplay).HasDefaultValue("");
            entity.Property(e => e.PaymentMethodId).HasDefaultValue((byte)1);
            entity.Property(e => e.Payments).HasDefaultValue((byte)1);
            entity.Property(e => e.ReferringUrl).HasDefaultValue("");
            entity.Property(e => e.ReplyCode).HasDefaultValue("");
            entity.Property(e => e.TerminalNumber).HasDefaultValue("");
            entity.Property(e => e.TransactionTypeId).HasDefaultValue(7);

            entity.HasOne(d => d.CreditCard).WithMany(p => p.TblCompanyTransApprovals).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.CurrencyNavigation).WithMany(p => p.TblCompanyTransApprovals).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.DebitCompany).WithMany(p => p.TblCompanyTransApprovals).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.RecurringSeriesNavigation).WithMany(p => p.TblCompanyTransApprovals).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.TransAnswer).WithOne(p => p.TblCompanyTransApproval).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.TransPayerInfo).WithMany(p => p.TblCompanyTransApprovals)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_tblCompanyTransApproval_TransPayerInfo");

            entity.HasOne(d => d.TransPaymentMethod).WithMany(p => p.TblCompanyTransApprovals)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_tblCompanyTransApproval_TransPaymentMethod");
        });

        modelBuilder.Entity<TblCompanyTransCrypto>(entity =>
        {
            entity.ToTable("tblCompanyTransCrypto", tb => tb.HasComment("Transactions Crypto"));

            entity.Property(e => e.ConvertedCurrencyIsoCode).IsFixedLength();
            entity.Property(e => e.OriginalCurrencyIsoCode).IsFixedLength();
            entity.Property(e => e.TimeCreated).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Company).WithMany(p => p.TblCompanyTransCryptos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblCompanyTransCrypto_companyID");

            entity.HasOne(d => d.ConvertedCurrencyIsoCodeNavigation).WithMany(p => p.TblCompanyTransCryptoConvertedCurrencyIsoCodeNavigations).HasConstraintName("FK_tblCompanyTransCrypto_ConvertedCurrencyIsoCode");

            entity.HasOne(d => d.OriginalCurrencyIsoCodeNavigation).WithMany(p => p.TblCompanyTransCryptoOriginalCurrencyIsoCodeNavigations).HasConstraintName("FK_tblCompanyTransCrypto_OriginalCurrencyIsoCode");

            entity.HasOne(d => d.TransPayerInfo).WithMany(p => p.TblCompanyTransCryptos)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_tblCompanyTransCrypto_TransPayerInfo");

            entity.HasOne(d => d.TransPaymentBillingAddress).WithMany(p => p.TblCompanyTransCryptos)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_tblCompanyTransCrypto_TransPaymentBillingAddress");
        });

        modelBuilder.Entity<TblCompanyTransFail>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.ToTable("tblCompanyTransFail", tb =>
                {
                    tb.HasTrigger("trTransFail_SetDebitRuleLastFailDate_Ins");
                    tb.HasTrigger("trTransFail_SetOldTransSource_InsUpd");
                    tb.HasTrigger("trTransFail_SetPMDetailID_InsUpd");
                    tb.HasTrigger("trTransFail_TrackMerchantActivity_Ins");
                    tb.HasTrigger("trgCompanyTransFailAddBlockCC");
                });

            entity.HasIndex(e => e.CustomerId, "IX_tblCompanyTransFail_CustomerID").HasFilter("([CustomerID]>(0))");

            entity.HasIndex(e => new { e.CompanyId, e.InsertDate, e.Currency }, "IX_tblCompanyTransFail_companyID_InsertDate_Currency")
                .IsDescending(false, true, false)
                .HasFillFactor(90);

            entity.Property(e => e.ApprovalNumber).HasDefaultValue("");
            entity.Property(e => e.AutoRefundStatus).HasDefaultValue(0);
            entity.Property(e => e.Comment).HasDefaultValue("");
            entity.Property(e => e.DebitReferenceCode).HasDefaultValue("");
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Ipaddress).HasDefaultValue("");
            entity.Property(e => e.Ipcountry).HasDefaultValue("--");
            entity.Property(e => e.Is3Dsecure).HasDefaultValue(false);
            entity.Property(e => e.OrderNumber).HasDefaultValue("");
            entity.Property(e => e.PayId).HasDefaultValue(0);
            entity.Property(e => e.PayerIdUsed).HasDefaultValue("");
            entity.Property(e => e.PaymentMethodDisplay).HasDefaultValue("");
            entity.Property(e => e.PaymentMethodId).HasDefaultValue((byte)1);
            entity.Property(e => e.Payments).HasDefaultValue((byte)1);
            entity.Property(e => e.ReferringUrl).HasDefaultValue("");
            entity.Property(e => e.ReplyCode).HasDefaultValue("");
            entity.Property(e => e.TerminalNumber).HasDefaultValue("");
            entity.Property(e => e.TransactionTypeId).HasDefaultValue(7);

            entity.HasOne(d => d.CheckDetails).WithMany(p => p.TblCompanyTransFails).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.CreditCard).WithMany(p => p.TblCompanyTransFails).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.CurrencyNavigation).WithMany(p => p.TblCompanyTransFails).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.DebitCompany).WithMany(p => p.TblCompanyTransFails).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.TransPayerInfo).WithMany(p => p.TblCompanyTransFails)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_tblCompanyTransFail_TransPayerInfo");

            entity.HasOne(d => d.TransPaymentMethod).WithMany(p => p.TblCompanyTransFails)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_tblCompanyTransFail_TransPaymentMethod");
        });

        modelBuilder.Entity<TblCompanyTransInstallment>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.ToTable("tblCompanyTransInstallments", tb => tb.HasTrigger("trgCompanyTransInstallmentsUpdateSetUnsettled"));

            entity.HasIndex(e => e.PayId, "IX_tblCompanyTransInstallments_payID").HasFillFactor(90);

            entity.Property(e => e.Comment)
                .HasDefaultValue("")
                .IsFixedLength();
            entity.Property(e => e.MerchantPd).HasDefaultValueSql("('')");

            entity.HasOne(d => d.TransAns).WithMany(p => p.TblCompanyTransInstallments).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TblCompanyTransPass>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.ToTable("tblCompanyTransPass", tb =>
                {
                    tb.HasTrigger("trTransPass_RecurringBlockOnCHB_InsUpd");
                    tb.HasTrigger("trTransPass_SetOldTransSource_InsUpd");
                    tb.HasTrigger("trTransPass_SetPMDetailID_InsUpd");
                    tb.HasTrigger("trTransPass_SetUnsettled_Upd");
                    tb.HasTrigger("trTransPass_TrackMerchantActivity_Ins");
                    tb.HasTrigger("trgCompanyTransPassSetTerminalCHBCount");
                });

            entity.HasIndex(e => e.CustomerId, "IX_tblCompanyTransPass_CustomerID").HasFilter("([CustomerID]>(0))");

            entity.HasIndex(e => e.DebitReferenceCode, "IX_tblCompanyTransPass_DebitReferenceCode(InsertDate)")
                .HasFilter("([DebitReferenceCode] IS NOT NULL)")
                .HasFillFactor(80);

            entity.HasIndex(e => e.DebitReferenceNum, "IX_tblCompanyTransPass_DebitReferenceNum")
                .HasFilter("([DebitReferenceNum] IS NOT NULL)")
                .HasFillFactor(80);

            entity.HasIndex(e => e.PhoneDetailsId, "IX_tblCompanyTransPass_PhoneDetailsID").HasFilter("([PhoneDetailsID] IS NOT NULL)");

            entity.Property(e => e.ApprovalNumber).HasDefaultValue("");
            entity.Property(e => e.Comment).HasDefaultValue("");
            entity.Property(e => e.DebitReferenceCode).HasDefaultValue("");
            entity.Property(e => e.DeniedAdminComment).HasDefaultValue("");
            entity.Property(e => e.DeniedDate).HasDefaultValue(new DateTime(1900, 1, 1, 0, 0, 1, 0, DateTimeKind.Unspecified));
            entity.Property(e => e.DeniedPrintDate).HasDefaultValue(new DateTime(1900, 1, 1, 0, 0, 1, 0, DateTimeKind.Unspecified));
            entity.Property(e => e.DeniedSendDate).HasDefaultValue(new DateTime(1900, 1, 1, 0, 0, 1, 0, DateTimeKind.Unspecified));
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Ipaddress).HasDefaultValue("");
            entity.Property(e => e.Ipcountry).HasDefaultValue("--");
            entity.Property(e => e.Is3Dsecure).HasDefaultValue(false);
            entity.Property(e => e.IsChargeback).HasComputedColumnSql("(CONVERT([bit],case when [DeniedStatus]=(8) OR [DeniedStatus]=(6) OR [DeniedStatus]=(4) OR [DeniedStatus]=(2) OR [DeniedStatus]=(1) then (1) else (0) end,(0)))", true);
            entity.Property(e => e.IsRetrievalRequest).HasComputedColumnSql("(CONVERT([bit],case when [DeniedStatus]=(3) OR [DeniedStatus]=(5) then (1) else (0) end,(0)))", false);
            entity.Property(e => e.MerchantPd).HasDefaultValueSql("('')");
            entity.Property(e => e.EzzygateFeeChbCharge).HasDefaultValue(0m);
            entity.Property(e => e.EzzygateFeeClrfCharge).HasDefaultValue(0m);
            entity.Property(e => e.Oamount).HasDefaultValue(0m);
            entity.Property(e => e.Ocurrency).HasDefaultValue((byte)0);
            entity.Property(e => e.OrderNumber).HasDefaultValue("");
            entity.Property(e => e.PayId).HasDefaultValue(";0;");
            entity.Property(e => e.PayerIdUsed).HasDefaultValue("");
            entity.Property(e => e.PaymentMethodDisplay).HasDefaultValue("");
            entity.Property(e => e.PaymentMethodId).HasDefaultValue((byte)1);
            entity.Property(e => e.Payments).HasDefaultValue((byte)1);
            entity.Property(e => e.Pd).HasDefaultValueSql("('')");
            entity.Property(e => e.ReferringUrl).HasDefaultValue("");
            entity.Property(e => e.ReplyCode).HasDefaultValue("");
            entity.Property(e => e.TerminalNumber).HasDefaultValue("");
            entity.Property(e => e.TransactionTypeId).HasDefaultValue(7);

            entity.HasOne(d => d.CheckDetails).WithMany(p => p.TblCompanyTransPasses).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.Company).WithMany(p => p.TblCompanyTransPasses).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.CreditCard).WithMany(p => p.TblCompanyTransPasses).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.CurrencyNavigation).WithMany(p => p.TblCompanyTransPasses).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.DebitCompany).WithMany(p => p.TblCompanyTransPasses).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.RecurringSeriesNavigation).WithMany(p => p.TblCompanyTransPasses).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.TransPayerInfo).WithMany(p => p.TblCompanyTransPasses)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_tblCompanyTransPass_TransPayerInfo");

            entity.HasOne(d => d.TransPaymentMethod).WithMany(p => p.TblCompanyTransPasses).HasConstraintName("FK_tblCompanyTransPass_TransPaymentMethod");
        });

        modelBuilder.Entity<TblCompanyTransPending>(entity =>
        {
            entity.HasKey(e => e.CompanyTransPendingId).HasFillFactor(90);

            entity.ToTable("tblCompanyTransPending", tb =>
                {
                    tb.HasTrigger("trTransPending_SetOldTransSource_InsUpd");
                    tb.HasTrigger("trTransPending_SetPMDetailID_InsUpd");
                    tb.HasTrigger("trTransPending_TrackMerchantActivity_Ins");
                });

            entity.HasIndex(e => new { e.DebitReferenceCode, e.DebitCompanyId }, "IX_tblCompanyTransPending_DebitReferenceCode")
                .HasFilter("([DebitReferenceCode] IS NOT NULL)")
                .HasFillFactor(90);

            entity.HasIndex(e => new { e.DebitReferenceNum, e.DebitCompanyId }, "IX_tblCompanyTransPending_DebitReferenceNum")
                .HasFilter("([DebitReferenceNum] IS NOT NULL)")
                .HasFillFactor(90);

            entity.HasIndex(e => e.FraudDetectionLogId, "IX_tblCompanyTransPending_FraudDetectionLogID").HasFilter("([FraudDetectionLog_id]>(0))");

            entity.Property(e => e.Comment).HasDefaultValue("");
            entity.Property(e => e.CompanyId1).HasComputedColumnSql("([company_id])", false);
            entity.Property(e => e.Currency).HasComputedColumnSql("([trans_currency])", false);
            entity.Property(e => e.DebitApprovalNumber).HasDefaultValue("");
            entity.Property(e => e.DebitReferenceCode).HasDefaultValue("");
            entity.Property(e => e.Id).HasComputedColumnSql("([companyTransPending_id])", false);
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Ipaddress).HasDefaultValue("");
            entity.Property(e => e.Is3Dsecure).HasDefaultValue(false);
            entity.Property(e => e.OrderNumber).HasDefaultValue("");
            entity.Property(e => e.PayerIdUsed).HasDefaultValue("");
            entity.Property(e => e.PaymentMethodDisplay).HasDefaultValue("");
            entity.Property(e => e.ReplyCode).HasDefaultValue("");
            entity.Property(e => e.TerminalNumber).HasDefaultValue("");
            entity.Property(e => e.TransOrder).HasDefaultValue("");
            entity.Property(e => e.TransPayments).HasDefaultValue((byte)1);
            entity.Property(e => e.TransactionSourceId).HasDefaultValue(22);

            entity.HasOne(d => d.Company).WithMany(p => p.TblCompanyTransPendings).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.CreditCard).WithMany(p => p.TblCompanyTransPendings).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.DebitCompany).WithMany(p => p.TblCompanyTransPendings).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.TransCurrencyNavigation).WithMany(p => p.TblCompanyTransPendings).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.TransPayerInfo).WithMany(p => p.TblCompanyTransPendings)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_tblCompanyTransPending_TransPayerInfo");

            entity.HasOne(d => d.TransPaymentMethod).WithMany(p => p.TblCompanyTransPendings)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_tblCompanyTransPending_TransPaymentMethod");
        });

        modelBuilder.Entity<TblCompanyTransRemoved>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.Property(e => e.ApprovalNumber).HasDefaultValue("");
            entity.Property(e => e.Comment).HasDefaultValue("");
            entity.Property(e => e.Currency).HasDefaultValue((short)1);
            entity.Property(e => e.DateDel).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DebitCompanyId).HasDefaultValue((short)1);
            entity.Property(e => e.DeniedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Ipaddress).HasDefaultValue("");
            entity.Property(e => e.IpaddressDel).HasDefaultValue("");
            entity.Property(e => e.OrderNumber).HasDefaultValue("");
            entity.Property(e => e.PayDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PaymentMethodDisplay).HasDefaultValue("");
            entity.Property(e => e.PaymentMethodId).HasDefaultValue((byte)1);
            entity.Property(e => e.Payments).HasDefaultValue(1);
            entity.Property(e => e.ReferringUrl).HasDefaultValue("");
            entity.Property(e => e.ReplyCode).HasDefaultValue("");
            entity.Property(e => e.TerminalNumber).HasDefaultValue("");
            entity.Property(e => e.TransactionTypeId).HasDefaultValue(7);

            entity.HasOne(d => d.TransPayerInfo).WithMany(p => p.TblCompanyTransRemoveds)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_tblCompanyTransRemoved_TransPayerInfo");

            entity.HasOne(d => d.TransPaymentMethod).WithMany(p => p.TblCompanyTransRemoveds)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_tblCompanyTransRemoved_TransPaymentMethod");
        });

        modelBuilder.Entity<TblCreditCard>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.HasIndex(e => e.CcardFirst6, "IX_tblCreditCard_CCard_First6").HasFillFactor(90);

            entity.HasIndex(e => e.CcardLast4, "IX_tblCreditCard_CCard_Last4").HasFillFactor(90);

            entity.HasIndex(e => e.CcardNumber256, "IX_tblCreditCard_CCard_number").HasFillFactor(80);

            entity.HasIndex(e => e.CompanyId, "IX_tblCreditCard_CompanyID").HasFillFactor(85);

            entity.HasIndex(e => e.Member, "IX_tblCreditCard_Member").HasFillFactor(90);

            entity.HasIndex(e => e.Email, "IX_tblCreditCard_email").HasFillFactor(80);

            entity.Property(e => e.Bincountry).IsFixedLength();
            entity.Property(e => e.CcCui).HasDefaultValue("");
            entity.Property(e => e.CcInsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Comment).HasDefaultValue("");
            entity.Property(e => e.Email).HasDefaultValue("");
            entity.Property(e => e.ExpMm)
                .HasDefaultValue("")
                .IsFixedLength();
            entity.Property(e => e.ExpYy)
                .HasDefaultValue("")
                .IsFixedLength();
            entity.Property(e => e.Member).HasDefaultValue("''");
            entity.Property(e => e.PersonalNumber).HasDefaultValue("");
            entity.Property(e => e.PhoneNumber).HasDefaultValue("");

            entity.HasOne(d => d.BillingAddress).WithMany(p => p.TblCreditCards).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.BincountryNavigation).WithMany(p => p.TblCreditCards).HasConstraintName("FK_tblCreditCard_CountryList_CountryISOCode");
        });

        modelBuilder.Entity<TblCreditCardBin>(entity =>
        {
            entity.HasKey(e => e.Bin).HasFillFactor(90);

            entity.Property(e => e.BinId).ValueGeneratedOnAdd();
            entity.Property(e => e.BinLen).HasComputedColumnSql("(len([BIN]))", true);
            entity.Property(e => e.BinNumber).HasComputedColumnSql("(CONVERT([int],case when len([BIN])>(6) then left([BIN],(6)) else [BIN] end,(0)))", true);
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TblCreditCardRiskManagement>(entity =>
        {
            entity.HasKey(e => e.CcrmId).HasFillFactor(90);

            entity.Property(e => e.CcrmApplyVt).HasDefaultValue(true);
            entity.Property(e => e.CcrmBlockdays).HasComputedColumnSql("(CONVERT([int],[ccrm_blockhours]/(24),(0)))", false);
            entity.Property(e => e.CcrmCompanyId).HasDefaultValue(0);
            entity.Property(e => e.CcrmCreditType).HasDefaultValue((byte)1);
            entity.Property(e => e.CcrmCurrency).HasDefaultValue((short)-1);
            entity.Property(e => e.CcrmDays).HasComputedColumnSql("(CONVERT([int],[ccrm_hours]/(24),(0)))", false);
            entity.Property(e => e.CcrmInsDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CcrmReplyAmount).HasDefaultValue("586");
            entity.Property(e => e.CcrmReplyMaxTrans).HasDefaultValue("585");
            entity.Property(e => e.CcrmReplySource).HasDefaultValue(-1);
        });

        modelBuilder.Entity<TblCreditCardType>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.Property(e => e.Dbname).HasDefaultValue("");
            entity.Property(e => e.IconFileName).HasDefaultValue("");
            entity.Property(e => e.NameEng).HasDefaultValue("");
            entity.Property(e => e.NameHeb).HasDefaultValue("");
        });

        modelBuilder.Entity<TblCreditCardWhitelist>(entity =>
        {
            entity.Property(e => e.CcwlBinCountry).IsFixedLength();
            entity.Property(e => e.CcwlInsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CcwlIp).HasDefaultValue("");
            entity.Property(e => e.CcwlIsBurnt).HasComputedColumnSql("(CONVERT([bit],case when [ccwl_BurnDate] IS NULL then (0) else (1) end,(0)))", false);
            entity.Property(e => e.CcwlUsername).HasDefaultValueSql("(user_name())");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TblDebitBlock>(entity =>
        {
            entity.Property(e => e.DbDebitTerminalNumber).HasDefaultValue("");
            entity.Property(e => e.DbInsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DbUnblockDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TblDebitBlockLog>(entity =>
        {
            entity.Property(e => e.DblDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DblText).HasDefaultValue("");
            entity.Property(e => e.DblType).HasDefaultValue("");
        });

        modelBuilder.Entity<TblDebitCompany>(entity =>
        {
            entity.ToTable("tblDebitCompany", tb => tb.HasTrigger("DebitCompany_UpdateAccountID"));

            entity.Property(e => e.DcAutoRefundNotifyUsers).HasDefaultValue("");
            entity.Property(e => e.DcDescription).HasDefaultValue("");
            entity.Property(e => e.DcEmergencyContact).HasDefaultValue("");
            entity.Property(e => e.DcEmergencyPhone).HasDefaultValue("");
            entity.Property(e => e.DcIsActive).HasDefaultValue(true);
            entity.Property(e => e.DcIsAllowPartialRefund).HasDefaultValue(true);
            entity.Property(e => e.DcIsNotifyRetReqAfterRefund).HasDefaultValue(false);
            entity.Property(e => e.DcIsReturnCode).HasDefaultValue(true);
            entity.Property(e => e.DcMonthlyLimitChbnotifyUsers).HasDefaultValue("");
            entity.Property(e => e.DcMonthlyLimitChbnotifyUsersSms).HasDefaultValue("");
            entity.Property(e => e.DcMonthlyMclimitChbnotifyUsers).HasDefaultValue("");
            entity.Property(e => e.DcMonthlyMclimitChbnotifyUsersSms).HasDefaultValue("");
            entity.Property(e => e.DcName).HasDefaultValue("");
            entity.Property(e => e.DcUnblockAttemptString).HasDefaultValue("");

            entity.HasMany(d => d.TransAmountTypes).WithMany(p => p.DebitCompanies)
                .UsingEntity<Dictionary<string, object>>(
                    "DebitCompanyFeeExcludeInvoice",
                    r => r.HasOne<TransAmountType>().WithMany().HasForeignKey("TransAmountTypeId"),
                    l => l.HasOne<TblDebitCompany>().WithMany().HasForeignKey("DebitCompanyId"),
                    j =>
                    {
                        j.HasKey("DebitCompanyId", "TransAmountTypeId");
                        j.ToTable("DebitCompanyFeeExcludeInvoice");
                        j.IndexerProperty<int>("DebitCompanyId").HasColumnName("DebitCompany_id");
                        j.IndexerProperty<int>("TransAmountTypeId").HasColumnName("TransAmountType_id");
                    });
        });

        modelBuilder.Entity<TblDebitCompanyCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.Property(e => e.ChargeFail).HasDefaultValue(true);
            entity.Property(e => e.Code).HasDefaultValue("");
            entity.Property(e => e.DescriptionCustomerEng).HasDefaultValue("");
            entity.Property(e => e.DescriptionCustomerHeb).HasDefaultValue("");
            entity.Property(e => e.DescriptionMerchantEng).HasDefaultValue("");
            entity.Property(e => e.DescriptionMerchantHeb).HasDefaultValue("");
            entity.Property(e => e.DescriptionOriginal).HasDefaultValue("");
            entity.Property(e => e.LocalError).HasDefaultValue("");
        });

        modelBuilder.Entity<TblDebitCompanyFee>(entity =>
        {
            entity.Property(e => e.DcfChbcurrency).HasDefaultValue((byte)0);
            entity.Property(e => e.DcfFixedCurrency).HasDefaultValue((byte)0);
            entity.Property(e => e.DcfMinPrecFee).HasDefaultValue(0m);
            entity.Property(e => e.DcfPayIndays).HasDefaultValue("");
            entity.Property(e => e.DcfPayTransDays).HasDefaultValue("");
            entity.Property(e => e.DcfTerminalNumber).HasDefaultValue("");
        });

        modelBuilder.Entity<TblDebitCompanyLoginDatum>(entity =>
        {
            entity.HasKey(e => e.DebitCompanyLoginDataId).HasFillFactor(90);

            entity.Property(e => e.Password).HasDefaultValue("");
            entity.Property(e => e.Username).HasDefaultValue("");
        });

        modelBuilder.Entity<TblDebitCompanyPaymentTokenization>(entity =>
        {
            entity.HasKey(e => e.DebitCompanyPaymentTokenizationId)
                .HasName("PK_DebitCompanyPaymentTokenization")
                .HasFillFactor(90);

            entity.HasOne(d => d.DebitCompany).WithMany(p => p.TblDebitCompanyPaymentTokenizations).HasConstraintName("FK_DebitCompanyPaymentTokenization_DebitCompanyID");
        });

        modelBuilder.Entity<TblDebitRule>(entity =>
        {
            entity.ToTable("tblDebitRule", tb => tb.HasTrigger("trgDebitRuleAddSetRating"));

            entity.Property(e => e.DrLastFailDate).HasDefaultValueSql("((0))");
            entity.Property(e => e.DrLastUnblockDate).HasDefaultValueSql("((0))");
            entity.Property(e => e.DrNotifyUsers).HasDefaultValue("");
            entity.Property(e => e.DrNotifyUsersSms).HasDefaultValue("");
            entity.Property(e => e.DrReplyCodes).HasDefaultValue("");
        });

        modelBuilder.Entity<TblDebitTerminal>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.HasIndex(e => e.TerminalNumber, "IX_tblTerminalsInfo_terminalNumber").HasFillFactor(90);

            entity.Property(e => e.AccountId).HasDefaultValue("");
            entity.Property(e => e.AccountId3D).HasDefaultValue("");
            entity.Property(e => e.AccountSubId).HasDefaultValue("");
            entity.Property(e => e.AccountSubId3D).HasDefaultValue("");
            entity.Property(e => e.DebitCompany).HasDefaultValue((byte)1);
            entity.Property(e => e.DtCommentsAmericanexp).HasDefaultValue("");
            entity.Property(e => e.DtCommentsDiners).HasDefaultValue("");
            entity.Property(e => e.DtCommentsDirect).HasDefaultValue("");
            entity.Property(e => e.DtCommentsIsracard).HasDefaultValue("");
            entity.Property(e => e.DtCommentsMastercard).HasDefaultValue("");
            entity.Property(e => e.DtCommentsVisa).HasDefaultValue("");
            entity.Property(e => e.DtCompanyNumAmericanexp).HasDefaultValue("");
            entity.Property(e => e.DtCompanyNumDiners).HasDefaultValue("");
            entity.Property(e => e.DtCompanyNumDirect).HasDefaultValue("");
            entity.Property(e => e.DtCompanyNumIsracard).HasDefaultValue("");
            entity.Property(e => e.DtCompanyNumMastercard).HasDefaultValue("");
            entity.Property(e => e.DtCompanyNumVisa).HasDefaultValue("");
            entity.Property(e => e.DtContractNumber).HasDefaultValue("");
            entity.Property(e => e.DtDescriptor).HasDefaultValue("");
            entity.Property(e => e.DtMcc).HasDefaultValue("");
            entity.Property(e => e.DtMonthlyChbsendDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DtMonthlyLimitChbnotifyUsers).HasDefaultValue("");
            entity.Property(e => e.DtMonthlyLimitChbnotifyUsersSms).HasDefaultValue("");
            entity.Property(e => e.DtMonthlyMcchbsendDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DtMonthlyMclimitChbnotifyUsers).HasDefaultValue("");
            entity.Property(e => e.DtMonthlyMclimitChbnotifyUsersSms).HasDefaultValue("");
            entity.Property(e => e.DtName).HasDefaultValue("");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ProcessingMethod).HasDefaultValue((byte)1);
            entity.Property(e => e.TerminalNotes).HasDefaultValue("");
            entity.Property(e => e.TerminalNumber).HasDefaultValue("");
            entity.Property(e => e.TerminalNumber3D).HasDefaultValue("");

            entity.HasOne(d => d.SiccodeNumberNavigation).WithMany(p => p.TblDebitTerminals)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_DebitTerminals_SICCodeNumber");
        });

        modelBuilder.Entity<TblEpaFileLog>(entity =>
        {
            entity.Property(e => e.ActionDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.ActionTypeNavigation).WithMany(p => p.TblEpaFileLogs).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<TblEpaPending>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Installment).HasDefaultValue(1);
        });

        modelBuilder.Entity<TblExternalCardCustomer>(entity =>
        {
            entity.HasOne(d => d.ExternalCardTerminal).WithMany(p => p.TblExternalCardCustomers).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Merchant).WithMany(p => p.TblExternalCardCustomers).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<TblExternalCardCustomerPayment>(entity =>
        {
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.ExternalCardCustomer).WithMany(p => p.TblExternalCardCustomerPayments).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<TblExternalCardTerminal>(entity =>
        {
            entity.HasOne(d => d.ExternalCardProvider).WithMany(p => p.TblExternalCardTerminals).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<TblExternalCardTerminalToMerchant>(entity =>
        {
            entity.HasOne(d => d.ExternalCardTerminal).WithMany(p => p.TblExternalCardTerminalToMerchants).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Merchant).WithMany(p => p.TblExternalCardTerminalToMerchants).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<TblFraudCcBlackList>(entity =>
        {
            entity.HasKey(e => e.FraudCcBlackListId).HasFillFactor(90);

            entity.HasIndex(e => new { e.FcblCcNumber256, e.FcblCcrmid, e.FcblInsertDate, e.FraudCcBlackListId }, "IX_tblFraudCCBlackList_fcbl_ccNumber256_CCRMID_InsertDate_id")
                .IsUnique()
                .IsDescending(false, true, true, true)
                .HasFillFactor(85);

            entity.Property(e => e.FcblCcDisplay).HasDefaultValue("");
            entity.Property(e => e.FcblComment).HasDefaultValue("");
            entity.Property(e => e.FcblInsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FcblReplyCode).HasDefaultValue("");
            entity.Property(e => e.FcblUnblockDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.FcblBlockLevelNavigation).WithMany(p => p.TblFraudCcBlackLists).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<TblGlobalDatum>(entity =>
        {
            entity.HasKey(e => new { e.GdId, e.GdGroup, e.GdLng }).HasFillFactor(90);
        });

        modelBuilder.Entity<TblImportChargebackBn>(entity =>
        {
            entity.HasIndex(e => e.CaseId, "IX_tblImportChargebackBNS_CASEID")
                .IsUnique()
                .IsDescending()
                .HasFillFactor(90);

            entity.Property(e => e.AmountEuro).HasDefaultValue("");
            entity.Property(e => e.AmountTxn).HasDefaultValue("");
            entity.Property(e => e.CardholderNumber).HasDefaultValue("");
            entity.Property(e => e.CaseId).HasDefaultValue("");
            entity.Property(e => e.Comments).HasDefaultValue("");
            entity.Property(e => e.Cur).HasDefaultValue("");
            entity.Property(e => e.Deadline).HasDefaultValue("");
            entity.Property(e => e.IcbChargebackDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IcbFileName).HasDefaultValue("");
            entity.Property(e => e.IcbUser).HasDefaultValueSql("(user_name())");
            entity.Property(e => e.MerNo).HasDefaultValue("");
            entity.Property(e => e.Mrf).HasDefaultValue("");
            entity.Property(e => e.RefNo).HasDefaultValue("");
            entity.Property(e => e.Rsn).HasDefaultValue("");
            entity.Property(e => e.TicketNo).HasDefaultValue("");
            entity.Property(e => e.TxnDate).HasDefaultValue("");
        });

        modelBuilder.Entity<TblImportChargebackJcc>(entity =>
        {
            entity.Property(e => e.Acronym).HasDefaultValue("");
            entity.Property(e => e.Amount).HasDefaultValue("");
            entity.Property(e => e.AuthorizationCode).HasDefaultValue("");
            entity.Property(e => e.CardNumber).HasDefaultValue("");
            entity.Property(e => e.ChargebackDate).HasDefaultValue("");
            entity.Property(e => e.ChargingStatus).HasDefaultValue("");
            entity.Property(e => e.CurrencyCode).HasDefaultValue("");
            entity.Property(e => e.IcbChargebackDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IcbFileName).HasDefaultValue("");
            entity.Property(e => e.IcbUser).HasDefaultValueSql("(user_name())");
            entity.Property(e => e.MerchantNumber).HasDefaultValue("");
            entity.Property(e => e.MicrofilmRefNumber).HasDefaultValue("");
            entity.Property(e => e.NetworkCode).HasDefaultValue("");
            entity.Property(e => e.OutletNumber).HasDefaultValue("");
            entity.Property(e => e.PosBrandCode).HasDefaultValue("");
            entity.Property(e => e.ProcessingDate).HasDefaultValue("");
            entity.Property(e => e.ReasonCode).HasDefaultValue("");
            entity.Property(e => e.SourceAmount).HasDefaultValue("");
            entity.Property(e => e.SourceCurrencyCode).HasDefaultValue("");
            entity.Property(e => e.TransactionDate).HasDefaultValue("");
            entity.Property(e => e.TrnId).HasDefaultValue("");
        });

        modelBuilder.Entity<TblInvikRefundBatch>(entity =>
        {
            entity.Property(e => e.IrbDownloadDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IrbInsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IrbUser).HasDefaultValueSql("(user_name())");
        });

        modelBuilder.Entity<TblInvoiceDocument>(entity =>
        {
            entity.ToTable("tblInvoiceDocument", tb =>
                {
                    tb.HasTrigger("trgInvoiceDocumentAddGetLines");
                    tb.HasTrigger("trgInvoiceDocumentDeleteResetTransactionPay");
                });

            entity.Property(e => e.IdBillToName).HasDefaultValue("");
            entity.Property(e => e.IdBillingCompanyAddress).HasDefaultValue("");
            entity.Property(e => e.IdBillingCompanyEmail).HasDefaultValue("");
            entity.Property(e => e.IdBillingCompanyLanguage)
                .HasDefaultValue("heb")
                .IsFixedLength();
            entity.Property(e => e.IdBillingCompanyName).HasDefaultValue("");
            entity.Property(e => e.IdBillingCompanyNumber).HasDefaultValue("");
            entity.Property(e => e.IdCurrencyRate).HasDefaultValue(1m);
            entity.Property(e => e.IdInsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IdIsPrinted).HasComputedColumnSql("(CONVERT([bit],case when [id_InsertDate]=[id_PrintDate] then (0) else (1) end,(0)))", false);
            entity.Property(e => e.IdPrintDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IdUsername).HasDefaultValue("");

            entity.HasOne(d => d.IdBillingCompany).WithMany(p => p.TblInvoiceDocuments).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IdCurrencyNavigation).WithMany(p => p.TblInvoiceDocuments).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<TblInvoiceLine>(entity =>
        {
            entity.Property(e => e.IlAmount).HasComputedColumnSql("(CONVERT([money],[il_Price]*[il_Quantity],(0)))", false);
            entity.Property(e => e.IlBillToName).HasDefaultValue("");
            entity.Property(e => e.IlCurrencyRate).HasDefaultValue(1m);
            entity.Property(e => e.IlDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IlQuantity).HasDefaultValue(1f);
            entity.Property(e => e.IlText).HasDefaultValue("");
            entity.Property(e => e.IlUsername).HasDefaultValue("");

            entity.HasOne(d => d.IlCurrencyNavigation).WithMany(p => p.TblInvoiceLines).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.IlDocument).WithMany(p => p.TblInvoiceLines).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TblLogChargeAttempt>(entity =>
        {
            entity.HasIndex(e => e.LcaDateStart, "IX_LcaDateStart")
                .IsDescending()
                .HasFillFactor(90);

            entity.HasIndex(e => e.LcaMerchantNumber, "IX_MerchantNumber").HasFillFactor(90);

            entity.HasIndex(e => new { e.LcaTransNum, e.LcaReplyCode }, "IX_tblLogChargeAttempts_TransNum_ReplyCode")
                .IsDescending(true, false)
                .HasFilter("([Lca_TransNum]>(0))")
                .HasFillFactor(90);

            entity.Property(e => e.LcaDateStart).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TransactionTypeId).HasDefaultValue((short)0);
        });

        modelBuilder.Entity<TblLogCreditCardWhitelist>(entity =>
        {
            entity.Property(e => e.LccwInsertDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TblLogDebitRefund>(entity =>
        {
            entity.ToTable("tblLogDebitRefund", tb => tb.HasTrigger("trgLogDebitRefund"));

            entity.Property(e => e.LdrAnswer).HasDefaultValue("");
            entity.Property(e => e.LdrInsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LdrReplyCode).HasDefaultValue("");
        });

        modelBuilder.Entity<TblLogImportEpa>(entity =>
        {
            entity.HasIndex(e => new { e.TransId, e.Installment }, "IX_tblLogImportEPA_TransID_Installment")
                .IsUnique()
                .HasFillFactor(90);

            entity.Property(e => e.Installment).HasDefaultValue((byte)1);
            entity.Property(e => e.PaidInsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.RefundedInsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TransInstallment).HasComputedColumnSql("((str([TransID])+'_')+str([Installment]))", true);
        });

        modelBuilder.Entity<TblLogMasavDetail>(entity =>
        {
            entity.HasKey(e => e.LogMasavDetailsId).HasFillFactor(90);

            entity.Property(e => e.PayeeBankDetails).HasDefaultValue("");
            entity.Property(e => e.StatusNote).HasDefaultValue("");
            entity.Property(e => e.StatusUserName).HasDefaultValue("");
        });

        modelBuilder.Entity<TblLogMasavFile>(entity =>
        {
            entity.HasKey(e => e.LogMasavFileId).HasFillFactor(90);

            entity.Property(e => e.AttachedFileExt).HasDefaultValue("");
            entity.Property(e => e.InsertDate).HasDefaultValueSql("('')");
            entity.Property(e => e.LogonUser).HasDefaultValue("");
            entity.Property(e => e.PayedBankCode).HasDefaultValue("");
            entity.Property(e => e.PayedBankDesc).HasDefaultValue("");
        });

        modelBuilder.Entity<TblLogNoConnection>(entity =>
        {
            entity.HasKey(e => e.LogNoConnectionId).HasFillFactor(90);

            entity.ToTable("tblLog_NoConnection", tb => tb.HasTrigger("trgLogNoConnectionAddSetAutoRefundStatus"));

            entity.HasIndex(e => e.LncCompanyId, "IX_tblLog_NoConnection_CompanyID").HasFillFactor(90);

            entity.Property(e => e.LncAutoRefundDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LncAutoRefundReply).HasDefaultValue("");
            entity.Property(e => e.LncCompanyName).HasDefaultValue("");
            entity.Property(e => e.LncCurrency).HasDefaultValue((byte)1);
            entity.Property(e => e.LncDebitReferenceCode).HasDefaultValue("");
            entity.Property(e => e.LncDebitReturnCode)
                .HasDefaultValue("")
                .IsFixedLength();
            entity.Property(e => e.LncHttpError).HasDefaultValue("");
            entity.Property(e => e.LncInsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LncIpAddress).HasDefaultValue("");
            entity.Property(e => e.LncTerminalNumber).HasDefaultValue("");
        });

        modelBuilder.Entity<TblLogPaymentPage>(entity =>
        {
            entity.Property(e => e.LppDateStart).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TblLogPaymentPageTran>(entity =>
        {
            entity.Property(e => e.LpptDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LpptDateTime).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.TransFail).WithMany(p => p.TblLogPaymentPageTrans)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_LogPaymentPageTrans_tblCompanyTransFail_TransFail_id");

            entity.HasOne(d => d.TransPass).WithMany(p => p.TblLogPaymentPageTrans)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_LogPaymentPageTrans_tblCompanyTransPass_TransPass_id");

            entity.HasOne(d => d.TransPending).WithMany(p => p.TblLogPaymentPageTrans)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_LogPaymentPageTrans_tblCompanyTransPending_TransPending_id");
        });

        modelBuilder.Entity<TblLogPendingFinalize>(entity =>
        {
            entity.HasIndex(e => e.TransApprovalId, "IX_tblLogPendingFinalize_TransApprovalID")
                .IsDescending()
                .HasFilter("([TransApprovalID] IS NOT NULL)");

            entity.HasIndex(e => e.TransFailId, "IX_tblLogPendingFinalize_TransFailID")
                .IsDescending()
                .HasFilter("([TransFailID] IS NOT NULL)");

            entity.HasIndex(e => e.TransPassId, "IX_tblLogPendingFinalize_TransPassID")
                .IsDescending()
                .HasFilter("([TransPassID] IS NOT NULL)");

            entity.Property(e => e.PendingId).ValueGeneratedNever();
            entity.Property(e => e.FinalizeDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.TransApproval).WithMany(p => p.TblLogPendingFinalizes)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_tblLogPendingFinalize_TransApprovalID");

            entity.HasOne(d => d.TransFail).WithMany(p => p.TblLogPendingFinalizes).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.TransPass).WithMany(p => p.TblLogPendingFinalizes).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TblLogTerminalJump>(entity =>
        {
            entity.Property(e => e.LtjInsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LtjTerminalNumber).HasDefaultValue("");
        });

        modelBuilder.Entity<TblMerchantBankAccount>(entity =>
        {
            entity.Property(e => e.MbaAba).HasDefaultValue("");
            entity.Property(e => e.MbaAba2).HasDefaultValue("");
            entity.Property(e => e.MbaAccountName).HasDefaultValue("");
            entity.Property(e => e.MbaAccountName2).HasDefaultValue("");
            entity.Property(e => e.MbaAccountNumber).HasDefaultValue("");
            entity.Property(e => e.MbaAccountNumber2).HasDefaultValue("");
            entity.Property(e => e.MbaBankAddress).HasDefaultValue("");
            entity.Property(e => e.MbaBankAddress2).HasDefaultValue("");
            entity.Property(e => e.MbaBankAddressCity).HasDefaultValue("");
            entity.Property(e => e.MbaBankAddressCity2).HasDefaultValue("");
            entity.Property(e => e.MbaBankAddressSecond).HasDefaultValue("");
            entity.Property(e => e.MbaBankAddressSecond2).HasDefaultValue("");
            entity.Property(e => e.MbaBankAddressState).HasDefaultValue("");
            entity.Property(e => e.MbaBankAddressState2).HasDefaultValue("");
            entity.Property(e => e.MbaBankAddressZip).HasDefaultValue("");
            entity.Property(e => e.MbaBankAddressZip2).HasDefaultValue("");
            entity.Property(e => e.MbaBankName).HasDefaultValue("");
            entity.Property(e => e.MbaBankName2).HasDefaultValue("");
            entity.Property(e => e.MbaIban).HasDefaultValue("");
            entity.Property(e => e.MbaIban2).HasDefaultValue("");
            entity.Property(e => e.MbaSepaBic).HasDefaultValue("");
            entity.Property(e => e.MbaSepaBic2).HasDefaultValue("");
            entity.Property(e => e.MbaSortCode).HasDefaultValue("");
            entity.Property(e => e.MbaSortCode2).HasDefaultValue("");
            entity.Property(e => e.MbaSwiftNumber).HasDefaultValue("");
            entity.Property(e => e.MbaSwiftNumber2).HasDefaultValue("");
        });

        modelBuilder.Entity<TblMerchantGroup>(entity =>
        {
            entity.ToTable("tblMerchantGroup", tb => tb.HasTrigger("trgMerchantGroupRemoveResetMerchants"));
        });

        modelBuilder.Entity<TblMerchantProcessingDatum>(entity =>
        {
            entity.Property(e => e.MerchantId).ValueGeneratedNever();
            entity.Property(e => e.MpdCffResetDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.MpdCffResetDate2).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TblMerchantRecurringSetting>(entity =>
        {
            entity.Property(e => e.MerchantId).ValueGeneratedNever();
            entity.Property(e => e.ForceMd5onModify).HasDefaultValue(true);
            entity.Property(e => e.MaxCharges).HasDefaultValue(200);
            entity.Property(e => e.MaxStages).HasDefaultValue(6);
            entity.Property(e => e.MaxYears).HasDefaultValue(3);
        });

        modelBuilder.Entity<TblParentCompany>(entity =>
        {
            entity.Property(e => e.PcCode).HasDefaultValue("");
            entity.Property(e => e.PcRecurringEcheckUrl).HasDefaultValue("");
            entity.Property(e => e.PcRecurringUrl).HasDefaultValue("");
            entity.Property(e => e.PcSmsUrl).HasDefaultValue("");
        });

        modelBuilder.Entity<TblPasswordHistory>(entity =>
        {
            entity.HasKey(e => new { e.LphId, e.LphRefId, e.LphRefType }).HasFillFactor(90);

            entity.Property(e => e.LphInsert).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LphIp).HasDefaultValue("");
            entity.Property(e => e.LphLastFail).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LphLastSuccess).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TblPeriodicFee>(entity =>
        {
            entity.HasKey(e => new { e.MerchantId, e.TypeId }).HasFillFactor(90);
        });

        modelBuilder.Entity<TblPeriodicFeeType>(entity =>
        {
            entity.ToTable("tblPeriodicFeeType", tb => tb.HasTrigger("trgPeriodicFeeTypeAddPeriodicFee"));

            entity.HasOne(d => d.Currency).WithMany(p => p.TblPeriodicFeeTypes).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<TblRecurringAttempt>(entity =>
        {
            entity.ToTable("tblRecurringAttempt", tb =>
                {
                    tb.HasTrigger("trgRecurringAttemptAddSetTransPassRecurringFields");
                    tb.HasTrigger("trgRecurringAttemptUpdateCharge");
                    tb.HasTrigger("trgRecurringAttemptUpdateChargeAttemptCount");
                });

            entity.HasIndex(e => e.RaCharge, "IX_tblRecurringAttempt_Charge")
                .IsDescending()
                .HasFillFactor(90);

            entity.Property(e => e.RaComments).HasDefaultValue("");
            entity.Property(e => e.RaDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.RaReplyCode).HasDefaultValue("");

            entity.HasOne(d => d.RaChargeNavigation).WithMany(p => p.TblRecurringAttempts).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.RaCreditCardNavigation).WithMany(p => p.TblRecurringAttempts).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.RaEcheckNavigation).WithMany(p => p.TblRecurringAttempts).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.RaTransPassNavigation).WithMany(p => p.TblRecurringAttempts).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.TransPaymentMethod).WithMany(p => p.TblRecurringAttempts).HasConstraintName("FK_RecurringAttempt_TransPaymentMethod");
        });

        modelBuilder.Entity<TblRecurringCharge>(entity =>
        {
            entity.ToTable("tblRecurringCharge", tb => tb.HasTrigger("trgRecurringChargeUpdateSeries"));

            entity.Property(e => e.RcComments).HasDefaultValue("");
            entity.Property(e => e.RcCreditCard).HasDefaultValue(0);
            entity.Property(e => e.RcDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.RcEcheck).HasDefaultValue(0);

            entity.HasOne(d => d.RcCreditCardNavigation).WithMany(p => p.TblRecurringCharges).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.RcCurrencyNavigation).WithMany(p => p.TblRecurringCharges).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.RcEcheckNavigation).WithMany(p => p.TblRecurringCharges).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.TransPaymentMethod).WithMany(p => p.TblRecurringCharges).HasConstraintName("FK_RecurringCharge_TransPaymentMethod");
        });

        modelBuilder.Entity<TblRecurringSeries>(entity =>
        {
            entity.Property(e => e.RsComments).HasDefaultValue("");
            entity.Property(e => e.RsCompany).HasDefaultValue(0);
            entity.Property(e => e.RsCreditCard).HasDefaultValue(0);
            entity.Property(e => e.RsEcheck).HasDefaultValue(0);
            entity.Property(e => e.RsIp).HasDefaultValue("");
            entity.Property(e => e.RsStartDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.RsCreditCardNavigation).WithMany(p => p.TblRecurringSeries).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.RsEcheckNavigation).WithMany(p => p.TblRecurringSeries).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.TransPaymentMethod).WithMany(p => p.TblRecurringSeries).HasConstraintName("FK_RecurringSeries_TransPaymentMethod");
        });

        modelBuilder.Entity<TblRefundAsk>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.HasIndex(e => e.CompanyId, "IX_tblRefundAsk_CompanyID").HasFillFactor(90);

            entity.HasIndex(e => e.TransId, "IX_tblRefundAsk_transID").HasFillFactor(90);

            entity.Property(e => e.RefundAskComment).HasDefaultValue("");
            entity.Property(e => e.RefundAskConfirmationNum).HasDefaultValue("");
            entity.Property(e => e.RefundAskDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.RefundAskStatusHistory).HasDefaultValue("");

            entity.HasOne(d => d.RefundAskCurrencyNavigation).WithMany(p => p.TblRefundAsks).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Trans).WithMany(p => p.TblRefundAsks).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<TblRefundAskLog>(entity =>
        {
            entity.Property(e => e.RalDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.RalDescription).HasDefaultValue("");
            entity.Property(e => e.RalUser).HasDefaultValue("");
        });

        modelBuilder.Entity<TblRetrivalRequest>(entity =>
        {
            entity.Property(e => e.RrInsertDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.TransPass).WithMany(p => p.TblRetrivalRequests).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TblSecurityDocument>(entity =>
        {
            entity.ToTable("tblSecurityDocument", tb =>
                {
                    tb.HasTrigger("trgSecurityDocumentAdd");
                    tb.HasTrigger("trgSecurityDocumentRemove");
                });

            entity.Property(e => e.SdTitle).HasDefaultValue("Untitled");
            entity.Property(e => e.SdUrl).HasDefaultValue("");
        });

        modelBuilder.Entity<TblSecurityDocumentGroup>(entity =>
        {
            entity.Property(e => e.SdgGroupId).HasComputedColumnSql("(case when [sdg_Group]>(0) then [sdg_Group]  end)", true);
            entity.Property(e => e.SdgUserId).HasComputedColumnSql("(case when [sdg_Group]<(0) then  -[sdg_Group]  end)", true);

            entity.HasOne(d => d.SdgGroupNavigation).WithMany(p => p.TblSecurityDocumentGroups).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.SdgUser).WithMany(p => p.TblSecurityDocumentGroups).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TblSecurityGroup>(entity =>
        {
            entity.ToTable("tblSecurityGroup", tb =>
                {
                    tb.HasTrigger("trgSecurityGroupAdd");
                    tb.HasTrigger("trgSecurityGroupRemove");
                });

            entity.Property(e => e.SgDescription).HasDefaultValue("");
            entity.Property(e => e.SgIsActive).HasDefaultValue(true);
            entity.Property(e => e.SgName).HasDefaultValue("");
        });

        modelBuilder.Entity<TblSecurityLog>(entity =>
        {
            entity.Property(e => e.SlDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.SlDocumentNavigation).WithMany(p => p.TblSecurityLogs).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.SlUserNavigation).WithMany(p => p.TblSecurityLogs).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TblSecurityUser>(entity =>
        {
            entity.ToTable("tblSecurityUser", tb =>
                {
                    tb.HasTrigger("trgSecurityUserAdd");
                    tb.HasTrigger("trgSecurityUserDelete");
                    tb.HasTrigger("trgSecurityUserRemove");
                });

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.SuIsActive).HasDefaultValue(true);
            entity.Property(e => e.SuMail).HasDefaultValue("");
            entity.Property(e => e.SuMailEmergency).HasDefaultValue("");
            entity.Property(e => e.SuName).HasDefaultValue("");
            entity.Property(e => e.SuSms).HasDefaultValue("");
            entity.Property(e => e.SuUsername).HasDefaultValue("");
        });

        modelBuilder.Entity<TblSystemBankList>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.Property(e => e.Address).HasDefaultValue("");
            entity.Property(e => e.BankName).HasDefaultValue("");
            entity.Property(e => e.Fax).HasDefaultValue("");
            entity.Property(e => e.Phone).HasDefaultValue("");
            entity.Property(e => e.Zip).HasDefaultValue("");
        });

        modelBuilder.Entity<TblSystemCurrency>(entity =>
        {
            entity.ToTable("tblSystemCurrencies", tb => tb.HasTrigger("SystemCurrencies_NewCurrencyList_Upd"));

            entity.Property(e => e.CurBaseRate).HasDefaultValue(1m);
            entity.Property(e => e.CurFullName).HasDefaultValue("");
            entity.Property(e => e.CurInsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CurIsoname)
                .HasDefaultValue("   ")
                .IsFixedLength();
            entity.Property(e => e.CurSymbol).HasDefaultValue("");
        });

        modelBuilder.Entity<TblTransactionAmount>(entity =>
        {
            entity.HasIndex(e => e.TransApprovalId, "IX_tblTransactionAmount_TransApprovalID")
                .IsDescending()
                .HasFilter("([TransApprovalID] IS NOT NULL)");

            entity.HasIndex(e => e.TransFailId, "IX_tblTransactionAmount_TransFailID")
                .IsDescending()
                .HasFilter("([TransFailID] IS NOT NULL)");

            entity.HasIndex(e => e.TransPassId, "IX_tblTransactionAmount_TransPassID")
                .IsDescending()
                .HasFilter("([TransPassID] IS NOT NULL)");

            entity.HasIndex(e => e.TransPendingId, "IX_tblTransactionAmount_TransPendingID")
                .IsDescending()
                .HasFilter("([TransPendingID] IS NOT NULL)");

            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.SettlementAmount).WithMany(p => p.TblTransactionAmounts).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TblTransactionPay>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.HasIndex(e => e.CompanyId, "IX_tblTransactionPay_CompanyID").HasFillFactor(90);

            entity.HasIndex(e => e.PayDate, "IX_tblTransactionPay_PayDate").HasFillFactor(90);

            entity.Property(e => e.BillingCompanyAddress).HasDefaultValue("");
            entity.Property(e => e.BillingCompanyEmail).HasDefaultValue("");
            entity.Property(e => e.BillingCompanyName).HasDefaultValue("");
            entity.Property(e => e.BillingCompanyNumber).HasDefaultValue("");
            entity.Property(e => e.BillingCompanysId).HasDefaultValue(2);
            entity.Property(e => e.BillingCreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.BillingLanguageShow).HasDefaultValue("");
            entity.Property(e => e.BillingToInfo).HasDefaultValue("");
            entity.Property(e => e.Comment).HasDefaultValue("");
            entity.Property(e => e.IsChargeVat).HasDefaultValue(true);
            entity.Property(e => e.PayDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PayPercent).HasDefaultValue(100m);
            entity.Property(e => e.PrimePercent).HasDefaultValue(10.0);
            entity.Property(e => e.TerminalType).HasDefaultValue((short)1);
            entity.Property(e => e.TotalAmountFee).HasComputedColumnSql("((((([TotalFeeProcessCapture]+[TotalFeeClarification])+[TotalFeeFinancing])+[TotalFeeHandling])+[TotalFeeChb])+[TotalFeeBank])", true);
            entity.Property(e => e.TotalAmountTrans).HasComputedColumnSql("(([TotalCaptureAmount]-[TotalRefundAmount])-[TotalChbAmount])", true);
            entity.Property(e => e.TotalPayout).HasComputedColumnSql("(round(CONVERT([money],((((((([TotalCaptureAmount]-[TotalRefundAmount])-[TotalChbAmount])+[TotalSystemAmount])+[TotalAdminAmount])-((((([TotalFeeProcessCapture]+[TotalFeeClarification])+[TotalFeeFinancing])+[TotalFeeHandling])+[TotalFeeChb])+[TotalFeeBank])*((1.0)+case [IsChargeVAT] when (1) then [VatAmount] else (0) end))-[TotalDirectDeposit])+[TotalRollingRelease])-[TotalRollingReserve],(0)),(2)))", true);
        });

        modelBuilder.Entity<TblTransactionPayFee>(entity =>
        {
            entity.Property(e => e.CcfExchangeTo).HasDefaultValue((short)-1);
        });

        modelBuilder.Entity<TblWalletIdentity>(entity =>
        {
            entity.Property(e => e.WiBrandName).HasDefaultValue("");
            entity.Property(e => e.WiCompanyName).HasDefaultValue("");
            entity.Property(e => e.WiContentFolder).HasDefaultValue("");
            entity.Property(e => e.WiContentUrl).HasDefaultValue("");
            entity.Property(e => e.WiCopyRight).HasDefaultValue("");
            entity.Property(e => e.WiCurrencies).HasDefaultValue("");
            entity.Property(e => e.WiCustomerUrl).HasDefaultValue("");
            entity.Property(e => e.WiDevCenterUrl).HasDefaultValue("");
            entity.Property(e => e.WiFolder).HasDefaultValue("");
            entity.Property(e => e.WiFormsInbox).HasDefaultValue("");
            entity.Property(e => e.WiIdentity).HasDefaultValue("");
            entity.Property(e => e.WiMailFrom).HasDefaultValue("");
            entity.Property(e => e.WiMailPassword).HasDefaultValue("");
            entity.Property(e => e.WiMailServer).HasDefaultValue("");
            entity.Property(e => e.WiMailTo).HasDefaultValue("");
            entity.Property(e => e.WiMailUsername).HasDefaultValue("");
            entity.Property(e => e.WiMerchantUploadsFolder).HasDefaultValue("");
            entity.Property(e => e.WiMerchantUrl).HasDefaultValue("");
            entity.Property(e => e.WiName).HasDefaultValue("");
            entity.Property(e => e.WiProcessUrl).HasDefaultValue("");
        });

        modelBuilder.Entity<TblWhiteListBin>(entity =>
        {
            entity.Property(e => e.Bin).HasDefaultValue("");
            entity.Property(e => e.LastTransPassDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<TblWireMoney>(entity =>
        {
            entity.HasKey(e => e.WireMoneyId).HasFillFactor(90);

            entity.ToTable("tblWireMoney", tb => tb.HasTrigger("trgWireMoneyAdd"));

            entity.HasIndex(e => e.WireSourceTblId, "IX_tblTransactionPayWire_TransPayDateID").HasFillFactor(90);

            entity.HasIndex(e => e.CompanyId, "IX_tblWireMoney_Company_id")
                .IsDescending()
                .HasFillFactor(90);

            entity.Property(e => e.AffiliatePaymentsId).HasComputedColumnSql("(case [WireType] when (3) then [WireSourceTbl_id]  end)", true);
            entity.Property(e => e.IsShow).HasDefaultValue(true);
            entity.Property(e => e.PaymentOrderId).HasComputedColumnSql("(case [WireType] when (2) then [WireSourceTbl_id]  end)", true);
            entity.Property(e => e.SettlementId).HasComputedColumnSql("(case [WireType] when (1) then [WireSourceTbl_id]  end)", true);
            entity.Property(e => e.WireComment).HasDefaultValue("");
            entity.Property(e => e.WireCompanyLegalName).HasDefaultValue("");
            entity.Property(e => e.WireCompanyLegalNumber).HasDefaultValue("");
            entity.Property(e => e.WireCompanyName).HasDefaultValue("");
            entity.Property(e => e.WireConfirmationNum).HasDefaultValue("");
            entity.Property(e => e.WireDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.WireIdnumber).HasDefaultValue("");
            entity.Property(e => e.WireInsertDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.WirePaymentAbroadAba).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadAba2).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadAccountName).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadAccountName2).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadAccountNumber).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadAccountNumber2).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadBankAddress).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadBankAddress2).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadBankAddressCity).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadBankAddressCity2).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadBankAddressSecond).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadBankAddressSecond2).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadBankAddressState).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadBankAddressState2).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadBankAddressZip).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadBankAddressZip2).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadBankName).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadBankName2).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadIban).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadIban2).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadSepaBic).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadSepaBic2).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadSortCode).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadSortCode2).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadSwiftNumber).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAbroadSwiftNumber2).HasDefaultValue("");
            entity.Property(e => e.WirePaymentAccount).HasDefaultValue("");
            entity.Property(e => e.WirePaymentBank).HasDefaultValue(0);
            entity.Property(e => e.WirePaymentBranch).HasDefaultValue("");
            entity.Property(e => e.WirePaymentMethod).HasDefaultValue("");
            entity.Property(e => e.WirePaymentPayeeName).HasDefaultValue("");
            entity.Property(e => e.WirePrintApprovalStatusDate).HasDefaultValueSql("('')");
            entity.Property(e => e.WirePrintApprovalStatusUser).HasDefaultValue("");
            entity.Property(e => e.WireStatusDate).HasDefaultValueSql("('')");
            entity.Property(e => e.WireStatusUser).HasDefaultValue("");
            entity.Property(e => e.WireType).HasDefaultValue((byte)1);

            entity.HasOne(d => d.PaymentOrder).WithMany(p => p.TblWireMoneys).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Settlement).WithMany(p => p.TblWireMoneys).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TblWireMoneyFile>(entity =>
        {
            entity.Property(e => e.WmfDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.WmfDescription).HasDefaultValue("");
            entity.Property(e => e.WmfFileName).HasDefaultValue("");
            entity.Property(e => e.WmfUser).HasDefaultValue("");
        });

        modelBuilder.Entity<TblWireMoneyLog>(entity =>
        {
            entity.HasKey(e => e.WireMoneyLogId).HasFillFactor(90);

            entity.ToTable("tblWireMoneyLog", tb => tb.HasTrigger("trgWireMoneyLogAddSetWireMoneyLastLogDate"));

            entity.HasIndex(e => e.WireMoneyId, "wireMoneyId").HasFillFactor(90);

            entity.Property(e => e.WireMoneyId).HasDefaultValue(0);
            entity.Property(e => e.WmlDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.WmlDescription).HasDefaultValue("");
            entity.Property(e => e.WmlUser).HasDefaultValue("");

            entity.HasOne(d => d.LogMasavFile).WithMany(p => p.TblWireMoneyLogs)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_tblWireMoneyLog_LogMasavFileID");

            entity.HasOne(d => d.WireMoney).WithMany(p => p.TblWireMoneyLogs)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_tblWireMoney_WireMoneyID");
        });

        modelBuilder.Entity<TimeZone>(entity =>
        {
            entity.Property(e => e.TimeZoneOffsetMinutes).ValueGeneratedNever();
        });

        modelBuilder.Entity<TransAmountType>(entity =>
        {
            entity.Property(e => e.TransAmountTypeId).ValueGeneratedNever();
        });

        modelBuilder.Entity<TransAmountTypeGroup>(entity =>
        {
            entity.Property(e => e.TransAmountTypeGroupId).ValueGeneratedNever();

            entity.HasMany(d => d.TransAmountTypes).WithMany(p => p.TransAmountTypeGroups)
                .UsingEntity<Dictionary<string, object>>(
                    "TransAmountTypeToGroup",
                    r => r.HasOne<TransAmountType>().WithMany().HasForeignKey("TransAmountTypeId"),
                    l => l.HasOne<TransAmountTypeGroup>().WithMany().HasForeignKey("TransAmountTypeGroupId"),
                    j =>
                    {
                        j.HasKey("TransAmountTypeGroupId", "TransAmountTypeId");
                        j.ToTable("TransAmountTypeToGroup", "List");
                        j.IndexerProperty<int>("TransAmountTypeGroupId").HasColumnName("TransAmountTypeGroup_id");
                        j.IndexerProperty<int>("TransAmountTypeId").HasColumnName("TransAmountType_id");
                    });
        });

        modelBuilder.Entity<TransHistory>(entity =>
        {
            entity.HasIndex(e => e.TransHistoryTypeId, "IX_TransHistory_HistoryType_id").HasFillFactor(80);

            entity.HasIndex(e => e.InsertDate, "IX_TransHistory_InsertDate").HasFillFactor(90);

            entity.HasIndex(e => e.TransPendingId, "IX_TransHistory_Pending_id")
                .HasFilter("([TransPending_id] IS NOT NULL)")
                .HasFillFactor(90);

            entity.HasIndex(e => e.TransFailId, "IX_TransHistory_TransFail_id")
                .HasFilter("([TransFail_id] IS NOT NULL)")
                .HasFillFactor(90);

            entity.HasIndex(e => e.TransPassId, "IX_TransHistory_TransPass_id")
                .HasFilter("([TransPass_id] IS NOT NULL)")
                .HasFillFactor(90);

            entity.HasIndex(e => e.TransPendingId, "IX_TransHistory_TransPending_id").HasFilter("([TransPending_id] IS NOT NULL)");

            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Merchant).WithMany(p => p.TransHistories).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.TransFail).WithMany(p => p.TransHistories).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.TransHistoryType).WithMany(p => p.TransHistories).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.TransPass).WithMany(p => p.TransHistories).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.TransPending).WithMany(p => p.TransHistories).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.TransPreAuth).WithMany(p => p.TransHistories).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TransMatchPending>(entity =>
        {
            entity.ToTable("TransMatchPending", "Data", tb => tb.HasComment("Updates of fraud waiting to be matched to a transaction, records here are deleted after moved to trans history"));

            entity.Property(e => e.CurrencyIsocode).IsFixedLength();

            entity.HasOne(d => d.DebitCompany).WithMany(p => p.TransMatchPendings)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SetRiskRule_DebitCompany");

            entity.HasOne(d => d.ExternalServiceType).WithMany(p => p.TransMatchPendings).HasConstraintName("FK_TransMatchPending_ExternalServiceTypeID");
        });

        modelBuilder.Entity<TransPayerAdditionalInfo>(entity =>
        {
            entity.ToTable("TransPayerAdditionalInfo", "Trans", tb => tb.HasComment("Transaction payer extra info as requested by the merchant"));

            entity.HasOne(d => d.TransPayerInfo).WithMany(p => p.TransPayerAdditionalInfos).HasConstraintName("FK_TransPayerAdditionalInfo_TransPayerInfo");
        });

        modelBuilder.Entity<TransPayerInfo>(entity =>
        {
            entity.HasOne(d => d.Merchant).WithMany(p => p.TransPayerInfos).HasConstraintName("FK_TransPayerInfo_tblCompany_MerchantID");

            entity.HasOne(d => d.TransPayerShippingDetail).WithMany(p => p.TransPayerInfos).HasConstraintName("FK_TransPayerInfo_TransPayerShippingDetail");
        });

        modelBuilder.Entity<TransPayerShippingDetail>(entity =>
        {
            entity.Property(e => e.CountryIsocode).IsFixedLength();
            entity.Property(e => e.StateIsocode).IsFixedLength();
        });

        modelBuilder.Entity<TransPaymentBillingAddress>(entity =>
        {
            entity.HasKey(e => e.TransPaymentBillingAddressId).HasName("PK_PaymentBillingAddress");

            entity.Property(e => e.CountryIsocode).IsFixedLength();
            entity.Property(e => e.StateIsocode).IsFixedLength();
        });

        modelBuilder.Entity<TransPaymentMethod>(entity =>
        {
            entity.Property(e => e.IssuerCountryIsoCode).IsFixedLength();
            entity.Property(e => e.Value1First6Text).IsFixedLength();
            entity.Property(e => e.Value1Last4Text).IsFixedLength();

            entity.HasOne(d => d.Merchant).WithMany(p => p.TransPaymentMethods)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TransPaymentMethod_tblCompany");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.TransPaymentMethods)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaymentMethod_PaymentMethod");

            entity.HasOne(d => d.TransPaymentBillingAddress).WithMany(p => p.TransPaymentMethods).HasConstraintName("FK_TransPaymentMethod_TransPaymentBillingAddress");
        });

        modelBuilder.Entity<TransSource>(entity =>
        {
            entity.HasKey(e => e.TransSourceId).HasFillFactor(90);
        });

        modelBuilder.Entity<TransactionAmount>(entity =>
        {
            entity.Property(e => e.CurrencyIsocode).IsFixedLength();
            entity.Property(e => e.Total).HasComputedColumnSql("([FixedAmount]+[PercentAmount])", false);

            entity.HasOne(d => d.AmountType).WithMany(p => p.TransactionAmounts).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SettlementType).WithMany(p => p.TransactionAmounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TransactionAmount_SettlementType");

            entity.HasOne(d => d.TransFail).WithMany(p => p.TransactionAmounts)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_TransactionAmount_tblCompanyTransFail");

            entity.HasOne(d => d.TransPass).WithMany(p => p.TransactionAmounts)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_TransactionAmount_tblCompanyTransPass");

            entity.HasOne(d => d.TransPreAuth).WithMany(p => p.TransactionAmounts)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_TransactionAmount_tblCompanyTransApproval");
        });

        modelBuilder.Entity<ViewBin>(entity =>
        {
            entity.ToView("viewBIN");
        });

        modelBuilder.Entity<ViewBnsTransactionsWithoutEpa>(entity =>
        {
            entity.ToView("viewBnsTransactionsWithoutEpa");

            entity.Property(e => e.Currency).IsFixedLength();
            entity.Property(e => e.TransDate).IsFixedLength();
        });

        modelBuilder.Entity<ViewCurrency>(entity =>
        {
            entity.ToView("viewCurrencies");
        });

        modelBuilder.Entity<ViewCustomerTran>(entity =>
        {
            entity.ToView("viewCustomerTrans");
        });

        modelBuilder.Entity<ViewCustomerTransFail>(entity =>
        {
            entity.ToView("viewCustomerTransFail");
        });

        modelBuilder.Entity<ViewCustomerTransPass>(entity =>
        {
            entity.ToView("viewCustomerTransPass");
        });

        modelBuilder.Entity<ViewDebitTerminalMonthlyLimit>(entity =>
        {
            entity.ToView("viewDebitTerminalMonthlyLimits");
        });

        modelBuilder.Entity<ViewGetNewid>(entity =>
        {
            entity.ToView("viewGetNEWID");
        });

        modelBuilder.Entity<ViewImportChargeback>(entity =>
        {
            entity.ToView("viewImportChargeback");
        });

        modelBuilder.Entity<ViewImportChargebackBn>(entity =>
        {
            entity.ToView("viewImportChargebackBNS");
        });

        modelBuilder.Entity<ViewImportChargebackExcel>(entity =>
        {
            entity.ToView("viewImportChargebackExcel");

            entity.Property(e => e.RefundCurrency).IsFixedLength();
            entity.Property(e => e.TransCurrency).IsFixedLength();
        });

        modelBuilder.Entity<ViewImportChargebackJcc>(entity =>
        {
            entity.ToView("viewImportChargebackJCC");
        });

        modelBuilder.Entity<ViewInvoice>(entity =>
        {
            entity.ToView("viewInvoices");

            entity.Property(e => e.IdBillingCompanyLanguage).IsFixedLength();
        });

        modelBuilder.Entity<ViewMerchantMailingList>(entity =>
        {
            entity.ToView("viewMerchantMailingList");
        });

        modelBuilder.Entity<ViewMerchantTerminal>(entity =>
        {
            entity.ToView("viewMerchantTerminals");
        });

        modelBuilder.Entity<ViewMerchantsInactive>(entity =>
        {
            entity.ToView("viewMerchantsInactive");

            entity.Property(e => e.LastSettlement).IsFixedLength();
            entity.Property(e => e.LastTransFail).IsFixedLength();
            entity.Property(e => e.LastTransPass).IsFixedLength();
            entity.Property(e => e.LastTransPending).IsFixedLength();
            entity.Property(e => e.LastTransPreAuth).IsFixedLength();
            entity.Property(e => e.SignupDate).IsFixedLength();
        });

        modelBuilder.Entity<ViewMerchantsIsracard>(entity =>
        {
            entity.ToView("viewMerchantsIsracard");
        });

        modelBuilder.Entity<ViewMerchantsMailing>(entity =>
        {
            entity.ToView("viewMerchantsMailing");

            entity.Property(e => e.OpenDate).IsFixedLength();
        });

        modelBuilder.Entity<ViewEzzygate014>(entity =>
        {
            entity.ToView("viewEzzygate014");
        });

        modelBuilder.Entity<ViewNonAmericanGamer>(entity =>
        {
            entity.ToView("viewNonAmericanGamers");
        });

        modelBuilder.Entity<ViewPaymentsInfo>(entity =>
        {
            entity.ToView("viewPaymentsInfo");
        });

        modelBuilder.Entity<ViewRandNumber>(entity =>
        {
            entity.ToView("viewRandNumber");
        });

        modelBuilder.Entity<ViewRecurringActiveCharge>(entity =>
        {
            entity.ToView("viewRecurringActiveCharges");
        });

        modelBuilder.Entity<ViewRecurringAdmin>(entity =>
        {
            entity.ToView("viewRecurringAdmin");

            entity.Property(e => e.Bincountry).IsFixedLength();
            entity.Property(e => e.CurrencyName).IsFixedLength();
        });

        modelBuilder.Entity<ViewRecurringToKeepPaymentInfo>(entity =>
        {
            entity.ToView("viewRecurringToKeepPaymentInfo");
        });

        modelBuilder.Entity<ViewRecurringUnprocessed>(entity =>
        {
            entity.ToView("viewRecurringUnprocessed");
        });

        modelBuilder.Entity<ViewRetrivalAfterRefund>(entity =>
        {
            entity.ToView("viewRetrivalAfterRefund");
        });

        modelBuilder.Entity<ViewSettlementsReport>(entity =>
        {
            entity.ToView("viewSettlementsReport");
        });

        modelBuilder.Entity<ViewSummaryBn>(entity =>
        {
            entity.ToView("viewSummaryBns");

            entity.Property(e => e.Cur).IsFixedLength();
        });

        modelBuilder.Entity<ViewSummaryBnsDecline>(entity =>
        {
            entity.ToView("viewSummaryBnsDeclines");

            entity.Property(e => e.Cur).IsFixedLength();
        });

        modelBuilder.Entity<ViewSummaryBnsDeclinesArchive>(entity =>
        {
            entity.ToView("viewSummaryBnsDeclinesArchive");

            entity.Property(e => e.Cur).IsFixedLength();
        });

        modelBuilder.Entity<ViewSummaryBnsSale>(entity =>
        {
            entity.ToView("viewSummaryBnsSales");

            entity.Property(e => e.Cur).IsFixedLength();
        });

        modelBuilder.Entity<ViewTerminalsWithZeroFee>(entity =>
        {
            entity.ToView("viewTerminalsWithZeroFee");
        });

        modelBuilder.Entity<ViewTriggersDcl>(entity =>
        {
            entity.ToView("viewTriggersDCL");
        });

        modelBuilder.Entity<VwQnagroup>(entity =>
        {
            entity.ToView("vwQNAGroup");
        });

        modelBuilder.Entity<Wire>(entity =>
        {
            entity.HasKey(e => e.WireId).HasFillFactor(95);

            entity.Property(e => e.CurrencyIsocode).IsFixedLength();
            entity.Property(e => e.CurrencyIsocodeProcessed).IsFixedLength();

            entity.HasOne(d => d.Account).WithMany(p => p.Wires)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Wire_AccountrID");

            entity.HasOne(d => d.AccountPayee).WithMany(p => p.Wires).HasConstraintName("FK_Wire_AccountPayeeID");

            entity.HasOne(d => d.AffiliateSettlement).WithMany(p => p.Wires).HasConstraintName("FK_Wire_AffiliateSettlementID");

            entity.HasOne(d => d.CurrencyIsocodeNavigation).WithMany(p => p.WireCurrencyIsocodeNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Wire_CurrencyISOCode");

            entity.HasOne(d => d.CurrencyIsocodeProcessedNavigation).WithMany(p => p.WireCurrencyIsocodeProcessedNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Wire_CurrencyISOCodeProcessed");

            entity.HasOne(d => d.MerchantSettlement).WithMany(p => p.Wires).HasConstraintName("FK_Wire_MerchantSettlementID");

            entity.HasOne(d => d.WireBatch).WithMany(p => p.Wires).HasConstraintName("FK_Wire_WireBatchID");

            entity.HasOne(d => d.WireProvider).WithMany(p => p.Wires).HasConstraintName("FK_Wire_WireProviderID");
        });

        modelBuilder.Entity<WireAccount>(entity =>
        {
            entity.Property(e => e.CurrencyIsocode).IsFixedLength();

            entity.HasOne(d => d.WireProvider).WithMany(p => p.WireAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WireAccount_WireProvider");
        });

        modelBuilder.Entity<WireBatch>(entity =>
        {
            entity.HasKey(e => e.WireBatchId).HasFillFactor(95);

            entity.Property(e => e.BatchCurrencyIsocode).IsFixedLength();

            entity.HasOne(d => d.BatchCurrencyIsocodeNavigation).WithMany(p => p.WireBatches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WireBatch_[BatchCurrencyISOCode");

            entity.HasOne(d => d.WireProvider).WithMany(p => p.WireBatches).HasConstraintName("FK_WireBatch_WireProviderID");
        });

        modelBuilder.Entity<WireHistory>(entity =>
        {
            entity.HasKey(e => e.WireHistoryId).HasFillFactor(95);

            entity.HasOne(d => d.Wire).WithMany(p => p.WireHistories).HasConstraintName("FK_Wire_WireID");
        });

        modelBuilder.Entity<WorldRegion>(entity =>
        {
            entity.HasKey(e => e.WorldRegionIsocode).HasFillFactor(100);

            entity.Property(e => e.WorldRegionIsocode).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
