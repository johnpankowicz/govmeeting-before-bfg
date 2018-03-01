using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Govmeeting.Backend.Model;

namespace GM.WebApp.Features.Viewmeetings
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


}
