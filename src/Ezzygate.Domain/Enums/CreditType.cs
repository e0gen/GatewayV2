namespace Ezzygate.Domain.Enums;

public enum CreditType
{
    Unknown = -1,
    Refund = 0,
    Regular = 1,
    DelayedCharge = 2,
    CreditCharge = 6,
    Installments = 8,
    Debit = 30,
    Credit = 31
}