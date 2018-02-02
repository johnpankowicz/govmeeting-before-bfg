using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using WebApp.Features.Shared;

namespace WebApp.Models
{
    public class FixasrRepository : IFixasrRepository
    {
        const string WORK_FOLDER = "R4-FixText";
        const string WORK_FILE = "ToFix.json";
        const int MAX_BACKUPS = 20;   // maximum backups

        string datafiles;

        public FixasrRepository(IOptions<TypedOptions> options)
        //public FixasrRepository()
        {
            datafiles = options.Value.DatafilesPath;
        }

        // Directories under Datafiles are named as follows:
        //    <country>_<state>_<county>_<town-or-city>_<gov-entity>_<language>/<date>/R4-FixText/<part>
        // Example, calling:
        //     Get("johnpank", "USA", "PA", "Philadelphia", "Philadelphia", "CityCouncil", "en", "2016-03-17",2)
        // using this folder:
        //     "Datafiles/USA_PA_Philadelphia_Philadelphia_CityCouncil_en/2016-03-17/R4-FixText"/part02"
        // We will likely change this convention once the number of files grows and we need a deeper folder structure.

        public Fixasr Get(string username, string country, string state, string county, string city, string govEntity, string language, string meetingDate, int part)
        {
            string meetingFolder = country + "_" + state + "_" + county + "_" + city + "_" + govEntity + "_" + language + "\\" + meetingDate;
            string workFolder = meetingFolder + "\\" + WORK_FOLDER;
            string partFolder = workFolder + $"\\part{part:D2}";


            // Todo-g - Remove later - for development: If the data is not in Datafiles folder, copy it from testdata.
            UseTestData.CopyIfNeeded(workFolder, datafiles);

            string partFolderPathr = Path.Combine(datafiles, partFolder);

            CircularBuffer cb = new CircularBuffer(partFolderPathr, WORK_FILE, MAX_BACKUPS);

            string latestFixes = cb.GetLatest();
            Fixasr fixasr = JsonConvert.DeserializeObject<Fixasr>(latestFixes);
            return fixasr;
        }

        public bool Put(Fixasr value, string username, string country, string state, string county, string city, string govEntity, string language, string meetingDate, int part)
        {
            string subpath = country + "_" + state + "_" + county + "_" + city + "_" + govEntity + "\\" + meetingDate;
            string meetingSegmentFolder = System.IO.Path.Combine(datafiles, subpath);

            string stringValue = JsonConvert.SerializeObject(value, Formatting.Indented);

            CircularBuffer cb = new CircularBuffer(meetingSegmentFolder, WORK_FILE, MAX_BACKUPS);
            bool success = cb.WriteLatest(stringValue);
            return success;
        }

        // Sample test data
        public static string getTagEditsString()
        {
            return @"{ 'data': [
                { startTime: '0:00', said: 'the tuesday october 11 selectmen'},
                { startTime: '0:02', said: 'meeting i will apologize apologize for'},
                { startTime: '0:06', said: 'my voice i can hardly speak i woke up'},
                { startTime: '0:08', said: 'Saturday with a terrible cold so if you'},
                { startTime: '0:10', said: 'can\'t hear me just speak up and i\'ll try'},
                { startTime: '0:13', said: 'to speak louder and you may want to stay'},
                { startTime: '0:14', said: 'in the back yeah you guys today in the'},
                { startTime: '0:17', said: 'background is yeah I\'m like have our'},
                { startTime: '0:20', said: 'full board with us tonight and we have'},
                { startTime: '0:24', said: 'our recording secretary Kelly the ghost'},
                { startTime: '0:26', said: 'town manager Tom women and selected'},
                { startTime: '0:30', said: 'Trish Warren wendy wolf myself Denise'}
            ] }";
        }
    }
}
