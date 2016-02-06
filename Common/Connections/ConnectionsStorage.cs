// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Timers;

namespace Common.Connections
{
    public sealed class ConnectionsStorage
    {
        //todo: remove if some time sessionId was not asked
        public static Lazy<ConnectionsStorage> _instanse = new Lazy<ConnectionsStorage>(() => new ConnectionsStorage());  
        private readonly List<Guid> _sessions;
        /////private readonly Timer _cleaningTimer;
        ////private bool _disposed;

        private ConnectionsStorage()
        {
            _sessions = new List<Guid>();
        }

        public static ConnectionsStorage Instanse { get { return _instanse.Value; } }

        public void Add(Guid sessionId)
        {
            lock (_sessions)
            {
                _sessions.Add(sessionId);
            }
        }

        public bool Contains(Guid sessionId)
        {
            lock (_sessions)
            {
                return _sessions.Contains(sessionId);
            }
        }

        public void Remove(Guid sessionId)
        {
            lock (_sessions)
            {
                _sessions.Remove(sessionId);
            }
        }
    }
}