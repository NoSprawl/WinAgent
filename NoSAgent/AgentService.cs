using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Configuration;

namespace NoSAgent
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class AgentService : IAgentService
    {
        // Implement the ICalculator methods.
        public bool StopAgent()
        {
            Global.AgentWorker.StopAgent();
            Global.WriteToEventLog("NoSAgent stopped", false);
            return true;
        }

        public bool StartAgent()
        {
            Global.AgentWorker.StartAgent();
            Global.WriteToEventLog("NoSAgent started", false);
            return true;
        }

        public Settings GetSettings()
        {
            return Global.GetSettings();
        }

        public bool SaveSettings(Settings settings)
        {
            Global.SaveSettings(settings);
            Global.AgentWorker.Reload();
            return true;
        }

        public bool Send()
        {
            Global.AgentWorker.Send();
            return true;
        }
    }
}
