using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;

namespace Govmeeting.Volunteer.PreProcessTranscripts
{
    // TODO: All methods in TranscriptFixes are in need of unit tests.

    public static class TranscriptFixes
    {
        /*
         * Convert a PDF file to a text file by extracting just the text.
        */
        public static void ConvertPdfToText(string infile, string outfile)
        {
            StringBuilder strPdfContent = new StringBuilder();

            PdfReader reader = new PdfReader(infile);

            /*
             * This conversion code is thanks to the developers of iTextSharp and asturcon at
             * http://www.codeproject.com/Questions/770857/Convert-PDF-tp-text-formatted-using-iTextSharp-csh 
             * Before this was used, manual conversion was done with Adobe Acrobat or Microsoft Word.
             * They both convert very badly - missing spaces, linefeeds, reversed lines, etc. Their problems appear
             * to be related to how they handle default character encoding on Windows. For an explanation, see:
             * https://www.informit.com/guides/content.aspx?g=dotnet&seqNum=163
             */
            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                ITextExtractionStrategy objExtractStrategy = new SimpleTextExtractionStrategy();
                string strLineText = PdfTextExtractor.GetTextFromPage(reader, i, objExtractStrategy);
                strLineText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(strLineText)));

                strPdfContent.Append(strLineText);
                strPdfContent.Append("\n");
            }
            reader.Close();
            string text = strPdfContent.ToString();
            File.WriteAllText(outfile, text);
        }

        public static string LinesBetween(string text, string before, string after)
        {
            int i = text.IndexOf(before);
            int start = i + before.Length;
            int end = text.IndexOf(after);
            int len = end - start;
            return text.Substring(start, len);
        }

        public static string LinesFromAndUpto(string text, string from, string upto)
        {
            int start = text.IndexOf(from);
            int end = text.IndexOf(upto);
            int len = end - start;
            return text.Substring(start, len);
        }

        public static void DeleteStrings(ref string text, string[] ToDelete)
        {
            StringBuilder textBuilder = new StringBuilder(text);
            foreach (string s in ToDelete)
            {
                textBuilder.Replace(s, "");
            }
            text = textBuilder.ToString();
        }

        public static void MakeAllIndents4Spaces(ref string text)
        {
            string pattern = "^ +";
            string replacement = "    ";
            text = Regex.Replace(text, pattern, replacement, RegexOptions.Multiline);

        }

        public static void RemoveOneAndTwoDigitNumbersAtStartOfLine(ref string text)
        {
            string pattern = "^[0-9][0-9]?[ ]?";
            string replacement = "";
            text = Regex.Replace(text, pattern, replacement, RegexOptions.Multiline);
        }

        public static void RemoveSpacesAtStartOfLine(ref string text)
        {
            string pattern = "^[ ]+";
            string replacement = "";
            Regex rgx = new Regex(pattern);
            text = Regex.Replace(text, pattern, replacement, RegexOptions.Multiline);
        }
 

        /*
         * For debugging transcript formats - outputs the file in hex.
         */
        static void DisplayFileInHex(string filename)
        {
            using (TextReader reader = File.OpenText(filename))
            {
                while (reader.Peek() > -1)
                {
                    string line = reader.ReadLine();
                    Console.WriteLine(line);
                    char[] chars = line.ToCharArray();
                    foreach (char c in chars)
                    {
                        int value = Convert.ToInt32(c);
                        string hex = String.Format("{0:X}", value);
                        Console.Write("{0} ", hex);
                    }
                    Console.WriteLine();
                }
            }
        }

    }
}
