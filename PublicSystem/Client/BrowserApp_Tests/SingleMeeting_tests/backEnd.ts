/// <reference path="../scripts/typings/angularjs/angular.d.ts" />

module SingleMeeting {

    export interface IBackEnd {
        meetingDataUrl: string;
        getMeetingInfo(ctrl: {});
        getTopicNames(ctrl: {});
        getSpeakerNames(ctrl: {});
        getTopicDiscussions(ctrl: {});
    }

    export class BackEndSrv {

        http: ng.IHttpService;
        meetingDataUrl: string;
        httpPromise: any;

        constructor($http: ng.IHttpService) {
            this.http = $http;
            this.meetingDataUrl = "testdata\\BBH-2014-09-08.json";
            this.httpPromise = null;
        }


        // We do not know which piece of information will be requested first.
        // Therefore, in each instance, we call "getMeetingData()" to check if
        // the call to the backend for the meeting's data was initiated.
        // In each case the call will return immediatedly. The data requested
        // will later be added as a property on the object passed in the call.
        // This is usually that of the "this" pointer of the controller calling us.
        
        getMeetingInfo(ctrl) {
            this.getMeetingData();
            this.httpPromise.success(function (data) {
                ctrl.meetingInfo = data.meetingInfo;
            });
        }

        getTopicNames(ctrl) {
            this.getMeetingData();
            this.httpPromise.success(function (data) {
                ctrl.topicNames = data.topicNames;
            });
        }

        getSpeakerNames(ctrl) {
            this.getMeetingData();
            this.httpPromise.success(function (data) {
                ctrl.speakerNames = data.speakerNames;
            });
        }

        getTopicDiscussions(ctrl) {
            this.getMeetingData();
            this.httpPromise.success(function (data) {
                ctrl.topicDiscussions = data.topicDiscussions;
            });
        }

        getMeetingData() {
            if (this.httpPromise == null) {
                this.httpPromise = this.http.get(this.meetingDataUrl);
            };
        }
    }
}
 