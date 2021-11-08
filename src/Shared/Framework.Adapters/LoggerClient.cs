using System;
using Framework.Interfaces;
using Microsoft.Extensions.Logging;

namespace Framework.Adapters
{
    public class LoggerClient<T> : ILoggerClient<T> where T : class
    {
        private readonly ILogger<T> _logger;

        public LoggerClient(
            ILogger<T> logger
        )
        {
            _logger = logger;
        }

        public void Error(Exception exception, string message)
        {
            _logger.LogError(exception, message);
        }

        public void Error(string message)
        {
            _logger.LogError(message);
        }

        public void Info(string message)
        {
            _logger.LogInformation(message);
        }

        public void Trace(string message)
        {
            _logger.LogTrace(message);
        }
    }
}