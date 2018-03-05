using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GM.Shared.Configuration
{
    public class AppSettings
    {
        public string DatafilesPath { get; set; }
        public string TestfilesPath { get; set; }
        public bool IsDevelopment { get; set; }
    }
}
