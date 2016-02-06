using System;

namespace Common.Communication.Requests
{
    [Serializable]
    public class GetFileNamesRequest : RequestBase
    {
        public string VolumeName { get; set; }

        public string NameSubstring { get; set; }
    }
}