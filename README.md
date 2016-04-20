# govmeeting

[![Join the chat at https://gitter.im/govmeeting/govmeeting](https://badges.gitter.im/govmeeting/govmeeting.svg)](https://gitter.im/govmeeting/govmeeting?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
## Purpose

Meetings are the heart and soul of democracy. They are where people of different opinions listen to others and present their points of view. In early democracy, it was physically possible for most citizens to all attend at once. 

In the Athenian Assembly of 500 BC, a quorum of 6,000 of the 43,000 citizens was needed to conduct business. In the 10th century, the yearly Icelandic Althing was held in a natural outdoor amphitheater for two weeks and all citizens could attend.

Today it is rare to see more than a dozen citizens at a council meeting in a town of 30,000 people. Is this still democracy? Even finding out what was said at these meetings is difficult to impossible. The vast majority do not even produce a transcript.

Most only produce “minutes”, which is a list of the topics discussed and actions taken. If there is a transcript, it is usually difficult to access and read. You need to download it. There is never a table of contents. Finding anything in it is very difficult. 

This project attempt to obtain transcripts as soon as possible after meetings conclude. It then processes the transcripts, extracts the information and makes it available in a very user friendly way. 


## Features

Shortly after a government meeting takes place, you will be able to:
* Go online and see what issues were discussed and exactly what was said and by whom.
* Receive, on your phone or via email, a summary of issues discussed. 
* Receive a phone alert or email whenever a specific issue is discussed. You would sign up prior for alerts.

At any time, you can go online and see:
* See all issues discussed in past meetings.
* See all that was said in the past on any specific issue.
* See exactly what any specific official said on any specific issue. This is especially helpful at election time.

## Design

* A front-end based on AngularJS.
* A back-end based on C# and ASP.NET
* An SQL database with Entity Framework as the Object-Relational mapping.


If transcipts already exist, the above is all that is needed. However, for places where they do not exist, this project aims to build a system for generating transcripts from video recordings.

The design information can be found in the Wiki tab above or click here: [WIKI Design Documentation.](https://github.com/govmeeting/govmeeting/wiki/Design-Documentation)
 
## Goals

The first goal is to build a working system for those towns or cities that provide online transcripts. Specifically, the first test place will be Philadelphia, PA. USA.

The next goal is to build the system for converting video recordings into transcipts.

People throughout the world deal with the same problems. This software will be designed to be used anywhere, wherever there is democracy and public access to government meetings.

Perhaps once the software is working at the town and city level, we can look into expanding to state and federal government bodies.

-------------------------------------------------------------------------------------------------------

<script>
  (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
  (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
  m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
  })(window,document,'script','//www.google-analytics.com/analytics.js','ga');

  ga('create', 'UA-75868363-1', 'auto');
  ga('send', 'pageview');

</script>
