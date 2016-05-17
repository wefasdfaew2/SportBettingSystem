(function () {
    'use strict';

    app.directive('matchesDirective', function () {
        return {
            restrict: 'A',
            templateUrl: 'views/directives/matches-list.html',
            replace: true
        }
    });
})();