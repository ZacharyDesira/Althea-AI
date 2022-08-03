(function () {
    'use strict';

    angular.module('Recognize').service('RecognizeService', RecognizeService);

    RecognizeService.$inject = ['$http'];

    function RecognizeService($http) {

        this.detectFace = function (image) {
            return $http.post('api/Recognize/DetectFace', image).then(detectFaceSuccess, detectFaceFailed);

            function detectFaceSuccess(response) {
                return response;
            }

            function detectFaceFailed(error) {
                bootbox.alert('Face detection failed');
            }
        }

        this.analyzeImage = function (image) {
            return $http.post('api/Recognize/AnalyzeImage', image).then(detectFaceSuccess, detectFaceFailed);

            function detectFaceSuccess(response) {
                return response;
            }

            function detectFaceFailed(error) {
                bootbox.alert('Face detection failed');
            }
        }
    }
})();