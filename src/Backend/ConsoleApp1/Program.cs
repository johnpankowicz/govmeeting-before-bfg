using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // create service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // entry to run app
            serviceProvider.GetService<App>().Run();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // add logging
            serviceCollection.AddSingleton(new LoggerFactory()
            .AddConsole()
            .AddDebug());
            serviceCollection.AddLogging();

            // add services
            serviceCollection.AddTransient<ITestService, TestService>();

            // add app
            serviceCollection.AddTransient<App>();
        }
    }
}


/*
 * Install-Package Microsoft.Extensions.DependencyInjection
 * Install-Package Microsoft.Extensions.Configuration.Json
 * Install-Package Microsoft.Extensions.Configuration.FileExtensions
 * Install-Package Microsoft.Extensions.Configuration
 * Install-Package Microsoft.Extensions.Options.ConfigurationExtensions
 * Install-Package Microsoft.Extensions.Logging
 * Install-Package Microsoft.Extensions.Logging.Console
 * Install-Package Microsoft.Extensions.Logging.Debug
 * 
 */
