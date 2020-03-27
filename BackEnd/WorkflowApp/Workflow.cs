using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using GM.Configuration;
using GM.LoadDatabase;

namespace GM.Workflow
{

    public class WorkflowController
    {
        //private readonly ITestService _testService;
        private readonly ILogger<WorkflowController> _logger;
        private readonly AppSettings _config;
        private readonly RetrieveOnlineFiles _retrieveOnlineFiles;
        private readonly ProcessIncomingFiles _processNewFiles;
        private readonly ProcessFixedAsr _processFixedAsr;
        private readonly ProcessTagged _processTagged;
        private readonly INotifyManager _notifyManager;
        private readonly ILoadTranscript _loadTranscript;

        public WorkflowController(
            //ITestService testService,
            IOptions<AppSettings> config,
            ILogger<WorkflowController> logger,
            RetrieveOnlineFiles retrieveOnlineFiles,
            ProcessIncomingFiles processIncomingFiles,
            ProcessFixedAsr processFixedAsr,
            ProcessTagged processTagged,
            INotifyManager notifyManager,
            ILoadTranscript loadTranscript
            )
        {
            //_testService = testService;
            _logger = logger;
            _config = config.Value;
            _retrieveOnlineFiles = retrieveOnlineFiles;
            _processNewFiles = processIncomingFiles;
            _processFixedAsr = processFixedAsr;
            _processTagged = processTagged;
            _notifyManager = notifyManager;
            _loadTranscript = loadTranscript;
        }

        public void Run()
        {
            _logger.LogInformation($"Start Workflow - datafilesPath = {_config.DatafilesPath}");

            string testfilesPath = _config.TestfilesPath;
            string datafilesPath = _config.DatafilesPath;
            InitializeFileTestData.CopyTestData(testfilesPath, datafilesPath);

            // Retreive online transcripts or recordings
            _retrieveOnlineFiles.Run();

            // Process new files - auto speech recognition of recordings and
            // pre-processing of transcript files
            _processNewFiles.Run();

            // Process the fixed transcripts to get ready for tagging
            _processFixedAsr.Run();

            // Process tagged transcripts - Create browsable transcript and get ready for loading database
            _processTagged.Run();

            // Load completed transcript data into database
            _loadTranscript.Run();

            // Notify manager(s) if approval is needed on any steps.
            _notifyManager.Run();

            System.Console.ReadKey();
        }
    }
}
