using Ninject;
using Ninject.Modules;
using Wdh.RubberChicken.DAL.Interfaces;

namespace Wdh.RubberChicken.DAL
{
    public sealed class DalModule : NinjectModule
    {
        public override void Load()
        {
            var sessionStorage = new SessionManager();
            Bind<ISessionManager>().ToMethod(c => sessionStorage);
            Bind<ISessionProvider>().ToMethod(c => sessionStorage);
            Bind<Persister>().ToSelf().InSingletonScope();
            Bind<IPersister>().ToMethod(c => c.Kernel.Get<Persister>());
            Bind<IAccessor>().ToMethod(c => c.Kernel.Get<Persister>());
        }
    }
}
