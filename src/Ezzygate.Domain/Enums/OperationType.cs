// ReSharper disable InconsistentNaming
namespace Ezzygate.Domain.Enums;

public enum OperationType
{
    Unknown,
    Sale,
    Sale3DS,
    Authorization,
    Authorization3DS,
    AuthorizationRelease,
    AuthorizationCapture,
    RecurringInit,
    RecurringInit3DS,
    RecurringSale,
    Refund,
    Finalize,
    PhoneConfirmation
}