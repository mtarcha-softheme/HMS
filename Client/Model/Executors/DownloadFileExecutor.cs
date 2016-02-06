// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using Client.Common.Queries;
using Common.Communication.Requests;
using Common.Communication.Responses;

namespace Client.Model.Executors
{
    public class DownloadFileExecutor : ExecutorBase<DownloadFileQuery, SentFile>
    {
        public DownloadFileExecutor(DownloadFileQuery query, NetworkItemsFactory networkItemsFactory) : base(query, networkItemsFactory) {}
        
        protected override RequestBase CreateRequest()
        {
            return new DownloadFileRequest
            {
                FileFullName = Query.FileFullName
            };
        }
        
        protected override DataReceiver<SentFile> GetWaiter()
        {
            return NetworkItemsFactory.GetFileReceiver();
        }
        
        protected override ExecutorStatus GetResultStatus(SentFile data)
        {
            return data.DownloadResult == DownloadResult.Succeded ? ExecutorStatus.Succeded : ExecutorStatus.Failed;
        }
    }
}