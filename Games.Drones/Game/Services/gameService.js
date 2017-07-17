(function () {
    angular.module("dronesApp")
	.factory("gameService", ["baseSvc", function (baseService) {
	    var endPoint = '/api/games';

	    var addNew = function (game) {
	        var data = {
	            Player1: game.Player1,
	            Player2: game.Player2,
	            Winner: game.Winner

	        };
	        var url = endPoint;
	        return baseService.postRequest(data, url);
	    };
	    var getById = function (gameId) {
	        var query = endPoint + "/" + gameId;
	        return baseService.getRequest(query);
	    };

	    var getStats = function () {
	        var query = endPoint + "/Statistics";
	        return baseService.getRequest(query);
	    };

	    return {
	        addNew: addNew,
	        getById: getById,
	        getStats: getStats
	    };
	}]);
})();