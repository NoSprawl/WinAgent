using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace NoSAgent
{
    public class InstalledApplications
    {

        private List<string> regKeys = new List<string>();

        public InstalledApplications()
        {
            regKeys.Add(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            regKeys.Add(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
        }

        public List<InstalledApplication> GetInstalledApplications()
        {
            List<InstalledApplication> installedApps = new List<InstalledApplication>();

            foreach (string currentKey in regKeys)
            {

                using (RegistryKey regKey = Registry.LocalMachine.OpenSubKey(currentKey))
                {
                    foreach (string subKeyName in regKey.GetSubKeyNames())
                    {
                        using (RegistryKey appKey = regKey.OpenSubKey(subKeyName))
                        {
                            try
                            {
                                InstalledApplication app = new InstalledApplication();
                                app.Name = appKey.GetValue("DisplayName").ToString();
                                app.Version = appKey.GetValue("DisplayVersion").ToString();
                                app.Publisher = appKey.GetValue("Publisher").ToString();
                                app.LatestVersion = "unknown";
                                installedApps.Add(app);
                            }
                            catch (Exception ex)
                            { }
                        }
                    }
                }
            }


            return installedApps;

        }
    }

    public class InstalledApplication
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("installed_version")]
        public string Version { get; set; }

        [JsonProperty("vendor")]
        public string Publisher { get; set; }

        [JsonProperty("is_service")]
        public bool IsService { get; set; }

        [JsonProperty("is_running")]
        public bool IsRunning { get; set; }

        [JsonProperty("latest_version")]
        public string LatestVersion { get; set; }

    }
}


