namespace Ezzygate.Common.Enums
{
    public enum PendingEventType
    {
        Unknown = 0,
        FeesTransaction = 10,
        InfoEmailSendClient = 100,
        InfoEmailSendMerchant = 101,
        InfoEmailSendAffiliate = 102,
        InfoEmailSendRiskMultipleCardsOnEmail = 103,
        ResetPasswordEmailCustomer = 110,
        ResetPasswordEmailMerchant = 111,
        ResetPasswordEmailAffiliate = 112,
        ResetPasswordEmailAdmin = 113,
        CreateRecurringSeries = 200,
        RecurringSendNotify = 201,
        RefundRequestSendNotify = 202,
        PendingSendNotify = 203,
        AutoCapture = 300,
        GenerateInstallments = 400,
        GenerateOldInstallments = 401,
        CreateInvoice = 402,
        InfoSmsSendClient = 403,
        SmsRegisterDevice = 404,
        ResetPinCodeAccount = 500,
        WalletRegisterEmail = 600,
        EmailPhotoCopy = 2001,
        EmailChargeBack = 2002,
        EmailMerchantFraud = 2003,
        EmailMerchantAccountBlocked = 2004,
        EmailMerchantIntegration = 2005,
        EmailCustomerHppLink = 2010
    }
}