using Wdh.RubberChicken.BL.Interfaces;
using Wdh.RubberChicken.Logging.Interfaces;

namespace Wdh.RubberChicken.BL.Decorators
{
    internal sealed class ImportantServiceDecorator : IImportantService
    {
        private readonly ImportantService service;
        private readonly IActionQueue actionQueue;
        private readonly ILogging logging;

        public ImportantServiceDecorator(ImportantService service, IActionQueue actionQueue, ILogging logging)
        {
            this.service = service;
            this.actionQueue = actionQueue;
            this.logging = logging;
        }

        public void Duplicate(string sessionId)
        {
            actionQueue.QueueCommand(sessionId, () =>
            {
                logging.Log($"Starting Duplicate on {sessionId}");
                service.Duplicate(sessionId);
                logging.Log($"Duplicate on {sessionId} done");
            });
        }

        public string GetValue(string sessionId)
        {
            return actionQueue.QueueQuery(sessionId, () =>
            {
                logging.Log($"Starting Duplicate on {sessionId}");
                var result = service.GetValue(sessionId);
                logging.Log($"Duplicate on {sessionId} done");
                return result;
            });
        }

        public void SetInitial(string sessionId, string data)
        {
            actionQueue.QueueCommand(sessionId, () =>
            {
                logging.Log($"Starting Duplicate on {sessionId}");
                service.SetInitial(sessionId, data);
                logging.Log($"Duplicate on {sessionId} done");
            });
        }

        public void Truncate(string sessionId, int number)
        {
            logging.Log($"Starting Duplicate on {sessionId}");
            service.Truncate(sessionId, number);
            logging.Log($"Duplicate on {sessionId} done");
        }
    }
}
