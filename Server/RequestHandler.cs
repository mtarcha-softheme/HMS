// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System;
using System.Collections.Generic;
using System.Net;
using Common.Communication.Requests;
using Common.Connections;
using Server.Console.Executors;
using Server.Console.Queries;
using Server.Console.ResponseSenders;

namespace Server.Console
{
    public class RequestHandler : IRequestHandler
    {
        private readonly Dictionary<Type, Func<RequestBase, ITaskExecutor>> _executorByRequestType = new Dictionary<Type, Func<RequestBase, ITaskExecutor>>
                                                                  {
                                                                      {typeof(ConnectionRequest), x => ToConnectionExecutor((ConnectionRequest)x)},
                                                                      {typeof(GetFileNamesRequest), x => ToFileNamesSearchExecutor((GetFileNamesRequest)x)},
                                                                       {typeof(DownloadFileRequest), x => ToDownloadFileExecutor((DownloadFileRequest)x)}
                                                                  };
        
        public void Handle(RequestBase request)
        {
            var executor = _executorByRequestType[request.GetType()].Invoke(request);

            if (executor != null)
            {
                executor.Execute();
            }
        }

        private static ITaskExecutor ToConnectionExecutor(ConnectionRequest request)
        {
            var query = new ConnectionQuery(request.ResponseEndPoint)
                   {
                       Id = request.Id,
                       UserName = request.UserName,
                       Password = request.Password
                   };

            return new ConnectionExecutor(query, new XMLSerializedResponseSender());
        }

        private static ITaskExecutor ToFileNamesSearchExecutor(GetFileNamesRequest request)
        {
            if (ConnectionsStorage.Instanse.Contains(request.SessionId))
            {
                var query = new GetFileNamesQuery(request.ResponseEndPoint)
                            {
                                Id = request.Id,
                                NameSubstring = request.NameSubstring,
                                VolumeName = request.VolumeName,
                            };

                return new FileNamesSearchExecutor(query, new XMLSerializedResponseSender());
            }

            return null;
        }

        private static ITaskExecutor ToDownloadFileExecutor(DownloadFileRequest request)
        {
            if (ConnectionsStorage.Instanse.Contains(request.SessionId))
            {
                var query = new DownloadFileQuery(request.ResponseEndPoint)
                {
                    Id = request.Id,
                    FileFullName = request.FileFullName
                };

                return new DownloadFileExecutor(query);
            }

            return null;
        }
    }
}