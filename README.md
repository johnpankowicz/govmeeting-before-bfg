# govmeeting
Software to help us be involved with our local governments

## Features

Shortly after a government meeting takes place, you will be able to:
* Go online and see what issues were discussed and exactly what was said and by whom.
* Receive, on your phone or via email, a summary of issues discussed. 
* Receive a phone alert or email whenever a specific issue is discussed. You would sign up prior for alerts.

At any time, you can go online and see:
* See all issues discussed in past meetings.
* See all that was said in the past on any specific issue.
* See exactly what any specific official said on any specific issue. This is especially helpful at election time.

![Mr. T](http://govmeeting.org/attachments/download/4/mr-t-mrt-36834265-320-254-24kb.png)
“Enough with the jibber-jabber, fool!
Show me how it works!”

Well, the code is in progress. But here is a [SMALL DEMO](http://demo.govmeeting.org) of one possible feature.  It shows what was discussed at a past meeting of the Boothbay Harbor Selectmen in Maine, USA. The final version will have many more features.

## Design

The software consists of two separate systems:

* A Public System that anyone can use to access information.
* A Volunteer System that community volunteers use to process meeting transcripts.

#### Public System
* A front-end using Typescript and AngularJS.
* A WEB API implemented in NodeJS or ASP.NET/C# or both.
* An SQL database which could be MySql, SQL Server or another. 
* Object-Relational mapping using C# and Entity Framework.

#### Volunteer System
* A crowd-sourcing app for creating or processing existing transcripts – in Typescript and AngularJS.
* An existing content management system, the Drupal CMS, for volunteer workflow coordination.
* Utility modules for audio processing of meeting recordings. Language to be determined.
* Utility scripts to preprocess existing written transcripts.  Language to be determined.

Whether you develop on Linux, Mac or Windows, your contributions are needed. An attempt has been made to keep the processes "OS neutral". For example, we are using npm, bower for dependencies, gulp for automated tasks and karma and jasmine Javascript testing.

For more design information, see the [WIKI Design Documentation.](https://github.com/govmeeting/govmeeting/wiki/Design-Documentation)

## Goals

We can attend government meetings or watch them on TV. But how many times have we ever done this? How many people in a town of 50,000 people do this? Maybe 500. Democracy can’t work if only one percent are involved. And people need to be involved continually -- not just once every four years.

Many Open Data projects rely on data supplied by the government. This project instead uses a grass-roots effort to gather data. Government is more likely supply us with technical data. But they would naturally be less willing to supply us data about themselves  - namely exactly what they said and how they acted on all public issues.

People throughout the world deal with the same problems. This software will be designed to be used anywhere, wherever there is democracy and public access to government meetings.

The first goal is to get the software fully working at the local government level. Then we will expand the software to include state and federal government bodies.


