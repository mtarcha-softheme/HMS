// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System;
using Common.Communication;

namespace Server.Console.Queries
{
    public class QueryBase
    {
        public QueryBase(CustomIPEndPoint responsEndPoint)
        {
            ResponseEndPoint = responsEndPoint;
        }

        public Guid Id { get; set; }

        public CustomIPEndPoint ResponseEndPoint { get; set; }
    }
}