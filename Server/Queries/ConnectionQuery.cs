// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using Common.Communication;

namespace Server.Console.Queries
{
    public class ConnectionQuery : QueryBase
    {
        public ConnectionQuery(CustomIPEndPoint responseEndPoint) : base(responseEndPoint)
        {
        }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}