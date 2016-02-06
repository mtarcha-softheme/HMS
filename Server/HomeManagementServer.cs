// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Communication;
using Common.Communication.Requests;
using Common.Serialization;

namespace Server.Console
{
    public class HomeManagementServer
    {
        private Socket _serverSocket;
        private Thread _acceptThread;
        private readonly XMLSerializationService<RequestBase> _serializationService;
        private readonly IConfigurationServise _configurationServise;

        public HomeManagementServer(IConfigurationServise configurationServise)
        {
            _configurationServise = configurationServise;
            _serializationService = new XMLSerializationService<RequestBase>();
        }

        public void Start()
        {
            SetupServerSocket();
            _acceptThread = new Thread(HandleConnections) {IsBackground = true};
            _acceptThread.Start();
        }

        private void HandleConnections()
        {
            while (true)
            {
                var socket = _serverSocket.Accept();
                var request = _serializationService.DeserializeFromStream(new NetworkStream(socket));
                Task.Run(() => new RequestHandler().Handle(request));
            }
        }

        private void SetupServerSocket()
        {
            var myEndpoint = new IPEndPoint(_configurationServise.GetCurrentIp(), _configurationServise.GetPort());

            _serverSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Bind(myEndpoint);
            System.Console.WriteLine(((IPEndPoint)_serverSocket.LocalEndPoint).Port);
            _serverSocket.Listen((int)SocketOptionName.MaxConnections);
        }
    }
}