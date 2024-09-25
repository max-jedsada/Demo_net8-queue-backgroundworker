namespace net8_queue_backgroundworker.Interface
{
    public interface IBackgroundTaskQueue
    {
        //void QueueBackgroundWorkItem(Func<CancellationToken, ValueTask> workItem);
        Task<Func<CancellationToken, ValueTask>> DequeueAsync(CancellationToken cancellationToken);
        ValueTask QueueBackgroundWorkItem(Func<CancellationToken, ValueTask> workItem);

    }
}
