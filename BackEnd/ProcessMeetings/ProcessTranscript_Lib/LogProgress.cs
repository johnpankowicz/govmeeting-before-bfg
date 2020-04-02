using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProcessTranscript_Lib
{
    class LogProgress
    {
        string officersNames;
        string meetingInfo;
        //protected string meetingDate;
        string logDirectory;

        int step = 1;

        public LogProgress(string _logDirectory)
        {
            logDirectory = _logDirectory;
        }

        public void SetParts(string info, string names)
        {
            meetingInfo = info;
            officersNames = names;
        }

        public void Log(string fix_step, string transcriptText)
        {
            string outputFile = logDirectory + "\\" + "2-" + step + " " + fix_step + ".txt";
            step++;

            File.WriteAllText(outputFile, meetingInfo + "-----------------------------\n" + officersNames + "-----------------------------\n" + transcriptText);
        }
    }
}
