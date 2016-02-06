using System.Net;
using System.Net.Sockets;

namespace Common.Connections
{
    public class Connection
    {
        private readonly IPEndPoint _endPoint;
        public Connection(IPEndPoint endPoint)
        {
            _endPoint = endPoint;
        }

        public IPEndPoint EndPoint { get { return _endPoint; } }

        public Socket Socket { get; set; }
    }
}