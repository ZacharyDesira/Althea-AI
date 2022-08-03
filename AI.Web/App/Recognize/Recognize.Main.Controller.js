(function () {
    'use strict';

    angular.module('Recognize').controller('RecognizeController', RecognizeController);

    RecognizeController.$inject = ['RecognizeService'];

    function RecognizeController(RecognizeService) {
        var vm = this;

        var image = {
            imageType: "",
            imageData: ""
        }

        vm.FaceAttributes = [];
        vm.Selection = "detectFace";

        vm.Capture = function () {
            Webcam.snap(function (data_uri) {
                // display results in page
                document.getElementById('snapshot').innerHTML = '<img id="imageSnapshot" src="' + data_uri + '"/>';
                drawSnapshot();

                var imageType = data_uri.split(";")[0]; //gets the image type from the the data url
                var base64 = data_uri.split(",")[1]; //gets the base 64 string from the data url

                image.imageType = imageType;
                image.imageData = base64;

                switch (vm.Selection) {
                    case 'detectFace':
                        callDetectFaceService(image);
                        break;
                    case 'analyzeImage':
                        callAnalyzeImageService(image);
                        break;
                }
            });
        }

        vm.Clear = function () {
            vm.FaceAttributes = []
        }

        function setup() {
            setupWebcam();
            attachWebcam();
        }

        function getSnapshotCanvasContext() {
            var canvas = $("#snapshot");
            return canvas[0].getContext("2d");
        }

        function drawSnapshot() {
            var context = getSnapshotCanvasContext();
            var snapshot = $("#imageSnapshot")[0];

            var img = new Image();
            img.onload = function () {
                context.drawImage(img, 0, 0);
            }
            img.src = snapshot.src;
        }

        function drawRect(x, y, w, h) {
            var context = getSnapshotCanvasContext();
            context.beginPath();
            context.strokeStyle = "#ff0000";
            context.rect(x, y, w, h);
            context.stroke();
        }

        function drawFaceLandmarks(x, y) {
            var context = getSnapshotCanvasContext();
            context.beginPath();
            context.strokeStyle = "#ff0000";
            context.rect(x, y, 1, 1);
            context.stroke();
        }

        function fillText(text, x , y) {
            var context = getSnapshotCanvasContext();
            var font = 18;
            context.font = font +"px Arial";
            context.fillStyle = "#ff0000";
            context.fillText(text, x + 2, y + font);
        }

        function callDetectFaceService(image) {
            RecognizeService.detectFace(image).then(function (response) {
                var data = response.data;
                if (data.length > 0) {
                    var faceRect = data[0].faceRectangle;
                    var faceLandmarks = data[0].faceLandmarks;
                    vm.FaceAttributes = data[0].faceAttributes;

                    drawRect(faceRect.left, faceRect.top, faceRect.width, faceRect.height);
                    angular.forEach(faceLandmarks, function (faceLandmark) {
                        drawFaceLandmarks(faceLandmark.x, faceLandmark.y);
                    });
                }
                else {
                    bootbox.alert("Did not detect any face landmarks or attributes");
                }
            });
        }

        function callAnalyzeImageService(image) {
            RecognizeService.analyzeImage(image).then(function (response) {
                var data = response.data;
                vm.Objects = data.objects;
                angular.forEach(vm.Objects, function (object) {
                    var rectangle = object.rectangle;
                    drawRect(rectangle.x, rectangle.y, rectangle.w, rectangle.h);
                    fillText(object.object, rectangle.x, rectangle.y);
                });
            });
        }

        function setupWebcam() {
            Webcam.set({
                width: 320,
                height: 240,
                image_format: 'jpeg',
                jpeg_quality: 90
            });
        }

        function attachWebcam() {
            Webcam.attach('#camera');
        }

        setup();
    }
})();