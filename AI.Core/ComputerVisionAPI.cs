using AI.Core.Helpers;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Core
{
    public class ComputerVisionAPI : AzureClient
    {
        ComputerVisionClient _computerVisionClient;

        public ComputerVisionAPI()
        {
            _computerVisionClient = ConnectionHelper.startComputerVisionClientConnection();
        }

        public override ComputerVisionClient getAzureClient<ComputerVisionClient>()
        {
            return default(ComputerVisionClient);
            //return _computerVisionClient;
        }
    }
}
