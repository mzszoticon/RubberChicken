using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Wdh.RubberChicken.BL.Interfaces;
using Wdh.RubberChicken.Logging;
using Wdh.RubberChicken.Logging.Interfaces;

namespace Wdh.RubberChicken.BL
{
    internal class ActionQueue : IActionQueue, IDisposable
    {
        public ActionQueue(ILogging logging)
        {
            this.logging = logging;
        }

        ConcurrentDictionary<string, QueueWorker> workers = new ConcurrentDictionary<string, QueueWorker>();
        private readonly ILogging logging;

        public void QueueCommand(string sessionId, Action action)
        {
            logging.Log($"QueueCommand called on {sessionId}");
            var worker = GetQueueWorker(sessionId);
            worker.QueueAction(action);
        }

        private QueueWorker GetQueueWorker(string sessionId)
        {
            return workers.GetOrAdd(sessionId, s => new QueueWorker());
        }

        public TRet QueueQuery<TRet>(string sessionId, Func<TRet> function)
        {
            logging.Log($"QueueFunction called on {sessionId}");
            var worker = GetQueueWorker(sessionId);
            return worker.QueueFunction(function);
        }

        public void Dispose()
        {
            workers.Values.ToList().ForEach(l => l.Dispose());
        }

        private class QueueWorker : ProducerConsumerBase<Action>
        {
            protected override void Process(Action workUnit)
            {
                workUnit();
            }

            public void QueueAction(Action action)
            {
                Enqueue(action);
            }

            public T QueueFunction<T>(Func<T> function)
            {
                using (var e = new AutoResetEvent(false))
                {
                    T result = default(T);
                    Enqueue(() =>
                    {
                        result = function();
                        e.Set();
                    });
                    e.WaitOne();
                    return result;
                }
            }
        }
    }
}
