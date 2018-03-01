using Microsoft.Extensions.Logging;

namespace ConsoleApp1
{
    public class App
    {
        private readonly ITestService _testService;
        private readonly ILogger<App> _logger;

        public App(
            ITestService testService,
            ILogger<App> logger
            )
        {
            _testService = testService;
            _logger = logger;
        }

        public void Run()
        {
            //_logger.LogInformation($"This is a console application for {_config.Title}");
            _logger.LogInformation($"This is a console application for _config.Title");
            _testService.Run();
            System.Console.ReadKey();
        }
    }
}
