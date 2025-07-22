namespace Ezzygate.Domain.Enums
{
    public enum ProcessTransactionType
    {
        Unknown = -1,
        Authorize = 1,
        Capture = 2,
        Sale = 3,
        Reverse = 4,
        Refund = 5,
        Credit = 6,
        BankBack = 7,
    }
}