namespace Wdh.RubberChicken.DAL.Interfaces
{
    internal interface ISession
    {
        object GetData();
        void SetData(object data);
    }
}