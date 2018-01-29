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

        string language;

        public ProcessFiles(string _language)
        {
            language = _language;
        }

        // Watch the "Datafiles\INCOMING" folder for the arrival of new files
        // and process them as they arrive.
        public void WatchIncoming()
        {
            string incoming = datafiles + @"\INCOMING";
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

            // "doWork" will get called when a new file is written to Datafiles\INCOMING.
            watcher.watch(incoming, "", doWork);
        }

        public void doWork(string filename)
        {
            // Todo--g Remove for production
            // FOR DEVELOPMENT: DELETE PRIOR MEETING FOLDER IF IT EXISTS.
            MeetingFolder mf = new MeetingFolder(datafiles, filename);
            mf.Delete();


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

            // Move the original files to "COMPLETED" folder
            string completed = datafiles + @"\COMPLETED";
            if (!Directory.Exists(completed))
            {
                Directory.CreateDirectory(completed);
            }
            File.Move(filename, completed);
        }

        private void ProcessTranscript(string filename, bool test)
        {
            ProcessTranscripts pt = new ProcessTranscripts(datafiles);
            pt.Process(filename);

        }

        public void ProcessVideo(string filename)
        {
            string meetingFolder;
            MeetingFolder mf = new MeetingFolder(datafiles, filename);
            meetingFolder = mf.GetName();
            if (!mf.Create())
            {
                // We were not able to create a folder for processing this video.
                // This can be because the folder already exists. (actually this is the only way, as of now)
                Console.WriteLine("ERROR: could not create meeting folder");
                return;
            }
                
            ProcessRecording processRec = new ProcessRecording();
            processRec.Process(filename, meetingFolder, language);
        }
    }
}
