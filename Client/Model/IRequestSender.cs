// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using Common.Communication;
using Common.Communication.Requests;

namespace Client.Model
{
    public interface IRequestSender
    {
        void Send(RequestBase request, CustomIPEndPoint endPoint);
    }
}