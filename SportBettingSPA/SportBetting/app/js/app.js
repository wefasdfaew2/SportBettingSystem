'use strict';

var app = angular.module('myApp', ['ngRoute', 'ngResource', 'ui.bootstrap']).
    config(['$routeProvider', function ($routeProvider) {
        $routeProvider
            .when('/matches', {
                templateUrl: 'views/partials/matches.html',
                controller: 'MatchesController'
            })
            .otherwise({ redirectTo: '/' });
    }])
    .constant('baseServiceUrl', 'http://localhost:2276');