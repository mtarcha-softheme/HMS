// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using Common.Communication.Responses;

namespace Client.Model
{
    public class SentFile
    {
        public string Path { get; set; }

        public DownloadResult DownloadResult { get; set; }
    }
}