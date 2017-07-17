var app = angular.module('dronesApp');
app.controller('statsCtrl',
        ["$scope",
         "gameService", function ($scope, gameService) {
             gameService.getStats().then(function successCallback(response) {
                 $scope.stats = response.data;
                 $scope.totalDisplayed = 10;

             }, function errorCallback(response) {
                 console.log(response);
                 //TODO: add to logging file
             });

             $scope.loadMore = function () {
                 $scope.totalDisplayed += 10;
             };
         }]);