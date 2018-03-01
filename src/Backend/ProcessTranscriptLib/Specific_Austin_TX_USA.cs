using System.IO;

namespace GM.Backend.ProcessTranscriptLib
{
    class Specific_Austin_TX_USA
    {
        TranscriptFixes tf = new TranscriptFixes();
        string basefilename;
        string filename;
        string officersNames = "";
        string meetingInfo = "";
        string transcript = "";

        int step = 1;

        public string Fix(string _transcript, string _filename)
        {
            transcript = _transcript;
            filename = _filename;
            basefilename = filename.Substring(filename.LastIndexOf("\\") + 1);

            LOGPROGRESS("Start");

            return transcript;
        }
        void LOGPROGRESS(string fix_step)
        {
            string outputFile = filename + "_step" + step + "_" + fix_step + ".txt";
            step++;

            File.WriteAllText(outputFile, meetingInfo + "-----------------------------\n" + officersNames + "-----------------------------\n" + transcript);
        }


    }
}
