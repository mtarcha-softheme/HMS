using System;
using System.Xml.Serialization;

namespace Common.Communication.Requests
{
    [XmlInclude(typeof(GetFileNamesRequest))]
    [XmlInclude(typeof(ConnectionRequest))]
    [XmlInclude(typeof(DownloadFileRequest))]
    [Serializable]
    public class RequestBase
    {
        public RequestBase()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid SessionId { get; set; }

        public CustomIPEndPoint ResponseEndPoint { get; set; }
    }
}
