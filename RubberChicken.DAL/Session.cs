using Wdh.RubberChicken.DAL.Interfaces;

namespace Wdh.RubberChicken.DAL
{
    internal sealed class Session : ISession
    {
        private object data;

        public object GetData()
        {
            return data;
        }

        public void SetData(object data)
        {
            this.data = data;
        }
    }
}
