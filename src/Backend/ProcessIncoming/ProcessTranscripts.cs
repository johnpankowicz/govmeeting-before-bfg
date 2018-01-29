using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GM.ProcessTranscriptLib;
using GM.SpecificTranscriptFixes;
using GM.Utilities;
using GM.ProcessIncoming.Shared;

namespace GM.ProcessIncoming
{
    class ProcessTranscripts
    {
        string datafiles;

        public ProcessTranscripts(string _datafiles)
        {
            datafiles = _datafiles;
        }

        public bool Process(string filename)
        {
            string meetingFolder;
            bool created;
            MeetingFolder mf = new MeetingFolder(datafiles, filename);
            if (!(created = mf.Create()))
            {
                Console.WriteLine("ERROR: Could not create meeting folder. It may already exist.");
                return false;
            }
            meetingFolder = mf.GetName();

            // Copy PDF to meeting directory
            FileInfo infile = new FileInfo(filename);
            string outfile = meetingFolder + "\\" + "Step 1 - PDF of transcript.pdf";
            File.Copy(filename, outfile);

            // Convert the PDF file to text
            string text = ConvertPdfToText.Convert(outfile);

            // Make the specific fixes to the philly data
            Philadelphia_PA_USA philly = new Philadelphia_PA_USA("2016-03-17", meetingFolder);
            string transcript = philly.Fix(text);

            // Convert the fixed transcript to JSON
            ConvertToJson.Convert(ref transcript);
            string jsonfile = meetingFolder + "\\" + "Step 3 - JSON output.json";
            File.WriteAllText(jsonfile, transcript);

            return true;
        }

    }
}
