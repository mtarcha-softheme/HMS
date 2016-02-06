// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using Client.Common.Queries;
using Client.Model.Executors;

namespace Client.Model
{
    public interface IModel
    {
        ConnectExecutor CreateConnectExecutor(ConnectionQuery query);

        GetFileNamesExecutor CreateFileNamesExecutor(GetFileNamesQuery query);

        DownloadFileExecutor CreateDownloadFileExecutor(DownloadFileQuery query);
    }
}