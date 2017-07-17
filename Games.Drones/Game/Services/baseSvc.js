"use strict";
(function () {
    angular.module("dronesApp")
        .factory("baseSvc", ["$http", "$q", function ($http, $q) {



            var baseUrl = window.location.origin;


            var getRequest = function (query) {
                var deferred = $q.defer();
                $http({
                    url: baseUrl + query,
                    method: "GET",
                    headers: {
                        "accept": "application/json;odata=verbose",
                        "content-Type": "application/json;odata=verbose"
                    }
                }).then(function successCallback(response) {
                    deferred.resolve(response);
                }, function errorCallback(response) {
                    deferred.reject(response);
                });

                return deferred.promise;
            };
            var postRequest = function (data, url) {
                var deferred = $q.defer();
                $http({
                    url: baseUrl + url,
                    method: "POST",
                    headers: {
                        "accept": "application/json;odata=verbose",
                        "content-Type": "application/json;odata=verbose"
                    },
                    data: JSON.stringify(data)
                }).then(function successCallback(response) {
                    deferred.resolve(response);
                }, function errorCallback(response) {
                    deferred.reject(response);
                });
                return deferred.promise;
            };           
       return {
                getRequest: getRequest,
                postRequest: postRequest
            };
        }]);
})();