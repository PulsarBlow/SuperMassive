using Microsoft.WindowsAzure.Storage.Table;

namespace SuperMassive.Logging.AzureTable
{
    public class ApplicationLogEntity : TableEntity
    {
        #region Properties
        /// <summary>
        /// ApplicationName
        /// </summary>
        public string ApplicationName { get; set; }
        /// <summary>
        /// EventId
        /// </summary>
        public int EventId { get; set; }
        /// <summary>
        /// Priority
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// Severity
        /// </summary>
        public string Severity { get; set; }
        /// <summary>
        /// Category
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// MachineName
        /// </summary>
        public string MachineName { get; set; }
        /// <summary>
        /// AppDomainName
        /// </summary>
        public string AppDomainName { get; set; }
        /// <summary>
        /// ProcessId
        /// </summary>
        public string ProcessId { get; set; }
        /// <summary>
        /// ProcessName
        /// </summary>
        public string ProcessName { get; set; }
        /// <summary>
        /// ThreadName
        /// </summary>
        public string ThreadName { get; set; }
        /// <summary>
        /// Win32ThreadId
        /// </summary>
        public string Win32ThreadId { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// FormattedMessage
        /// </summary>
        public string FormattedMessage { get; set; }
        #endregion
    }
}
