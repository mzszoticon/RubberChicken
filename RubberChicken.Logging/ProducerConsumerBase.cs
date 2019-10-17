using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Wdh.RubberChicken.Logging
{
    public abstract class ProducerConsumerBase<TContained> : IDisposable
    {
        private BlockingCollection<TContained> workUnits = new BlockingCollection<TContained>();
        private Thread workerThread;

        protected ProducerConsumerBase()
        {
            workerThread = new Thread(Worker);
            workerThread.Start();
        }

        private void Worker(object obj)
        {
            foreach (var workUnit in workUnits.GetConsumingEnumerable())
            {
                Process(workUnit);
            }
            workUnits.Dispose();
        }

        protected void Enqueue(TContained workUnit)
        {
            workUnits.Add(workUnit);
        }

        protected abstract void Process(TContained workUnit);

        public void Dispose()
        {
            workUnits.CompleteAdding();
            workerThread.Join();
        }

        public override string ToString()
        {
            return $"{{{this.GetType().FullName}}}: worker threadId: {workerThread.ManagedThreadId}";
        }
    }
}
