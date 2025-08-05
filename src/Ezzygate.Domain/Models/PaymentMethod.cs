namespace Ezzygate.Domain.Models;

public class PaymentMethod
{
    public short Id { get; init; }
    public byte? GroupId { get; init; }
    public int? Type { get; init; }
    public string? Name { get; init; }
    public string? Abbreviation { get; init; }
    public bool IsBillingAddressMandatory { get; init; }
    public bool IsPopular { get; init; }
    public bool IsPull { get; init; }
    public bool IsInfoMandatory { get; init; }
    public bool IsTerminalRequired { get; init; }
    public bool IsExpirationDateMandatory { get; init; }
    public string? Value1Caption { get; init; }
    public string? Value2Caption { get; init; }
    public string? Value1ValidationRegex { get; init; }
    public string? Value2ValidationRegex { get; init; }
    public byte? PaymentMethodTypeId { get; init; }
    public bool IsPersonalIdRequired { get; init; }
    public int? PendingKeepAliveMinutes { get; init; }
    public short? PendingCleanupDays { get; init; }
    public string? BaseBin { get; init; }
}
