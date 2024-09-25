using Microsoft.AspNetCore.Mvc;
using net8_queue_backgroundworker.Interface;
using net8_queue_backgroundworker.Models;
using net8_queue_backgroundworker.Service;

namespace net8_queue_backgroundworker.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITestService _testService;
        private readonly IBackgroundTaskQueue _backgroundTaskQueue;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ITestService testService, IBackgroundTaskQueue backgroundTaskQueue)
        {
            _logger = logger;
            _testService = testService;
            _backgroundTaskQueue = backgroundTaskQueue;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Get .....");

            return Ok("Get");
        }

        [HttpGet]
        public async Task<IActionResult> DoTask()
        {
            var data = new Customer().Initialize();

            await _backgroundTaskQueue.QueueBackgroundWorkItem(async token =>
            {
                await _testService.Add(data);
            });

            return Ok("DoTask");

        }


    }
}
