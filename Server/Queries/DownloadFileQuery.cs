// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using Common.Communication;

namespace Server.Console.Queries
{
    public class DownloadFileQuery : QueryBase
    {
        public DownloadFileQuery(CustomIPEndPoint responsEndPoint) : base(responsEndPoint)
        {
        }

        public string FileFullName { get; set; }
    }
}