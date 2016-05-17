(function () {
    'use strict';

    app.controller('MatchesController', ['$scope', '$interval', 'matches',
        function ($scope, $interval, matches) {

            $scope.initPaging = initPaging;
            $scope.setPageSize = setPageSize;
            $scope.updateMatches = updateMatches;
            $scope.currentPage = 1;
            $scope.maxSize = 10;

            $scope.items = [1, 3, 5, 10, 15];

            $scope.request = {
                page: 1,
                pageSize: 3
            };

            matches.getMatches($scope.request)
                .then(function (data) {
                    $scope.allMatches = data.TodayMatches;
                    $scope.totalMatchesCount = data.TodayMatchesCount;
                }).then(function () {
                    updateMatches();
                });

            function updateMatches() {
                $interval(function () {
                    matches.getUpdatedMatches($scope.request)
                       .then(function (data) {
                           if (data.length !== 0) {
                               angular.forEach(data, function (updatedItem, key) {
                                   angular.forEach($scope.allMatches, function (element, index) {
                                       if (updatedItem.Id === element.Id) {
                                           $scope.allMatches[index] = updatedItem;
                                           //angular.merge(element, updatedItem);
                                       }
                                   });
                               });
                           }
                       });
                }, 60000);
            }

            function initPaging() {
                matches.getMatches($scope.request)
                    .then(function (data) {
                        $scope.allMatches = data.TodayMatches;
                    })
            }

            function setPageSize(size) {
                $scope.request.pageSize = size;
                $scope.initPaging();
            }
        }]);
})();