/// <reference path="../scripts/typings/angularjs/angular.d.ts" />

//var singleMeetingModule = angular.module('singleMeeting', ['utilities']);

var singleMeetingModule = angular.module('singleMeeting', []);

singleMeetingModule.factory("BackEndSrv", ['$http', ($http)
    => new SingleMeeting.BackEndSrv($http)]);

//singleMeetingModule.controller("headingCtrl", SingleMeeting.headingCtrl);

////singleMeetingModule.controller("headingCtrl", [()
////    => new SingleMeeting.headingCtrl(BackEndSrv)]);

//singleMeetingModule.controller("BrowseMeetingCtrl", SingleMeeting.BrowseMeetingCtrl);

//singleMeetingModule.controller("MeetingOptionsCtrl", SingleMeeting.MeetingOptionsCtrl);

