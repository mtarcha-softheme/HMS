// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using Client.Common.Queries;
using Common.Communication.Requests;
using Common.Communication.Responses;

namespace Client.Model.Executors
{
    public class GetFileNamesExecutor : ExecutorBase<GetFileNamesQuery, GetFileNamesResponse>
    {       
        public GetFileNamesExecutor(GetFileNamesQuery query, NetworkItemsFactory networkItemsFactory) 
            : base(query, networkItemsFactory) { }
                
        protected override RequestBase CreateRequest()
        {
            return new GetFileNamesRequest
            {
                VolumeName = Query.VolumeName,
                NameSubstring = Query.NameSubstring,
            };
        }

        protected override ExecutorStatus GetResultStatus(GetFileNamesResponse data)
        {
            return string.IsNullOrEmpty(data.ErrorMessage)
                ? ExecutorStatus.Succeded
                : ExecutorStatus.Failed;
        }

        protected override DataReceiver<GetFileNamesResponse> GetWaiter()
        {
            return NetworkItemsFactory.GetResponseReceiver<GetFileNamesResponse>();
        }        
    }
}