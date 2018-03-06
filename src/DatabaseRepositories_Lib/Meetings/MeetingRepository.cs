using System;
using System.Collections.Generic;
using GM.DatabaseModel;
using System.Linq;

namespace GM.DatabaseRepositories
{
    public class MeetingRepository : IMeetingRepository
    {
        IGovBodyRepository _govBodyRepository;

        public MeetingRepository(IGovBodyRepository govBodyRepository)
        {
            _govBodyRepository = govBodyRepository;
        }

        public Meeting Get(long meetingId)
        {
            Meeting meeting = GetTestMeeting(meetingId);
            return meeting;
        }

        // Work folders under Datafiles are named as follows:
        //    <country>_<state>_<county>_<town-or-city>_<gov-entity>_<language>/<date>
        // Example:
        //     "USA", "PA", "Philadelphia", "Philadelphia", "CityCouncil", "en", "2016-03-17"
        // uses this folder:
        //     "Datafiles/USA_PA_Philadelphia_Philadelphia_CityCouncil_en/2016-03-17"
        //

        public string GetMeetingFolder(long meetingId)
        {
            Meeting meeting = Get(meetingId);
            GovernmentBody govBody = _govBodyRepository.Get(meeting.GovernmentBodyId);

            //DateTime dt = DateTime.Parse(meeting.date, null, System.Globalization.DateTimeStyles.RoundtripKind);
            string date = string.Format("{0:yyyy-MM-dd}", meeting.Date);

            string path = govBody.Country + "_" + govBody.State + "_" + govBody.County + "_" + govBody.Municipality +
                "_" + govBody.Name + "_" + govBody.Languages[0].Name + "\\" + date;

            return path;
        }

        //////////////////////////////////////////////////
        // Use list as test/development repository.
        // We will normally be accessing the database.

        private Meeting GetTestMeeting(long meetingId)
        {
            Meeting meeting = testMeetings.Single(m => m.Id == meetingId);
            return meeting;
        }

        private List<Meeting> testMeetings = new List<Meeting>
        {
            new Meeting()           // Boothbay Harbor 9/8/2014
            {
                Id = 1,
                Name = "Monthly Regular",
                Date = new DateTime(2014,9, 8),
                Length = 1810,
                TopicDiscussions = null,
                GovernmentBodyId = 1
           },
            new Meeting()           // Philadelphia 9/25/2014
            {
                Id = 2,
                Name = "Monthly Regular",
                Date = new DateTime(2014,9, 25),
                Length = 3550,
                TopicDiscussions = null,
                GovernmentBodyId = 2
           },
            new Meeting()           // Boothbay Harbor 2/15/2017
            {
                Id = 3,
                Name = "Monthly Regular",
                Date = new DateTime(2017,2, 15),
                Length = 2109,
                TopicDiscussions = null,
                GovernmentBodyId = 1
           },
            new Meeting()           // Philadelphia 3/175/2016
            {
                Id = 4,
                Name = "Monthly Regular",
                Date = new DateTime(2017,2, 15),
                Length = 3466,
                TopicDiscussions = null,
                GovernmentBodyId = 2
            }
        };
    }
}
