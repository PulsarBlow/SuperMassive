using System.Collections.Generic;
using System.Diagnostics;

namespace SuperMassive.Logging
{
    /// <summary>
    /// Logging facade
    /// </summary>
    public class Logger
    {
        protected ILogWriter LogWriter { get; private set; }
        protected bool CanLog { get; private set; }
        public string Name { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="logWriter"></param>
        public Logger(ILogWriter logWriter)
            : this(logWriter, "Default")
        { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="logWriter"></param>
        /// <param name="name"></param>
        public Logger(ILogWriter logWriter, string name)
        {
            Guard.ArgumentNotNullOrEmpty(name, "name");
            this.Name = name;
            this.LogWriter = logWriter;
            if (this.LogWriter != null)
                this.CanLog = true;
        }
        /// <summary>
        /// Logs a information message
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="eventId"></param>
        public virtual void TraceInformation(string title, string message, int eventId)
        {
            TraceMessage(title, message, eventId, TraceEventType.Information);
        }
        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="eventId"></param>
        public virtual void TraceWarning(string title, string message, int eventId)
        {
            TraceMessage(title, message, eventId, TraceEventType.Warning);
        }
        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="eventId"></param>
        public virtual void TraceError(string title, string message, int eventId)
        {
            TraceMessage(title, message, eventId, TraceEventType.Error);
        }

        ///// <summary>
        ///// Logs an exception
        ///// </summary>
        ///// <param name="exception"></param>
        ///// <param name="title"></param>
        ///// <param name="eventId"></param>
        //public virtual void TraceException(Exception exception, string title, int eventId)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    FormatException(exception, sb);
        //    TraceMessage(title, sb.ToString(), eventId, TraceEventType.Error);
        //}

        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="eventId"></param>
        /// <param name="eventType"></param>
        public virtual void TraceMessage(string title, string message, int eventId, TraceEventType eventType)
        {
            if (CanLog)
                this.LogWriter.Write(new LogEntry(this.Name, message, "General", 1, eventId, eventType, title, null));
        }

        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="category"></param>
        /// <param name="priority"></param>
        /// <param name="eventId"></param>
        /// <param name="eventType"></param>
        /// <param name="properties"></param>
        public void TraceMessage(string title, string message, string category, int priority, int eventId, TraceEventType eventType, IDictionary<string, object> properties)
        {
            if (!CanLog) { return; }
            this.LogWriter.Write(new LogEntry(Name, message, category, priority, eventId, eventType, title, properties));
        }
    }
}
