using System;

namespace Common.Communication.Requests
{
    [Serializable]
    public class ConnectionRequest : RequestBase
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}