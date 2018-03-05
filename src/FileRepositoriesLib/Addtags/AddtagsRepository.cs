using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Options;
using GM.DatabaseRepositories;
using GM.FileDataModel;
using GM.Configuration;

namespace GM.FileRepositories
{
    public class AddtagsRepository : IAddtagsRepository
    {
        const string WORK_FOLDER = "Addtags";
        const string WORK_FILE = "ToTag.json";
        const int MAX_BACKUPS = 20;   // maximum backups

        private readonly AppSettings _config;
        IMeetingRepository _meetingRepository;

        public AddtagsRepository(
            IOptions<AppSettings> config,
            IMeetingRepository meetingRepository
            )
        {
            _config = config.Value;
            _meetingRepository = meetingRepository;
    }

        public AddtagsView Get(long meetingId)
        {
            string meetingFolder = _meetingRepository.GetMeetingFolder(meetingId);

            string workFolder = meetingFolder + "\\" + WORK_FOLDER;

            // Todo-g - Remove later - For development: If the data is not in Datafiles folder, copy it from testdata.
            UseTestData.CopyIfNeeded(workFolder, _config.DatafilesPath, _config.TestfilesPath);

            string toTagFolderPath = Path.Combine(_config.DatafilesPath, workFolder);

            CircularBuffer cb = new CircularBuffer(toTagFolderPath, WORK_FILE, MAX_BACKUPS);

            string latestFixes = cb.GetLatest();
            AddtagsView addtags = JsonConvert.DeserializeObject<AddtagsView>(latestFixes);
            return addtags;
        }

        //public void Put(string value)
        public bool Put(AddtagsView value, long meetingId)
        {
            string meetingFolder = _meetingRepository.GetMeetingFolder(meetingId);
            string workFolder = meetingFolder + "\\" + WORK_FOLDER;

            string meetingTotagFolder = Path.Combine(_config.DatafilesPath, workFolder);

            string stringValue = JsonConvert.SerializeObject(value, Formatting.Indented);

            CircularBuffer cb = new CircularBuffer(meetingTotagFolder, WORK_FILE, MAX_BACKUPS);
            bool success = cb.WriteLatest(stringValue);
            return success;
        }
    }
}
