/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />

var singleMeetingModule = angular.module('singleMeeting', ['utilities']);

singleMeetingModule.controller("headingCtrl", SingleMeeting.headingCtrl);

singleMeetingModule.controller("BrowseMeetingCtrl", SingleMeeting.BrowseMeetingCtrl);

singleMeetingModule.controller("MeetingOptionsCtrl", SingleMeeting.MeetingOptionsCtrl);

