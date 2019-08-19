# Project Notes

## == ProcessTranscript_Lib ==

This project pre-processes transcript files obtained from original sources. 

If a government body does offer transcipts of it's meetings, they could be in many different formats.

* They may come as PDFs, plain text files or even as  scrapes of HTML screens.
* There will be various ways to indicate the start of spoken text and the name of the speaker.
* The file may or may not contain:
	* line numbers
	* page headers or footers
	* indications of the time elapsed from start of meeting
	* a list of the officials present

The code in this project needs to:
	* convert the file to a standard text file
	* remove unneeded text like page footers, headers and line numbers. 
Then it needs to parse the text to find:
	* meeting designations
	* date and time of start of meeting
	* list of officials present (if this is available)
	* start of meeting sections, for example "introductions", "committee reports", etc
	* the start of each spoken text by a new speaker
	* the name of the speaker 

The final output will be a standardized format which the next stage of the software can easily consume.


The current state of the code (as of 2016-03-11) just represents handling a single example: a PDF file from the Philadelphia City Council. As we get more examples, we will need to see how this code can be generalized to find common sub-tasks.

#### Possible options for pre-processing transcripts include:

#### Option 1

The developers of the Govmeeting project can continue to generalize the conversion library as we gain more knowledge of each new transcript that we try to process. However it is unknown whether it would be possible to eventually have the code generalized enough that most new formats can be processed without 
changing the code.

#### Option 2

* Write a public specification for the standardized format we expect.
* Write a plugin capability for adding conversion routines to the system.
* Allow a programmer in the community to write a conversion routine in whatever language he/she would like to use (Ruby, PHP, etc).
* Support the use of these languages on the server

#### Option 3

Write a general conversion program that is based on "conversion definitions" which are input by a user in the community. The conversion definitions might be regular expressions that define changes to the text (deletion, changes, additions, re-format, etc)

## == ProcessRecording_Lib ==

The library is for auto pre-processing recordings of meetings.
This includes code to:
* Extract the audio
* Auto-transcribe the segments of the recording using the Google Speech API.
* Convert the transcript into a JSON object which will be the input for further volunteer processing.
* Split the recording into smaller segments so that multiple parts can be worked on at one time.


https://cloud.google.com/speech/docs/encoding

One of the most popular digital audio techniques (popularized in use of the Compact Disc) is known as Pulse Code Modulation (or PCM). udio is sampled at set intervals, and the amplitude of the sampled wave at that point is stored as a digital value using the sample's bit depth.

Linear PCM (which indicates that the amplitude response is linearly uniform across the sample) is the standard used within CDs, and within the Speech API's LINEAR16 encoding.

Reading a one-channel stream of bytes encoded using Linear PCM, you could count off every 16 bits (2 bytes), for example, to get another amplitude value of the waveform. Almost all devices can manipulate such digital data natively — you can even crop Linear PCM audio files using a text editor — but (obviously) uncompressed audio is not the most efficient way to transport or store digital audio. For that reason, most audio uses digital compressions techniques.

The Speech API supports two lossless encodings: FLAC and LINEAR16. Technically, LINEAR16 isn't "lossless compression" because no compression is involved in the first place. If file size or data transmission is important to you, choose FLAC as your audio encoding choice.

Although the Speech API supports several lossy formats, you should avoid them if you have control over the source audio. Although the removal of such data through lossy compression may not noticably affect audio as heard by the human ear, loss of such data to a speech recognition engine may significantly degrade accuracy.


# Misc Notes

## Automatically Updating NuGet Packages

We can use "dotnet remove <package>" followed by "dotnet add package" to update to the latest version of some package.

## Exclude node_modules folder in Visual Studio
I got the error: "It is an error to use a section registered as allowDefinition='MachineToApplication' beyond application level.
The reason was because of a "web.config" file in the selenium-webdriver npm package.
https://stackoverflow.com/questions/43009009/asp-net-mvc-project-with-a-section-written-in-
I excluded node_modules folder from the Asp.Net copiler by setting it hidden using FileExplorer.
Some of the other suggested options were to:
(1) Install rimraf: npm install rimraf and then add this script to package.json:
'script': {'postinstall': 'rimraf node_modules/**/web.config'}
(2) Call the asp.net compile exe directly which has an option to ignore specified folders. (I lost the link on how to do this.)

