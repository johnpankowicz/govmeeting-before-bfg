# govmeeting

[![Join the chat at https://gitter.im/govmeeting/govmeeting](https://badges.gitter.im/govmeeting/govmeeting.svg)](https://gitter.im/govmeeting/govmeeting?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
## Purpose

To inform people of decisions that our government officials are considering. This allows citizens the time to have a voice in the decisions.  

It is normally during town or city public meetings, where new laws are proposed. It is at these meetings where the public is allowed to give their input on pending laws. 

This project attempt to obtain transcripts as soon as possible after meetings conclude. It then processes the transcripts, extracts the information and makes it available to anyone who is interested. 


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


Some towns and cities provide full transcripts of public meetings online. For example the Philadelphia City Council does this. In case like this, the project has the information it needs.

However most other places only provide either meeting minutes or a video recording. Minutes provide only summary information. The do not capture the detailed discussion and debate on the issues.

Therefore this project also aims to build a system for generating transcripts from video recordings.

The design information can be found in the Wiki tab above or click here: [WIKI Design Documentation.](https://github.com/govmeeting/govmeeting/wiki/Design-Documentation)
 
## Goals

The first goal is to build a working system for those towns or cities that provide online transcripts. Specifically, the first test place will be Philadelphia, PA. USA.

The next goal is to build the system for converting video recordings into transcipts.

We can attend government meetings or watch them on TV. But how many times have we ever done this? How many people in a town of 50,000 people do this? Maybe 500. Democracy can’t work if only one percent are involved. And people need to be involved continually -- not just once every four years.

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
