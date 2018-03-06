using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM.DatabaseModel
{
    /// <summary>
    /// A topic of discussion
    /// </summary>
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TopicDiscussion> TopicDiscussions { get; set; }
        public List<Category> Categories { get; set; }
        public int GovernmentBodyId { get; set; }
    }
}
