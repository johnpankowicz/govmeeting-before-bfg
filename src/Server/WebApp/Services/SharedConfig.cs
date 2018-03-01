using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GM.WebApp.Services
{
    public interface ISharedConfig
    {
        string DatafilesPath { get; }
        string TestdataPath { get; }
    }

    public class SharedConfig : ISharedConfig
    {
        string _DatafilesPath;
        string _TestdataPath;

        public SharedConfig(string datafilesPath, string testdataPath)
        {
            _DatafilesPath = datafilesPath;
            _TestdataPath = testdataPath;
        }

        public string DatafilesPath { get { return _DatafilesPath; } }

        public string TestdataPath { get { return _TestdataPath; } }
    }
}
