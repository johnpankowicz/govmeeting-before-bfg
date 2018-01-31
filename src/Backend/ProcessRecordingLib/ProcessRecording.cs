using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GM.ProcessIncoming.Shared;
using Newtonsoft.Json;

namespace GM.ProcessRecordingLib
{
    public class ProcessRecording
    {
        public void Process(string videoFile, string meetingFolder, string language)
        {
            // Copy video to meeting folder

            FileInfo infile = new FileInfo(videoFile);
            string videofileCopy = meetingFolder + "\\" + "R0-Video.mp4";
            File.Copy(videoFile, videofileCopy);

            // Todo-g Remove this for production and uncomment the above "File.Copy" statement.
            // #### FOR DEVELOPMENT: WE SHORTEN THE RECORDING FILE. ####
            //SplitRecording splitRecording = new SplitRecording();
            //splitRecording.ExtractPart(videoFile, videofileCopy, 60, 4 * 60);

            // Extract the audio.

            ExtractAudio extract = new ExtractAudio();
            string audioFile = meetingFolder + "\\" + "R1-Audio.flac";
            extract.Extract(videofileCopy, audioFile);

            // Transcribe the audio file.

            // We want the object name in the cloud to be the original video file name with ".flac" extension.
            string objectName = Path.GetFileNameWithoutExtension(videoFile) + ".flac";
            TranscribeAudio transcribe = new TranscribeAudio(language);

            TranscribeResponse transcript = transcribe.MoveToCloudAndTranscribe(audioFile, objectName, language);
            // For development and it's already in cloud
            //TranscribeResponse transcript = transcribe.TranscribeInCloud(objectName, language);

            string stringValue = JsonConvert.SerializeObject(transcript, Formatting.Indented);
            string outputJsonFile = meetingFolder + "\\" + "R2-Transcribed.json";
            File.WriteAllText(outputJsonFile, stringValue);

            // Reformat the JSON transcript to match what the fixasr routine will use.

            ModifyTranscriptJson convert = new ModifyTranscriptJson();
            outputJsonFile = meetingFolder + "\\" + "R3-ToBeFixed.json";
            Fixasr fixasr = convert.Modify(transcript);
            stringValue = JsonConvert.SerializeObject(fixasr, Formatting.Indented);
            File.WriteAllText(outputJsonFile, stringValue);

            // Split the video, audio and transcript into multiple work segments

            SplitIntoWorkSegments split = new SplitIntoWorkSegments();
            split.Split(meetingFolder, videofileCopy, outputJsonFile);

            // Copy the media files into the server's wwwroot/assets folder.
            CopyMediaToAssets(meetingFolder, videoFile);
        }

        // It will be best if all the datafiles can be kept in src/Datafiles.
        // However, it has not yet been figured out how to serve the video files to the 
        // the videoangular component in the client app via the API.
        // cSo until this is solved, we will copy the media files
        // into src/Server/Webapp/wwwroot/assets.
        void CopyMediaToAssets(string meetingFolder, string videoFile)
        {
            MeetingInfo mi = new MeetingInfo("USA_ME_LincolnCounty_BoothbayHarbor_Selectmen_en_2017-01-09.mp4");
            string source = meetingFolder + "\\R4-FixText";
            string assets = Environment.CurrentDirectory + @"\..\..\Server\Webapp\wwwroot\assets";
            string destination = assets + "\\" + mi.location + "\\" + mi.date + "\\R4-FixText";

            Directories.Copy(source, destination);
        }

    }
}
