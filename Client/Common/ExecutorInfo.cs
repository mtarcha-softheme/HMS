// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System;
using Client.Model;
using Client.Model.Executors;

namespace Client.Common
{
    public class ExecutorInfo : NotifyPropertyChanged
    {
        private ExecutorStatus _status;
        private DateTime _creationTime;
        private DateTime _startTime;
        private DateTime _endTime;
        
        public ExecutorStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }
        
        public DateTime CreationTime
        {
            get { return _creationTime; }
            set
            {
                _creationTime = value;
                OnPropertyChanged("CreationTime");
            }
        }

        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                OnPropertyChanged("StartTime");
            }
        }

        public DateTime EndTime
        {
            get { return _endTime; }
            set
            {
                _endTime = value;
                OnPropertyChanged("EndTime");
            }
        }        
    }
}