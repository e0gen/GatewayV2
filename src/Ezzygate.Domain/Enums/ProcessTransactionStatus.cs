namespace Ezzygate.Domain.Enums;

public enum ProcessTransactionStatus
{
    Unknown = -1,
    Unsupported = 1,
    Pending = 2,
    Approved = 3,
    Declined = 4,
    Redirect = 5
}