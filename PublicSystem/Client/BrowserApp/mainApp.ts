/// <reference path="scripts/typings/angularjs/angular.d.ts" />

// mainApp module
// Code related to the entire application can be put here,
// as services, controllers, etc. on mainApp.

//(function () {
    //"use strict";
 
    //angular.module('singleMeeting', ['utilities']);
    var appModule = angular.module('mainApp', ['singleMeeting']);

    interface IGovmeetingScope extends ng.IScope {
        GovMeetingAppVersion: string;
    }

    //angular.module('mainApp')
    appModule.config([function () {
        console.log("Configuring Main App")
    }])
        .run(["$rootScope", function ($rootScope: IGovmeetingScope){
        console.log("Running Main App");
        $rootScope.GovMeetingAppVersion = "0.0.0.1";
    }]);

    //appModule.controller("headingCtrl", [()
    //    => new Application.Controllers.headingCtrl()]);

    //appModule.controller("headingCtrl",SingleMeeting.headingCtrl);

//})();



// TODO Add user dialog for meeting to display 
// TODO fix failed tests