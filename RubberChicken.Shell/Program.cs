using Ninject;
using Wdh.RubberChicken.Application.Interfaces;
using Wdh.RubberChicken.Logging.Interfaces;
using Wdh.RubberChicken.Shell;

namespace RubberChicken.Shell
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using (var container = DiFactory.GetContainer())
            {
                var logging = container.Get<ILogging>();
                logging.Log("Created container");

                var application = container.Get<IApplication>();
                application.DoSomeWork(10);

                logging.Log("Created appl4ication");
                logging.Log("Finished!");


                logging.Log("All done!");
            }
        }
    }
}
