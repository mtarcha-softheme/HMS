// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Communication.Responses;
using Server.Console.Queries;
using Server.Console.ResponseSenders;

namespace Server.Console.Executors
{
    public class FileNamesSearchExecutor : ITaskExecutor
    {
        private static int MaxThreads = 10;
        private readonly GetFileNamesQuery _query;
        private readonly IResponseSender _responseSender;
        private readonly StringBuilder _searchResult;

        public FileNamesSearchExecutor(GetFileNamesQuery query, IResponseSender responseSender)
        {
            _query = query;
            _responseSender = responseSender;
            Id = Guid.NewGuid();
            _searchResult = new StringBuilder();
        }

        public Guid Id { get; private set; }

        public QueryBase Query { get { return _query; } }

        public void Execute()
        {
            var drives = string.IsNullOrEmpty(_query.VolumeName) ? Environment.GetLogicalDrives() : new[] { _query.VolumeName };

            foreach (var di in drives.Select(dr => new DriveInfo(dr)))
            {
                if (!di.IsReady)
                {
                    _searchResult.AppendLine(string.Format("The drive {0} could not be read", di.Name));
                    continue;
                }

                var rootDir = di.RootDirectory;
                WalkDirectoryTree(rootDir, _query.NameSubstring, 0);
            }

            SendResponse();
        }

        private void WalkDirectoryTree(DirectoryInfo root, string partialName, int runningTasks)
        {
            FileInfo[] files = null;

            try
            {
                files = root.GetFiles("*" + partialName + "*.*");
            }
            catch { }

            if (files != null)
            {
                foreach (var fi in files)
                {
                    try
                    {
                        _searchResult.AppendLine(fi.FullName);
                    }
                    catch (Exception)
                    {
                        _searchResult.AppendLine(string.Format("...to long path\\{0}", fi.Name));
                    }
                    
                }

                var subDirs = root.GetDirectories();

                foreach (var dirInfo in subDirs)
                {
                    System.Console.WriteLine(runningTasks);
                    if (runningTasks < MaxThreads)
                    {
                        var task = new Task(() => WalkDirectoryTree(dirInfo, partialName, ++runningTasks));

                        task.Start();
                        Task.WaitAll(task);
                        --runningTasks;
                    }
                    else
                    {
                        WalkDirectoryTree(dirInfo, partialName, runningTasks);
                    }
                }
            }
        }

        private void SendResponse()
        {
            var response = new GetFileNamesResponse
                           {
                               Id = _query.Id,
                               SearchResult = _searchResult.ToString()
                           };

            _responseSender.Send(response, _query.ResponseEndPoint);
        }
    }
}