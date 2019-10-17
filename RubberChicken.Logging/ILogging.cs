namespace Wdh.RubberChicken.Logging.Interfaces
{
    public interface ILogging
    {
        void Log(string message);
        void Log(object objectToLog);
    }
}