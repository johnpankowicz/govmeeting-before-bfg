var SingleMeeting;
(function (SingleMeeting) {
    var BackEndSrv = (function () {
        function BackEndSrv($http) {
            this.http = $http;
            this.meetingDataUrl = "testdata\\BBH-2014-09-08.json";
            this.httpPromise = null;
        }
        BackEndSrv.prototype.getMeetingInfo = function (ctrl) {
            this.getMeetingData();
            this.httpPromise.success(function (data) {
                ctrl.meetingInfo = data.meetingInfo;
            });
        };
        BackEndSrv.prototype.getTopicNames = function (ctrl) {
            this.getMeetingData();
            this.httpPromise.success(function (data) {
                ctrl.topicNames = data.topicNames;
            });
        };
        BackEndSrv.prototype.getSpeakerNames = function (ctrl) {
            this.getMeetingData();
            this.httpPromise.success(function (data) {
                ctrl.speakerNames = data.speakerNames;
            });
        };
        BackEndSrv.prototype.getTopicDiscussions = function (ctrl) {
            this.getMeetingData();
            this.httpPromise.success(function (data) {
                ctrl.topicDiscussions = data.topicDiscussions;
            });
        };
        BackEndSrv.prototype.getMeetingData = function () {
            if (this.httpPromise == null) {
                this.httpPromise = this.http.get(this.meetingDataUrl);
            }
            ;
        };
        return BackEndSrv;
    })();
    SingleMeeting.BackEndSrv = BackEndSrv;
})(SingleMeeting || (SingleMeeting = {}));
//# sourceMappingURL=backEnd.js.map