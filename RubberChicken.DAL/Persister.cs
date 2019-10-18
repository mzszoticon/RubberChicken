using Wdh.RubberChicken.DAL.Interfaces;
using Wdh.RubberChicken.Logging.Interfaces;

namespace Wdh.RubberChicken.DAL
{
    internal class Persister : IPersister, IAccessor
    {
        private readonly ISessionProvider sessionProvider;
        private readonly ILogging logging;

        public Persister(ISessionProvider sessionProvider, ILogging logging)
        {
            this.sessionProvider = sessionProvider;
            this.logging = logging;
        }

        public string GetData(string sessionId)
        {
            logging.Log($"Requesting value for session {sessionId}");
            var data = this.sessionProvider.GetSession(sessionId).GetData();
            logging.Log(data);
            return data;
        }

        public void SetData(string sessionId, string data)
        {
            logging.Log($"Setting data to session {sessionId}: {{{data}}}");
            this.sessionProvider.GetSession(sessionId).SetData(data);
        }
    }
}
