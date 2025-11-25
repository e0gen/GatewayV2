namespace Ezzygate.Infrastructure.Scheduling;

public interface IDelayedTaskScheduler
{
    void Schedule<TPayload>(Type taskType, TPayload payload, TimeSpan delay) where TPayload : class;

    void Schedule<TTask, TPayload>(TPayload payload, TimeSpan delay)
        where TTask : IDelayedTask<TPayload>
        where TPayload : class;
}