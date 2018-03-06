using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Google.Cloud.Speech.V1;
using Grpc.Auth;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Newtonsoft.Json;
using Google.Protobuf.Collections;

// This is in the process of being written.  

namespace GM.ProcessRecording
{
    public class TranscribeAudio
    {
        public string language;

        public TranscribeAudio(string _language)
        {
            language = _language;
        }

        //public void TranscribeAll(string inputFolder, string outputFolder)
        //{
        //    Directory.CreateDirectory(outputFolder);
        //    foreach (string f in Directory.GetFiles(inputFolder, "*.flac"))
        //    {
        //        string name = Path.GetFileNameWithoutExtension(f);
        //        Console.WriteLine("TranscribeAudio.cs - " + f);

        //        TranscribeToFile(inputFolder + "\\" + name + ".flac", outputFolder + "\\" + name + ".txt", false);
        //    }
        //}

        //public void TranscribeToFile(string inputFile, string outputFile, bool inCloud)
        //{
        //    TranscribeResponse transcript = Transcribe(inputFile, inCloud);

        //    string stringValue = JsonConvert.SerializeObject(transcript, Formatting.Indented);

        //    File.WriteAllText(outputFile, stringValue);
        //}


        /// <param name="language">Language of the audio. This is the ISO 639 code</param>
        /// The audio file name may have been shortened. For the cloud object name, we want to use the original full
        /// name of the video, but change the extension to ".flac".
        public TranscribeResponse MoveToCloudAndTranscribe(string audiofilePath, string videoFileName, string language)
        {
            string objectName = Path.GetFileNameWithoutExtension(videoFileName) + ".flac";
            GoogleBucket gb = new GoogleBucket();
            gb.UploadFile("govmeeting-transcribe", audiofilePath, objectName, "audio/x-flac");
            TranscribeResponse transcript = TranscribeInCloud(objectName, language);
            return transcript;
        }

        public TranscribeResponse TranscribeInCloud(string objectName, string language)
        {
            var speech = SpeechClient.Create();

                string fileOnCloudStorage = "gs://govmeeting-transcribe/" + objectName;
                RecognitionAudio recogAudio = RecognitionAudio.FromStorageUri(fileOnCloudStorage);

                var longOperation = speech.LongRunningRecognize(new RecognitionConfig()
                {
                    Encoding = RecognitionConfig.Types.AudioEncoding.Flac,
                    SampleRateHertz = 48000,
                    EnableWordTimeOffsets = true,
                    LanguageCode = language,
                }, recogAudio);
                longOperation = longOperation.PollUntilCompleted();
                var response = longOperation.Result;

            // Transform the Google response into a more usable object.
            // TranscribeResponse transcript = GetLongTranscribeResponse(response);

            TranscribeResponse transcript = new TranscribeResponse();

            foreach (var result in response.Results)
            {
                foreach (var alternative in result.Alternatives)
                {
                    Console.WriteLine($"Transcript: { alternative.Transcript}");
                    Console.WriteLine("Word details:");
                    Console.WriteLine($" Word count:{alternative.Words.Count}");

                    RspAlternative alt = new RspAlternative(alternative.Transcript)
                    {
                        wordCount = alternative.Words.Count
                    };

                    int count = 1;
                    foreach (var item in alternative.Words)
                    {
                        alt.words.Add(new RspWord(item.Word, ParseDuration(item.StartTime),
                            ParseDuration(item.EndTime), count++));

                        Console.WriteLine($"  {item.Word}");
                        Console.WriteLine($"    WordStartTime: {item.StartTime}");
                        Console.WriteLine($"    WordEndTime: {item.EndTime}");
                    }
                    transcript.alternatives.Add(alt);
                }
            }


            return transcript;
        }


        // Transcribe a local audio file. We can only use this with audios up to 1 minute long.
        public TranscribeResponse TranscribeFile(string fileName, string language)
        {
            var speech = SpeechClient.Create();
            RecognitionAudio recogAudio = RecognitionAudio.FromFile(fileName);

            var response = speech.Recognize(new RecognitionConfig()
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Flac,
                SampleRateHertz = 48000,
                EnableWordTimeOffsets = true,
                LanguageCode = language,
            }, recogAudio);

            // Transform the Google response into a more usable object.
            TranscribeResponse transcript = GetShortTranscribeResponse(response);
            return transcript;
        }

        TranscribeResponse GetLongTranscribeResponse(LongRunningRecognizeResponse response)
        {
            return GetTranscribeResponse(response.Results);
        }

        TranscribeResponse GetShortTranscribeResponse(RecognizeResponse response)
        {
            return GetTranscribeResponse(response.Results);
        }

        public TranscribeResponse GetTranscribeResponse(RepeatedField<SpeechRecognitionResult> results)
        {
            TranscribeResponse transcript = new TranscribeResponse();

            foreach (var result in results)
            {
                foreach (var alternative in result.Alternatives)
                {
                    Console.WriteLine($"Transcript: { alternative.Transcript}");
                    Console.WriteLine("Word details:");
                    Console.WriteLine($" Word count:{alternative.Words.Count}");

                    RspAlternative alt = new RspAlternative(alternative.Transcript)
                    {
                        wordCount = alternative.Words.Count
                    };

                    int count = 1;
                    foreach (var item in alternative.Words)
                    {
                        alt.words.Add(new RspWord(item.Word, ParseDuration(item.StartTime),
                            ParseDuration(item.EndTime), count++));

                        Console.WriteLine($"  {item.Word}");
                        Console.WriteLine($"    WordStartTime: {item.StartTime}");
                        Console.WriteLine($"    WordEndTime: {item.EndTime}");
                    }
                    transcript.alternatives.Add(alt);
                }
            }
            return transcript;
        }

        int ParseDuration(Google.Protobuf.WellKnownTypes.Duration duration)
        {
            String s = duration.ToString();
            s = s.Replace("s", "");
            s = s.Replace("\"", "");
            double d = Double.Parse(s);
            int milliseconds = (int) (d * 1000);
            return milliseconds;
        }

        SpeechClient GetSpeechClient()
        {
            //The following code can be used to avoid getting the location of the credentials file from the environment
            // variable "GOOGLE_APPLICATION_CREDENTIALS" and instead directly access the this file ourselves.

            // Todo-g The following is a hack. We need to get the location of the credentials file from configuration.
            string credentialsFilePath = Environment.CurrentDirectory + @"\..\..\..\..\_SECRETS\TranscribeAudio.json";

            GoogleCredential googleCredential;
            using (Stream m = new FileStream(credentialsFilePath, FileMode.Open))
                googleCredential = GoogleCredential.FromStream(m);
            var channel = new Grpc.Core.Channel(SpeechClient.DefaultEndpoint.Host,
                googleCredential.ToChannelCredentials());
            var speech = SpeechClient.Create(channel);
            return speech;
        }
    }
}


