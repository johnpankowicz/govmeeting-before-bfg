using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM.DataAccess.Model
{
    /// <summary>
    /// A talk represents the text that was said and the speaker who said it.
    /// </summary>
    public class Talk
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Sequence { get; set; }
        public Speaker Speaker { get; set; }
        public int SpeakerId { get; set; }
        public int TopicDiscussionId { get; set; }
    }
}
