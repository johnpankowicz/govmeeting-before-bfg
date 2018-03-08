using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GM.ProcessRecording;
using GM.ProcessTranscript;
using GM.FileDataModel;
using Microsoft.Extensions.Options;
using GM.Configuration;
using GM.FileDataRepositories;

namespace GM.WorkFlow
{
    public class ProcessIncomingFiles
    {

        /*   ProcessIncomingFiles watches the "INCOMING" folder for new files and processes them.
         *   Currently the file types can be either PDF or MP4.
         *   The names of the files must be in the format: <country>_<state>_<county>_<town-or-city>_<gov-entity>_<language>_<date>.<extension>
         *   For example:  USA_TX_TravisCounty_Austin_CityCouncil_en_2017-12-14.pdf
         * It creates a work folder in the Datafiles folder based on the name of the file.
         *    For example: USA_TX_TravisCounty_Austin_CityCouncil_en/2017-12-14
         * For new MP4 files, it calls: ProcessRecording
         * For new PDF files, it calls: ProcessTranscript
        */

        bool _isDevelopment;
        private string _datafilesPath;
        private string incomingPath;
        private string processedPath;
        MeetingInfo _meetingInfo;
        TranscriptProcess _processTranscript;
        RecordingProcess _processRecording;

        public ProcessIncomingFiles(
            IOptions<AppSettings> config,
            TranscriptProcess processTranscript,
            RecordingProcess processRecording
           )
        {
            // Todo-g We need to get the location of the credentials file path from configuration.
            string credentialsFilePath = Environment.CurrentDirectory + @"\..\..\..\..\_SECRETS\TranscribeAudio.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsFilePath);

            _datafilesPath = config.Value.DatafilesPath;
            _isDevelopment = config.Value.IsDevelopment;

            incomingPath = _datafilesPath + @"\INCOMING";
            Directory.CreateDirectory(incomingPath);

            processedPath = _datafilesPath + @"\PROCESSED";
            Directory.CreateDirectory(processedPath);

            _processTranscript = processTranscript;
            _processRecording = processRecording;
        }

        // Watch the incoming folder and process new files as they arrive.
        public void Run()
        {
            // Process any existing files in the folder
            foreach (string f in Directory.GetFiles(incomingPath))
            {
                doWork(f);
            }

            DirectoryWatcher watcher = new DirectoryWatcher();

            // Call "doWork" for new file.
            watcher.watch(incomingPath, "", doWork);
        }

        public void doWork(string filename)
        {
            _meetingInfo = new MeetingInfo(filename);
            if (!_meetingInfo.valid)
            {
                Console.WriteLine($"ProcessFiles.cs - filename is invalid: {filename}");
                return;
            }
            string meetingFolder = _meetingInfo.MeetingFolderFullPath(_datafilesPath);
            string language = _meetingInfo.language;

            // FOR DEVELOPMENT: WE DELETE PRIOR MEETING FOLDER IF IT EXISTS.
            if (_isDevelopment)
            {
                FileDataRepositories.FileAccess.DeleteDirectoryRecursively(meetingFolder);

            }

            if (!FileDataRepositories.FileAccess.CreateDirectory(meetingFolder))
            {
                // We were not able to create a folder for processing this video.
                // Probably because the folder already exists.
                Console.WriteLine("ProcessFiles.cs - ERROR: could not create meeting folder");
                return;
            }

            FileInfo file = new FileInfo(filename);
            string extension = file.Extension;
            switch (extension)
            {
                case ".pdf":
                    _processTranscript.Process(filename, meetingFolder);
                    break;
                case ".mp4":
                    _processRecording.Process(filename, meetingFolder, language);
                    break;
            }

            // Move the original file to "PROCESSED" folder
            MoveFileToProcessedFolder(filename);
        }

        private void MoveFileToProcessedFolder(string filename)
        {
            string newFile = processedPath + "\\" + Path.GetFileName(filename);
            File.Move(filename, newFile);
        }
    }
}
