var singleMeetingModule = angular.module('singleMeeting', []);
singleMeetingModule.factory("BackEndSrv", ['$http', function ($http) { return new SingleMeeting.BackEndSrv($http); }]);
//# sourceMappingURL=singleMeeting.js.map