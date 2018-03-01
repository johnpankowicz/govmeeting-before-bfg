using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Features.Shared
{
    // This class is for development time and also in production until we have real data to use.
    // If the front end ask for data (for "fixasr" and "addtags" and there is none yet available,
    // this class will copy over some sample data from the testdata folder.

    // It will create the appropriate folders in Datafiles and copy the data.
    // If "copyToAssets" is true, it will also copy the data to wwwroot/assets.
    // This option is there because we haven't yet figured out how to serve video file
    // for videogular using the WebApi. Therefore, for now, they are accessed as
    // static files in wwroot/assets.

    public static class UseTestData
    {
        public static void CopyIfNeeded(string baseMeetingFolder, string datafiles, string testFolder)
        {
            string meetingFolder = Path.Combine(datafiles, baseMeetingFolder);
            string testMeetingFolder = Path.Combine(testFolder, baseMeetingFolder);

            if (!Directory.Exists(meetingFolder))
            {
                Directory.CreateDirectory(meetingFolder);
                FileSystem.CopyFilesRecursively(new DirectoryInfo(testMeetingFolder), new DirectoryInfo(meetingFolder));
            }
        }
    }
}
