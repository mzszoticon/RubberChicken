using Ninject.Modules;
using Wdh.RubberChicken.Logging.Interfaces;

namespace Wdh.RubberChicken.Logging
{
    public class LoggingModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogging>().To<Logging>().InSingletonScope();
        }
    }
}
