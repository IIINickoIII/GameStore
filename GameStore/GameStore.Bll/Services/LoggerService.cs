using System.Diagnostics.CodeAnalysis;
using GameStore.Bll.Interfaces;
using NLog;

namespace GameStore.Bll.Services
{
    [ExcludeFromCodeCoverage]
    public class LoggerService : ILoggerService
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public void LogError(string message)
        {
            Logger.Error(message);
        }
    }
}