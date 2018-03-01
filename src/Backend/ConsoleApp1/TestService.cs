using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public interface ITestService
    {
        void Run();
    }

    class TestService : ITestService
    {
        private readonly ILogger<TestService> _logger;

        public TestService(ILogger<TestService> logger)
        {
            _logger = logger;
        }

        public void Run()
        {
            _logger.LogWarning("Wow! We are now in the test service.");
        }
    }
}
