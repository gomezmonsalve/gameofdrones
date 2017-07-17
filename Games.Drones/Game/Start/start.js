var app = angular.module('dronesApp');
app.controller('startCtrl',
        ["$scope",
         "gameService",function ($scope, gameService) {
             $scope.addNewGame = function () {
                 if ($scope.users&&$scope.users.Player1 && $scope.users.Player2)
                 {
                      var game = {
                     Player1: $scope.users.Player1,
                     Player2: $scope.users.Player2,
                     Winner: "None"
                     };
                     gameService.addNew(game).then(function successCallback(response) {
                         window.location.href = '/Game/round/Index.html?Id=' + response.data.Id;
                         //redirect to rounds
                     }, function errorCallback(response) {
                         console.log(response);
                         //TODO: add to logging file
                     });
                 }                   
                };
            }]);