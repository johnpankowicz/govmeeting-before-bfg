using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using GM.WebApp.Features.Shared;
using GM.WebApp.Services;
using Govmeeting.Backend.Model;

namespace GM.WebApp.Features.Fixasr
{
    public class FixasrRepository : IFixasrRepository
    {
        const string SEGMENT_WORK_FOLDER = "R4-FixText";
        const string WORK_FILE = "ToFix.json";
        const int MAX_BACKUPS = 20;   // maximum backups

        string DatafilesPath;
        string TestdataPath;
        IMeetingWorkFolder meetingWorkFolder;

        public FixasrRepository(ISharedConfig config, IMeetingWorkFolder _meetingWorkFolder)
        {
            DatafilesPath = config.DatafilesPath;
            TestdataPath = config.TestdataPath;
            meetingWorkFolder = _meetingWorkFolder;
        }

        public FixasrView Get(long meetingId, int part)
        {
            string meetingFolder = meetingWorkFolder.GetPath(meetingId);

            string workFolder = meetingFolder + "\\" + SEGMENT_WORK_FOLDER;
            string partFolder = workFolder + $"\\part{part:D2}";


            // Todo-g - Remove later - for development: If the data is not in Datafiles folder, copy it from testdata.
            UseTestData.CopyIfNeeded(workFolder, DatafilesPath, TestdataPath);

            string partFolderPath = Path.Combine(DatafilesPath, partFolder);

            CircularBuffer cb = new CircularBuffer(partFolderPath, WORK_FILE, MAX_BACKUPS);

            string latestFixes = cb.GetLatest();
            FixasrView fixasr = JsonConvert.DeserializeObject<FixasrView>(latestFixes);
            return fixasr;
        }

        public bool Put(FixasrView value, long meetingId, int part)
        {
            string meetingFolder = meetingWorkFolder.GetPath(meetingId);

            //string subpath = country + "_" + state + "_" + county + "_" + city + "_" + govEntity + "\\" + meetingDate;

            string meetingSegmentFolder = System.IO.Path.Combine(DatafilesPath, meetingFolder);

            string stringValue = JsonConvert.SerializeObject(value, Formatting.Indented);

            CircularBuffer cb = new CircularBuffer(meetingSegmentFolder, WORK_FILE, MAX_BACKUPS);
            bool success = cb.WriteLatest(stringValue);
            return success;
        }
    }
}
