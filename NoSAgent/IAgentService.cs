using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace NoSAgent
{
    // Define a service contract.
    [ServiceContract]
    public interface IAgentService
    {
        [OperationContract]
        bool StopAgent();
        [OperationContract]
        bool StartAgent();
        [OperationContract]
        bool SaveSettings(Settings settings);
        [OperationContract]
        Settings GetSettings();
        [OperationContract]
        bool Send();
    }
}
