using System;
namespace Framework.Interfaces
{
    public interface ILoggerClient<T> where T : class
    {
        void Info(string message);

        void Error(Exception exception, string message);

        void Error(string message);

        void Trace(string message);
    }
}