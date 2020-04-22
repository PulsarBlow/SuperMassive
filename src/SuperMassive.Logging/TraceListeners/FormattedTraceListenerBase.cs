namespace SuperMassive.Logging.TraceListeners
{
    using System.Diagnostics;
    using SuperMassive.Logging.Formatters;

    /// <summary>
    /// Base class for <see cref="TraceListener"/>s that deal with formatters.
    /// </summary>
    public abstract class FormattedTraceListenerBase : TraceListener
    {
        /// <summary>
        /// Specifies whether this TraceListener is threadsafe
        /// </summary>
        public override bool IsThreadSafe
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// The <see cref="ILogFormatter"/> used to format the trace messages.
        /// </summary>
        public ILogFormatter Formatter { get; set; }

        /// <summary>
        /// Initalizes a new instance of <see cref="FormattedTraceListenerBase"/>.
        /// </summary>
        protected FormattedTraceListenerBase()
        {
        }

        /// <summary>
        /// Initalizes a new instance of <see cref="FormattedTraceListenerBase"/> with a <see cref="ILogFormatter"/>.
        /// </summary>
        /// <param name="formatter">The <see cref="ILogFormatter"/> to use when tracing a <see cref="LogEntry"/>.</param>
        protected FormattedTraceListenerBase(ILogFormatter formatter)
        {
            Formatter = formatter;
        }

        /// <summary>
        /// Overriding TraceData method for the base TraceListener class because it calls the 
        /// private WriteHeader and WriteFooter methods which actually call the Write method again
        /// and this amounts to multiple log messages 
        /// </summary>
        /// <param name="eventCache">The context information provided by <see cref="System.Diagnostics"/>.</param>
        /// <param name="source">The name of the trace source that delivered the trace data.</param>
        /// <param name="eventType">The type of event.</param>
        /// <param name="id">The id of the event.</param>
        /// <param name="data">The data to trace.</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if ((Filter == null) || Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                string text1 = string.Empty;
                if (data != null)
                {
                    text1 = data.ToString();

                    WriteLine(text1);
                }
            }
        }
    }
}
