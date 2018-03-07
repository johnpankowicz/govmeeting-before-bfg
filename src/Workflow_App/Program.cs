using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using GM.Configuration;
using GM.ProcessRecording;
using GM.ProcessTranscript;
using GM.FileDataRepositories;
using GM.DatabaseRepositories;

namespace GM.WorkFlow
{
    /* Eventually This will become part of WebApp. It is in a separate proceess
     * for development. We want to start it in the same way that WebApp will start it.
     * So we use dependency injection with providers for logging, configuration and other services.
     */

    class Program
    {
        static void Main(string[] args)
        {
            // https://pioneercode.com/post/dependency-injection-logging-and-configuration-in-a-dot-net-core-console-app

            // create service collection
            var services = new ServiceCollection();
            ConfigureServices(services);

            // create service provider
            var serviceProvider = services.BuildServiceProvider();

            // entry to run app
            serviceProvider.GetService<WorkFlowController>().Run();
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            // add configured instance of logging
            services.AddSingleton(new LoggerFactory()
                .AddConsole()
                .AddDebug());

            // add logging
            services.AddLogging();

            // build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            services.AddOptions();
            services.Configure<AppSettings>(configuration.GetSection("DataPaths"));

            // add services
            //services.AddTransient<ITestService, TestService>();
            services.AddTransient<ProcessIncomingFiles>();
            services.AddTransient<RecordingProcess>();
            services.AddTransient<TranscriptProcess>();
            //services.AddTransient<LoadTranscript>();
            services.AddTransient<ProcessTaggedTranscriptions>();
            services.AddTransient<ProcessFixedTranscriptions>();
            services.AddTransient<AddtagsRepository>();
            services.AddTransient<FixasrRepository>();
            services.AddTransient<IMeetingRepository, MeetingRepository>();
            services.AddTransient<IGovBodyRepository, GovBodyRepository>();

            // add app
            services.AddTransient<WorkFlowController>();
        }
    }
}
