namespace Ezzygate.Domain.Enums;

public enum OperationType
{
    Unknown,
    Sale,
    Sale3Ds,
    Authorization,
    Authorization3Ds,
    AuthorizationRelease,
    AuthorizationCapture,
    RecurringInit,
    RecurringInit3Ds,
    RecurringSale,
    Refund,
    Finalize,
    PhoneConfirmation
}