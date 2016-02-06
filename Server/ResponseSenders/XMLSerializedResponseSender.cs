// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System.Net;
using System.Net.Sockets;
using Common.Communication;
using Common.Communication.Responses;
using Common.Serialization;

namespace Server.Console.ResponseSenders
{
    public class XMLSerializedResponseSender : IResponseSender
    {
        private readonly XMLSerializationService<ResponseBase> _serializationService = new XMLSerializationService<ResponseBase>();

        public void Send(ResponseBase response, CustomIPEndPoint responseEndPoint)
        {
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
           
            socket.Connect(new IPEndPoint(responseEndPoint.Ip, responseEndPoint.Port));

            _serializationService.SerializeToStream(new NetworkStream(socket), response);
            socket.Shutdown(SocketShutdown.Send);
        }
    }
}