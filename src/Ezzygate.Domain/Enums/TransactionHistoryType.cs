namespace Ezzygate.Domain.Enums;

public enum TransactionHistoryType
{
    Unknown = 0,
    Authorize = 1,
    Capture = 2,
    Refund = 3,
    Chargeback = 4,
    UpdateFees = 5,
    RetrievalRequest = 6,
    FraudSet = 7,
    FraudUnset = 8,
    InfoEmailSendPhotoCopy = 20,
    InfoEmailSendChargeBack = 21,
    InfoEmailSendFraud = 22,
    InfoEmailSendAccountBlocked = 23,
    InfoCreateRecurring = 41,
    InfoAutoRefund = 42,
    InfoRecurringSendNotify = 43,
    InfoRefundRequestSendNotify = 44,
    InfoPendingSendNotify = 45,
    InfoProcessingSendNotify = 46,
    InfoEmailSendClient = 51,
    InfoSmsSendClient = 52,
    InfoEmailSendMerchant = 53,
    InfoEmailSendAffiliate = 54,
    InfoEmailSendRiskMultipleCardsOnEmail = 55,
    AutoCapture = 80,
    Installment = 91,
    OldInstallment = 92,
    DetectRetrivalReqAfterRefund = 100,
    CreateInvoice = 120
}