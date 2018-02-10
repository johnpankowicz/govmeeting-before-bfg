using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GM.ProcessIncoming.Shared;
using GM.ProcessRecordingLib;
using GM.Utilities;

namespace GM.ProcessIncoming
{
    class ProcessFiles
    {
        // Todo - This should come from configuration when this code is called from WebApp
        string datafiles = Environment.CurrentDirectory + @"\..\..\Datafiles";

        // Watch the "Datafiles\INPROGRESS" folder for the arrival of new files
        // and process them as they arrive.
        public void WatchIncoming()
        {
            string incoming = datafiles + @"\INPROGRESS";
            if (!Directory.Exists(incoming))
            {
                Directory.CreateDirectory(incoming);
            }

            // Process any existing files in the folder
            foreach (string f in Directory.GetFiles(incoming))
            {
                doWork(f);
            }

            DirectoryWatcher watcher = new DirectoryWatcher();

            // "doWork" will get called when a new file is written to Datafiles\INPROGRESS.
            watcher.watch(incoming, "", doWork);
        }

        public void doWork(string filename)
        {
            MeetingInfo mi = new MeetingInfo(filename);
            if (!mi.valid)
            {
                Console.WriteLine($"ProcessFiles.cs - filename is invalid: {filename}");
                return;
            }
            // Todo--g Remove for production
            // FOR DEVELOPMENT: WE DELETE PRIOR MEETING FOLDER IF IT EXISTS.
            mi.MeetingFolder(datafiles);


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
            string completed = datafiles + @"\COMPLETED";
            if (!Directory.Exists(completed))
            {
                Directory.CreateDirectory(completed);
            }
            string newFile = completed + "\\" + Path.GetFileName(filename);
            File.Move(filename, newFile);
        }

        private void ProcessTranscript(string filename, bool test)
        {
            ProcessTranscripts pt = new ProcessTranscripts(datafiles);
            pt.Process(filename);

        }

        public void ProcessVideo(string filename)
        {
            MeetingInfo mi = new MeetingInfo(filename);
            string language = mi.language;
            string meetingFolder = mi.MeetingFolder(datafiles);
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
