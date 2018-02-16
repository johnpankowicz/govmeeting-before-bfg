using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
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
