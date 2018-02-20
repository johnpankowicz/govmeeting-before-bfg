# Govmeeting

Meetings are the heart and soul of democracy. They are where people  present opposing opinions on important issues and formally make final decisions. 

In the Athenian Assembly of 500 BC, a quorum of 6,000 of the 43,000 citizens was needed to conduct business. In the 10th century, the Icelandic Althing was held in a natural outdoor amphitheater and all citizens could attend.

Today it is rare to see more than a dozen attendees at a council meeting in a town of 30,000 people. Is this still democracy?

Some public meetings are broadcast on local TV.  But a very small percent of people actually watch. Local newspaper report on some issues. But they choose what to report and a small percentage read their local paper.

The few attending a meeting, including some council members, may strongly oppose a decision. But it is too easy for the council majority to totally ignore them. They know that less than one percent of the voters will ever hear what they said.

The purpose of this project is to preserve those discussions and make them easily available to all. It uses a combination of intelligent software and community volunteer effort.

Disillusion with government starts at the local level. Fixing this requires a grassroots effort at that level.

## Functional overview

Shortly after a government meeting takes place, you will be able to:
* Receive a summary of issues discussed via text message or email.
* Optionally receive a full transcript of the meeting via email.

At any time, you can go online and:
* Sign up to receive text alert or emails whenever a specific issue is discussed.
* Use a more powerful tool to browse a specific meeting.
* Search all past meetings for issues discussed.
* See all that was said in the past on any specific issues.
* See what any specific official said on any specific issue in all past meetings. This is especially helpful at election time.

For a detailed functional design, see: [Functional Design](https://github.com/govmeeting/govmeeting/wiki/functional-design)

<img src="images/mr-t-mrt-36834265-320-254-24kb.png" alt="Photo of Mr.T">
<!--This also works: ![Photo of Mr.T](images/mr-t-mrt-36834265-320-254-24kb.png) -->

 “Enough with the jibber-jabber, fool!
 Show me how it works!”

Well, the work is in progress. But here you can find [Demos of working code](http://govmeeting.org).

## Technical overview

* The front-end is written in Typescript and Angular.
* The web server and backend is based on ASP.NET Core and C#.
* SQL database using Entity Framework Core as an ORM.

The "Core"version of ASP.NET Core and Entity Framework is open-source and runs on Windows, Mac and Linux.

For a detailed system design, see: [System Design
](https://github.com/govmeeting/govmeeting/wiki/system-design)

## Quickstart

* Clone the repo: `git clone https://github.com/govmeeting/govmeeting.git`
or download zip file:`https://github.com/govmeeting/govmeeting/archive/master.zip`

#### If using Visual Studio

  * Open solution "govmeeting.sln"

The front-end is the code that you see running at [govmeeting.org](govmeeting.org). To run this:

  * Set startup project in Visual Studio to Server/WebApp. Press F5.

The back-end is the code that processes incoming video recordings and existing transript files.

* To do the auto transcription required on the recording, you will first need to  set up a Google Cloud Platform project. Instructions are here:  [Create GCP project](https://github.com/govmeeting/govmeeting/wiki)

* Set the startup project in Visual Studio to `Backend/ProcessingIncoming`. Press F5.

* Copy (don't move) either the either sample MP4 recording or PDF transcript files from testdata to Datafiles/INPROGRESS.

  The program will recognize that a new file has appeared and start processing it.
  The test file will be moved to "COMPLETED" when done.

  


### If using Visual Studio Code

Install the following VS Code extensions:
* "Debugger for Chrome" by Microsoft
* "C# for Visual Studio Code" by Microsoft

For how to debug both Typescript and C# together in VS Code, see:
  https://github.com/Microsoft/vscode-recipes/tree/master/Angular-SpaTemplates

Open the repository folder in VS Code.

### Process new meeting recordings

The backend can be used to process recordings of your own.

* First name the file in this format:

    [county]_[state]_[county]_[municipality]_[goverment body]_[language]_[date].mp4

    For example: "USA_NJ_Hudson_JerseyCity_CityCouncil_en_2018-02-24"

    "country" is the standard country abbeviation.
    "state" is an abbreviation for the second level sub-division for that country.
    "county" is whatever is the third level sub-division for that country.
    "municipality" is lowest level government division.
    "language" is the ISO 639 language code. This is used during transcription.
    "date" is the date of the meeting in the format localized for that country.

    Some countries may need more levels of sub-divsion. In that case include the 3rd up to the lowest level,
    in the "county" field and put "-" between the included names.

* Then copy the file into the Datafiles/INPROGRESS folder and run the Backend/ProcessIncoming process.




## Contacts
[![Join the chat at https://gitter.im/govmeeting/govmeeting](https://badges.gitter.im/govmeeting/govmeeting.svg)](https://gitter.im/govmeeting/govmeeting?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

<info@govmeeting.org>

