using GM.ProcessIncoming.Shared;
using GM.ProcessRecordingLib;
using Newtonsoft.Json;
using System;
using System.IO;

namespace GM.ProcessIncoming
{

    /*  This program processes raw transcripts and recordings of government meetings.
        Currently it is a separate process from the WebApp. But it will eventually become a child process
        or a library that loaded by WebApp.

        It watches the "INPROCESS" folder for new files and processes them.
        It creates a meeting folder in the Datafiles folder based on the name of the file.

        For transcript files, it produces a file in the subfolder "T3-ToTag" of the meeting folder.
        This file becomes input for the next step (step 4) of the transcript processing, adding tags.

        For recordings, it produces a group of files in the subfolder "R4-FixText" of the meeting folder.
        Each of these files contain a segment of the meeting text which are ready for the next step (step 5) of
        the recording processings, fixing the voice recognition text.
     */
    class Program
    {
        // Todo-g - These should come from configuration
        static string datafilesPath = Environment.CurrentDirectory + @"\..\..\Datafiles";
        static string testdataPath = Environment.CurrentDirectory + @"\..\..\testdata";

        // Todo-g We need to get the location of the credentials file path from configuration.
        public static string credentialsFilePath = Environment.CurrentDirectory + @"\..\..\..\..\_SECRETS\TranscribeAudio.json";

        static void Main(string[] args)
        {
            Directory.CreateDirectory(datafilesPath + "\\INCOMING");
            Directory.CreateDirectory(datafilesPath + "\\INPROGRESS");
            Directory.CreateDirectory(datafilesPath + "\\COMPLETED");

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsFilePath);

            // FOR DEVELOPMENT
            //TestMoveToCloudAndTranscribe(language);
            //return;
            //TestTranscriptionOfFileInCloud(language);
            //return;
            //TestTranscriptionOfLocalFile(language);
            //return;
            //TestVideoProcessing();
            //return;
            //TestReformatOfTranscribeResponse();
            //return;
            //TestSplitIntoWorkSegments();
            //return;
            //CopyMediaToAssets();
            //return;
            //TestSplitTranscript();
            //return;

            ProcessFiles processFiles = new ProcessFiles();
            processFiles.WatchIncoming();
            string s = Console.ReadLine();
        }

        static void CopyMediaToAssets()
        {
            MeetingInfo mi = new MeetingInfo("USA_ME_LincolnCounty_BoothbayHarbor_Selectmen_en_2017-02-15.mp4");
            string meetingFolder = mi.MeetingFolder(datafilesPath);
            string source = meetingFolder + "\\parts";
            string assets = Environment.CurrentDirectory + @"\..\..\Server\Webapp\wwwroot\assets";
            string destination = assets + "\\" + mi.location + "\\" + mi.date + "\\R4-FixText";

            Directories.Copy(source, destination);
        }

        static void TestSplitTranscript()
        {
            string fixasrFile = @"C:\GOVMEETING\_SOURCECODE\src\Datafiles\USA_ME_LincolnCounty_BoothbayHarbor_Selectmen_en\2017-02-15\R3-ToBeFixed.json";
            string stringValue = File.ReadAllText(fixasrFile);
            Fixasr fixasr = JsonConvert.DeserializeObject<Fixasr>(stringValue);
            string outputFolder = @"C:\GOVMEETING\_SOURCECODE\src\Datafiles\USA_ME_LincolnCounty_BoothbayHarbor_Selectmen_en\2017-02-15\R4-FixText";
            int sectionSize = 180;
            int overlap = 5;
            int parts = 4;

            SplitTranscript st = new SplitTranscript();
            st.split(fixasr, outputFolder, sectionSize, overlap, parts);

        }
        static void TestSplitIntoWorkSegments()
        {

            //string videoFile = testdata + @"\TestMoveToCloudAndTranscribe - Copy\USA_ME_LincolnCounty_BoothbayHarbor_Selectmen_EN_2017-02-15.mp4";
            //string transcriptFile = testdata + @"\TestMoveToCloudAndTranscribe - Copy\USA_ME_LincolnCounty_BoothbayHarbor_Selectmen_EN_2017-02-15.json";

            //string outputFolder = testdata + "\\" + "TestSplitIntoWorkSegments";
            //DeleteAndCreateDirectory(outputFolder);

            string outputFolder = @"C:\GOVMEETING\_SOURCECODE\src\Datafiles\USA_ME_LincolnCounty_BoothbayHarbor_Selectmen_EN\2017-01-09";
            string videoFile = outputFolder + "\\" + "R0-Video.mp4";
            string transcriptFile = outputFolder + "\\" + "R3-ToBeFixed.json";

            SplitIntoWorkSegments split = new SplitIntoWorkSegments();
            split.Split(outputFolder, videoFile, transcriptFile);
        }

        static void TestReformatOfTranscribeResponse()
        {
            string inputFile = testdataPath + @"\USA_ME_LincolnCounty_BoothbayHarbor_Selectmen_EN_2017-02-15-rsp.json";

            string outputFolder = testdataPath + "\\" + "TestReformatOfTranscribeResponse";
            DeleteAndCreateDirectory(outputFolder);
            string outputFile = outputFolder + @"\USA_ME_LincolnCounty_BoothbayHarbor_Selectmen_EN_2017-02-15.json";

            string stringValue = File.ReadAllText(inputFile);
            var transcript = JsonConvert.DeserializeObject<TranscribeResponse>(stringValue);

            ModifyTranscriptJson convert = new ModifyTranscriptJson();
            Fixasr fixasr = convert.Modify(transcript);

            stringValue = JsonConvert.SerializeObject(fixasr, Formatting.Indented);
            File.WriteAllText(outputFile, stringValue);
        }

        static void TestMoveToCloudAndTranscribe(string language)
        {
            string baseName = "USA_ME_LincolnCounty_BoothbayHarbor_Selectmen_EN_2017-02-15";
            string videoFile = testdataPath + "\\" + baseName + ".mp4";
            string outputFolder = testdataPath + "\\" + "TestMoveToCloudAndTranscribe";

            DeleteAndCreateDirectory(outputFolder);

            string outputBasePath = outputFolder + "\\" + baseName;
            string shortFile = outputBasePath + ".mp4";
            string audioFile = outputBasePath + ".flac";
            string jsonFile = outputBasePath + ".json";


            // Extract short version
            SplitRecording splitRecording = new SplitRecording();
            splitRecording.ExtractPart(videoFile, shortFile, 60, 4 * 60);

            // Extract audio.
            ExtractAudio extract = new ExtractAudio();
            extract.Extract(shortFile, audioFile);

            // Transcribe
            TranscribeAudio ta = new TranscribeAudio(language);
            TranscribeResponse response = ta.MoveToCloudAndTranscribe(audioFile, baseName + ".flac", language);

            string stringValue = JsonConvert.SerializeObject(response, Formatting.Indented);
            File.WriteAllText(outputBasePath + "-rsp.json", stringValue);

            // Modify Transcript json format
            ModifyTranscriptJson mt = new ModifyTranscriptJson();
            Fixasr fixasr = mt.Modify(response);

            // Create JSON file
            stringValue = JsonConvert.SerializeObject(fixasr, Formatting.Indented);
            File.WriteAllText(jsonFile, stringValue);
        }

        static void TestTranscriptionOfFileInCloud(string language)
        {
            TranscribeAudio ta = new TranscribeAudio(language);

            // Test transcription of a file already in the cloud storage bucket
            TranscribeResponse transcript = ta.TranscribeInCloud("USA_ME_LincolnCounty_BoothbayHarbor_Selectmen_EN_2017-01-09_00-01-40.flac", "en");
            //TranscribeResponse transcript = ta.TranscribeInCloud("Step 0 original#00-06-40.flac", "en");

            string stringValue = JsonConvert.SerializeObject(transcript, Formatting.Indented);
        }

        static void TestTranscriptionOfLocalFile(string language)
        {
            TranscribeAudio ta = new TranscribeAudio(language);

            // Test transcription on a local file. We will use sychronous calls to the Google Speech API. These allow a max of 1 minute per request.
            string folder = datafilesPath + @"..\testdata\BBH Selectmen\USA_ME_LincolnCounty_BoothbayHarbor_Selectmen\2017-01-09\step 2 extract\";
            TranscribeResponse transcript = ta.TranscribeFile(folder + "USA_ME_LincolnCounty_BoothbayHarbor_Selectmen_EN_2017-01-09#00-01-40.flac", language);

            string stringValue = JsonConvert.SerializeObject(transcript, Formatting.Indented);
        }

        static void TestVideoProcessing(string language)
        {
            // Todo - This should come from configuration when this code is called from WebApp
            string testdata = Environment.CurrentDirectory + @"\..\..\testdata\";
            string videoFileName = "USA_ME_LincolnCounty_BoothbayHarbor_Selectmen_EN_2017-02-15.mp4";
            string videoFile = testdata + videoFileName;

            // Delete the prior data if it exists.
            MeetingInfo mi = new MeetingInfo(videoFileName);
            string meetingFolder = mi.MeetingFolder(datafilesPath);
            Directories.Delete(meetingFolder);

            ProcessFiles processFiles = new ProcessFiles();
            processFiles.ProcessVideo(videoFile);
        }

        static void DeleteAndCreateDirectory(string folder)
        {
            if (Directory.Exists(folder))
            {
                Directory.Delete(folder, true);
            }
            Directory.CreateDirectory(folder);
        }


    }
}
