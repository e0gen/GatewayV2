namespace Ezzygate.Domain.Enums;

public enum ProcessTransactionSource
{
    Unknown = -1,
    Manual = 0,
    Swipe = 1,
    Moto = 2,
    Ivr = 3,
    ECommerce = 4,
    ECommerce3D = 5,
    Sms = 6,
    PaymentPage = 7,
    System = 8
}