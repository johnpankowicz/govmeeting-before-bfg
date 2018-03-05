﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GM.DataAccess.FileDataModel;

namespace GM.Backend.ProcessRecordingLib
{
    public class SplitIntoWorkSegments
    {
        public void Split(string meetingFolder, string videofile, string fixasrFile)
        {
            int segmentSize = 180; // segment size in seconds to split the recording into.
            int segmentOverlap = 5; // amount in seconds to overlap the sections.

            string splitFolder = meetingFolder + "\\" + "R4-FixText";

            // The processed recording will next go through the following workflow:
            //   1. Users will fix errors in the text generated by auto voice recognition.
            //   2. Users will add metadata tags to the transcript.
            // To facilitate this, we will split the video, audio and transcript files into smaller segments.
            // This has the advantages that:
            //   1. More than one volunteer can work on the recording at the same time.
            //   2. Less video or audio data needs to be downloaded to the user at one time.

            string stringValue = File.ReadAllText(fixasrFile);
            FixasrView fixasr = JsonConvert.DeserializeObject<FixasrView>(stringValue);

            // Split the recording into parts and put them each in subfolders of subfolder "parts".
            SplitRecording splitRecording = new SplitRecording();
            int parts = splitRecording.Split(videofile, splitFolder, segmentSize, segmentOverlap);

            // Also extract the audio from each of these segments.
            // Some user may prefer to work with the audio for fixing the transcript.
            // We will put the audio files in the same folder as the video.
            ExtractAudio extract = new ExtractAudio();
            extract.ExtractAll(splitFolder);

            // Split the full transcript into segments that match the audio and video segments in size.
            SplitTranscript splitTranscript = new SplitTranscript();
            splitTranscript.split(fixasr, splitFolder, segmentSize, segmentOverlap, parts);

        }
    }
}
