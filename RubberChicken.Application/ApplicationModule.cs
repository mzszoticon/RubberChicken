using Ninject.Modules;
using Wdh.RubberChicken.Application.Interfaces;

namespace Wdh.RubberChicken.Application
{
    public class ApplicationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISessionWorker>().To<SessionWorker>().InSingletonScope();
            Bind<IApplication>().To<Application>().InSingletonScope();
        }
    }
}
