using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using GM.Configuration;
using GM.LoadDatabase;
//using GM.LoadDatabase;

namespace GM.WorkFlow
{

    public class WorkFlowController
    {
        /*  WorkFlowController is the "controller" for the Govmeeting processing steps:
         *  (1) It watches for new recording and transcript files that are either uploaded
         *      or retrieved automatically from YouTube, etc. and calls the pre-processing routines.
         *  (2) When the corrections are completed of the auto voice recognition text,
         *      it transforms the JSON format for the next step in the process: adding tags.
         *  (3) When the addition of tags is completed, it loads the completed transcript into
         *      the database.
         */


        //private readonly ITestService _testService;
        private readonly ILogger<WorkFlowController> _logger;
        private readonly AppSettings _config;
        private readonly ProcessIncomingFiles _processIncomingFiles;
        private ProcessFixedTranscriptions _processFixedTranscriptions;
        private ProcessTaggedTranscriptions _processTaggedTranscriptions;
        private LoadTranscript _loadTranscript;

        public WorkFlowController(
            //ITestService testService,
            IOptions<AppSettings> config,
            ILogger<WorkFlowController> logger,
            ProcessIncomingFiles processIncomingFiles,
            ProcessFixedTranscriptions processFixedTranscriptions,
            ProcessTaggedTranscriptions processTaggedTranscriptions,
            LoadTranscript loadTranscript
            )
        {
            //_testService = testService;
            _logger = logger;
            _config = config.Value;
            _processIncomingFiles = processIncomingFiles;
            _processFixedTranscriptions = processFixedTranscriptions;
            _processTaggedTranscriptions = processTaggedTranscriptions;
            _loadTranscript = loadTranscript;
        }

        public void Run()
        {
            _logger.LogInformation($"Start Workflow - datafilesPath = {_config.DatafilesPath}");

            _processIncomingFiles.Run();

            _processFixedTranscriptions.Run();

            _processTaggedTranscriptions.Run();

            _loadTranscript.Run();

            System.Console.ReadKey();
        }
    }
}
