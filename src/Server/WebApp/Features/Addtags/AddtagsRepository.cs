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

namespace GM.WebApp.Features.Addtags
{
    public class AddtagsRepository : IAddtagsRepository
    {
        const string WORK_FOLDER = "T3-ToTag";
        const string WORK_FILE = "ToTag.json";
        const int MAX_BACKUPS = 20;   // maximum backups

        string DatafilesPath;
        string TestdatPath;
        IMeetingWorkFolder meetingWorkFolder;

        public AddtagsRepository(ISharedConfig config, IMeetingWorkFolder _meetingWorkFolder)
        {
            DatafilesPath = config.DatafilesPath;
            TestdatPath = config.TestdataPath;
            meetingWorkFolder = _meetingWorkFolder;
        }

        public AddtagsView Get(long meetingId)
        {
            string meetingFolder = meetingWorkFolder.GetPath(meetingId);
            //string toTagFolder = country + "_" + state + "_" + city + "_" + county + "_" + govEntity + "_" + language + "\\" + meetingDate + "\\" + WORK_FOLDER;
            string workFolder = meetingFolder + "\\" + WORK_FOLDER;

            // Todo-g - Remove later - For development: If the data is not in Datafiles folder, copy it from testdata.
            UseTestData.CopyIfNeeded(workFolder, DatafilesPath, TestdatPath);

            string toTagFolderPath = Path.Combine(DatafilesPath, workFolder);

            CircularBuffer cb = new CircularBuffer(toTagFolderPath, WORK_FILE, MAX_BACKUPS);

            string latestFixes = cb.GetLatest();
            AddtagsView addtags = JsonConvert.DeserializeObject<AddtagsView>(latestFixes);
            return addtags;
        }

        //public void Put(string value)
        public bool Put(AddtagsView value, long meetingId)
        {
            string meetingFolder = meetingWorkFolder.GetPath(meetingId);
            string workFolder = meetingFolder + "\\" + WORK_FOLDER;

            //string path = country + "_" + state + "_" + city + "_" + county + "_" + govEntity + "_" + language + "\\" + meetingDate + "\\" + WORK_FOLDER;
            string meetingTotagFolder = Path.Combine(DatafilesPath, workFolder);

            string stringValue = JsonConvert.SerializeObject(value, Formatting.Indented);

            CircularBuffer cb = new CircularBuffer(meetingTotagFolder, WORK_FILE, MAX_BACKUPS);
            bool success = cb.WriteLatest(stringValue);
            return success;
        }
    }
}
