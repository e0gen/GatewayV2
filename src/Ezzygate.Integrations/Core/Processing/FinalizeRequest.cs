using Ezzygate.Domain.Enums;

namespace Ezzygate.Integrations.Core.Processing;

public sealed class FinalizeRequest
{
    public required string DebitReferenceCode { get; init; }
    public string? DebitReferenceNum { get; init; }
    public int ChargeAttemptLogId { get; init; }
    public OperationType OperationType { get; init; } = OperationType.Finalize;
    public string? RequestContent { get; init; }
    public string? FormData { get; init; }
    public string? QueryString { get; init; }
    public bool IsAutomatedRequest { get; init; }
    public string? AutomatedStatus { get; init; }
    public string? AutomatedCode { get; init; }
    public string? AutomatedMessage { get; init; }
    public object? AutomatedPayload { get; init; }
}