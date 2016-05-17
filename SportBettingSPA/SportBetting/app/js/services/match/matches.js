(function () {
    "use strict";

    app.factory('matches', ['$http', '$q', 'data', function ($http, $q, data) {

        var getMatches = function (params) {
            return data.get('/matches', params);
        };

        var getUpdatedMatches = function (params) {
            return data.get('/matches/update', params);
        };

        return {
            getMatches: getMatches,
            getUpdatedMatches: getUpdatedMatches
        }
    }]);
})();