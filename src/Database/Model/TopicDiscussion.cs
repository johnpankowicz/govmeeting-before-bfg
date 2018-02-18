using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Govmeeting.Backend.Model
{
    public class TopicDiscussion
    {
        public int Id { get; set; }
        public Topic Topic { get; set; }
        public List<Talk> Talks { get; set; }
        public int Sequence { get; set; }
        public int MeetingId { get; set; } 
    }
}
