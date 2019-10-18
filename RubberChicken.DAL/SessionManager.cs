using System;
using System.Collections.Concurrent;
using Wdh.RubberChicken.DAL.Interfaces;

namespace Wdh.RubberChicken.DAL
{
    internal class SessionManager : ISessionManager, ISessionProvider
    {
        private readonly ConcurrentDictionary<string, ISession> sessionStorage = new ConcurrentDictionary<string, ISession>();

        public void CloseSession(string sessionId)
        {
            if (!sessionStorage.TryRemove(sessionId, out _))
                throw new InvalidOperationException();
        }

        public void StartSession(string sessionId)
        {
            sessionStorage.AddOrUpdate(sessionId, s => new Session(), (s, s1) => new Session());
        }

        ISession ISessionProvider.GetSession(string sessionId)
        {
            if (!sessionStorage.TryGetValue(sessionId, out var ret))
                throw new InvalidOperationException();

            return ret;
        }
    }
}
