// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

namespace Client.Common.Queries
{
    public class ConnectionQuery : Query
    {
        private string _name;
        private string _password;

        public string UserName
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("UserName");
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
    }
}