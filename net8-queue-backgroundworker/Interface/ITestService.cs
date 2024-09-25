using net8_queue_backgroundworker.Models;

namespace net8_queue_backgroundworker.Interface
{
    public interface ITestService
    {
        ValueTask Add(List<Customer> data);
    }
}
