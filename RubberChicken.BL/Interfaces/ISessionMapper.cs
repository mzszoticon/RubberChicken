using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RubberChicken.Shell")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Wdh.RubberChicken.BL.Interfaces
{
    internal interface ISessionMapper
    {
        string StartOrGetSession(string sessionId);

        bool TryGetExistingSession(string sessionId, out string mediumSessionId);

        void CloseSession(string sessionId);
    }
}
