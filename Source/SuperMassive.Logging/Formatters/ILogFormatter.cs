
namespace SuperMassive.Logging.Formatters
{
    /// <summary>
    /// Represents the interface for formatting log entry messsages.
    /// </summary>
    public interface ILogFormatter
    {
        /// <summary>
        /// Formats a log entry and return a string to be outputted.
        /// </summary>
        /// <param name="log">Log entry to format.</param>
        /// <returns>String representing the log entry.</returns>
        string Format(LogEntry log);
    }
}
