using System;

namespace Wdh.RubberChicken.BL.Interfaces
{
    public interface IActionQueue
    {
        void QueueCommand(string sessionId, Action action);

        TRet QueueQuery<TRet>(string sessionId, Func<TRet> function);
    }
}
