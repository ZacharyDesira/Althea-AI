(function () {
    'use strict';

    var RecognizeModule = angular.module('Recognize', ['ngRoute']);

    RecognizeModule.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $locationProvider.hashPrefix("");

        $routeProvider.when('/Recognize', {
            templateUrl: 'App/Recognize/Recognize.Main.html',
            controller: 'RecognizeController',
            controllerAs: 'vm'
        });
    }]);
})();