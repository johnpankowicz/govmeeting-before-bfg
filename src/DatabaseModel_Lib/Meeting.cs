using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM.DatabaseModel
{
    public enum WorkStatus {
        Retrieving, Retrieved,
        Preprocessing, Preprocessed,
        Transcribing, Transcribed,
        FixingAsr, FixedAsr,
        Tagging, Tagged,
        Viewing, Viewed};

    /// <summary>
    /// The meeting object is all the data associated with one specific meeting.
    /// </summary>
    public class Meeting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Length { get; set; }
        public List<TopicDiscussion> TopicDiscussions { get; set; }
        public int GovernmentBodyId { get; set; }
        public WorkStatus WorkStatus { get; set; }

        /*
         * If we were to say: public virtual List<Talk> ....
         * then EF would lazy load these as they are accessed.
         */
    }
}
