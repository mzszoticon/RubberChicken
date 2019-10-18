using Wdh.RubberChicken.Application.Interfaces;
using Wdh.RubberChicken.BL.Interfaces;
using Wdh.RubberChicken.Logging.Interfaces;

namespace Wdh.RubberChicken.Application
{
    internal sealed class SessionWorker : ISessionWorker
    {
        private readonly IImportantService service;
        private readonly ILogging logging;

        public SessionWorker(IImportantService service, ILogging logging)
        {
            this.service = service;
            this.logging = logging;
        }

        public string Work(string sessionId, string initialData)
        {
            logging.Log("Starting work on session " + sessionId);
            service.SetInitial(sessionId, initialData);
            service.Duplicate(sessionId);
            service.Truncate(sessionId, 5);
            var returnValue = service.GetValue(sessionId);
            logging.Log("Work on session " + sessionId + " finished");

            return returnValue;
        }
    }
}
