/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="userchoices.ts" />

var utilitiesModule = angular.module('utilities', []);

utilitiesModule.factory("UserChoiceSrv", [() =>
    new Utilities.UserChoiceSrv()]);

utilitiesModule.factory("BackEndSrv", ['$http', ($http: ng.IHttpService) =>
    new Utilities.BackEndSrv($http)]);

// Javascript code before converting to Typescript.
//(function () {
//
//    var utilitiesModule = angular.module('utilities', []);
//
//    utilitiesModule.factory("UserChoiceSrv", [() =>
//        new Utilities.UserChoiceSrv()]);
//
//})();