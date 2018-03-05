using System;
using System.IO;

namespace GM.DataAccess.FileDataModel
{
    // If file is "USA_PA_Philadelphia_Philadelphia_CityCouncil_EN_2016-03-17.pdf"
    //
    //      country = USA
    //      state = PA
    //      county = Philadelphia
    //      municipality = Philadelphia
    //      governmentBody = CityCouncil
    //      language = EN
    //      date = 2016-03-17
    //      location = USA_PA_Philadelphia_Philadelphia_CityCouncil_EN
    //
    // For the file "USA_PA_Philadelphia_Philadelphia_CityCouncil_EN_2016-03-17.mp4",
    // we will create a folder in Datafiles for processing the file named:
    //      Datafiles/USA_PA_Philadelphia_Philadelphia_CityCouncil_EN/2016-03-17
    //
    // In the beginning, we only want to nest the data a couple of levels deep.
    // But as we add more locations, we will probably want to change this.
    // And ultimately it could become, for example:
    //      Datafiles/USA/PA/Philadelphia/Philadelphia/CityCouncil_EN/2016-03-17

    public class MeetingInfo
    {
        public string datafiles { get; }
        public string filename { get; }
        public string location { get; }
        public string country { get; }
        public string state { get; }
        public string county { get; }
        public string municipality { get; }
        public string governmentBody { get; }
        public string language { get; }
        public string date { get; }
        public bool valid { get; }

        public string MeetingFolderFullPath(string datafiles)
        {
            return datafiles + "\\" + location + "\\" + date;
        }

        public MeetingInfo(string _filename)
        {
            filename = _filename;
            valid = true;

            try
            {
                string name = Path.GetFileNameWithoutExtension(filename);
                string[] parts = name.Split("_");

                country = parts[0];
                state = parts[1];
                county = parts[2];
                municipality = parts[3];
                governmentBody = parts[4];
                language = parts[5];
                date = parts[6];

                location = name.Substring(0, name.Length - 11);
            }
            catch
            {
                valid = false;
            }
        }
    }
}
