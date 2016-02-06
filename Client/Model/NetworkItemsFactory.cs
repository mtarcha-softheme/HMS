// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using Common.Communication;
using Common.Communication.Responses;

namespace Client.Model
{
    public class NetworkItemsFactory
    {
        private readonly IConfigurationServise _configurationServise;

        public NetworkItemsFactory(IConfigurationServise configurationServise)
        {
            _configurationServise = configurationServise;
        }

        public CustomIPEndPoint GetEndPoint()
        {
            return new CustomIPEndPoint
            {
                Ip = _configurationServise.GetCurrentIp(),
                Port = _configurationServise.GetFreePort()
            };
        }

        public DataReceiver<T> GetResponseReceiver<T>()
            where T : ResponseBase
        {
            return new ResponseReceiver<T>();
        }
        
        public DataReceiver<SentFile> GetFileReceiver()
        {
            return new FileReceiver();
        }

        public IRequestSender GetRequestSender()
        {
            return new RequestSender();
        }
    }
}