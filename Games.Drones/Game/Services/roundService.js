(function () {
    angular.module("dronesApp")
	.factory("roundService", ["baseSvc", function (baseService) {
	    var endPoint = '/api/Rounds';


	    var addNew = function (game) {
	        var data = {
	            GameId: game.GameId,
	            Player1MoveId: game.Player1MoveId,
	            Player2MoveId: game.Player2MoveId
	        };
	        var url = endPoint;
	        return baseService.postRequest(data, url);
	    };

	    var getByGameId = function (gameId) {
	        var query = endPoint + "/Game/" + gameId;
	        return baseService.getRequest(query);
	    };

	    return {
	        addNew: addNew,
	        getByGameId: getByGameId
	    };
	}]);
})();