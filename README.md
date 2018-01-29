# Govmeeting

Meetings are the heart and soul of democracy. They are where people can present opposing opinions on important issues and formally vote on how to resolve these issues. 

In the Athenian Assembly of 500 BC, a quorum of 6,000 of the 43,000 citizens was needed to conduct business. In the 10th century, the Icelandic Althing was held in a natural outdoor amphitheater and all citizens could attend.

Today it is rare to see more than a dozen attendees at a council meeting in a town of 30,000 people. Is this still democracy?

Some public meetings are broadcast on local TV.  But a very small percent of people actually watch. Sometimes very contentious issues are debated and voted on. But there is usually no public record kept of what was said.

Sometimes many people at the meeting, including some council members, may strongly oppose some decision. But it is too easy today for the majority of the council to totally ignore them. They know that less than one percent of the voters will ever hear what they said.

The purpose of this project is to preserve those discussions and make them easily available to all citizens of the community. It will do this through a combination of intelligent software and community volunteer effort.

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

For a demo of some working code, see:  [govmeeting.org](http://govmeeting.org)

## Technical overview

* The front-end is written in Typescript and Angular.
* The web server and backend is based on ASP.NET Core and C#.
* SQL database using Entity Framework Core as an ORM.

The "Core"version of ASP.NET Core and Entity Framework is open-source and runs on Windows, Mac and Linux.

For a detailed system design, see: [System Design
](https://github.com/govmeeting/govmeeting/wiki/system-design)

## Quickstart

### Visual Studio
  * Opening the "govmeeting.sln" will load all the projects
  * Opening src\Server\WebApp\WebApp.sln will load just the Angular - Asp.Net Core app.

### Visual Studio Code

Install the following extensions:
* "Debugger for Chrome" by Microsoft
* "C# for Visual Studio Code" by Microsoft

## Contacts
[![Join the chat at https://gitter.im/govmeeting/govmeeting](https://badges.gitter.im/govmeeting/govmeeting.svg)](https://gitter.im/govmeeting/govmeeting?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

<info@govmeeting.org>

