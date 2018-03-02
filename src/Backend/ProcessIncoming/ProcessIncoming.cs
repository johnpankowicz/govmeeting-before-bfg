using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GM.Backend.ProcessIncoming
{
    /*  This component processes raw transcripts and recordings of government meetings.
        For development, it is simpler to keep it in a process seperate from  WebApp.
        But it will eventually will become part of WebApp.

        It watches the "INCOMING" folder for new files and processes them.
        It creates a work folder in the Datafiles folder based on the name of the file.

        For transcript files, it produces a file in the subfolder "T3-ToTag" of the meeting folder.
        This file becomes input for the next step (step 4) of the transcript processing, adding tags.

        For recordings, it produces a group of files in the subfolder "R4-FixText" of the meeting folder.
        Each of these files contain a segment of the meeting text which are ready for the next step (step 5) of
        the recording processings, fixing the voice recognition text.
     */

    public class ProcessIncoming
    {
        //private readonly ITestService _testService;
        private readonly ILogger<ProcessIncoming> _logger;
        private readonly AppSettings _config;

        public ProcessIncoming(
            //ITestService testService,
            IOptions<AppSettings> config,
            ILogger<ProcessIncoming> logger
            )
        {
            //_testService = testService;
            _logger = logger;
            _config = config.Value;
        }

        public void Run()
        {
            _logger.LogInformation($"Start watching the incoming folder for work. datafilesPath = {_config.DatafilesPath}");

            //_testService.Run();

            // For development only. If a work folder for this meeting exists, delete it.
            bool deleteMeetingFolderIfExists = true;

            ProcessFiles processFiles = new ProcessFiles(deleteMeetingFolderIfExists);
            processFiles.WatchIncoming();

            System.Console.ReadKey();
        }
    }
}
