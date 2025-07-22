namespace Ezzygate.Domain.Enums
{
    public enum PaymentMethodType
    {
        Unknown = 0,
        System = 1,
        CreditCard = 2,
        ECheck = 3, // usa ach transaction
        BankTransfer = 4, // european pull / push bank transfer
        PhoneDebit = 5,
        Wallet = 6,
        ExternalWallet = 7
    }
}