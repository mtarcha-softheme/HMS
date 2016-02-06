// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System;
using System.Collections.Generic;
using Client.Common;
using Client.Common.Queries;
using Client.Model.Executors;
using Common;
using Common.Communication;

namespace Client.Model
{
    public class Model : IModel
    {
        private readonly NetworkItemsFactory _itemsFactory;
       
        public Model(IConfigurationServise configurationServise)
        {
            _itemsFactory = new NetworkItemsFactory(configurationServise);
        }
        
        public ConnectExecutor CreateConnectExecutor(ConnectionQuery query)
        {
            return new ConnectExecutor(query, _itemsFactory);
        }

        public GetFileNamesExecutor CreateFileNamesExecutor(GetFileNamesQuery query)
        {
            return new GetFileNamesExecutor(query, _itemsFactory);
        }

        public DownloadFileExecutor CreateDownloadFileExecutor(DownloadFileQuery query)
        {
            return new DownloadFileExecutor(query, _itemsFactory);
        }
    }
}