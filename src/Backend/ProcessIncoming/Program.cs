using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace GM.Backend.ProcessIncoming
{
    /* Eventually ProcessIncoming will become part of WebApp. It is in a separate proceess
     * for development. We want to start it in the same way that WebApp will start it:
     * Use dependency injection with providers for logging, configuration, etc.
     */

    class Program
    {
        // Todo-g - This should come from configuration
        static string datafilesPath = Environment.CurrentDirectory + @"\..\..\Datafiles";


        static void Main(string[] args)
        {
            // https://pioneercode.com/post/dependency-injection-logging-and-configuration-in-a-dot-net-core-console-app

            // create service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // entry to run app
            serviceProvider.GetService<ProcessIncoming>().Run();
        }
        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // add configured instance of logging
            serviceCollection.AddSingleton(new LoggerFactory()
                .AddConsole()
                .AddDebug());

            // add logging
            serviceCollection.AddLogging();

            // build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            serviceCollection.AddOptions();
            serviceCollection.Configure<AppSettings>(configuration.GetSection("DataPaths"));

            // add services
            serviceCollection.AddTransient<ITestService, TestService>();

            // add app
            serviceCollection.AddTransient<ProcessIncoming>();
        }
    }
}
