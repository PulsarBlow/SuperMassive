using SuperMassive.ExceptionHandling.Logging.Properties;
using SuperMassive.Logging;
using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace SuperMassive.ExceptionHandling.Logging
{
    /// <summary>
    /// Represents an <see cref="IExceptionHandler"/> that formats the exception into a log message
    /// </summary>
    public class LoggingExceptionHandler : IExceptionHandler
    {
        #region Members
        private readonly string applicationName;
        private readonly string logCategory;
        private readonly int eventId;
        private readonly TraceEventType severity;
        private readonly string defaultTitle;
        private readonly Type formatterType;
        private readonly int minimumPriority;
        private readonly ILogWriter logWriter;
        #endregion

        /// <summary>
        /// Handler name
        /// </summary>
        public string Name
        {
            get { return this.GetType().Name; }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingExceptionHandler"/> class with the log category, the event ID, the <see cref="TraceEventType"/>,
        /// the title, minimum priority, the formatter type, and the <see cref="LogWriter"/>.
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="logCategory">The default category</param>
        /// <param name="eventId">An event id.</param>
        /// <param name="severity">The severity.</param>
        /// <param name="title">The log title.</param>
        /// <param name="priority">The minimum priority.</param>
        /// <param name="formatterType">The type <see cref="ExceptionFormatter"/> type.</param>
        /// <param name="writer">The <see cref="LogWriter"/> to use.</param>
        /// <remarks>
        /// The type specified for the <paramref name="formatterType"/> attribute must have a public constructor with
        /// parameters of type <see name="TextWriter"/>, <see cref="Exception"/> and <see cref="Guid"/>.
        /// </remarks>
        public LoggingExceptionHandler(
            string applicationName,
            string logCategory,
            int eventId,
            TraceEventType severity,
            string title,
            int priority,
            Type formatterType,
            ILogWriter writer)
        {
            this.applicationName = applicationName;
            this.logCategory = logCategory;
            this.eventId = eventId;
            this.severity = severity;
            this.defaultTitle = title;
            this.minimumPriority = priority;
            this.formatterType = formatterType;
            this.logWriter = writer;
        }

        /// <summary>
        /// <para>Handles the specified <see cref="Exception"/> object by formatting it and writing to the configured log.</para>
        /// </summary>
        /// <param name="exception"><para>The exception to handle.</para></param>        
        /// <returns><para>Modified exception to pass to the next handler in the chain.</para></returns>
        public Exception HandleException(Exception exception)
        {
            WriteToLog(CreateMessage(exception), exception.Data);
            return exception;
        }

        /// <summary>
        /// Writes the specified log message using the Logging Application Block's 
        /// method.
        /// </summary>
        /// <param name="logMessage">The message to write to the log.</param>
        /// <param name="exceptionData">The exception's data.</param>
        protected virtual void WriteToLog(string logMessage, IDictionary exceptionData)
        {
            LogEntry entry = new LogEntry(this.applicationName, logMessage, logCategory, minimumPriority, eventId, severity, defaultTitle, null);

            foreach (DictionaryEntry dataEntry in exceptionData)
            {
                if (dataEntry.Key is string)
                {
                    entry.ExtendedProperties.Add(dataEntry.Key as string, dataEntry.Value);
                }
            }

            this.logWriter.Write(entry);
        }

        /// <summary>
        /// Creates an instance of the <see cref="StringWriter"/>
        /// class using its default constructor.
        /// </summary>
        /// <returns>A newly created <see cref="StringWriter"/></returns>
        protected virtual StringWriter CreateStringWriter()
        {
            return new StringWriter(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Creates an <see cref="ExceptionFormatter"/>
        /// object based on the configured ExceptionFormatter
        /// type name.
        /// </summary>
        /// <param name="writer">The stream to write to.</param>
        /// <param name="exception">The <see cref="Exception"/> to pass into the formatter.</param>
        /// <returns>A newly created <see cref="ExceptionFormatter"/></returns>
        protected virtual ExceptionFormatter CreateFormatter(
            StringWriter writer,
            Exception exception)
        {
            ConstructorInfo constructor = GetFormatterConstructor();
            return (ExceptionFormatter)constructor.Invoke(
                new object[] { writer, exception }
                );
        }

        private ConstructorInfo GetFormatterConstructor()
        {
            Type[] types = new Type[] { typeof(TextWriter), typeof(Exception) };
            ConstructorInfo constructor = formatterType.GetConstructor(types);
            if (constructor == null)
            {
                throw new ExceptionHandlingException(
                    string.Format(CultureInfo.CurrentCulture, Resources.MissingConstructor, formatterType.AssemblyQualifiedName));
            }
            return constructor;
        }

        private string CreateMessage(Exception exception)
        {
            StringWriter writer = null;
            StringBuilder stringBuilder = null;
            try
            {
                writer = CreateStringWriter();
                ExceptionFormatter formatter = CreateFormatter(writer, exception);
                formatter.Format();
                stringBuilder = writer.GetStringBuilder();

            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }

            return stringBuilder.ToString();
        }
    }
}
