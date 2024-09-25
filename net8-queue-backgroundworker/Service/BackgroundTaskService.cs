using net8_queue_backgroundworker.Interface;

namespace net8_queue_backgroundworker.Service
{
    public class BackgroundTaskService : BackgroundService
    {
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly ILogger<BackgroundTaskService> _logger;

        public BackgroundTaskService(IBackgroundTaskQueue taskQueue, ILogger<BackgroundTaskService> logger)
        {
            _taskQueue = taskQueue;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var workItem = await _taskQueue.DequeueAsync(stoppingToken);
                    if (workItem == null) continue;

                    _logger.LogInformation("Execute task start .....");

                    // do task
                    await workItem(stoppingToken);
                }
                catch (Exception)
                {
                    // Log bla bla bla.
                    _logger.LogError("Execute task catch .....");
                }
            }
        }
    }
}
