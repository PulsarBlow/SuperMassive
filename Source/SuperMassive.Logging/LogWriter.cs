namespace SuperMassive.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// Instance based class to write log messages based on a given configuration.
    /// Messages are routed based on category.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The LogWriter works as an entry point to the <see cref="System.Diagnostics"/> trace listeners. 
    /// It will trace the <see cref="LogEntry"/> through the <see cref="TraceListener"/>s associated with the it 
    /// for all the matching categories in the elements of the <see cref="LogEntry.Categories"/> property of the log entry. 
    /// If the "all events" special log source is configured, the log entry will be traced through the log source regardles of other categories 
    /// that might have matched.
    /// If the "all events" special log source is not configured and the "unprocessed categories" special log source is configured,
    /// and the category specified in the logEntry being logged is not defined, then the logEntry will be logged to the "unprocessed categories"
    /// special log source.
    /// If both the "all events" and "unprocessed categories" special log sources are not configured and the property LogWarningsWhenNoCategoriesMatch
    /// is set to true, then the logEntry is logged to the "logging errors and warnings" special log source.
    /// </para>
    /// </remarks>
    public class LogWriter : IDisposable, ILogWriter
    {
        /// <summary>
        /// EventID used on LogEntries that occur when internal LogWriter mechanisms fail.
        /// </summary>
        public const int LogWriterFailureEventID = 1337;

        private const int DefaultPriority = -1;
        private const TraceEventType DefaultSeverity = TraceEventType.Information;
        private const int DefaultEventId = 1;
        private const string DefaultTitle = "";
        private static readonly ICollection<string> _emptyCategoriesList = new string[0];
        private readonly IList<TraceListener> _traceListeners;
        private bool _disposed;
        private string _sourceName;
        private readonly bool _autoflush;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogWriter"/> class.
        /// </summary>
        /// <param name="sourceName">Typicaly the name of the application that will generated the log events</param>
        /// <param name="autoflush">Autoflush</param>
        /// <param name="traceListeners"></param>
        public LogWriter(string sourceName, bool autoflush, IList<TraceListener> traceListeners)
        {
            Guard.ArgumentNotNullOrEmpty(sourceName, "sourceName");
            Guard.ArgumentNotNull(traceListeners, "traceListeners");
            this._sourceName = sourceName;
            this._autoflush = autoflush;
            this._traceListeners = traceListeners;
        }

        #region IDisposable Members
        /// <summary>
        /// Releases the resources used by the <see cref="LogWriter"/>.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Releases the resources used by the <see cref="LogWriter"/>.
        /// </summary>
        /// <param name="disposing"><see langword="true"/> when disposing, <see langword="false"/> otherwise.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    foreach (TraceListener listener in _traceListeners)
                    {
                        listener.Dispose();
                    }
                }
                _disposed = true;
            }
        }
        #endregion

        #region Write Overloads
        /// <overloads>
        /// Write a new log entry to the default category.
        /// </overloads>
        /// <summary>
        /// Write a new log entry to the default category.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        public void Write(object message)
        {
            this.Write(
                message,
                _emptyCategoriesList,
                DefaultPriority,
                DefaultEventId,
                DefaultSeverity,
                DefaultTitle,
                null);
        }

        /// <summary>
        /// Write a new log entry to a specific category.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
        public void Write(object message, string category)
        {
            this.Write(message, category, DefaultPriority, DefaultEventId, DefaultSeverity, DefaultTitle, null);
        }

        /// <summary>
        /// Write a new log entry with a specific category and priority.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        public void Write(object message, string category, int priority)
        {
            this.Write(message, category, priority, DefaultEventId, DefaultSeverity, DefaultTitle, null);
        }

        /// <summary>
        /// Write a new log entry with a specific category, priority and event id.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        /// <param name="eventId">Event number or identifier.</param>
        public void Write(object message, string category, int priority, int eventId)
        {
            this.Write(message, category, priority, eventId, DefaultSeverity, DefaultTitle, null);
        }

        /// <summary>
        /// Write a new log entry with a specific category, priority, event id and severity.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        /// <param name="eventId">Event number or identifier.</param>
        /// <param name="severity">Log entry severity as a <see cref="TraceEventType"/> enumeration. (Unspecified, Information, Warning or Error).</param>
        public void Write(object message, string category, int priority, int eventId, TraceEventType severity)
        {
            this.Write(message, category, priority, eventId, severity, DefaultTitle, null);
        }

        /// <summary>
        /// Write a new log entry with a specific category, priority, event id, severity
        /// and title.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        /// <param name="eventId">Event number or identifier.</param>
        /// <param name="severity">Log message severity as a <see cref="TraceEventType"/> enumeration. (Unspecified, Information, Warning or Error).</param>
        /// <param name="title">Additional description of the log entry message</param>
        public void Write(
            object message,
            string category,
            int priority,
            int eventId,
            TraceEventType severity,
            string title)
        {
            this.Write(message, category, priority, eventId, severity, title, null);
        }

        /// <summary>
        /// Write a new log entry and a dictionary of extended properties.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="properties">Dictionary of key/value pairs to log.</param>
        public void Write(object message, IDictionary<string, object> properties)
        {
            this.Write(
                message,
                _emptyCategoriesList,
                DefaultPriority,
                DefaultEventId,
                DefaultSeverity,
                DefaultTitle,
                properties);
        }

        /// <summary>
        /// Write a new log entry to a specific category with a dictionary 
        /// of extended properties.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
        /// <param name="properties">Dictionary of key/value pairs to log.</param>
        public void Write(object message, string category, IDictionary<string, object> properties)
        {
            this.Write(
                message,
                category,
                DefaultPriority,
                DefaultEventId,
                DefaultSeverity,
                DefaultTitle,
                properties);
        }

        /// <summary>
        /// Write a new log entry to with a specific category, priority and a dictionary 
        /// of extended properties.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        /// <param name="properties">Dictionary of key/value pairs to log.</param>
        public void Write(object message, string category, int priority, IDictionary<string, object> properties)
        {
            this.Write(message, category, priority, DefaultEventId, DefaultSeverity, DefaultTitle, properties);
        }

        /// <summary>
        /// Write a new log entry with a specific category, priority, event Id, severity
        /// title and dictionary of extended properties.
        /// </summary>
        /// <example>The following example demonstrates use of the Write method with
        /// a full set of parameters.
        /// <code></code></example>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        /// <param name="eventId">Event number or identifier.</param>
        /// <param name="severity">Log message severity as a <see cref="TraceEventType"/> enumeration. (Unspecified, Information, Warning or Error).</param>
        /// <param name="title">Additional description of the log entry message.</param>
        /// <param name="properties">Dictionary of key/value pairs to log.</param>
        public void Write(
            object message,
            string category,
            int priority,
            int eventId,
            TraceEventType severity,
            string title,
            IDictionary<string, object> properties)
        {
            this.Write(message, new string[] { category }, priority, eventId, severity, title, properties);
        }

        /// <summary>
        /// Write a new log entry to a specific collection of categories.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
        public void Write(object message, IEnumerable<string> categories)
        {
            this.Write(message, categories, DefaultPriority, DefaultEventId, DefaultSeverity, DefaultTitle, null);
        }

        /// <summary>
        /// Write a new log entry with a specific collection of categories and priority.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        public void Write(object message, IEnumerable<string> categories, int priority)
        {
            this.Write(message, categories, priority, DefaultEventId, DefaultSeverity, DefaultTitle, null);
        }

        /// <summary>
        /// Write a new log entry with a specific collection of categories, priority and event id.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        /// <param name="eventId">Event number or identifier.</param>
        public void Write(object message, IEnumerable<string> categories, int priority, int eventId)
        {
            this.Write(message, categories, priority, eventId, DefaultSeverity, DefaultTitle, null);
        }

        /// <summary>
        /// Write a new log entry with a specific collection of categories, priority, event id and severity.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        /// <param name="eventId">Event number or identifier.</param>
        /// <param name="severity">Log entry severity as a <see cref="TraceEventType"/> enumeration. 
        /// (Unspecified, Information, Warning or Error).</param>
        public void Write(
            object message,
            IEnumerable<string> categories,
            int priority,
            int eventId,
            TraceEventType severity)
        {
            this.Write(message, categories, priority, eventId, severity, DefaultTitle, null);
        }

        /// <summary>
        /// Write a new log entry with a specific collection of categories, priority, event id, severity
        /// and title.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        /// <param name="eventId">Event number or identifier.</param>
        /// <param name="severity">Log message severity as a <see cref="TraceEventType"/> enumeration. (Unspecified, Information, Warning or Error).</param>
        /// <param name="title">Additional description of the log entry message</param>
        public void Write(
            object message,
            IEnumerable<string> categories,
            int priority,
            int eventId,
            TraceEventType severity,
            string title)
        {
            this.Write(message, categories, priority, eventId, severity, title, null);
        }

        /// <summary>
        /// Write a new log entry to a specific collection of categories with a dictionary of extended properties.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
        /// <param name="properties">Dictionary of key/value pairs to log.</param>
        public void Write(object message, IEnumerable<string> categories, IDictionary<string, object> properties)
        {
            this.Write(message, categories, DefaultPriority, DefaultEventId, DefaultSeverity, DefaultTitle, properties);
        }

        /// <summary>
        /// Write a new log entry to with a specific collection of categories, priority and a dictionary 
        /// of extended properties.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        /// <param name="properties">Dictionary of key/value pairs to log.</param>
        public void Write(
            object message,
            IEnumerable<string> categories,
            int priority,
            IDictionary<string,
            object> properties)
        {
            this.Write(message, categories, priority, DefaultEventId, DefaultSeverity, DefaultTitle, properties);
        }
        /// <summary>
        /// Write a new log entry with a specific category, priority, event Id, severity
        /// title and dictionary of extended properties.
        /// </summary>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="categories">Category names used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        /// <param name="eventId">Event number or identifier.</param>
        /// <param name="severity">Log message severity as a <see cref="TraceEventType"/> enumeration. (Unspecified, Information, Warning or Error).</param>
        /// <param name="title">Additional description of the log entry message.</param>
        /// <param name="properties">Dictionary of key/value pairs to log.</param>
        public void Write(
            object message,
            IEnumerable<string> categories,
            int priority,
            int eventId,
            TraceEventType severity,
            string title,
            IDictionary<string, object> properties)
        {
            LogEntry log = new LogEntry();
            log.Message = message.ToString();
            log.Categories = categories.ToArray();
            log.Priority = priority;
            log.EventId = eventId;
            log.Severity = severity;
            log.Title = title;
            log.ExtendedProperties = properties;

            this.Write(log);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns true if the log writer contains active trace listeners
        /// </summary>
        /// <returns></returns>
        public bool IsLoggingEnabled()
        {
            return this._traceListeners != null && this._traceListeners.Count > 0;
        }

        /// <summary>
        /// Returns true if the given <see cref="LogEntry"/> should be logged.
        /// </summary>
        /// <param name="logEntry"></param>
        /// <returns></returns>
        public bool ShouldLog(LogEntry logEntry)
        {
            return true;
        }

        /// <summary>
        /// Writes a new log entry as defined in the <see cref="LogEntry"/> parameter.
        /// </summary>
        /// <param name="log">Log entry object to write.</param>
        public void Write(LogEntry log)
        {
            Guard.ArgumentNotNull(log, "log");
            if (!IsLoggingEnabled())
                return;
            var traceEventCache = new TraceEventCache();
            if (ShouldLog(log))
            {
                ProcessLog(log, traceEventCache);
            }

        }
        #endregion

        #region Private Methods
        private void ProcessLog(LogEntry log, TraceEventCache traceEventCache)
        {
            foreach (var listener in this._traceListeners)
            {
                try
                {
                    if (!listener.IsThreadSafe) Monitor.Enter(listener);

                    listener.TraceData(traceEventCache, this._sourceName, log.Severity, log.EventId, log);

                    if (this._autoflush)
                    {
                        listener.Flush();
                    }
                }
                finally
                {
                    if (!listener.IsThreadSafe) Monitor.Exit(listener);
                }
            }
        }
        #endregion
    }
}
