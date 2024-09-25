using net8_queue_backgroundworker.Interface;
using System.Collections.Concurrent;
using System.Threading.Channels;

namespace net8_queue_backgroundworker.Service
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly ConcurrentQueue<Func<CancellationToken, ValueTask>> _queue = new();
        private readonly SemaphoreSlim _signal = new(0);

        public async ValueTask QueueBackgroundWorkItem(Func<CancellationToken, ValueTask> workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            _queue.Enqueue(workItem);
            _signal.Release();
        }

        public async Task<Func<CancellationToken, ValueTask>> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _queue.TryDequeue(out var workItem);

            return workItem!;
        }


    }
}
