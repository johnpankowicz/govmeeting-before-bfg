using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Location
    {
        public long locationId { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string county { get; set; }
        public string municipality { get; set; }
    }
}
