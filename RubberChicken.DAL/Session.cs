using Wdh.RubberChicken.DAL.Interfaces;

namespace Wdh.RubberChicken.DAL
{
    internal sealed class Session : ISession
    {
        private string data;

        public string GetData()
        {
            return data;
        }

        public void SetData(string data)
        {
            this.data = data;
        }
    }
}
