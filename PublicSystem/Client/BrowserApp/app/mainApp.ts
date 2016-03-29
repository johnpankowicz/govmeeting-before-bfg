/// <reference path="../scripts/typings/angularjs/angular.d.ts" />

// mainApp module

var appModule = angular.module('mainApp', ['singleMeeting']);

interface IGovmeetingScope extends ng.IScope {
    GovMeetingAppVersion: string;
}

appModule.config([function () {
    console.log("Configuring Main App")
}])
    .run(["$rootScope", function ($rootScope: IGovmeetingScope){
    console.log("Running Main App");
    $rootScope.GovMeetingAppVersion = "0.0.0.1";
}]);



// TODO Transpile Typescript files with gulp.
// TODO Add dialog for user to select which meeting to display 
// TODO fix failed tests