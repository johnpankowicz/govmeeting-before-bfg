using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using GM.Configuration;


namespace GM.WebFlow
{

    public class WorkFlow
    {
        /*  The "WorkFlow" component will eventually become the "controller" for the 
         *  Govmeeting process steps.
         *  At this moment, it only handles one step: that of pre-processing new files.
         */


        //private readonly ITestService _testService;
        private readonly ILogger<WorkFlow> _logger;
        private readonly AppSettings _config;
        private readonly ProcessIncomingFiles _processIncomingFiles;

        public WorkFlow(
            //ITestService testService,
            IOptions<AppSettings> config,
            ILogger<WorkFlow> logger,
            ProcessIncomingFiles processIncomingFiles
            )
        {
            //_testService = testService;
            _logger = logger;
            _config = config.Value;
            _processIncomingFiles = processIncomingFiles;
        }

        public void Run()
        {
            _logger.LogInformation($"Start watching the incoming folder for work. datafilesPath = {_config.DatafilesPath}");

            //_testService.Run();

            _processIncomingFiles.WatchIncoming();

            System.Console.ReadKey();
        }
    }
}
