
The  project "WorkflowApp" in folder "govmeeting/BackEnd/WorkflowApp" does all the auto-processing of recordings and transcripts.
It also co-ordinates manual processing with auto-processing.

When the project runs, it watches for new files to arrive into the folder "Datafiles/TO_PROCESS".

If the new file is a video,
it does speech recognition to produce a transcript. The transcript can be found in the "Datafiles/WORK" folder in a subfolder named after
the government agency whose meeting it is for. This transcript is
now ready to be proofread.

If the new file is text (.pdf or .txt), it processes it and
produces a JSON object that has the contents of the transcript in a structured format.
Separate fields in this object will contain spoken text, the name of the speaker, setion name, etc.

While processing it outputs trace files to the Datafiles/WORK folder. Each trace file contains the complete text of the transcript, after each particular fix is applied.
Therefore if the final fixed transcript contains strange or invalid text, it is easy to trace
back to where those errors were introduced.

## Existing Transcripts

As new government entities are added, this software will need to be tweaked. Pages on which the data is found can greatly vary and will need some custumization. Once enough individual cases are properly handled, the software can be improved to start to handle new variants automatically.
