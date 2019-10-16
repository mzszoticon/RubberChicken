namespace Wdh.RubberChicken.DAL.Interfaces
{
    public interface ISessionManager
    {
        void StartSession(string sessionId);
        void CloseSession(string sessionId);
    }
}
