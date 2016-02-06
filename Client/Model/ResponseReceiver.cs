// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System.Net.Sockets;
using Common.Communication.Responses;
using Common.Serialization;

namespace Client.Model
{
    public sealed class ResponseReceiver<T> : DataReceiver<T>
        where T : ResponseBase
    {
        protected override T GetData(Socket socket)
        {
            var xmlSer = new XMLSerializationService<ResponseBase>();
            return (T)xmlSer.DeserializeFromStream(new NetworkStream(socket));
        }

        protected override DataReceivedEventArgs<T> CreateEventArgs(T data)
        {
            return new DataReceivedEventArgs<T>(data);
        }
    }
}