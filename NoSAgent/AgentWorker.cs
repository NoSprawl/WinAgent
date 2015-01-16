using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using System.Configuration;

namespace NoSAgent
{
    public class AgentWorker
    {
        Timer timer = new Timer();
        
        public AgentWorker()
        {
            SetupTimer();
        }

        private void SetupTimer()
        {
            Settings settings = Global.GetSettings();
            if(settings.Interval != 0)
            {
                TimeSpan timeSpan = new TimeSpan(settings.Interval, 0, 0);
                timer = new Timer(timeSpan.TotalMilliseconds);
                timer.Elapsed += timer_Elapsed;
                timer.Start();
            }
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ProcessQueue();
        }

        public void StopAgent()
        {
            timer.Stop();
        }

        public void StartAgent()
        {
            timer.Start();
        }

        public void Send()
        {
            ProcessQueue();
        }

        public void Reload()
        {
            SetupTimer();
        }

        private void ProcessQueue()
        {
            try
            {
                // Get the profile and applications
                Profile profile = new Profile();
                string json = JsonConvert.SerializeObject(profile);

                AWSSQS sqs = new AWSSQS();
                sqs.Enqueue(json);
            }
            catch(Exception ex)
            {
                Global.WriteToEventLog("Unable to process queue: " + ex.Message, true);
            }
        }

    }
}
