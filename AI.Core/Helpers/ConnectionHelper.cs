using Azure.Storage.Blobs;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AI.Core.Helpers
{
    public class ConnectionHelper
    {
        public static IFaceClient startFaceClientConnection()
        {
            string faceApiKey = ConfigurationManager.AppSettings[AppSettingKeys.FaceApiKey];
            string faceApiEndpoint = ConfigurationManager.AppSettings[AppSettingKeys.FaceApiEndpoint];
            return new FaceClient(new Microsoft.Azure.CognitiveServices.Vision.Face.ApiKeyServiceClientCredentials(faceApiKey), new DelegatingHandler[] { }) { Endpoint = faceApiEndpoint };
        }

        //TODO - currently not working - reverted back to instantiating in controller
        public static ComputerVisionClient startComputerVisionClientConnection()
        {
            string ComputerVisionKey = ConfigurationManager.AppSettings[AppSettingKeys.ComputerVisionKey];
            string ComputerVisionEndpoint = ConfigurationManager.AppSettings[AppSettingKeys.ComputerVisionEndpoint];
            return new ComputerVisionClient(new Microsoft.Azure.CognitiveServices.Vision.ComputerVision.ApiKeyServiceClientCredentials(ComputerVisionKey), new DelegatingHandler[] { }) { Endpoint = ComputerVisionEndpoint };
        }

        public static BlobContainerClient startBlobClientConnection()
        {
            string blobConnectionString = ConfigurationManager.AppSettings[AppSettingKeys.BlobConnectionString];
            string blobContainer = ConfigurationManager.AppSettings[AppSettingKeys.BlobContainer];
            return new BlobContainerClient(blobConnectionString, blobContainer);
        }
    }
}
