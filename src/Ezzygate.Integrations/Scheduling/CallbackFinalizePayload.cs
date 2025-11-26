namespace Ezzygate.Integrations.Scheduling;

public sealed record CallbackFinalizePayload
{
    public required string Tag { get; init; }
    public required string EventName { get; init; }
    public required string ApprovalNumber { get; init; }
    public required string DebitReferenceCode { get; init; }
    public required string DebitReferenceNum { get; init; }
    public required int ChargeAttemptLogId { get; init; }
    public string? AutomatedStatus { get; init; }
    public string? AutomatedErrorMessage { get; init; }
    public object? AutomatedPayload { get; init; }
}