// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System;

namespace Common.Communication
{
    [Serializable]
    public class CustomIPEndPoint
    {
        public long Ip { get; set; }

        public int Port { get; set; }
    }
}