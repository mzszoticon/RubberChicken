using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Wdh.RubberChicken.DAL.Interfaces;

[assembly: InternalsVisibleTo("RubberChicken.Shell")]
[assembly: InternalsVisibleTo("RubberChicken.Tests")]
namespace Wdh.RubberChicken.DAL
{
    internal class SessionManager : ISessionManager, ISessionProvider
    {
        private readonly Dictionary<string, ISession> sessionStorage = new Dictionary<string, ISession>();

        public void CloseSession(string sessionId)
        {
            if (!sessionStorage.Remove(sessionId))
                throw new InvalidOperationException();
        }

        public void StartSession(string sessionId)
        {
            sessionStorage[sessionId] = new Session();
        }

        ISession ISessionProvider.GetSession(string sessionId)
        {
            return sessionStorage[sessionId];
        }
    }
}
