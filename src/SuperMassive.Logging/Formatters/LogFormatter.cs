
namespace SuperMassive.Logging.Formatters
{
    /// <summary>
    /// Abstract implememtation of the <see cref="ILogFormatter"/> interface.
    /// </summary>
    public abstract class LogFormatter : ILogFormatter
    {
        /// <summary>
        /// Formats a log entry and return a string to be outputted.
        /// </summary>
        /// <param name="log">Log entry to format.</param>
        /// <returns>A string representing the log entry.</returns>
        public abstract string Format(LogEntry log);
    }
}
