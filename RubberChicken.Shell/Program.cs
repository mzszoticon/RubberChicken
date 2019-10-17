using Ninject;
using System;
using Wdh.RubberChicken.Shell;

namespace RubberChicken.Shell
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var container = Module.GetContainer();

            container.Get<Type>();
        }
    }
}
