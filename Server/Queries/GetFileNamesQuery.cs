// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using Common.Communication;

namespace Server.Console.Queries
{
    public class GetFileNamesQuery : QueryBase
    {
        public GetFileNamesQuery(CustomIPEndPoint responseEndPoint) : base(responseEndPoint)
        {
        }

        public string VolumeName { get; set; }

        public string NameSubstring { get; set; }
    }
}