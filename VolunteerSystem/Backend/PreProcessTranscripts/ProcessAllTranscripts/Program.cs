using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Govmeeting.Volunteer.PreProcessTranscripts
{
    class Program
    {
        static string TestDataFolder = @"..\..\..\testdata\";

        // We will need to research the various formats that each community uses
        // for its transcripts. We will initially create conversion routines for 
        // specific transcript formats. As we gain more knowledge of the required changes,
        // we can start generalizing these routines.

        static void Main(string[] args)
        {
            Philadelphia_PA_USA();
            Austin_TX_USA();
        }

        static void Philadelphia_PA_USA()
        {
            // original sources at: 	http://legislation.phila.gov/council-transcriptroom/

            string pdfFile = TestDataFolder + @"Philadelphia_CityCouncil_09-25-2014.pdf";

            // Save changes at various points so we can see the progress. 

            TranscriptFixes.ConvertPdfToText(pdfFile, "Philadelphia_Step1.txt");

            string text = File.ReadAllText("Philadelphia_Step1.txt");

            // Delete these strings
            string[] ToDelete = {
                "Strehlow & Associates, Inc.",
                "(215) 504-4622" };
            TranscriptFixes.DeleteStrings(ref text, ToDelete);

            // Remove page numbers and line numbers. They are 1 or 2 digits plus space at start of lines.
            TranscriptFixes.RemoveOneAndTwoDigitNumbersAtStartOfLine(ref text);

            // If spaces at start of lines, make them all four.
            TranscriptFixes.MakeAllIndents4Spaces(ref text);

            // Save progress to step2.
            File.WriteAllText("Philadelphia_Step2.txt", text);

            // Get the meeting information
            string MeetingInfo = TranscriptFixes.LinesFromAndUpto(text, "    COUNCIL OF THE CITY OF PHILADELPHIA", "PRESENT:\n");
            TranscriptFixes.RemoveSpacesAtStartOfLine(ref MeetingInfo);

            // Get the officers' names
            string OfficersNames = TranscriptFixes.LinesBetween(text, "PRESENT:\n", "    - - -\n");

            // Get the transcript of what was said
            string transcript = TranscriptFixes.LinesBetween(text, "    - - -\n", "    - - -\n");

            // TODO: put the list of officers in the beginning of file.

        }

        static void Austin_TX_USA()
        {
            string pdfFile = TestDataFolder + @"Austin_TX_CityCouncil_08-07-2014.pdf";

            TranscriptFixes.ConvertPdfToText(pdfFile, "Austin_Step1");

            string text = File.ReadAllText("Austin_Step1");
        }
    }
}
