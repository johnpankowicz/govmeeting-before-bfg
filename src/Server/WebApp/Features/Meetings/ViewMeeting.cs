using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ViewMeeting
    {
        public Meeting meeting { get; set; }
        public string[] topicNames { get; set; }
        public string[] speakerNames { get; set; }
        public Topicdiscussion[] topicDiscussions { get; set; }
    }

    //public class MeetingInfo
    //{
    //    public string name { get; set; }
    //    public string date { get; set; }
    //    public string xxx { get; set; }
    //}

    public class Topicdiscussion
    {
        public string name { get; set; }
        public Speaker[] speakers { get; set; }
    }

    public class Speaker
    {
        public string name { get; set; }
        public string text { get; set; }
    }


}
