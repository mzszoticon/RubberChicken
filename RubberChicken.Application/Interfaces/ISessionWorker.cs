namespace Wdh.RubberChicken.Application.Interfaces
{
    public interface ISessionWorker
    {
        string Work(string sessionId, string initialData);
    }
}