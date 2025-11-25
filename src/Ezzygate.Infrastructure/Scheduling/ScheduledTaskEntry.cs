namespace Ezzygate.Infrastructure.Scheduling;

internal sealed record ScheduledTaskEntry
{
    public required Type TaskType { get; init; }
    public required Type PayloadType { get; init; }
    public required object Payload { get; init; }
    public required DateTimeOffset ExecuteAt { get; init; }
    public required string TaskId { get; init; }
}