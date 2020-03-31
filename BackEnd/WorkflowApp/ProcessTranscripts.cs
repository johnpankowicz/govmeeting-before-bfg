using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GM.ProcessRecording;
using GM.ProcessTranscript;
using GM.ViewModels;
using Microsoft.Extensions.Options;
using GM.Configuration;
using GM.FileDataRepositories;
using GM.DatabaseRepositories;
using GM.DatabaseModel;

namespace GM.Workflow
{
    public class ProcessTranscripts
    {

        /*   ProcessIncomingFiles watches the "RECEIVED" folder for files to be processed.
         *   Currently the file types can be either PDF or MP4.
         *   The names of the files must be in the format: <country>_<state>_<county>_<town-or-city>_<gov-entity>_<language>_<date>.<extension>
         *   For example:  USA_TX_TravisCounty_Austin_CityCouncil_en_2017-12-14.pdf
         * It creates a work folder in the Datafiles folder based on the name of the file.
         *    For example: USA_TX_TravisCounty_Austin_CityCouncil_en/2017-12-14
         * For new MP4 files, it calls: ProcessRecording
         * For new PDF files, it calls: ProcessTranscript
        */

        AppSettings _config;
        MeetingFolder _meetingFolder;
        TranscriptProcess _transcriptProcess;
        IMeetingRepository _meetingRepository;
        IGovBodyRepository _govBodyRepository;

        public ProcessTranscripts(
            IOptions<AppSettings> config,
            TranscriptProcess transcriptProcess,
            MeetingFolder meetingFolder,
            IMeetingRepository meetingRepository,
            IGovBodyRepository govBodyRepository
           )
        {
            _config = config.Value;
            _meetingFolder = meetingFolder;
            _transcriptProcess = transcriptProcess;
            _meetingRepository = meetingRepository;
            _govBodyRepository = govBodyRepository;
        }

        // Watch the incoming folder and process new files as they arrive.
        public void Run()
        {

            List<Meeting> meetings = _meetingRepository.FindAll(SourceType.Transcript, WorkStatus.Received, true);

            foreach (Meeting meeting in meetings)
            {
                    doWork(meeting);
            }

        }

        public void doWork(Meeting meeting)
        {
            string workFolderPath = _config.DatafilesPath + "\\PROCESSING\\" + _meetingFolder.path;
            string language = _meetingFolder.language;

            // FOR DEVELOPMENT: WE DELETE PRIOR MEETING FOLDER IF IT EXISTS.
            //if (_config.IsDevelopment)
            //{
            //    FileDataRepositories.GMFileAccess.DeleteDirectoryAndContents(meetingFolder);

            //}

            if (!FileDataRepositories.GMFileAccess.CreateDirectory(workFolderPath))
            {
                // We were not able to create a folder for processing this video.
                // Probably because the folder already exists.
                Console.WriteLine("ProcessTranscriptsFiles.cs - ERROR: could not create work folder");
                return;
            }

            _transcriptProcess.Process(meeting.SourceFilename, workFolderPath, language);
        }
        //private void MoveFileToProcessedFolder(string filename)
        //{
        //    string processedPath = _config.DatafilesPath + @"\COMPLETED";
        //    string newFile = processedPath + "\\" + Path.GetFileName(filename);
        //    File.Move(filename, newFile);
        //}
    }
}
