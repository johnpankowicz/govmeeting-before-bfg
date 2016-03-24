/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />

var singleMeetingModule = angular.module('singleMeeting', ['utilities']);

singleMeetingModule.factory("BackEndSrvWithCtrl", ['$http', ($http: ng.IHttpService) =>
    new SingleMeeting.BackEndSrvWithCtrl($http)]);

singleMeetingModule.controller("headingCtrl", SingleMeeting.headingCtrl);

//singleMeetingModule.controller("headingCtrl", [()
//    => new SingleMeeting.headingCtrl(BackEndSrvWithCtrl)]);

singleMeetingModule.controller("BrowseMeetingCtrl", SingleMeeting.BrowseMeetingCtrl);

singleMeetingModule.controller("MeetingOptionsCtrl", SingleMeeting.MeetingOptionsCtrl);

