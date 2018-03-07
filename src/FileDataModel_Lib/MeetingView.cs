using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GM.FileDataModel
{
    /* The model of the meeting data for the JSON
     * data is different from that of the database
     * model. The types "TopicDiscussion", "Talk" &
     * "Meeting" are also defined in the database model.
     */


    public class MeetingView
    {
        Meeting meeting { get; set; }
        string[] topicNames { get; set; }
        string[] speakerNames { get; set; }
        Topicdiscussion[] topicDiscussions { get; set; }
    }

    class Topicdiscussion
    {
        public string name { get; set; }
        public Talk[] talks { get; set; }
    }

    class Talk
    {
        public string Speaker { get; set; }
        public string Text { get; set; }
    }

    class Meeting
    {
        public long meetingId { get; set; }
        public long locationId { get; set; }
        public string governmentBody { get; set; }
        public string language { get; set; }
        public string date { get; set; }
        public int meetingLength { get; set; }

    }
}
