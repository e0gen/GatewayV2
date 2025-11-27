namespace Ezzygate.Integrations.Core.Scheduling;

public sealed record TimeoutFinalizePayload
{
    public required string Tag { get; init; }
    public required string DebitReferenceCode { get; init; }
    public required int ChargeAttemptLogId { get; init; }
}