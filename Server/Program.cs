// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System;
using System.ServiceProcess;
using Common;
using Common.Communication;

namespace Server.Console
{
    class Program : ServiceBase
    {
        static void Main(string[] args)
        {
            var service = new Program();

            if (Environment.UserInteractive)
            {
                service.OnStart(args);
                System.Console.WriteLine("Press any key to stop program");
                System.Console.Read();
                service.OnStop();
            }
            else
            {
                Run(service);
            }
        }

        protected override void OnStart(string[] args)
        {
            var server = new HomeManagementServer(new ConfigurationService());
            server.Start();
        }
    }
}
