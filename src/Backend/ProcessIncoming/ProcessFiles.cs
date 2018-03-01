using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GM.Backend.ProcessRecordingLib;
using GM.Backend.ProcessTranscriptLib;
using GM.Shared.Models;
using GM.Shared.Utilities;

namespace GM.Backend.ProcessIncoming
{
    public class ProcessFiles
    {
        private string datafilesPath;
        private string incomingPath;
        private string completedPath;
        bool deleteMeetingFolderIfExists; // For development, we delete the processing results each time.

        public ProcessFiles(bool _deleteMeetingFolderIfExists)
        {
            deleteMeetingFolderIfExists = _deleteMeetingFolderIfExists;

            // Todo-g We need to get the location of the credentials file path from configuration.
            string credentialsFilePath = Environment.CurrentDirectory + @"\..\..\..\..\_SECRETS\TranscribeAudio.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsFilePath);

            // Todo-g - datafiles should come from configuration when this code is called from WebApp
            datafilesPath = Environment.CurrentDirectory + @"\..\..\Datafiles";

            incomingPath = datafilesPath + @"\INCOMING";
            Directory.CreateDirectory(incomingPath);

            completedPath = datafilesPath + @"\COMPLETED";
            Directory.CreateDirectory(completedPath);
        }

        // Watch the incoming folder and process new files as they arrive.
        public void WatchIncoming()
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
            MeetingInfo mi = new MeetingInfo(filename);
            if (!mi.valid)
            {
                Console.WriteLine($"ProcessFiles.cs - filename is invalid: {filename}");
                return;
            }

            // FOR DEVELOPMENT: WE DELETE PRIOR MEETING FOLDER IF IT EXISTS.
            if (deleteMeetingFolderIfExists)
            {
                string meetingFolder = mi.MeetingFolder(datafilesPath);
                Directories.Delete(meetingFolder);

            }

            FileInfo file = new FileInfo(filename);
            string extension = file.Extension;
            switch (extension)
            {
                case ".pdf":
                    ProcessTranscript(filename, false);
                    break;
                case ".mp4":
                    ProcessVideo(filename);
                    break;
            }

            // Move the original file to "COMPLETED" folder
            MoveFileToCompletedFolder(filename);
        }

        private void MoveFileToCompletedFolder(string filename)
        {
            string newFile = completedPath + "\\" + Path.GetFileName(filename);
            File.Move(filename, newFile);
        }

        private void ProcessTranscript(string filename, bool test)
        {
            ProcessTranscripts pt = new ProcessTranscripts(datafilesPath);
            pt.Process(filename);

        }

        public void ProcessVideo(string filename)
        {
            MeetingInfo mi = new MeetingInfo(filename);
            string language = mi.language;
            string meetingFolder = mi.MeetingFolder(datafilesPath);
            if (!Directories.Create(meetingFolder))
            {
                // We were not able to create a folder for processing this video.
                // This can be because the folder already exists. (actually this is the only way, as of now)
                Console.WriteLine("ProcessFiles.cs - ERROR: could not create meeting folder");
                return;
            }
                
            ProcessRecording processRec = new ProcessRecording();
            processRec.Process(filename, meetingFolder, language);
        }
    }
}
