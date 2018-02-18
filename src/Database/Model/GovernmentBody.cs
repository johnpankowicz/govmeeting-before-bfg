using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Govmeeting.Backend.Model
{
    /// <summary>
    /// The Government body
    /// </summary>
    public class GovernmentBody
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string Municipality { get; set; }
        public List<string> Languages { get; set; }
        public List<Meeting> Meetings { get; set; }
        public List<Topic> Topics { get; set; }
    }
}
