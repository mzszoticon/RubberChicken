using System;
using System.Runtime.CompilerServices;
using Wdh.RubberChicken.BL.Interfaces;
using Wdh.RubberChicken.DAL.Interfaces;
using Wdh.RubberChicken.Logging.Interfaces;

[assembly: InternalsVisibleTo("RubberChicken.Tests")]
namespace Wdh.RubberChicken.BL
{
    internal sealed class ImportantService : IImportantService
    {
        private readonly ISessionMapper sessionManager;
        private readonly IAccessor accessor;
        private readonly IPersister persister;
        private readonly ILogging logging;

        public ImportantService(
            ISessionMapper sessionMapper,
            IAccessor accessor,
            IPersister persister,
            ILogging logging)
        {
            this.sessionManager = sessionMapper;
            this.accessor = accessor;
            this.persister = persister;
            this.logging = logging;
        }

        public void Duplicate(string sessionId)
        {
            var data = accessor.GetData(sessionManager.StartOrGetSession(sessionId));
            logging.Log("Got data: " + data);
            persister.SetData(sessionId, data + data);
        }

        public string GetValue(string sessionId)
        {
            return accessor.GetData(sessionManager.StartOrGetSession(sessionId));
        }

        public void SetInitial(string sessionId, string data)
        {
            sessionManager.StartOrGetSession(sessionId);
            persister.SetData(sessionId, data);
        }

        public void Truncate(string sessionId, int number)
        {
            if (!sessionManager.TryGetExistingSession(sessionId, out var id))
            {
                throw new InvalidOperationException();
            }

            var data = accessor.GetData(id);
            logging.Log("Got data: " + data + " for session " + sessionId);
            persister.SetData(id, data.Substring(0, number));
        }
    }
}
