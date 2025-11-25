using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ezzygate.Infrastructure.Scheduling;

public sealed class DelayedTaskProcessor : BackgroundService
{
    private readonly DelayedTaskScheduler _scheduler;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<DelayedTaskProcessor> _logger;
    private readonly PriorityQueue<ScheduledTaskEntry, DateTimeOffset> _pendingTasks = new();
    private readonly SemaphoreSlim _signal = new(0);

    public DelayedTaskProcessor(
        DelayedTaskScheduler scheduler,
        IServiceScopeFactory scopeFactory,
        ILogger<DelayedTaskProcessor> logger)
    {
        _scheduler = scheduler;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("DelayedTaskProcessor started");

        var readerTask = ReadFromChannelAsync(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ProcessPendingTasksAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in task processing loop");
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }

        await readerTask;
        _logger.LogInformation("DelayedTaskProcessor stopped");
    }

    private async Task ReadFromChannelAsync(CancellationToken stoppingToken)
    {
        try
        {
            await foreach (var entry in _scheduler.Reader.ReadAllAsync(stoppingToken))
            {
                lock (_pendingTasks)
                {
                    _pendingTasks.Enqueue(entry, entry.ExecuteAt);
                }

                _signal.Release();
            }
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            // Expected during shutdown
        }
    }

    private async Task ProcessPendingTasksAsync(CancellationToken stoppingToken)
    {
        TimeSpan waitTime;
        lock (_pendingTasks)
        {
            if (_pendingTasks.Count == 0)
            {
                waitTime = TimeSpan.FromSeconds(30);
            }
            else
            {
                _pendingTasks.TryPeek(out _, out var nextExecuteAt);
                var timeUntilNext = nextExecuteAt - DateTimeOffset.UtcNow;
                waitTime = timeUntilNext > TimeSpan.Zero ? timeUntilNext : TimeSpan.Zero;
            }
        }

        if (waitTime > TimeSpan.Zero)
        {
            try
            {
                await _signal.WaitAsync(waitTime, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }

        while (true)
        {
            ScheduledTaskEntry? taskToExecute = null;

            lock (_pendingTasks)
            {
                if (_pendingTasks.Count > 0 &&
                    _pendingTasks.TryPeek(out var entry, out var executeAt) &&
                    executeAt <= DateTimeOffset.UtcNow)
                {
                    _pendingTasks.Dequeue();
                    taskToExecute = entry;
                }
            }

            if (taskToExecute == null)
                break;

            _ = ExecuteTaskAsync(taskToExecute, stoppingToken);
        }
    }

    private async Task ExecuteTaskAsync(ScheduledTaskEntry entry, CancellationToken stoppingToken)
    {
        try
        {
            _logger.LogDebug("Executing scheduled task {TaskId} of type {TaskType}",
                entry.TaskId, entry.TaskType.Name);

            await using var scope = _scopeFactory.CreateAsyncScope();

            var task = scope.ServiceProvider.GetRequiredService(entry.TaskType);

            var executeMethod = entry.TaskType.GetMethod("ExecuteAsync",
                [entry.PayloadType, typeof(CancellationToken)]);

            if (executeMethod == null)
            {
                _logger.LogError("Task {TaskType} does not have ExecuteAsync method for payload {PayloadType}",
                    entry.TaskType.Name, entry.PayloadType.Name);
                return;
            }

            var resultTask = executeMethod.Invoke(task, [entry.Payload, stoppingToken]) as Task;
            if (resultTask != null)
            {
                await resultTask;
            }

            _logger.LogDebug("Completed scheduled task {TaskId}", entry.TaskId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to execute scheduled task {TaskId} of type {TaskType}",
                entry.TaskId, entry.TaskType.Name);
        }
    }
}