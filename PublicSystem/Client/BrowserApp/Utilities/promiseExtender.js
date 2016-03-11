/// <reference path="../Scripts/angular.js" />

(function () {
    angular.module('utilities', [])
     .factory('promiseExtender', [
        '$q',
        function promiseExtender($q) {
            // interface
            var service = {
                httpDeferredExtender: $httpDeferredExtender,
                httpPromiseFromValue: $httpPromiseFromValue
            };
            return service;

            var self;
            self = this;

            // extends deferred with $http compatible .success and .error functions
            var $httpDeferredExtender = function (deferred) {
                deferred.promise.success = function (fn) {
                    deferred.promise.then(fn, null);
                    return deferred.promise;
                }
                deferred.promise.error = function (fn) {
                    deferred.promise.then(null, fn);
                    return deferred.promise;
                }
                return deferred;
            };

            // creates a resolved/rejected promise from a value
            var $httpPromiseFromValue = function ($q, val, reject) {
                var def = $q.defer();
                if (reject)
                    def.reject(val);
                else
                    def.resolve(val);
                self.$httpDeferredExtender(def);
                return def.promise;
            }
        }
    ]);
})();
