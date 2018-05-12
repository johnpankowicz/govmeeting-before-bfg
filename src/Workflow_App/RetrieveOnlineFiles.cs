using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using GM.Configuration;
using GM.FileDataRepositories;
using System.IO;

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

            string[] files = new string[]
            {
                "USA_PA_Philadelphia_Philadelphia_CityCouncil_en_2017-12-07.pdf",
                "USA_ME_LincolnCounty_BoothbayHarbor_Selectmen_en_2017-02-15.mp4"
            };

            foreach (string file in files)
            {
                string destination = datafilesPath + "\\" + "INCOMING" + "\\" + file;
                if (!File.Exists(destination))
                {
                    string source = testfilesPath + "\\" + file;
                    File.Copy(source, destination);
                }
            }
        }

    }
}
