using System;
using System.Collections.Generic;
using System.Threading;
using Wdh.RubberChicken.Application.Interfaces;
using Wdh.RubberChicken.BL.Interfaces;
using Wdh.RubberChicken.Logging.Interfaces;

namespace Wdh.RubberChicken.Application
{
    public sealed class Application : IApplication
    {
        private readonly IImportantService service;
        private readonly ILogging logging;

        public Application(IImportantService service, ILogging logging)
        {
            this.service = service;
            this.logging = logging;
        }

        public void Dispose()
        {
        }

        public void DoSomeWork(int units)
        {
            List<Thread> threads = new List<Thread>(units);

            for (int i = 0; i < units; ++i)
            {
                var thread = new Thread(DoWork);
                thread.Start(Guid.NewGuid().ToString());
                threads.Add(thread);
            }

            threads.ForEach(t => t.Join());
        }

        private void DoWork(object obj)
        {
            var sessionId = obj as string;
            logging.Log("Starting work on session " + sessionId);
            service.SetInitial(sessionId, Thread.CurrentThread.ManagedThreadId + " thread says hello!");
            service.Duplicate(sessionId);
            service.Truncate(sessionId, 5);
            logging.Log($"Final value for session {{{sessionId}}} is {{{service.GetValue(sessionId)}}}");
            logging.Log("Work on session " + sessionId + " finished");
        }
    }
}
