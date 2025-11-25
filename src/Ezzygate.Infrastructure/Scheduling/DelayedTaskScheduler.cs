using System.Threading.Channels;

namespace Ezzygate.Infrastructure.Scheduling;

public sealed class DelayedTaskScheduler : IDelayedTaskScheduler
{
    private readonly Channel<ScheduledTaskEntry> _channel = Channel.CreateUnbounded<ScheduledTaskEntry>(
        new UnboundedChannelOptions
        {
            SingleReader = false,
            SingleWriter = false
        });

    internal ChannelReader<ScheduledTaskEntry> Reader => _channel.Reader;

    public void Schedule<TPayload>(Type taskType, TPayload payload, TimeSpan delay) where TPayload : class
    {
        ArgumentNullException.ThrowIfNull(taskType);
        ArgumentNullException.ThrowIfNull(payload);

        var entry = new ScheduledTaskEntry
        {
            TaskType = taskType,
            PayloadType = typeof(TPayload),
            Payload = payload,
            ExecuteAt = DateTimeOffset.UtcNow.Add(delay),
            TaskId = Guid.NewGuid().ToString("N")
        };

        if (!_channel.Writer.TryWrite(entry))
        {
            throw new InvalidOperationException("Failed to schedule task - channel is closed");
        }
    }

    public void Schedule<TTask, TPayload>(TPayload payload, TimeSpan delay)
        where TTask : IDelayedTask<TPayload>
        where TPayload : class
    {
        Schedule(typeof(TTask), payload, delay);
    }
}