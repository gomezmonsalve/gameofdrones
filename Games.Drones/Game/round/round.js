var app = angular.module('dronesApp');
app.controller('roundCtrl',
        ["$scope",
         "gameService",
         "moveService",
         "roundService", function ($scope, gameService, moveService, roundService) {
             var currentGameId = getParameterByName('Id');
             $scope.playAgain = false;
             $scope.loadGameInfo = function () {
                 gameService.getById(currentGameId).then(function successCallback(response) {
                     console.log(response);
                     $scope.player1Turn = true;
                     $scope.player2Turn = false;
                     $scope.game = response.data;
                     
                     if ($scope.game.Winner != "None") {
                         $scope.playAgain = true;
                         //if winner has value show play again section
                     }


                 }, function errorCallback(response) {
                     console.log(response);
                 });
             };
             GetRelatedRounds = function () {
                 roundService.getByGameId(currentGameId).then(function successCallback(response) {
                     $scope.rounds = response.data;
                     $scope.roundCount = response.data.length + 1
                 }, function errorCallback(response) {
                     console.log(response);
                 });
             };
             $scope.Round = {
                 GameId: null,
                 Player1MoveId: null,
                 Player2MoveId: null
             };
             if (currentGameId)
             {
                 $scope.loadGameInfo();
                 GetRelatedRounds();
                 $scope.Round.GameId =currentGameId;                 
             }
             moveService.getAll().then(function successCallback(response) {
                 console.log(response);
                 $scope.availableMoves= response.data;
             }, function errorCallback(response) {
                 console.log(response);
                 //TODO: add to logging file
             });

             $scope.saveRoundInfo = function () {
                 if ($scope.Round.Player1MoveId) {
                     $scope.player1Turn = false;
                     $scope.player2Turn = true;
                 }
                 if ($scope.Round.Player2MoveId) {                     
                     roundService.addNew($scope.Round).then(function successCallback(response) {
                         console.log(response);
                         $scope.Round.Player1MoveId = null;
                         $scope.Round.Player2MoveId = null;
                         $scope.loadGameInfo();
                         GetRelatedRounds();
                     }, function errorCallback(response) {
                         console.log(response);
                         //TODO: add to logging file
                     });
                 }
             };

             $scope.createNewGame = function () {
                 var newGame = {
                     Player1: $scope.game.Player1,
                     Player2: $scope.game.Player2,
                     Winner: "None"
                 };
                 gameService.addNew(newGame).then(function successCallback(response) {
                     window.location.href = '/Game/round/Index.html?Id=' + response.data.Id;
                     //redirect to rounds
                 }, function errorCallback(response) {
                     console.log(response);
                     //TODO: add to logging file
                 });
                 //play again section creates new game with same data and redirects to round with different game id
             }
            
         }]);