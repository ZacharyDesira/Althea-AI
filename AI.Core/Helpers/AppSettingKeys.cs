using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Core.Helpers
{
    public class AppSettingKeys
    {
        internal static string FaceApiKey { get { return "FaceApiKey"; } }
        internal static string FaceApiEndpoint { get { return "FaceApiEndpoint"; } }
        internal static string ComputerVisionKey { get { return "ComputerVisionKey"; } }
        internal static string ComputerVisionEndpoint { get { return "ComputerVisionEndpoint"; } }
        internal static string BlobContainer { get { return "BlobContainer"; } }
        internal static string BlobConnectionString { get { return "BlobConnectionString"; } }
    }
}
