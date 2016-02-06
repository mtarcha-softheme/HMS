// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Client.Common;
using Client.Common.Queries;
using Common.Communication.Responses;

namespace Client.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _connectionInfo;
        private ObservableCollection<ProcessingInfo> _processingInfos;
        private ObservableCollection<string> _downloads;
        private ConnectionQuery _connectionQuery;
        private GetFileNamesQuery _getFileNamesQuery;
        private readonly DownloadFileQuery _downloadFileQuery;
        private readonly Model.Model _model;
        private Connection _currentConnection;

        public MainViewModel(Model.Model model)
        {
            _model = model;
            _connectionQuery = new ConnectionQuery();
            _getFileNamesQuery = new GetFileNamesQuery();
            _downloadFileQuery = new DownloadFileQuery();
            _downloads = new ObservableCollection<string>();
            _processingInfos = new ObservableCollection<ProcessingInfo>();
        }

        public string ConnectionInfo
        {
            get { return _connectionInfo; }

            set
            {
                _connectionInfo = value;
                OnPropertyChanged("ConnectionInfo");
            }
        }

        public ConnectionQuery ConnectionQuery
        {
            get { return _connectionQuery; }

            set
            {
                _connectionQuery = value;
                OnPropertyChanged("ConnectionQuery");
            }
        }

        public ObservableCollection<ProcessingInfo> ProcessingInfos
        {
            get { return _processingInfos; }

            set
            {
                _processingInfos = value;
                OnPropertyChanged("ProcessingInfos");
            }
        }

        public GetFileNamesQuery GetFileNamesQuery
        {
            get { return _getFileNamesQuery; }

            set
            {
                _getFileNamesQuery = value;
                OnPropertyChanged("GetFileNamesQuery");
            }
        }
        
        public ObservableCollection<string> Downloads
        {
            get { return _downloads; }

            set
            {
                _downloads = value;
                OnPropertyChanged("Downloads");
            }
        }

        public DownloadFileQuery DownloadFileQuery
        {
            get { return _downloadFileQuery; }
        }


        public ICommand Connect
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var executor = _model.CreateConnectExecutor(ConnectionQuery);
                    executor.Execute();
                    ConnectionInfo = string.Format("Connecting to {0}", ConnectionQuery.Ip);
                    executor.Ended += (_, __) =>
                                      {
                                          var response = executor.Result;
                                          if (response.Accepted)
                                          {
                                              ConnectionInfo = string.Format("Connected to {0}", ConnectionQuery.Ip); ;
                                              _currentConnection = new Connection(response.SessionId, ConnectionQuery.Ip, ConnectionQuery.Port);
                                          }
                                          else
                                          {
                                              ConnectionInfo = string.Format("Failed connect to {0}", ConnectionQuery.Ip);
                                          }
                                      };
                });
            }
        }

        public ICommand GetFileNamesByPartialName
        {
            get
            {
                return new RelayCommand(() =>
                                        {
                                            GetFileNamesQuery.Ip = _currentConnection.Ip;
                                            GetFileNamesQuery.Port = _currentConnection.Port;
                                            GetFileNamesQuery.SessionId = _currentConnection.SessionId;

                                            var executor = _model.CreateFileNamesExecutor(GetFileNamesQuery);
                                            executor.Execute();
                                            var processingInfo = new ProcessingInfo
                                                             {
                                                                 ProcessName = string.Format("File names that mutch '{0}'", GetFileNamesQuery.NameSubstring),
                                                                 Info = new ExecutorInfo
                                                                 {
                                                                     CreationTime = executor.CreationTime,
                                                                     StartTime = executor.StartTime,
                                                                     Status = executor.Status,
                                                                 }
                                            };

                                            Application.Current.Dispatcher.Invoke(() => ProcessingInfos.Insert(0, processingInfo));
                                            executor.Ended += (_, args) =>
                                                              {
                                                                  processingInfo.Result = string.Format("Elapsed time: {0} \n Search result: \n {1}", executor.EndTime - executor.StartTime, executor.Result.SearchResult);
                                                                  processingInfo.Info = new ExecutorInfo
                                                                  {
                                                                      CreationTime = executor.CreationTime,
                                                                      StartTime = executor.StartTime,
                                                                      EndTime = executor.EndTime,
                                                                      Status = executor.Status,
                                                                  };
                                                              };
                                        });
            }
        }

        public ICommand DownloadFile
        {
            get
            {
                return new RelayCommand(() =>
                {
                    DownloadFileQuery.Ip = _currentConnection.Ip;
                    DownloadFileQuery.Port = _currentConnection.Port;
                    DownloadFileQuery.SessionId = _currentConnection.SessionId;

                    var executor = _model.CreateDownloadFileExecutor(DownloadFileQuery);
                    executor.Execute();
                    var processingInfo = new ProcessingInfo
                    {
                        ProcessName = string.Format("Download file '{0}'", DownloadFileQuery.FileFullName),
                        Info = new ExecutorInfo
                        {
                            CreationTime = executor.CreationTime,
                            StartTime = executor.StartTime,                            
                            Status = executor.Status,                            
                        }
                    };

                    Application.Current.Dispatcher.Invoke(() => ProcessingInfos.Insert(0, processingInfo));
                    executor.Ended += (_, __) =>
                                      {
                                          var result = executor.Result;
                                          if (result.DownloadResult == DownloadResult.Succeded)
                                          {
                                              Application.Current.Dispatcher.Invoke(() => Downloads.Insert(0, result.Path));
                                          }

                                          processingInfo.Result = string.Format("Elapsed time: {0} \n '{1}'", executor.EndTime - executor.StartTime, result.DownloadResult);
                                          processingInfo.Info = new ExecutorInfo
                                          {
                                              CreationTime = executor.CreationTime,
                                              StartTime = executor.StartTime,
                                              EndTime = executor.EndTime,
                                              Status = executor.Status,
                                          };
                                      };
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}