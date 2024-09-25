using net8_queue_backgroundworker.Controllers;
using net8_queue_backgroundworker.Interface;
using net8_queue_backgroundworker.Models;

namespace net8_queue_backgroundworker.Service
{
    public class TestService : ITestService
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public TestService(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        public async ValueTask Add(List<Customer> data)
        {
            var guid = Guid.NewGuid().ToString();

            try
            {
                for (int i = 1; i <= data.Count(); i++)
                {
                    _logger.LogInformation("servide Add ..... count : " + i);
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }

            }
            catch (OperationCanceledException)
            {
            }

            _logger.LogInformation("servide Add ..... success ....");
            await Task.Delay(TimeSpan.FromSeconds(3));

            // noti to ... bla bla bla
        }

    }
}
