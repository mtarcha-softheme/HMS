// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

namespace Client.Common.Queries
{
    public class GetFileNamesQuery : Query
    {
        private string _volumeName;
        private string _nameSubstring;

        public string VolumeName
        {
            get { return _volumeName; }
            set
            {
                _volumeName = value;
                OnPropertyChanged("VolumeName");
            }
        }
        public string NameSubstring
        {
            get { return _nameSubstring; }
            set
            {
                _nameSubstring = value;
                OnPropertyChanged("NameSubstring");
            }
        }
    }
}