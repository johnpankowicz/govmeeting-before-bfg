using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Features.Shared
{
    public static class UseTestData
    {
        public static void  CopyIfNeeded(string baseMeetingFolder, string datafiles)
        {
            //string baseMeetingFolder = @"USA_PA_Philadelphia_Philadelphia_CityCouncil_en\2016-03-17"
            // If our test data is not already in "Datafiles", copy it from testdata folder.
            string meetingFolder = Path.Combine(datafiles, baseMeetingFolder);
            string testFolder = Path.Combine(datafiles, @"..\testdata");
            string testMeetingFolder = Path.Combine(testFolder, baseMeetingFolder);

            if (!Directory.Exists(meetingFolder))
            {
                Directory.CreateDirectory(meetingFolder);
                FileSystem.CopyFilesRecursively(new DirectoryInfo(testMeetingFolder), new DirectoryInfo(meetingFolder));
            }
        }


    }
}
