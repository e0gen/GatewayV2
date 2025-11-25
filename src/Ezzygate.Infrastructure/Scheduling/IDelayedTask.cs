namespace Ezzygate.Infrastructure.Scheduling;

public interface IDelayedTask<in TPayload> where TPayload : class
{
    Task ExecuteAsync(TPayload payload, CancellationToken cancellationToken = default);
}