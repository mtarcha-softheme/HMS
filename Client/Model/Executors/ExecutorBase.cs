// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System;
using System.Net;
using Client.Common.Queries;
using Common.Communication;
using Common.Communication.Requests;

namespace Client.Model.Executors
{
    public abstract class ExecutorBase<TQuery, TResult>   
        where TQuery : Query    
    {
        private readonly TQuery _query;
        private readonly NetworkItemsFactory _networkItemsFactory;

        protected ExecutorBase(TQuery query, NetworkItemsFactory networkItemsFactory)
        {
            _query = query;
            _networkItemsFactory = networkItemsFactory;

            Id = Guid.NewGuid();
            CreationTime = DateTime.Now;
            Status = ExecutorStatus.Created;
        }

        public event EventHandler<EventArgs> Ended;

        public Guid Id { get; private set; }
        
        public ExecutorStatus Status { get; protected set; }
        
        public DateTime CreationTime { get; protected set; }

        public DateTime StartTime { get; protected set; }

        public DateTime EndTime { get; protected set; }

        public int Progress { get; protected set; }
                
        public TQuery Query => _query;

        public TResult Result { get; protected set; }      
        
        protected NetworkItemsFactory NetworkItemsFactory { get { return _networkItemsFactory; } }
        
        public void Execute()
        {
            StartTime = DateTime.Now;
            Status = ExecutorStatus.Running;

            var endPoint = _networkItemsFactory.GetEndPoint();
            var request = CreateRequest();
            request.ResponseEndPoint = endPoint;
            request.SessionId = _query.SessionId;

            var remoteEndPoint = new CustomIPEndPoint { Ip = IPAddress.Parse(_query.Ip).Address, Port = _query.Port };
            _networkItemsFactory.GetRequestSender().Send(request, remoteEndPoint);

            var waiter = GetWaiter();
            waiter.Start(endPoint);
            waiter.DataReceived += OnResponseReceived;            
        }

        private void OnResponseReceived(object sender, DataReceivedEventArgs<TResult> args)
        {
            EndTime = DateTime.Now;
            Result = args.Data;
            Status = GetResultStatus(args.Data);        

            var handlers = Ended;
            if (handlers != null)
            {
                handlers(this, EventArgs.Empty);
            }
        }        

        protected abstract DataReceiver<TResult> GetWaiter();        

        protected abstract RequestBase CreateRequest();

        protected abstract ExecutorStatus GetResultStatus(TResult data);
    }
}