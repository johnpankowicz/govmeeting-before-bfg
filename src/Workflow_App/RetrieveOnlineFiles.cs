using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using GM.Configuration;
using GM.FileDataRepositories;
using System.IO;
using GM.ProcessRecording;

namespace GM.WorkFlow
{
    public class RetrieveOnlineFiles
    {
        // Todo - IMPLEMENT THIS CLASS

        AppSettings _config;

        public RetrieveOnlineFiles(
           IOptions<AppSettings> config
        )
        {
            _config = config.Value;
        }
       public void Run()
        {
            // For now, we will just move some test data from the "testdata" folder to simulate retrieving files.
            CopyTestData();
        }

        private void CopyTestData()
        {
            string testfilesPath = _config.TestfilesPath;
            string datafilesPath = _config.DatafilesPath;

            Directory.CreateDirectory(datafilesPath + "\\" + "INCOMING");
            Directory.CreateDirectory(datafilesPath + "\\" + "PROCESSED");

            string[] files = new string[]
            {
                "USA_PA_Philadelphia_Philadelphia_CityCouncil_en_2017-12-07.pdf",
                //"USA_ME_LincolnCounty_BoothbayHarbor_Selectmen_en_2017-02-15.mp4"
                "USA_ME_LincolnCounty_BoothbayHarbor_Selectmen_en_2017-01-09.mp4"
            };

            foreach (string file in files)
            {
                string destination = datafilesPath + "\\" + "INCOMING" + "\\" + file;
                if (!File.Exists(destination))
                {
                    string source = testfilesPath + "\\" + file;

                    // For testing, use the first 9 minutes of the video recordings.
                    if (file.EndsWith(".mp4"))
                    {
                        SplitRecording splitRecording = new SplitRecording();
                        splitRecording.ExtractPart(source, destination, 0, 540);  // 9 * 60 sec.
                    } else
                    {
                        File.Copy(source, destination);
                    }
                }
            }
        }

    }
}
