// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Common.Communication.Responses;

namespace Client.Model
{
    public class FileReceiver : DataReceiver<SentFile>
    {
        protected override SentFile GetData(Socket socket)
        {
            var metaData = new byte[8];
            socket.Receive(metaData, 8, SocketFlags.None);

            var result = (DownloadResult)BitConverter.ToInt32(metaData, 0);
            var nameLength = BitConverter.ToInt32(metaData, 4);

            var nameData = new byte[nameLength];
            socket.Receive(nameData, nameLength, SocketFlags.None);
            var fileName = Encoding.UTF8.GetString(nameData);

            if (result == DownloadResult.FileNotExist || result == DownloadResult.Failed)
            {
                return new SentFile
                       {
                           DownloadResult = result,
                           Path = fileName
                       };
            }

            var fileFullName = @"D:\HMS\" + fileName;
            Directory.CreateDirectory(Path.GetDirectoryName(fileFullName));

            var readBytes = -1;
            var buffer = new byte[1024];
            using (var bWrite = new BinaryWriter(File.Open(fileFullName, FileMode.CreateNew)))
            {
                while (readBytes != 0)
                {
                    readBytes = socket.Receive(buffer, 1024, SocketFlags.None);
                    bWrite.Write(buffer, 0, readBytes);
                }
            }

            return new SentFile
            {
                DownloadResult = DownloadResult.Succeded,
                Path = fileFullName
            };
        }

        protected override DataReceivedEventArgs<SentFile> CreateEventArgs(SentFile data)
        {
            return new DataReceivedEventArgs<SentFile>(data);
        }
    }
}