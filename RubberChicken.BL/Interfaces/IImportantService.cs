using System;
using System.Collections.Generic;
using System.Text;

namespace Wdh.RubberChicken.BL.Interfaces
{
    public interface IImportantService
    {
        void SetInitial(string sessionId, string data);

        void Duplicate(string sessionId);

        void Truncate(string sessionId, int number);

        string GetValue(string sessionId);
    }
}
