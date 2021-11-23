using NLog;
using RendezVousPolyclinique.Infra.Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendezVousPolyclinique.Infra.Logging
{
    public class LoggerManager : ILoggerManager
    {

        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }
        public void LogError(string message)
        {
            logger.Error(message);
        }
        public void LogInfo(string message)
        {
            logger.Info(message);
        }
        public void LogWarning(string message)
        {

            logger.Warn(message);
        }

    }
}
