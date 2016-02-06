// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using Client.Common.Queries;
using Common.Communication.Requests;
using Common.Communication.Responses;

namespace Client.Model.Executors
{
    public class ConnectExecutor : ExecutorBase<ConnectionQuery, ConnectionResponse>
    {
        public ConnectExecutor(ConnectionQuery query, NetworkItemsFactory networkItemsFactory) : base(query, networkItemsFactory) {}
        
        protected override RequestBase CreateRequest()
        {
            return new ConnectionRequest
                   {
                       UserName = Query.UserName,
                       Password = Query.Password,
                   };
        }
        
        protected override DataReceiver<ConnectionResponse> GetWaiter()
        {            
            return NetworkItemsFactory.GetResponseReceiver<ConnectionResponse>();
        }

        protected override ExecutorStatus GetResultStatus(ConnectionResponse data)
        {
            return data.Accepted ? ExecutorStatus.Succeded : ExecutorStatus.Failed;
        }
    }
}