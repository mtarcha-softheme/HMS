// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using Client.Common;

namespace Client.ViewModel
{
    public class ProcessingInfo : NotifyPropertyChanged
    {
        private string _name;
        private ExecutorInfo _info;
        private string _result;

        public string ProcessName
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("ProcessName");
            }
        }

        public ExecutorInfo Info
        {
            get { return _info; }
            set
            {
                _info = value;
                OnPropertyChanged("Info");
            }
        }

        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged("Result");
            }
        }
    }
}