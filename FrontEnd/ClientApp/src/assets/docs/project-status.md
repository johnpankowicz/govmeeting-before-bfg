* A number of pieces of the software are implemented (but need improving).
* A number of critical pieces need to be implemented.

# Implemented

## Software frameworks for the main systems
 * Front End Angular app
 * Server side Asp.Net Web API
 * Server side .Net Workflow processing


## Frontend
 * Navigation and dashboard
 * User interface to proofread a transcript
 * User interface to add tags to a transcript
 * User interface to browse a processed transcript

## Backend
* Transcribe a meeting recording using Google Speech Services.
* Auto-process an existing city transcript and extract the information.
* Relational database design.

## Authentication
* User registration and login
* 2-factor authentication and 3rd-party logins
* Authorization of Web API calls


# To Be Implemented

## Critical pieces
These are critical to the full operation of the software.
* Component to retrieve online transcripts and recordings.
* Design and implement a "reputation" system.
  <a href="https://github.com/govmeeting/govmeeting/issues/77">Issue #77</a>
* Implement "Register Government Entity" feature.
  <a href="https://github.com/govmeeting/govmeeting/issues/62">Issue #62</a>
* Work in Progress feature.
  <a href="https://github.com/govmeeting/govmeeting/issues/58">Issue #58</a>
* Implement User Alerts feature Feature.
  <a href="https://github.com/govmeeting/govmeeting/issues/20">Issue #20</a>
* Support multi-languages.
  <a href="https://github.com/govmeeting/govmeeting/issues/16">Issue #16</a>
* Component to identify political sub-divisions based on user's location.
  <a href="https://github.com/govmeeting/govmeeting/issues/13">Issue #13</a>

## Improvements
These improvements should to be implemented before a first release of the software.
* Use Phrase Hints to improve accuracy of voice recognition
  <a href="https://github.com/govmeeting/govmeeting/issues/66">Issue #66</a>
* Improve Proofreading user interface
* Improve Add Tags user interface
* Improve View Meeting user interface

## Extras
Though all very useful, these could be added later.
* Mobile app to record a meeting.
  <a href="https://github.com/govmeeting/govmeeting/issues/18">Issue #18</a>
* Mobile app to use voice commands to proofread a meeting.
  <a href="https://github.com/govmeeting/govmeeting/issues/55">Issue #55</a>
* Feature to enlist help with proofreading.
  <a href="https://github.com/govmeeting/govmeeting/issues/69">Issue #69</a>
* Download and process panoramic images for location headers.
  <a href="https://github.com/govmeeting/govmeeting/issues/76">Issue #76</a>
* Component to get political information about a government entity.
  <a href="https://github.com/govmeeting/govmeeting/issues/74">Issue #74</a>
* Re-write front-end Authentication code in Angular.
  <a href="https://github.com/govmeeting/govmeeting/issues/73">Issue #73</a>
* Addtags - make it a two step process?
  <a href="https://github.com/govmeeting/govmeeting/issues/67">Issue #67</a>
* Addtags - filter view by section.
  <a href="https://github.com/govmeeting/govmeeting/issues/65">Issue #65</a>
* Implement "Register Government Entity" feature.
  <a href="https://github.com/govmeeting/govmeeting/issues/61">Issue #61</a>
* Capture additional user info during Register.
  <a href="https://github.com/govmeeting/govmeeting/issues/47">Issue #47</a>
* Locate existing online transcripts or recordings.
  <a href="https://github.com/govmeeting/govmeeting/issues/13">Issue #13</a>


# Production Systems

## Running a Govmeeting site

Anyone can download the Govmeeting software and run it on their own servers or shared hosts.
This could be:
* A government body itself
* A citizen activist group
* An individual citizen

The scale and requirements of the installation will depend on size of the community it handles. This determines the potentail load on the system.

Requirements also depend on how much data will be saved. One option is to only save the processed transripts and the extracted data. Assume a transcript size of 20,000 words and average 6 character word size. An entire year of monthly town council meetings can fit into 1.5 meg of storage. This is a trivial amount of data

However, saving and hosting the video/audio of the meetings is another matter.
This would be needed to allow playback of selected sections of the meeting.
The stored transcript data contains the start/end time of every speakers' comments.
So that is a do-able feature and perhaps it is very useful.  

## Govmeeting.org

It would be helpful if a public site was available for those who
 don't want install and maintain their own system. This also means that the collected data will not be under
 their total control. So there is a trade-off to be made.

"govmeeting.org" is the current demo site for the software. It is now run on a low-cost shared host.
We can look into creating a non-profit that will own and operate this site. If many municipalities elect to use this site, it will need to be run on a cloud service like AWS or Azure, in order to dynamically increase capacity.

