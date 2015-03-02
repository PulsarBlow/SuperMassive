using SuperMassive.Logging.Formatters;
using System;
using System.Diagnostics;
using System.Globalization;

namespace SuperMassive.Logging.AzureTable
{
    /// <summary>
    /// This class is responsible for managing <see cref="ApplicationLogEntity"/> in the system.
    /// </summary>
    public static class ApplicationLogEntityManager
    {
        /// <summary>
        /// Returns a formatted row key
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public static string CreateRowKey(SortOrder sortOrder = SortOrder.Descending)
        {
            return CreateRowKey(Guid.NewGuid(), DateTime.UtcNow, sortOrder);
        }

        /// <summary>
        /// Returns a formatted row key
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string CreateRowKey(Guid guid, SortOrder sortOrder = SortOrder.Descending)
        {
            return CreateRowKey(guid, DateTime.UtcNow, sortOrder);
        }

        /// <summary>
        /// Returns a formatted row key
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="date"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public static string CreateRowKey(Guid guid, DateTime date, SortOrder sortOrder)
        {
            long ticks = date.Ticks;
            if (sortOrder == SortOrder.Descending)
                ticks = DateTime.MaxValue.Ticks - date.Ticks;

            return String.Format(CultureInfo.InvariantCulture, "{0:D19}_{1}", ticks, guid.ToString());
        }

        /// <summary>
        /// Returns a formatted partition key
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static string CreatePartitionKey(string applicationName, DateTimeOffset timeStamp)
        {
            Guard.ArgumentNotNullOrWhiteSpace(applicationName, "applicationName");

            return String.Format(CultureInfo.InvariantCulture, "{0}_{1}",
                applicationName.Trim(),
                timeStamp.ToString("yyyyMM"));
        }

        /// <summary>
        /// Creates a full populated new <see cref="ApplicationLogEntity"/> from a given <see cref="LogEntry"/>
        /// </summary>
        /// <param name="logEntry"></param>
        /// <param name="formatter"></param>
        /// <returns></returns>
        public static ApplicationLogEntity CreateFromLogEntry(LogEntry logEntry, ILogFormatter formatter)
        {
            Guard.ArgumentNotNull(logEntry, "logEntry");
            ApplicationLogEntity entity = new ApplicationLogEntity
            {
                AppDomainName = logEntry.AppDomainName,
                ApplicationName = logEntry.ApplicationName,
                Category = logEntry.Categories.Join(","),
                EventId = logEntry.EventId,
                MachineName = Environment.MachineName,
                Message = logEntry.Message,
                Priority = logEntry.Priority,
                ProcessId = logEntry.ProcessId,
                ProcessName = logEntry.ProcessName,
                Severity = logEntry.Severity.ToString(),
                ThreadName = logEntry.ManagedThreadName,
                Title = logEntry.Title,
                Win32ThreadId = logEntry.Win32ThreadId
            };

            entity.Timestamp = new DateTimeOffset(logEntry.TimeStamp, TimeSpan.Zero);
            entity.PartitionKey = CreatePartitionKey(entity.ApplicationName, entity.Timestamp);
            entity.RowKey = ApplicationLogEntityManager.CreateRowKey(SortOrder.Descending);
            entity.FormattedMessage = formatter != null ? formatter.Format(logEntry) : logEntry.Message;

            return entity;
        }

        public static ApplicationLogEntity Create(string title, string rawMessage, string formattedMessage, int eventId, string applicationName = "Default", TraceEventType severity = TraceEventType.Error, string category = "General", int priority = 1, string processId = null, string processName = null, string threadName = null, string wind32ThreadId = null)
        {
            return new ApplicationLogEntity
            {
                AppDomainName = "Unknown AppDomain",
                ApplicationName = applicationName,
                Category = category,
                EventId = eventId,
                FormattedMessage = formattedMessage,
                MachineName = Environment.MachineName,
                Message = rawMessage,
                PartitionKey = CreatePartitionKey(applicationName, DateTimeOffset.UtcNow),
                Priority = priority,
                ProcessId = processId,
                ProcessName = processName,
                RowKey = ApplicationLogEntityManager.CreateRowKey(SortOrder.Descending),
                Severity = severity.ToString(),
                ThreadName = threadName,
                Timestamp = DateTimeOffset.UtcNow,
                Title = title,
                Win32ThreadId = wind32ThreadId
            };
        }
    }
}
