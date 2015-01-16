using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Configuration;

namespace NoSAgent
{
    public class Global
    {
        public static AgentWorker AgentWorker = null;

        public static Settings GetSettings()
        {
            Settings settings = new Settings();
            settings.CustomerId = ReadSetting("CustomerId");
            string intervalString = ReadSetting("Interval");

            int interval = 0;
            if (Int32.TryParse(intervalString, out interval))
            {
                settings.Interval = interval;
            }
            else
            {
                settings.Interval = 0;
            }

            return settings;
        }

        public static void SaveSettings(Settings settings)
        {
            AddUpdateAppSettings("CustomerID", settings.CustomerId);
            AddUpdateAppSettings("Interval", settings.Interval.ToString());
        }

        private static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                return appSettings[key] ?? string.Empty;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }

            return string.Empty;

        }

        private static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        public static void WriteToEventLog(string message, bool isError)
        {
            string source = "NoSAgentService";
            string log = "Application";

            if (!EventLog.SourceExists(source))
                EventLog.CreateEventSource(source, log);

            if (isError)
            {
                EventLog.WriteEntry(source, message, EventLogEntryType.Error);
            }
            else
            {
                EventLog.WriteEntry(source, message, EventLogEntryType.Information);
            }
        }
    }
}
