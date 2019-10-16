using Ninject;
using System;
using Wdh.RubberChicken.Shell;

namespace RubberChicken.Shell
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Module.GetContainer();

            container.Get<Type>();
        }
    }
}
