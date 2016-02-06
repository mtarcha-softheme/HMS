// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System;
using System.DirectoryServices.AccountManagement;
using Common.Communication.Responses;
using Common.Connections;
using Server.Console.Queries;
using Server.Console.ResponseSenders;

namespace Server.Console.Executors
{
    public class ConnectionExecutor : ITaskExecutor
    {
        private readonly ConnectionQuery _query;
        private readonly IResponseSender _responseSender; 

        public ConnectionExecutor(ConnectionQuery query, IResponseSender responseSender)
        {
            Id = Guid.NewGuid();
            _query = query;
            _responseSender = responseSender;
        }

        public Guid Id { get; private set; }

        public QueryBase Query { get { return _query; } }
        
        public void Execute()
        {
            var queryFilter = new UserPrincipal(new PrincipalContext(ContextType.Machine), _query.UserName, _query.Password, true);
            var searcher = new PrincipalSearcher(queryFilter);
            var result = searcher.FindOne();

            var response = new ConnectionResponse { Id = _query.Id };
            if (result != null)
            {
                var sessionId = Guid.NewGuid();
                ConnectionsStorage.Instanse.Add(sessionId);
                response.Accepted = true;
                response.SessionId = sessionId;
            }

            _responseSender.Send(response, _query.ResponseEndPoint);
        }
    }
}