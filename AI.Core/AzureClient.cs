using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Core
{
    public abstract class AzureClient
    {
        // Created this abstract class so that the client apis will use this
        public abstract T getAzureClient<T>();
    }
}
