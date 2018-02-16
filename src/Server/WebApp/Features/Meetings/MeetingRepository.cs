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
using System.Globalization;

namespace WebApp.Models
{
    public class MeetingRepository : IMeetingRepository
    {
        private const string STEP4_BASE_NAME = "T4-tagged";
        private const string EXTENSION = "json";

        string DatafilesPath;
        string TestdataPath;

        public MeetingRepository(ISharedConfig config)
        {
            DatafilesPath = config.DatafilesPath;
            TestdataPath = config.TestdataPath;
        }

        public ViewMeeting GetViewmeeting(long meetingId)
        {
            Meeting meeting = GetById(meetingId);
            Location location = GetLocationById(meeting.locationId);
            DateTime dt = DateTime.Parse(meeting.date, null, System.Globalization.DateTimeStyles.RoundtripKind);
            string date = string.Format("{0:yyyy-MM-dd}", dt);
            //string date = dt.ToString("U", CultureInfo.CreateSpecificCulture("en-US")));
            //string date = string.Format("{0:yyyy-MM-dd_HH-mm-ss_ffff}", DateTime.Now);

            //string date = meeting.date;

            string path = location.country + "_" + location.state + "_" + location.county + "_" + location.municipality + 
                "_" + meeting.governmentBody + "_" + meeting.language + "\\" + date;

            // Todo-g - Remove later - For development: If the data is not in Datafiles folder, copy it from testdata.
            UseTestData.CopyIfNeeded(path, DatafilesPath, TestdataPath);

            string latestCopy = Path.Combine(DatafilesPath, path, STEP4_BASE_NAME + "." + EXTENSION);

            if (File.Exists(latestCopy))
            {
                return GetViewMeetingByPath(latestCopy);
            }
            else
            {
                return null;
            }
        }

        public Meeting GetById(long meetingId)
        {
            return testMeetings[(int) meetingId - 1];
        }

        //public ViewMeeting GetViewmeeting(string country, string state, string county, string city, string govEntity, string language, string meetingDate)
        //{
        //    string path = country + "_" + state + "_" + county + "_" + city + "_" + govEntity + "_" + language + "\\" + meetingDate;

        //    // Todo-g - Remove later - For development: If the data is not in Datafiles folder, copy it from testdata.
        //    UseTestData.CopyIfNeeded(path, DatafilesPath, TestdataPath);

        //    string latestCopy = Path.Combine(DatafilesPath, path, STEP4_BASE_NAME + "." + EXTENSION);

        //    if (File.Exists(latestCopy))
        //    {
        //        return GetViewMeetingByPath(latestCopy);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        private ViewMeeting GetViewMeetingByPath(string path)
        {
            string meetingString = Common.Readfile(path);
            if (meetingString != null)
            {
                ViewMeeting meeting = JsonConvert.DeserializeObject<ViewMeeting>(meetingString);
                return meeting;
            } else
            {
                return null;
            }
        }

        private Location GetLocationById(long locationId)
        {
            return testLocations[(int) locationId - 1];
        }

        private List<Meeting> testMeetings = new List<Meeting>
        {
            new Meeting()
            {
                meetingId = 1,
                locationId = 1,
                governmentBody = "Selectmen",
                language = "en",
                date = "2014-09-08T18:10:22.112Z",
                meetingLength = 1810
            },
            new Meeting()
            {
                meetingId = 2,
                locationId = 2,
                governmentBody = "City Council",
                language = "en",
                date = "2014-09-25T18:25:43.511Z",
                meetingLength = 3550
            }
        };

        private List<Location> testLocations = new List<Location>
        {
            new Location()
            {
                locationId = 1,
                country = "USA",
                state = "ME",
                county = "LincolnCounty",
                municipality = "BoothbayHarbor"
            },
            new Location()
            {
                locationId = 1,
                country = "USA",
                state = "PA",
                county = "Philadelphia",
                municipality = "Philadelphia"
            }
        };

    }
}
