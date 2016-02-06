// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System.Net;
using Common.Communication.Requests;

namespace Server.Console
{
    public interface IRequestHandler
    {
        void Handle(RequestBase request);
    }
}