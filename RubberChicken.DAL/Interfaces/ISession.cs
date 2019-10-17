using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RubberChicken.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Wdh.RubberChicken.DAL.Interfaces
{
    internal interface ISession
    {
        string GetData();
        void SetData(string data);
    }
}