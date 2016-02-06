// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Common.Communication;

namespace Client.Model
{
    public abstract class DataReceiver<TData>
    {
        private Socket _acceptSocket;
        private bool _dataIsNotReseived = true;

        public event EventHandler<DataReceivedEventArgs<TData>> DataReceived;

        public void Start(CustomIPEndPoint endPoint)
        {
            _acceptSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            _acceptSocket.Bind(new IPEndPoint(endPoint.Ip, endPoint.Port));
            _acceptSocket.Listen(1);

            Task.Run((Action)HandleConnection);
        }           

        private void HandleConnection()
        {
            while (_dataIsNotReseived)
            {
                var socket = _acceptSocket.Accept();

                var data = GetData(socket);
                _dataIsNotReseived = false;

                var handlers = DataReceived;
                if (handlers != null)
                {
                    handlers.Invoke(this, CreateEventArgs(data));
                }
            }
        }
        
        protected abstract TData GetData(Socket socket);

        protected abstract DataReceivedEventArgs<TData> CreateEventArgs(TData data);
    }
}