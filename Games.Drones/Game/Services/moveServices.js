(function () {
    angular.module("dronesApp")
	.factory("moveService", ["baseSvc", function (baseService) {
	    var endPoint = '/api/Moves';


	    var getAll = function (gameId) {
	        var query = endPoint;
	        return baseService.getRequest(query);
	    };


	    return {
	        getAll: getAll
	    };
	}]);
})();