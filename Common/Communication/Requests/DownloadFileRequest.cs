// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System;

namespace Common.Communication.Requests
{
    [Serializable]
    public class DownloadFileRequest : RequestBase
    {
        public string FileFullName { get; set; }
    }
}