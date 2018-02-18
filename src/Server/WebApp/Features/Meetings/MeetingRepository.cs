using System;
using System.Collections.Generic;
using WebApp.Services;
using Govmeeting.Backend.Model;
using System.Linq;

namespace WebApp.Models
{
    public class MeetingRepository : IMeetingRepository
    {
        string DatafilesPath;

        public MeetingRepository(ISharedConfig config)
        {
            DatafilesPath = config.DatafilesPath;
        }

        public Meeting Get(long meetingId)
        {
            Meeting meeting = GetTestMeeting(meetingId);
            return meeting;
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
            new Meeting()
            {
                Id = 1,
                Name = "Monthly Regular",
                Date = new DateTime(2014,9, 8),
                Length = 1810,
                TopicDiscussions = null,
                GovernmentBodyId = 1
                //Date = "2014-09-08T18:10:22.112Z",
           },
            new Meeting()
            {
                Id = 2,
                Name = "Monthly Regular",
                Date = new DateTime(2014,9, 25),
                Length = 3550,
                TopicDiscussions = null,
                GovernmentBodyId = 2
                //governmentBody = "City Council",
                //language = "en",
                //date = "2014-09-25T18:25:43.511Z",
            }
        };
    }
}
