using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ServiceProcess;
using System.ServiceModel;
using System.IO;

namespace NoSAgent
{
    public partial class NoSAgentService : ServiceBase
    {
        public ServiceHost serviceHost = null;

        public NoSAgentService()
        {
            // Name the Windows Service
            ServiceName = "NoSAgentService";
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (serviceHost != null)
                {
                    serviceHost.Close();
                }

                serviceHost = new ServiceHost(typeof(AgentService));

                // Open the ServiceHostBase to create listeners and start 
                // listening for messages.
                serviceHost.Open();
                Global.AgentWorker = new AgentWorker();

            }
            catch(Exception ex)
            {
                Global.WriteToEventLog("Could not start service: " + ex.Message, true);
                this.Stop();
            }
        }

        protected override void OnStop()
        {
            
        }
    }
}
