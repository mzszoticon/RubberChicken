using Ninject;
using Ninject.Modules;
using Ninject.Syntax;
using Wdh.RubberChicken.DAL;
using Wdh.RubberChicken.DAL.Interfaces;

namespace Wdh.RubberChicken.Shell
{
    public sealed class Module : NinjectModule
    {
        public override void Load()
        {
            var sessionStorage = new SessionManager();
            Bind<ISessionManager>().ToMethod(c => sessionStorage);
            Bind<ISessionProvider>().ToMethod(c => sessionStorage);
        }

        public static IResolutionRoot GetContainer()
        {
            var kernel = new StandardKernel(new Module());

            return kernel;
        }
    }
}
