using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMassive.Logging
{
    /// LogWriter Interface
    /// </summary>
    public interface ILogWriter
    {
        /// <summary>
        /// Returns true if the logging is enabled
        /// </summary>
        /// <returns></returns>
        bool IsLoggingEnabled();
        /// <summary>
        /// Returns true if the given <see cref="LogEntry"/> should be logged.
        /// </summary>
        /// <param name="logEntry"></param>
        /// <returns></returns>
        bool ShouldLog(LogEntry logEntry);
        /// <summary>
        /// Write the <see cref="LogEntry"/>
        /// </summary>
        /// <param name="log"></param>
        void Write(LogEntry log);
    }
}
