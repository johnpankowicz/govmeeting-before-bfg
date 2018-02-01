using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using WebApp.Features.Shared;

namespace WebApp.Models
{
    public class FixasrRepository : IFixasrRepository
    {
        static ConcurrentDictionary<string, Fixasr> _fixasr = new ConcurrentDictionary<string, Fixasr>();
        private TypedOptions _options { get; set; }

        string assets;

        const int MAX_BACKUPS = 20;
        private const string BASE_NAME = "part";        // base name of the file as produced by the Backend processing.
        private const string BASE_NAME_CORRECTED = "part-corrected";    // Base name of the file which has corrections.
        private const string EXTENSION = "json";

        // Whenever the user clicks "SAVE" after making corrections, we save the corrected text to a new file.
        // The original file containing the uncorrected text for each of the parts is named "part.json"
        // When the user clicks "SAVE" for the first time, the changed text is saved to "part-corrected-01-LAST.json".
        //    "01" indicates that this is the first set of corrections.
        //    "LAST" indicates this is most recent set of corrections.
        // When the user makes more corrections and clicks "SAVE" again, two things happen:
        //   The new second set of corrections are saved to "part-corrected-02-LAST.json".
        //   "part-corrected-01-LAST.json" is renamed "part-corrected-01.json"
        // If the user logs out and logs in again later to continue working, they get the file whose name ends with "LAST.json".

        // After "MAX_BACKUPS" files are created (currently 20), the latest file written will wipe out the older file.
        // Thus instead of the 21th file being named ""part-corrected-21-LAST.json", it is named "part-corrected-01-LAST.json".
        // Thus this arrangement acts like a circular buffer, where the newest entry wipes out the oldest one.

        // WE can provide the user an "Undo" function which will go back to a prior set of corrections.

        // It is possible to use a more sophisticated approach, like using mediawiki software to track revisions. But
        // this current approach only uses about 30 lines of code and it may be sufficient for our purposes.

        public FixasrRepository(IOptions<TypedOptions> options)
        //public FixasrRepository()
        {
            _options = options.Value;
            ////    Add(new Fixasr { Name = "Item1" });
        }

        // https://www.mikesdotnetting.com/article/302/server-mappath-equivalent-in-asp-net-core
        //private IHostingEnvironment _env;
        //public FixasrRepository(IHostingEnvironment env)
        //{
        //    _env = env;
        //    //    Add(new Fixasr { Name = "Item1" });
        //}


        /*public IEnumerable<Fixasr> GetAll()
        {
            return _fixasr.Values;
        }*/

        /*public void Add(Fixasr item)
        {
            item.Key = Guid.NewGuid().ToString();
            _fixasr[item.Key] = item;
        }*/

        public Fixasr Find(string key)
        {
            Fixasr item;
            _fixasr.TryGetValue(key, out item);
            return item;
        }

        // We are currently storing the data under the following structure. Directories under Datafiles are
        // named as follows: 
        //    <country>_<state>_<county>_<town-or-city>_<gov-entity>_<language>/<date>/R4-FixText/<part>
        // Example, calling:
        //     Get("johnpank", "USA", "PA", "Philadelphia", "Philadelphia", "CityCouncil", "en", "2016-03-17",2)
        // gets data from:
        //     "Datafiles/USA_PA_Philadelphia_Philadelphia_CityCouncil_en/2016-03-17/R4-FixText"/part02"
        // We will likely change this convention once the number of files grows and we need a deeper folder structure.

        public Fixasr Get(string username, string country, string state, string county, string city, string govEntity, string language, string meetingDate, int part)
        {
            string datafiles = _options.DatafilesPath;

            string baseMeetingFolder = country + "_" + state + "_" + county + "_" + city + "_" + govEntity + "_" + language + "\\" + meetingDate + $"\\R4-FixText\\part{part:D2}";

            // Todo-g - Remove later - For development: If the data is not in Datafiles folder, copy it from testdata.
            //UseTestData.CopyIfNeeded(path, datafiles);
            UseTestData.CopyIfNeeded(baseMeetingFolder, datafiles);
            CopyToAssets(baseMeetingFolder, datafiles);

            string fullpath = Path.Combine(datafiles, baseMeetingFolder);
            string latestCopy = Path.Combine(fullpath, BASE_NAME_CORRECTED + "." + EXTENSION);

            // If we already edited it, return the latest edit.
            if (File.Exists(latestCopy))
            {
                return GetByPath(latestCopy);
            }
            // Otherwise return the unedited one from step 2.
            else
            {
                string filename = Path.Combine(fullpath, BASE_NAME + "." + EXTENSION);
                return GetByPath(filename);
            }
        }

        public Fixasr GetByPath(string path)
        {
            string fixasrString = Common.Readfile(path);
            if (fixasrString != null)
            {
                Fixasr fixasr = JsonConvert.DeserializeObject<Fixasr>(fixasrString);
                return fixasr;
            } else
            {
                return null;
            }
        }

        public string GetStringByPath(string path)
        {
            string fixasrString = Common.Readfile(path);
            if (fixasrString != null)
            {
                return fixasrString;
            }
            else
            {
                return null;
            }
        }

        public void Put(Fixasr value, string username, string country, string state, string county, string city, string govEntity, string language, string meetingDate, int part)
        {
            string subpath = country + "_" + state + "_" + county + "_" + city + "_" + govEntity + "\\" + meetingDate;
            string fullpath = System.IO.Path.Combine(_options.DatafilesPath, subpath);

        }

        //public void PutByPath(string path, string value)
        public void PutByPath(Fixasr value)
        {
            string subpath = "USA_ME_LincolnCounty_BoothbayHarbor_Selectmen/2016-10-11";
            var fullpath = System.IO.Path.Combine(_options.DatafilesPath, subpath);
            WriteLatest(fullpath, value);
        }
        private void WriteLatest(string fullpath, Fixasr value)
        {
            // const string STEP3_BASE_NAME = "x";   // for testing
            const string SUFFIX = "-LAST";
            const string SEPERATOR = " - ";

            string stringValue = JsonConvert.SerializeObject(value, Formatting.Indented);

            string numOfNextLatest = "01";          // Assume the next latest is "01".

            // Find out what the current latest is.
            string latestCopy = getLatestFile(fullpath, BASE_NAME_CORRECTED, EXTENSION);
            if (latestCopy != null)
            {
                numOfNextLatest = getNumberOfNextLatest(latestCopy, SUFFIX, EXTENSION);
                RenameLatestCopy(latestCopy, SUFFIX);
            }

            string nextLatestCopy = fullpath + "/" + BASE_NAME_CORRECTED + SEPERATOR + numOfNextLatest + SUFFIX + "." + EXTENSION;
            File.WriteAllText(nextLatestCopy, stringValue);
        }

        // get filename of latest copy
        private string getLatestFile(string fullpath, string basename, string extension)
        {
            const string SUFFIX = "-LAST";

            string[] files = Directory.GetFiles(fullpath, basename + "*" + SUFFIX + "." + extension);
            if (files.Length == 1)
            {
                return files[0];
            }
            return null;
        }

        private void RenameLatestCopy(string latestCopy, string suffix)
        {
            string newname = latestCopy.Replace(suffix, "");
            File.Move(latestCopy, newname);
        }

        // get number of next latest copy (as string)
        private string getNumberOfNextLatest(string latestCopy, string suffix, string extension)
        {
            int numLast;
            int startOfnumLast = latestCopy.Length - suffix.Length - extension.Length - 3;

            string numpart = latestCopy.Substring(startOfnumLast, 2);
            bool res = int.TryParse(numpart, out numLast);
            if (!res)
            {
                // Todo-g - handle error
                return "01";
            }
            if (++numLast > MAX_BACKUPS)
            {
                numLast = 1;
            }
            // http://timtrott.co.uk/string-formatting-examples/
            return String.Format("{0:d2}", numLast);
        }

        /*public Fixasr Remove(string key)
           {
               Fixasr item;
               _fixasr.TryGetValue(key, out item);
               _fixasr.TryRemove(key, out item);
               return item;
           }*/

        public void Update(Fixasr item)
        {
            //Write out JSON data.
            //string output = JsonConvert.SerializeObject(gto, Formatting.Indented);
            //    _transcriptEdits[item.key] = item;
        }

        public static string getTagEditsString()
        {
            return @"{ 'data': [
                { startTime: '0:00', said: 'the tuesday october $YEAR 11 selectmen'},
                { startTime: '0:02', said: 'meeting i will apologize apologize for'},
                { startTime: '0:06', said: 'my voice i can hardly speak i woke up'},
                { startTime: '0:08', said: 'Saturday with a terrible cold so if you'},
                { startTime: '0:10', said: 'can\'t hear me just speak up and i\'ll try'},
                { startTime: '0:13', said: 'to speak louder and you may want to stay'},
                { startTime: '0:14', said: 'in the back yeah you guys today in the'},
                { startTime: '0:17', said: 'background is yeah I\'m like have our'},
                { startTime: '0:20', said: 'full board with us tonight and we have'},
                { startTime: '0:24', said: 'our recording secretary Kelly the ghost'},
                { startTime: '0:26', said: 'town manager Tom women and selected'},
                { startTime: '0:30', said: 'Trish Warren wendy wolf myself Denise'}
            ] }";
        }

        public void SetAssets(string _assets)
        {
            assets = _assets;
        }

        void CopyToAssets(string baseMeetingFolder, string datafiles)
        {
            // Copy the data also to wwwroot/assets. We need this until we figure out how to return media files to vidogular via the MVC API.
            if (assets != null)
            {
                string meetingFolder = Path.Combine(datafiles, baseMeetingFolder);
                string testFolder = Path.Combine(datafiles, @"..\testdata");
                string testMeetingFolder = Path.Combine(testFolder, baseMeetingFolder);

                string assetsMeetingFolder = assets + "\\" + baseMeetingFolder;
                if (!Directory.Exists(assetsMeetingFolder))
                {
                    Directory.CreateDirectory(assetsMeetingFolder);
                    FileSystem.CopyFilesRecursively(new DirectoryInfo(testMeetingFolder), new DirectoryInfo(assetsMeetingFolder));
                }
            }
        }
    }
}
