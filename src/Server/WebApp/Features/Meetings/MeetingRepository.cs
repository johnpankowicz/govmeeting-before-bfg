using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using System.IO;
using WebApp.Services;
using Microsoft.Extensions.Options;
using WebApp.Features.Shared;

namespace WebApp.Models
{
    public class MeetingRepository : IMeetingRepository
    {
        static ConcurrentDictionary<string, Meeting> _meetings = new ConcurrentDictionary<string, Meeting>();
        private const string STEP4_BASE_NAME = "T4-tagged";
        private const string EXTENSION = "json";

        public Meeting Get(string country, string state, string county, string city, string govEntity, string language, string meetingDate)
        {
            string datafiles = SharedConfiguration.DatafilesPath;

            string path = country + "_" + state + "_" + county + "_" + city + "_" + govEntity + "_" + language + "\\" + meetingDate;

            // Todo-g - Remove later - For development: If the data is not in Datafiles folder, copy it from testdata.
            UseTestData.CopyIfNeeded(path, datafiles);

            string latestCopy = Path.Combine(datafiles, path, STEP4_BASE_NAME + "." + EXTENSION);

            if (File.Exists(latestCopy))
            {
                return GetByPath(latestCopy);
            }
            else
            {
                return null;
            }
        }

        private Meeting GetByPath(string path)
        {
            string meetingString = Common.Readfile(path);
            if (meetingString != null)
            {
                Meeting meeting = JsonConvert.DeserializeObject<Meeting>(meetingString);
                return meeting;
            } else
            {
                return null;
            }
        }
    }
}
