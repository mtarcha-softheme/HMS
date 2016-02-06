// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using Common.Communication;
using Common.Communication.Responses;

namespace Server.Console.ResponseSenders
{
    public interface IResponseSender
    {
        void Send(ResponseBase response, CustomIPEndPoint responseEndPoint);
    }
}