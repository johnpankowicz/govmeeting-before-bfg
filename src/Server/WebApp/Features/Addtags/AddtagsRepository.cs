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
    public class AddtagsRepository : IAddtagsRepository
    {
        string datafiles;
        const string WORK_FOLDER = "T3-ToTag";
        const string WORK_FILE = "ToTag.json";
        const int MAX_BACKUPS = 20;   // maximum backups

        // https://www.mikesdotnetting.com/article/302/server-mappath-equivalent-in-asp-net-core
        //private IHostingEnvironment _env;
        public AddtagsRepository(IOptions<TypedOptions> options)
        {
            datafiles = options.Value.DatafilesPath;
        }

        public Addtags Get(string username, string country, string state, string county, string city, string govEntity, string language, string meetingDate)
        {
            string toTagFolder = country + "_" + state + "_" + city + "_" + county + "_" + govEntity + "_" + language + "\\" + meetingDate + "\\" + WORK_FOLDER;

            // Todo-g - Remove later - For development: If the data is not in Datafiles folder, copy it from testdata.
            UseTestData.CopyIfNeeded(toTagFolder, datafiles);

            string toTagFolderPath = Path.Combine(datafiles, toTagFolder);

            CircularBuffer cb = new CircularBuffer(toTagFolderPath, WORK_FILE, MAX_BACKUPS);

            string latestFixes = cb.GetLatest();
            Addtags addtags = JsonConvert.DeserializeObject<Addtags>(latestFixes);
            return addtags;
        }

        //public void Put(string value)
        public bool Put(Addtags value, string username, string country, string state, string county, string city, string govEntity, string meetingDate, string language)
        {
            string path = country + "_" + state + "_" + city + "_" + county + "_" + govEntity + "_" + language + "\\" + meetingDate + "\\" + WORK_FOLDER;
            string meetingTotagFolder = Path.Combine(datafiles, path);

            string stringValue = JsonConvert.SerializeObject(value, Formatting.Indented);

            CircularBuffer cb = new CircularBuffer(meetingTotagFolder, WORK_FILE, MAX_BACKUPS);
            bool success = cb.WriteLatest(stringValue);
            return success;
        }

        public static string getTagEditsString()
        {
            return @"{ 'data': [
            {speaker: 'Joe', said: 'Waz up', section: 'Invocation', topic: null, showSetTopic: false},
            {speaker: 'Mary', said: 'nutten', section: null, topic: null, showSetTopic: false},
            {speaker: 'Jo', said: 'haiyall', section: null, topic: null, showSetTopic: false},
            {speaker: 'Joe', said: '1111', section: 'Reports', topic: null, showSetTopic: false},
            {speaker: 'Mary', said: '1111111', section: null, topic: 'Topic1', showSetTopic: false},
            {speaker: 'Jo', said: '11111111', section: null, topic: null, showSetTopic: false},
            {speaker: 'Jose', said: '22', section: null, topic: null, showSetTopic: false},
            {speaker: 'Mary', said: '2222', section: null, topic: null, showSetTopic: false},
            {speaker: 'Jo', said: '2222222', section: null, topic: null, showSetTopic: false},
            {speaker: 'Joe', said: '33', section: 'Public Comment', topic: null, showSetTopic: false},
            {speaker: 'Mary', said: '33333', section: null, topic: 'Topic2', showSetTopic: false},
            {speaker: 'Jo', said: '33333333', section: null, topic: null, showSetTopic: false}
            ] }";
        }
    }
}
