using System;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Options;
using GM.Configuration;
using GM.FileDataModel;
using GM.DatabaseRepositories;

namespace GM.FileRepositories
{
    public class FixasrRepository : IFixasrRepository
    {
        const string SEGMENT_WORK_FOLDER = "Fixasr";
        const string WORK_FILE = "ToFix.json";
        const int MAX_BACKUPS = 20;   // maximum backups

        private readonly AppSettings _config;
        IMeetingRepository _meetingRepository;

        public FixasrRepository(
            IOptions<AppSettings> config,
            IMeetingRepository meetingRepository
            )
        {
            _config = config.Value;
            _meetingRepository = meetingRepository;
        }

        public FixasrView Get(long meetingId, int part)
        {
            string meetingFolder = _meetingRepository.GetMeetingFolder(meetingId);

            string workFolder = meetingFolder + "\\" + SEGMENT_WORK_FOLDER;
            string partFolder = workFolder + $"\\part{part:D2}";


            // Todo-g - Remove later - for development: If the data is not in Datafiles folder, copy it from testdata.
            UseTestData.CopyIfNeeded(workFolder, _config.DatafilesPath, _config.TestfilesPath);

            string partFolderPath = Path.Combine(_config.DatafilesPath, partFolder);

            CircularBuffer cb = new CircularBuffer(partFolderPath, WORK_FILE, MAX_BACKUPS);

            string latestFixes = cb.GetLatest();
            FixasrView fixasr = JsonConvert.DeserializeObject<FixasrView>(latestFixes);
            return fixasr;
        }

        public bool Put(FixasrView value, long meetingId, int part)
        {
            string meetingFolder = _meetingRepository.GetMeetingFolder(meetingId);


            string meetingSegmentFolder = System.IO.Path.Combine(_config.DatafilesPath, meetingFolder);

            string stringValue = JsonConvert.SerializeObject(value, Formatting.Indented);

            CircularBuffer cb = new CircularBuffer(meetingSegmentFolder, WORK_FILE, MAX_BACKUPS);
            bool success = cb.WriteLatest(stringValue);
            return success;
        }
    }
}
