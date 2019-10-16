namespace Wdh.RubberChicken.DAL.Interfaces
{
    internal interface ISessionProvider
    {
        ISession GetSession(string sessionId);
    }
}
