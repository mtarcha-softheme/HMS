// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Common.Communication.Responses;
using Server.Console.Queries;

namespace Server.Console.Executors
{
    public class DownloadFileExecutor : ITaskExecutor
    {
        private readonly DownloadFileQuery _query;

        public DownloadFileExecutor(DownloadFileQuery query)
        {
            _query = query;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        public QueryBase Query { get { return _query; } }

        public void Execute()
        {
            //// byte array where 0-3 bytes contans int value of DownloadResult
            //// 4-7 bytes contains int value (n) of fileName length
            //// 8-(n+8) bytes contains fileName
            //// (n+8)-... contains file content.
            byte[] clientData;

            if (File.Exists(_query.FileFullName))
            {
                try
                {
                    var fileInfo = new FileInfo(_query.FileFullName);
                    var shortName = Encoding.UTF8.GetBytes(fileInfo.Name);
                    var fileContent = File.ReadAllBytes(_query.FileFullName);

                    clientData = new byte[4 + 4 + shortName.Length + fileContent.Length];

                    BitConverter.GetBytes((int)DownloadResult.Succeded).CopyTo(clientData, 0);
                    BitConverter.GetBytes(shortName.Length).CopyTo(clientData, 4);
                    shortName.CopyTo(clientData, 8);
                    fileContent.CopyTo(clientData, 8 + shortName.Length);
                }
                catch
                {
                    clientData = new byte[4];
                    BitConverter.GetBytes((int)DownloadResult.Failed).CopyTo(clientData, 0);
                }
            }
            else
            {
                var name = Encoding.UTF8.GetBytes(_query.FileFullName);

                clientData = new byte[4 + 4 + name.Length];
                BitConverter.GetBytes((int)DownloadResult.FileNotExist).CopyTo(clientData, 0);
                BitConverter.GetBytes(name.Length).CopyTo(clientData, 4);
                name.CopyTo(clientData, 8);
            }

            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(_query.ResponseEndPoint.Ip, _query.ResponseEndPoint.Port));
            socket.Send(clientData);
            socket.Shutdown(SocketShutdown.Send);
        }
    }
}