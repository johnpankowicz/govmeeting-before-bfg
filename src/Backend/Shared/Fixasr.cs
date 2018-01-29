using System;
using System.Collections.Generic;
using System.Text;

namespace GM.ProcessIncoming.Shared
{
    public class Fixasr
    {
        public double lastedit;
        public List<AsrSegment> asrsegments { get; set; }

        public Fixasr()
        {
            asrsegments = new List<AsrSegment>();
        }
    }

    public class AsrSegment
    {
        public string startTime;     // start time
        public string said;        // what they said

        public AsrSegment(string _startTime, string _said)
        {
            startTime = _startTime;
            said = _said;
        }
    }

}
