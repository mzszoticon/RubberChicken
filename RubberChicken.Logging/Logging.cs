using System;
using Wdh.RubberChicken.Logging.Interfaces;

namespace Wdh.RubberChicken.Logging
{
    internal class Logging : ProducerConsumerBase<string>, ILogging
    {
        public void Log(string message)
        {
            Enqueue(message);
        }

        public void Log(object objectToLog)
        {
            Enqueue($"{{{objectToLog}}}");
        }

        protected override void Process(string workUnit)
        {
            Console.WriteLine(workUnit);
        }
    }
}
