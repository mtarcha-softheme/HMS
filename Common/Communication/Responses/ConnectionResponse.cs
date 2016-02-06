// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System;

namespace Common.Communication.Responses
{
    [Serializable]
    public class ConnectionResponse : ResponseBase
    {
        public bool Accepted { get; set; }

        public Guid SessionId { get; set; }
    }
}