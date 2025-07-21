namespace Ezzygate.Common.Enums
{
    public enum PaymentMethodEnum
    {
        Unknown = 0,
        ManualFees = 1,
        Admin = 2,
        SystemFees = 3,
        RollingReserve = 4,
        BankFees = 5,
        MaxInternal = 14,

        TotalsPM_Min = 20,

        CC_MIN = 20,
        CCUnknown = 20,
        CCIsracard = 21,
        CCVisa = 22,
        CCDiners = 23,
        CCAmex = 24,
        CCMastercard = 25,
        CCDirect = 26,
        CCOther = 27,
        CCMaestro = 28,
        CCTogglecard = 29,
        CCJcb = 31,
        CCDiscover = 32,
        CCCup = 33,
        CCLiveCash = 34,
        CCOneCard = 35,

        CC_LamdaCard = 98,
        CC_MAX = 99,

        EC_MIN = 100,
        ECCheck = 100,
        Giropay = 101,
        DirectPay24 = 102,
        PinELV = 103,
        PaySafeCard = 104,
        CashTicket = 105,
        Pezelewy24 = 106,
        EPS = 107,
        Wallie = 108,
        IDEAL = 109,
        TeleIngreso = 110,
        MoneyBookers = 111,
        ELV = 112,
        YellowPay = 113,
        Lastschrift = 114,
        Bankeinzug = 115,
        UKInstantDebit = 116,
        AutomatischeIncasso = 117,
        CargoBancario = 118,
        UKDirectDebit = 119,
        AustriaDirectDebit = 120,
        GermanyDirectDebit = 121,
        NetherlandsDirectDebit = 122,
        SpainDirectDebit = 123,
        PushBankTransfer = 149,
        EC_MAX = 150,

        CS_MIN = 151,
        WebMoney = 151, //custom group
        CS_MAX = 199,

        PD_MIN = 200,
        MicroPaymentsIN = 200,
        MicroPaymentsOut = 201,
        IvrPerCall = 202,
        IvrPerMin = 203,
        IvrFreeTime = 204,
        Qiwi = 205,
        PD_MAX = 230,

        PayPal = 231,
        GoogleCheckout = 232,
        MoneyBookersHpp = 233,
        GetCrypto = 234,
        BitcoBrokers = 235,
        GatewayStar = 236,
        BtcWallet = 237,
        FlutterWave = 238,

        TotalsPM_Max = 250,
        IsBpWallet = 239,
        IsApplePay = 240,
        IsGoogleCheckout = 241,

        PaymentMethodMax = 20000,


        //Min and Max values for PrePaid payment methods.
        PP_MIN = 5000, // For testing the value is 240 , the real value will be 5000
        PP_MAX = 20000 // For testing only , the real value will be 20000
    }
}
  