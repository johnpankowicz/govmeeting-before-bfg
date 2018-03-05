using System;
using System.Collections.Generic;
using System.Text;
using GM.DataAccess.FileDataModel;

namespace GM.Backend.ProcessRecordingLib
{

    /*
     * Modify the trancription output from Google speech format:

           {
             "alternatives": [
               {
                 "text": "and in when you came in this evening there's a copy of the ambulance ... ",
                 "wordCount": 69,
                 "words": [
                   {
                     "text": "and",
                     "startTime": 0,
                     "endTime": 1000,
                     "position": 1
                   },
                   {
                     "text": "in",
                     "startTime": 1000,
                     "endTime": 1600,
                     "position": 2
                   },

    * To the format expected by the "fixasr" component, where the user fixes errors in the Auto Speech Recognition text:

       {
         "lastedit": 0,
         "asrsegments": [
           {"startTime":"0:00","said":"the tuesday october 11 selectmen's"},
           {"startTime":"0:02","said":"meeting i will apologize apologize for"},
           {"startTime":"0:06","said":"my voice i can hardly speak i woke up"},
           {"startTime":"0:08","said":"Saturday with a terrible cold so if you"},


    * Roughly we are displaying up to 40 characters of text per line. This format will likely change as the fixasr user interface changes. 
    */

    public class ModifyTranscriptJson
    {
        public FixasrView Modify(TranscribeResponse transcript)
        {
            int MaxCharactersPerRecord = 40;
            FixasrView fixasr = new FixasrView();
            string line = "";
            int startTime = 0;

            fixasr.lastedit = 0;

            foreach (RspAlternative alternative in transcript.alternatives)
            {
                foreach (RspWord word in alternative.words)
                {
                    if (line.Length + word.text.Length > MaxCharactersPerRecord)
                    {
                        AsrSegment segment = NewSegment(startTime, line);
                        fixasr.asrsegments.Add(segment);
                        line = "";
                        startTime = word.startTime;
                    }
                    line = line + ((line.Length == 0) ? word.text : " " + word.text);
                }
            }
            if (line != "")
            {
                AsrSegment segment = NewSegment(startTime, line);
                fixasr.asrsegments.Add(segment);

            }
            return fixasr;
        }

        AsrSegment NewSegment(int startTime, string line)
        {
            TimeSpan t = new TimeSpan(0, 0, 0, 0, startTime);
            string format = @"hh\:mm\:ss";
            string start = t.ToString(format);
            AsrSegment segment = new AsrSegment(start, line);
            return segment;
        }
}
}
