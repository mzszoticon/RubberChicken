using System;
using System.Collections.Generic;
using System.Threading;
using Wdh.RubberChicken.BL.Interfaces;
using Wdh.RubberChicken.Logging;
using Wdh.RubberChicken.Logging.Interfaces;

namespace Wdh.RubberChicken.BL
{
    internal class ActionQueue : IActionQueue
    {
        public ActionQueue(ILogging logging)
        {
            this.logging = logging;
        }

        Dictionary<string, QueueWorker> workers = new Dictionary<string, QueueWorker>();
        private readonly ILogging logging;

        public void QueueCommand(string sessionId, Action action)
        {
            logging.Log($"QueueCommand called on {sessionId}");
            var worker = GetQueueWorker(sessionId);
            worker.QueueAction(action);
        }

        private QueueWorker GetQueueWorker(string sessionId)
        {
            if (workers.TryGetValue(sessionId, out var worker))
            {
                logging.Log($"Reusing worker {worker}");
                return worker;
            }

            worker = new QueueWorker();
            logging.Log($"Creating new worker {worker}");
            return worker;
        }

        public TRet QueueQuery<TRet>(string sessionId, Func<TRet> function)
        {
            logging.Log($"QueueFunction called on {sessionId}");
            var worker = GetQueueWorker(sessionId);
            return worker.QueueFunction(function);
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
                    return result;
                }
            }
        }
    }
}
