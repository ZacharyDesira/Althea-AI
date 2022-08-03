using AI.Core.Helpers;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AI.Core
{
    public class Face : AzureClient
    {
        IFaceClient _faceClient;

        public Face()
        {
            _faceClient = ConnectionHelper.startFaceClientConnection();
        }

        public override IFaceClient getAzureClient<IFaceClient>()
        {
            return (IFaceClient)_faceClient;
        }
    }
}
