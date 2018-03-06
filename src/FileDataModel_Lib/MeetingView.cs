using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GM.FileDataModel
{
    public class MeetingView
    {
        public Meeting meeting { get; set; }
        public string[] topicNames { get; set; }
        public string[] speakerNames { get; set; }
        public Topicdiscussion[] topicDiscussions { get; set; }
    }

    public class Topicdiscussion
    {
        public string name { get; set; }
        public Talk[] talks { get; set; }
    }

    public class Talk
    {
        public string Speaker { get; set; }
        public string Text { get; set; }
    }

    public class Meeting
    {
        public long meetingId { get; set; }
        public long locationId { get; set; }
        public string governmentBody { get; set; }
        public string language { get; set; }
        public string date { get; set; }
        public int meetingLength { get; set; }

    }
}
