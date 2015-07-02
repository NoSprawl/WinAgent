using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;

namespace NoSAgent
{
    public class Profile
    {

        public Profile()
        {
            //this.MacAddresses = new List<string>();
            this.Data = new Data();
            this.Data.Messages = new Messages();
        }

        [JsonProperty("job")]
        public string Job
        {
            get
            {
                return "ProcessAgentReport";
            }
        }


        //[JsonProperty("network")]
        //public List<string> MacAddresses { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty("message")]
        public Messages Messages { get; set; }
    }

    public class Messages
    {
        [JsonProperty("ips")]
        public List<string> Ips { get; set; }

        [JsonProperty("pkginfo")]
        public List<InstalledApplication> Packages { get; set; }
    }
}
