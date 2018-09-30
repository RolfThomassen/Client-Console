using System;
using Client_Console.Net;
using Client_Console.Net.SocketV2;
using Socket.Shared;
using System.Configuration;

namespace ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var ipAddresses = IPAddressResolver.GetList();
            var setting = GetConfigSetting();
            ClientSocket clientSocket;

            if (setting.UseConfigSetting)
            {
                clientSocket = new ClientSocket(setting.HostIPAddress, setting.Port);
            }
            else if (setting.UseAutoIPconfig)
            {
                foreach (var item in ipAddresses)
                {
                    HostIpAddress = item.Address;
                }
                HostPort = "3000";
                UseConfigSetting = "true";
                clientSocket = new ClientSocket(setting.HostIPAddress, setting.Port);
            }
            else 
            {
                Console.Write("Enter Host Name: ");
                var hostName = Console.ReadLine();

                Console.Write("Enter Port Number: ");
                var portNumber = Convert.ToInt32(Console.ReadLine());
                clientSocket = new ClientSocket(hostName, portNumber);
            }
            Console.WriteLine($"Connecting...{HostIpAddress}:{HostPort}");
            try
            {
                clientSocket.Connect();
            }
            catch (POCSocketException pocEx)
            {
                Console.WriteLine($"{pocEx.Message}");
                Console.Write($">");
                Console.ReadLine();
            }
        }



        #region ConfigSettings
        public static ConfigSetting GetConfigSetting()
        {
            var useConfigSetting = UseConfigSetting.ToLower().Equals("true");  
            var useAutoIPconfig = UseConfigSetting.ToLower().Equals("auto");  
            var hostIPAddress = ConfigurationManager.AppSettings["HostIPAddress"];
            var port = ConfigurationManager.AppSettings["Port"];

            return new ConfigSetting
            {
                UseConfigSetting = Convert.ToBoolean(useConfigSetting),
                UseAutoIPconfig = Convert.ToBoolean(useAutoIPconfig),
                HostIPAddress = hostIPAddress,
                Port = Convert.ToInt32(port)
            };
        }
        
        public static string HostIpAddress
        {
            get => GetSetting("HostIPAddress");
            set => SetSetting("HostIPAddress", value);
        }
        public static string HostPort
        {
            get => GetSetting("Port");
            set => SetSetting("Port", value);
        }
        public static string UseConfigSetting
        {
            get => GetSetting("UseConfigSetting");
            set => SetSetting("UseConfigSetting", value);
        }
        public static string GetSetting(string key)
        { return (" " + ConfigurationManager.AppSettings[key]).Trim(); }
        public static void SetSetting(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings.Remove(key);
            configuration.AppSettings.Settings.Add(key, value);
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }
        #endregion
    }

    public class ConfigSetting
    {
        public bool UseAutoIPconfig { get; set; }
        public bool UseConfigSetting { get; set; }
        public string HostIPAddress { get; set; }
        public int Port { get; set; }
    }

}