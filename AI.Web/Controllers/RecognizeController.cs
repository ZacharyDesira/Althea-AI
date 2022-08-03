using AI.Web.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AI.Core;
using AI.Core.Helpers;

namespace AI.Web.Controllers
{
    [RoutePrefix("api/Recognize")]
    public class RecognizeController : ApiController
    {
        private const string computerVisionkey = ""; //add computer vision key via environment variable
        private const string computerVisionEndpoint = ""; // add endpoint via environment varibale
        // Creating an instance of comnputer vision client to be used to call the DetectObjectsAsync method
        private ComputerVisionClient computerVisionClient = new ComputerVisionClient(new Microsoft.Azure.CognitiveServices.Vision.ComputerVision.ApiKeyServiceClientCredentials(computerVisionkey), new DelegatingHandler[] { }) { Endpoint = computerVisionEndpoint };

        //Create a blob container client instance 
        BlobContainerClient containerClient = ConnectionHelper.startBlobClientConnection();

        /// <summary>
        /// Detect a face image and returns its attributes
        /// </summary>
        /// <param name="image"></param>
        /// <returns>Returns a DetectedFace object which included the face attributes</returns>
        [HttpPost]
        [Route("DetectFace")]
        public async Task<IList<DetectedFace>> DetectFace(Image image)
        {
            try
            {
                // Creating Face object to initialize face client
                Face face = new Face();
                IFaceClient faceClient = face.getAzureClient<IFaceClient>();

                // Generating guid for file name
                var fileName = generateFileName();
                var bytes = Convert.FromBase64String(image.imageData);
                var faceAttributeTypes = fetchFaceAttributeTypes();

                // Creating blob client using the previously generated guid
                var blobClient = containerClient.GetBlobClient(fileName + ".jpg");
                // Setting the content type header for the request
                BlobHttpHeaders headers = new BlobHttpHeaders() { ContentType = image.imageType };
                

                using (Stream imageStream = new MemoryStream(bytes))
                {
                    // Saving blob in Azure
                    await blobClient.UploadAsync(imageStream, headers);
                    
                }

                // Fetching data from the FaceAPI
                return await faceClient.Face.DetectWithUrlAsync(blobClient.Uri.ToString(), returnFaceId: false, returnFaceLandmarks: true, detectionModel: DetectionModel.Detection01, returnFaceAttributes: faceAttributeTypes, recognitionModel: RecognitionModel.Recognition04);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Analyzes the image passed and returns the image categories
        /// </summary>
        /// <param name="image"></param>
        /// <returns>Returns a DetectedResult which contains the object categories</returns>
        [HttpPost]
        [Route("AnalyzeImage")]
        public async Task<DetectResult> AnalyzeImage(Image image)
        {
            try
            {
                //Creating guid for filename
                var fileName = generateFileName();
                var bytes = Convert.FromBase64String(image.imageData);

                // Creating blob client using the previously generated guid
                var blobClient = containerClient.GetBlobClient(fileName + ".jpg");
                // Setting the content type header for the request
                BlobHttpHeaders headers = new BlobHttpHeaders() { ContentType = image.imageType };


                using (Stream imageStream = new MemoryStream(bytes))
                {
                    // Saving blob in Azure
                    await blobClient.UploadAsync(imageStream, headers);

                }

                // Fetching data from computer vision api
                return await computerVisionClient.DetectObjectsAsync(blobClient.Uri.ToString());
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private string generateFileName()
        {
            return Guid.NewGuid().ToString();
        }

        private List<FaceAttributeType> fetchFaceAttributeTypes()
        {
            // other face attributes such as gender, age, etc are not supported any more 
            // https://docs.microsoft.com/en-us/azure/cognitive-services/computer-vision/concept-face-detection
            return new List<FaceAttributeType>()
                {
                   FaceAttributeType.Blur,
                   FaceAttributeType.Exposure,
                   FaceAttributeType.Noise,
                   FaceAttributeType.Glasses,
                   FaceAttributeType.Accessories
                };
        }

    }
}
