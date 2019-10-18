using System;
using System.Collections.Generic;
using System.Threading;
using Wdh.RubberChicken.Application.Interfaces;
using Wdh.RubberChicken.Logging.Interfaces;

namespace Wdh.RubberChicken.Application
{
    public sealed class Application : IApplication
    {
        private readonly ISessionWorker sessionWorker;
        private readonly ILogging logging;

        public Application(ISessionWorker sessionWorker, ILogging logging)
        {
            this.sessionWorker = sessionWorker;
            this.logging = logging;
        }

        public void DoSomeWork(int units)
        {
            List<Thread> threads = new List<Thread>(units);

            for (int i = 0; i < units; ++i)
            {
                var thread = new Thread(DoWork);
                thread.Start(Guid.NewGuid().ToString());
                logging.Log($"Started new thread with ID {thread.ManagedThreadId}");
                threads.Add(thread);
            }

            threads.ForEach(t => t.Join());
        }

        private void DoWork(object obj)
        {
            var sessionId = obj as string;
            sessionWorker.Work(sessionId, Thread.CurrentThread.ManagedThreadId + " thread says hello!");
        }
    }
}
