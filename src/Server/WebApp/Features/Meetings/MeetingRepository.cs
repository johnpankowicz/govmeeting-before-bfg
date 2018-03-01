using System;
using System.Collections.Generic;
using GM.WebApp.Services;
using Govmeeting.Backend.Model;
using System.Linq;

namespace GM.WebApp.Features.Meetings
{
    public class MeetingRepository : IMeetingRepository
    {

        public MeetingRepository(ISharedConfig config)
        {
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
