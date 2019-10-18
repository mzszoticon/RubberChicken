using System.Collections.Concurrent;
using Wdh.RubberChicken.BL.Interfaces;
using Wdh.RubberChicken.DAL.Interfaces;

namespace Wdh.RubberChicken.BL
{
    internal sealed class SessionMapper : ISessionMapper
    {
        private readonly ISessionManager sessionManager;
        private readonly ConcurrentDictionary<string, string> sessions = new ConcurrentDictionary<string, string>();

        public SessionMapper(ISessionManager sessionManager)
        {
            this.sessionManager = sessionManager;
        }

        public void CloseSession(string sessionId)
        {
            if (sessions.TryRemove(sessionId, out var storageSessionId))
                sessionManager.CloseSession(storageSessionId);
        }

        public string StartOrGetSession(string sessionId)
        {
            return sessions.GetOrAdd(sessionId, id =>
            {
                var newId = $"{id}_storageSession";
                sessionManager.StartSession(newId);
                return newId;
            });
        }

        public bool TryGetExistingSession(string sessionId, out string mediumSessionId) => sessions.TryGetValue(sessionId, out mediumSessionId);
    }
}
