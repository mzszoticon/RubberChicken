using Ninject;
using Wdh.RubberChicken.Application;
using Wdh.RubberChicken.BL;
using Wdh.RubberChicken.DAL;
using Wdh.RubberChicken.Logging;

namespace Wdh.RubberChicken.Shell
{
    public static class DiFactory
    {
        public static KernelBase GetContainer()
        {
            var kernel = new StandardKernel(
                new ApplicationModule(),
                new BlModule(),
                new DalModule(), 
                new LoggingModule());

            return kernel;
        }
    }
}
