// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Microsoft.Win32;

namespace Common.Communication
{
    public class ConfigurationService : IConfigurationServise
    {
        private const string RegistryPath = @"SOFTWARE\HMS\Port";

        public long GetCurrentIp()
        {
            var machineInfo = Dns.GetHostEntry(Dns.GetHostName());
            return machineInfo.AddressList.First(x => x.AddressFamily == AddressFamily.InterNetwork).Address;
        }

        public int GetPort()
        {
            var communication = GetPortRegistriKey();
            
            if (communication != null)
            {
                var port = 0;
                try
                {
                    port = (int)communication.GetValue("Port");
                }
                catch (Exception)
                {
                }

                port = 0;

                if (port == 0)
                {
                    port = GetFirstAvailablePort();
                    communication.SetValue("Port", port);
                }

                return port;
            }

            throw new Exception($"Registry key: {RegistryPath} does not exist");
        }

        public int GetFreePort()
        {
            return GetFirstAvailablePort();
        }

        private RegistryKey GetPortRegistriKey()
        {
            var regKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);

            try
            {
                return regKey.OpenSubKey(RegistryPath, true);
            }
            catch (Exception ex)
            {
                return regKey.CreateSubKey(RegistryPath);
            }
        }

        private static int GetFirstAvailablePort()
        {
            var port = 10000;
            while (!Available(port))
            {
                port++;
            }

            return port;
        }

        private static bool Available(int port)
        {
            var properties = IPGlobalProperties.GetIPGlobalProperties();
            var tcpNotFreePorts = properties.GetActiveTcpListeners().Select(x => x.Port);

            return !tcpNotFreePorts.Contains(port);
        }
    }
}