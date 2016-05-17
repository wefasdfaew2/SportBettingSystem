(function () {
    "use strict";

    app.factory('data', ['$http', '$q', '$routeParams', 'baseServiceUrl',
        function ($http, $q, $routeParams, baseServiceUrl) {

            var headers;
            var baseUrl = baseServiceUrl + '/api';
            var query = $routeParams;

            var get = function (url, params) {
                var defer = $q.defer();

                if (query !== {}) {
                    params = angular.extend({}, params, query);
                }

                $http.get(baseUrl + url, { params: params, headers: headers })
                    .success(function (data) {
                        defer.resolve(data);
                    })
                    .error(function (response) {
                        defer.reject(response)
                    });

                return defer.promise;
            };

            return {
                get: get,
            }
        }]);
})();