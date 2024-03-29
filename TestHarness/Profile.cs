﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestHarness
{
    public class Profile
    {
        [JsonProperty("customer_id")]
        public string CustomerID
        {
            get
            {
                return "12345";
            }
        }

        [JsonProperty("hostname")]
        public string HostName
        {
            get
            {
                return System.Environment.MachineName;
            }
        }

        [JsonProperty("last_update")]
        public DateTime LastUpdate
        {
            get
            {
                return DateTime.Now;
            }
        }

        [JsonProperty("installed")]
        public List<InstalledApplication> InstalledApplications
        {
            get
            {
                InstalledApplications ias = new InstalledApplications();
                List<InstalledApplication> apps = ias.GetInstalledApplications();
                return apps;
            }
        }
    }
}
