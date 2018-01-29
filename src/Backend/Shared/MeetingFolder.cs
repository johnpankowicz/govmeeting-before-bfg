using System;
using System.IO;

namespace GM.ProcessIncoming.Shared
{
    public class MeetingFolder
    {
        string datafiles;
        string filename;
        string meetingFolder;

        public MeetingFolder(string _datafiles, string _filename)
        {
            datafiles = _datafiles;
            filename = _filename;

            // If file is "USA_PA_Philadelphia_Philadelphia_CityCouncil 2016-03-17.pdf"
            //     location = USA_PA_Philadelphia_Philadelphia_CityCouncil
            //     meetingDate = 2016-03-17
            //     meetingFolder = USA_PA_Philadelphia_Philadelphia_CityCouncil/2016-03-17
            string name = Path.GetFileNameWithoutExtension(filename);
            string location = name.Substring(0, name.Length - 11);
            string meetingDate = name.Substring(name.Length - 10, 10);
            meetingFolder = datafiles + "\\" + location + "\\" + meetingDate;
        }

        public string GetName()
        {
            return meetingFolder;
        }

        public bool Create(bool deleteOldFolder = false)
        {
            if (deleteOldFolder)
            {
                Delete();
            }
            if (Directory.Exists(meetingFolder))
            {
                return false;
            }
            else
            {
                Directory.CreateDirectory(meetingFolder);
                return true;
            }
        }

        public bool Delete()
        {
            if (Directory.Exists(meetingFolder))
            {
                DirectoryInfo di = new DirectoryInfo(meetingFolder);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                di.Delete();
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
