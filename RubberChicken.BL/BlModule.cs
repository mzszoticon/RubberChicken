using Ninject.Modules;
using Wdh.RubberChicken.BL.Decorators;
using Wdh.RubberChicken.BL.Interfaces;

namespace Wdh.RubberChicken.BL
{
    public sealed class BlModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ImportantService>().ToSelf().InSingletonScope();
            Bind<IImportantService>().To<ImportantServiceDecorator>().InSingletonScope();
            Bind<IActionQueue>().To<ActionQueue>().InSingletonScope();
            Bind<ISessionMapper>().To<SessionMapper>().InSingletonScope();
        }
    }
}
