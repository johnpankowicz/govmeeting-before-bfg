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
            string videofileCopy = meetingFolder + "\\" + "Step 0 video.mp4";
            //File.Copy(videoFile, videofileCopy);

            // Todo-g Remove this for production and uncomment the above "File.Copy" statement.
            // #### FOR DEVELOPMENT: WE SHORTEN THE RECORDING FILE. ####
            SplitRecording splitRecording = new SplitRecording();
            splitRecording.ExtractPart(videoFile, videofileCopy, 60, 4 * 60);

            // Extract the audio. This will be used to do the trancription.
            ExtractAudio extract = new ExtractAudio();
            string audioFile = meetingFolder + "\\" + "step 1 audio.flac";
            extract.Extract(videofileCopy, audioFile);

            // Transcribe the audio file.
            TranscribeAudio transcribe = new TranscribeAudio(language);
            string originalName = Path.GetFileName(videoFile);
            TranscribeResponse transcript = transcribe.MoveToCloudAndTranscribe(audioFile, originalName, language);
            string stringValue = JsonConvert.SerializeObject(transcript, Formatting.Indented);
            string outputJsonFile = meetingFolder + "\\" + "step 2 transcript.json";
            File.WriteAllText(outputJsonFile, stringValue);

            // Reformat the JSON transcript file to match what the fixasr routine will use.
            ModifyTranscriptJson convert = new ModifyTranscriptJson();
            outputJsonFile = meetingFolder + "\\" + "step 3 fixasr.json";
            Fixasr fixasr = convert.Modify(transcript);
            stringValue = JsonConvert.SerializeObject(fixasr, Formatting.Indented);
            File.WriteAllText(outputJsonFile, stringValue);

            SplitIntoWorkSegments split = new SplitIntoWorkSegments();
            split.Split(meetingFolder, videofileCopy, outputJsonFile);
        }
    }
}
