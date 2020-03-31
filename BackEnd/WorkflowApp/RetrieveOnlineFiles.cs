using System;
using System.IO;
using Microsoft.Extensions.Options;
using GM.Configuration;
using GM.FileDataRepositories;
using GM.DatabaseRepositories;
using GM.DatabaseModel;

namespace GM.Workflow
{
    public class RetrieveOnlineFiles
    {
        // TODO - IMPLEMENT THIS CLASS
        
        /* RetrieveOnlineFiles will:
         * Read the meeting schedules for all government bodies in the database.
         * 1. If a current meeting may have taken place, it will:
         *    Check the website where either its transcript or a recording may be found.
         *    If found it will:
         * 2. Create a "meeting" record in the database for this meeting and set the WorkStatus field to "Retrieving".
         * 3. Start the file retrieval.
         * 4. Store the retieved file in the "Datafiles/RECEIVED" folder.
         * 5. Set the Workstatus on the meeting record to "Retrieved".
         * Repeat for each government body.
         *
         *  Files can also be placed in the RECEIVED folder by the phone app for recording a meeting.
         *  Files that are uploaded by a registered user are also placed in the RECEIVED folder.
         */

        AppSettings _config;
        MeetingFolder _meetingFolder;
        IGovBodyRepository _govBodyRepository;
        IMeetingRepository _meetingRepository;

        public RetrieveOnlineFiles(
            IOptions<AppSettings> config,
            MeetingFolder meetingFolder,
            IGovBodyRepository govBodyRepository,
             MeetingRepository meetingRepository
           )
        {
            _config = config.Value;
            _meetingFolder = meetingFolder;
            _govBodyRepository = govBodyRepository;
            _meetingRepository = meetingRepository;
        }
        public void Run()
        {
            string incomingPath = _config.DatafilesPath + @"\RECEIVED";
            Directory.CreateDirectory(incomingPath);
            
            // Process any existing files in the folder
            foreach (string f in Directory.GetFiles(incomingPath))
            {
                CheckFile(f);
            }

            RetrieveNewFiles();

            DirectoryWatcher watcher = new DirectoryWatcher();
            
            // Call "doWork" for new file.
            // TODO - uncomment next line
            //watcher.watch(incomingPath, "", CheckFile);
        }

        public void CheckFile(string filename)
        {
            // TODO - check if it's already been approved. If not, send manager(s) a message
            throw new NotImplementedException();
        }

        public void RetrieveNewFiles()
        {
            // TODO - read schedules in database and check for new files to retrieve.
            throw new NotImplementedException();
        }

        public bool CreateMeetingRecordFromFilename(string filename)
        {
            //string path = _config.DatafilesPath + @"\RECEIVED\" + meeting.SourceFilename;
            // Create a MeetingInfo instance. This takes the info in the filename string, for example,
            // "USA_TX_TravisCounty_Austin_CityCouncil_en/2017-12-14" and puts it into a strongly typed object.
            if (!_meetingFolder.SetFields(filename))
            {
                // If this is not a valid name, skip it.
                Console.WriteLine($"ProcessIncomingFiles.cs - filename is invalid: {filename}");
                return false;
            }

            // Check if there is a database record for this government body.
            long govBodyId = _govBodyRepository.GetId(
                _meetingFolder.country,
                _meetingFolder.state,
                _meetingFolder.county,
                _meetingFolder.municipality);

            // Check if there is database record for this meeting.
            long Id = _meetingFolder.GetId();
            Meeting meeting = _meetingRepository.Get(govBodyId, DateTime.Parse(_meetingFolder.date));

            return true;
        }
    }
}
