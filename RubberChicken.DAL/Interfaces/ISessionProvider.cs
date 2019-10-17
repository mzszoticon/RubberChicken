using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RubberChicken.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Wdh.RubberChicken.DAL.Interfaces
{
    internal interface ISessionProvider
    {
        ISession GetSession(string sessionId);
    }
}
