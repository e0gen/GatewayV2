namespace Ezzygate.Integrations.Core.Events;

public sealed class IntegrationEventContext
{
    public required string UrlPath { get; init; }
    public required string Payload { get; init; }
    public string? Signature { get; init; }
    public required string ApprovalNumber { get; init; }
    public required string EventName { get; init; }
    public string? AutomatedStatus { get; init; }
    public string? AutomatedErrorMessage { get; init; }
    public object? AutomatedPayload { get; init; }
    public Dictionary<string, string> Metadata { get; init; } = new(); // Additional integration-specific data.
}

