// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

namespace Client.Common.Queries
{
    public class DownloadFileQuery : Query
    {
        private string _name;

        public string FileFullName
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("FileFullName");
            }
        }
    }
}