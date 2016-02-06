// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System.Net;
using System.Net.Sockets;
using Common.Communication;
using Common.Communication.Requests;
using Common.Serialization;

namespace Client.Model
{
    public class RequestSender : IRequestSender
    {
        private readonly XMLSerializationService<RequestBase> _serializationService = new XMLSerializationService<RequestBase>();

        public void Send(RequestBase request, CustomIPEndPoint endPoint)
        {
            var socket = SetupSocket(endPoint.Ip, endPoint.Port);

            _serializationService.SerializeToStream(new NetworkStream(socket), request);

            socket.Shutdown(SocketShutdown.Send);
        }

        private static Socket SetupSocket(long ip, int port)
        {
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(ip, port));

            return socket;
        }
    }
}