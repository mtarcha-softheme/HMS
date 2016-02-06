// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System;

namespace Client.ViewModel
{
    public class Connection
    {
        public Connection(Guid sessionId, string ip, int port)
        {
            SessionId = sessionId;
            Ip = ip;
            Port = port;
        }

        public Guid SessionId { get; private set; }

        public string Ip { get; private set; }

        public int Port { get; private set; }
    }
}