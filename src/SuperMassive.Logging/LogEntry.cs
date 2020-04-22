namespace SuperMassive.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security;
    using System.Security.Permissions;
    using System.Text;
    using System.Threading;
    using System.Xml.Serialization;
    using SuperMassive.Logging.Formatters;

    /// <summary>
    /// Represents a log message.  Contains the common properties that are required for all log messages.
    /// </summary>
    [XmlRoot("logEntry")]
    [Serializable]
    public class LogEntry : ICloneable
    {
        private static readonly TextFormatter _textFormatter = new TextFormatter();

        [NonSerialized]
        private ICollection<string> _categories = new List<string>(0);
        private string[] _categoryStrings;

        private Guid _activityId;
        private string _machineName = string.Empty;
        private DateTime _timeStamp = DateTime.MaxValue;
        private StringBuilder _errorMessages;
        private IDictionary<string, object> _extendedProperties;
        private string _appDomainName;
        private string _processId;
        private string _processName;
        private string _threadName;
        private string _win32ThreadId;

        internal bool timeStampInitialized = false;
        internal bool appDomainNameInitialized = false;
        internal bool machineNameInitialized = false;
        internal bool processIdInitialized = false;
        internal bool processNameInitialized = false;
        internal bool win32ThreadIdInitialized = false;
        internal bool threadNameInitialized = false;
        internal bool activityIdInitialized = false;
        private bool unmanagedCodePermissionAvailable = false;
        private bool unmanagedCodePermissionAvailableInitialized = false;

        /// <summary>
        /// Message body to log.  Value from ToString() method from message object.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Category name used to route the log entry to a one or more trace listeners.
        /// </summary>
        public ICollection<string> Categories
        {
            get { return _categories; }
            set { _categories = value; }
        }

        /// <summary>
        /// Importance of the log message.  Only messages whose priority is between the minimum and maximum priorities (inclusive)
        /// will be processed.
        /// </summary>
        public int Priority { get; set; } = -1;

        /// <summary>
        /// Event number or identifier.
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Log entry severity as a <see cref="Severity"/> enumeration. (Unspecified, Information, Warning or Error).
        /// </summary>
        public TraceEventType Severity { get; set; } = TraceEventType.Information;

        /// <summary>
        /// <para>Gets the string representation of the <see cref="Severity"/> enumeration.</para>
        /// </summary>
        /// <value>
        /// <para>The string value of the <see cref="Severity"/> enumeration.</para>
        /// </value>
        public string LoggedSeverity
        {
            get { return Severity.ToString(); }
        }

        /// <summary>
        /// Additional description of the log entry message.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Application Name
        /// </summary>
        public string ApplicationName { get; set; } = string.Empty;

        /// <summary>
        /// Date and time of the log entry message.
        /// </summary>
        public DateTime TimeStamp
        {
            get
            {
                if (!timeStampInitialized)
                {
                    InitializeTimeStamp();
                }

                return _timeStamp;
            }
            set
            {
                _timeStamp = value;
                timeStampInitialized = true;
            }
        }

        /// <summary>
        /// Name of the computer.
        /// </summary>
        public string MachineName
        {
            get
            {
                if (!machineNameInitialized)
                {
                    InitializeMachineName();
                }

                return _machineName;
            }
            set
            {
                _machineName = value;
                machineNameInitialized = true;
            }
        }

        /// <summary>
        /// The <see cref="AppDomain"/> in which the program is running
        /// </summary>
        public string AppDomainName
        {
            get
            {
                if (!appDomainNameInitialized)
                {
                    InitializeAppDomainName();
                }

                return _appDomainName;
            }
            set
            {
                _appDomainName = value;
                appDomainNameInitialized = true;
            }
        }

        /// <summary>
        /// The Win32 process ID for the current running process.
        /// </summary>
        public string ProcessId
        {
            get
            {
                if (!processIdInitialized)
                {
                    InitializeProcessId();
                }

                return _processId;
            }
            set
            {
                _processId = value;
                processIdInitialized = true;
            }
        }

        /// <summary>
        /// The name of the current running process.
        /// </summary>
        public string ProcessName
        {
            get
            {
                if (!processNameInitialized)
                {
                    InitializeProcessName();
                }

                return _processName;
            }
            set
            {
                _processName = value;
                processNameInitialized = true;
            }
        }

        /// <summary>
        /// The name of the .NET thread.
        /// </summary>
        ///  <seealso cref="Win32ThreadId"/>
        public string ManagedThreadName
        {
            get
            {
                if (!threadNameInitialized)
                {
                    InitializeThreadName();
                }

                return _threadName;
            }
            set
            {
                _threadName = value;
                threadNameInitialized = true;
            }
        }

        /// <summary>
        /// The Win32 Thread ID for the current thread.
        /// </summary>
        public string Win32ThreadId
        {
            get
            {
                if (!win32ThreadIdInitialized)
                {
                    InitializeWin32ThreadId();
                }

                return _win32ThreadId;
            }
            set
            {
                _win32ThreadId = value;
                win32ThreadIdInitialized = true;
            }
        }

        /// <summary>
        /// Dictionary of key/value pairs to record.
        /// </summary>
        public IDictionary<string, object> ExtendedProperties
        {
            get
            {
                if (_extendedProperties == null)
                {
                    _extendedProperties = new Dictionary<string, object>();
                }
                return _extendedProperties;
            }
            set { _extendedProperties = value; }
        }

        /// <summary>
        /// Read-only property that returns the timeStamp formatted using the current culture.
        /// </summary>
        public string TimeStampString
        {
            get { return TimeStamp.ToString(CultureInfo.CurrentCulture); }
        }

        /// <summary>
        /// Tracing activity id
        /// </summary>
        public Guid ActivityId
        {
            get
            {
                if (!activityIdInitialized)
                {
                    InitializeActivityId();
                }

                return _activityId;
            }
            set
            {
                _activityId = value;
                activityIdInitialized = true;
            }
        }

        /// <summary>
        /// Related activity id
        /// </summary>
        public Guid? RelatedActivityId { get; set; }

        /// <summary>
        /// Gets the error message with the <see cref="LogEntry"></see>
        /// </summary>
        public string ErrorMessages
        {
            get
            {
                if (_errorMessages == null)
                    return null;
                else
                    return _errorMessages.ToString();
            }
        }

        private bool UnmanagedCodePermissionAvailable
        {
            get
            {
                if (!unmanagedCodePermissionAvailableInitialized)
                {
                    // check whether the unmanaged code permission is available to avoid three potential stack walks
                    bool internalUnmanagedCodePermissionAvailable = false;
                    var permissionSet = new PermissionSet(PermissionState.None);
                    permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
                    // avoid a stack walk by checking for the permission on the current assembly. this is safe because there are no
                    // stack walk modifiers before the call.
                    if (permissionSet.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet))
                    {
                        try
                        {
                            permissionSet.Demand();
                            internalUnmanagedCodePermissionAvailable = true;
                        }
                        catch (SecurityException)
                        { }
                    }

                    UnmanagedCodePermissionAvailable = internalUnmanagedCodePermissionAvailable;
                }

                return unmanagedCodePermissionAvailable;
            }
            set
            {
                unmanagedCodePermissionAvailable = value;
                unmanagedCodePermissionAvailableInitialized = true;
            }
        }

        /// <summary>
        /// Tracing activity id as a string to support WMI Queries
        /// </summary>
        public string ActivityIdString
        {
            get { return ActivityId.ToString(); }
        }

        /// <summary>
        /// Category names used to route the log entry to a one or more trace listeners.
        /// This readonly property is available to support WMI queries
        /// </summary>
        public string[] CategoriesStrings
        {
            get
            {
                string[] categoriesStrings = new string[Categories.Count];
                Categories.CopyTo(categoriesStrings, 0);
                return categoriesStrings;
            }
        }

        /// <summary>
        /// Initialize a new instance of a <see cref="LogEntry"/> class.
        /// </summary>
        public LogEntry()
        {
        }

        /// <summary>
        /// Create a new instance of <see cref="LogEntry"/> with a full set of constructor parameters
        /// </summary>
        /// <param name="applicationName">Application name</param>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="category">Category name used to route the log entry to a one or more trace listeners.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        /// <param name="eventId">Event number or identifier.</param>
        /// <param name="severity">Log entry severity as a <see cref="Severity"/> enumeration. (Unspecified, Information, Warning or Error).</param>
        /// <param name="title">Additional description of the log entry message.</param>
        /// <param name="properties">Dictionary of key/value pairs to record.</param>
        public LogEntry(string applicationName, object message, string category, int priority, int eventId,
                        TraceEventType severity, string title, IDictionary<string, object> properties)
            : this(applicationName, message, BuildCategoriesCollection(category), priority, eventId, severity, title, properties)
        {
        }

        /// <summary>
        /// Create a new instance of <see cref="LogEntry"/> with a full set of constructor parameters
        /// </summary>
        /// <param name="applicationName">Application name</param>
        /// <param name="message">Message body to log.  Value from ToString() method from message object.</param>
        /// <param name="categories">Collection of category names used to route the log entry to a one or more sinks.</param>
        /// <param name="priority">Only messages must be above the minimum priority are processed.</param>
        /// <param name="eventId">Event number or identifier.</param>
        /// <param name="severity">Log entry severity as a <see cref="Severity"/> enumeration. (Unspecified, Information, Warning or Error).</param>
        /// <param name="title">Additional description of the log entry message.</param>
        /// <param name="properties">Dictionary of key/value pairs to record.</param>
        public LogEntry(string applicationName, object message, ICollection<string> categories, int priority, int eventId,
                        TraceEventType severity, string title, IDictionary<string, object> properties)
        {
            Guard.ArgumentNotNull(applicationName, "applicationName");
            Guard.ArgumentNotNull(message, "message");
            Guard.ArgumentNotNull(categories, "categories");

            ApplicationName = applicationName;
            Message = message.ToString();
            Priority = priority;
            Categories = categories;
            EventId = eventId;
            Severity = severity;
            Title = title;
            ExtendedProperties = properties;
        }

        /// <summary>
        /// Returns a <see cref="String"/> that represents the current <see cref="LogEntry"/>, 
        /// using a default formatting template.
        /// </summary>
        /// <returns>A <see cref="String"/> that represents the current <see cref="LogEntry"/>.</returns>
        public override string ToString()
        {
            return _textFormatter.Format(this);
        }

        /// <summary>
        /// Creates a new <see cref="LogEntry"/> that is a copy of the current instance.
        /// </summary>
        /// <remarks>
        /// If the dictionary contained in <see cref="ExtendedProperties"/> implements <see cref="ICloneable"/>, the resulting
        /// <see cref="LogEntry"/> will have its ExtendedProperties set by calling <c>Clone()</c>. Otherwise the resulting
        /// <see cref="LogEntry"/> will have its ExtendedProperties set to <see langword="null"/>.
        /// </remarks>
        /// <implements>ICloneable.Clone</implements>
        /// <returns>A new <c>LogEntry</c> that is a copy of the current instance.</returns>
        public object Clone()
        {
            LogEntry result = new LogEntry
            {
                ApplicationName = ApplicationName,
                Message = Message,
                EventId = EventId,
                Title = Title,
                Severity = Severity,
                Priority = Priority,
                TimeStamp = TimeStamp,
                MachineName = MachineName,
                AppDomainName = AppDomainName,
                ProcessId = ProcessId,
                ProcessName = ProcessName,
                ManagedThreadName = ManagedThreadName,
                ActivityId = ActivityId,
                // clone categories
                Categories = new List<string>(Categories),
            };

            // clone extended properties
            if (_extendedProperties != null)
                result.ExtendedProperties = new Dictionary<string, object>(_extendedProperties);

            // clone error messages
            if (_errorMessages != null)
            {
                result._errorMessages = new StringBuilder(_errorMessages.ToString());
            }

            return result;
        }

        /// <summary>
        /// Add an error or warning message to the start of the messages string builder.
        /// </summary>
        /// <param name="message">Message to be added to this instance</param>
        public virtual void AddErrorMessage(string message)
        {
            if (_errorMessages == null)
            {
                _errorMessages = new StringBuilder();
            }
            _errorMessages.Insert(0, Environment.NewLine);
            _errorMessages.Insert(0, Environment.NewLine);
            _errorMessages.Insert(0, message);
        }

        /// <summary>
        /// Gets the current process name.
        /// </summary>
        /// <returns>The process name.</returns>
        public static string GetProcessName()
        {
            return LogEntryContext.GetProcessName();
        }

        /// <summary>
        /// Set the intrinsic properties such as MachineName and UserIdentity.
        /// </summary>
        internal void CollectIntrinsicProperties()
        {
            InitializeTimeStamp();
            InitializeActivityId();
            InitializeMachineName();
            InitializeAppDomainName();
            InitializeProcessId();
            InitializeProcessName();
            InitializeThreadName();
            InitializeWin32ThreadId();
        }

        // Serialization customization methods

        // The Categories collection may be non-serializable. We copy it to an array so that
        // the categories are guaranteed to be serialized.
        [OnSerializing]
        private void OnSerializing(StreamingContext context)
        {
            if (_categories != null)
            {
                _categoryStrings = _categories.ToArray();
            }
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            // We've just be deserialized, stick in our categories collection.
            _categories = _categoryStrings;
        }

        private void InitializeTimeStamp()
        {
            TimeStamp = DateTime.UtcNow;
        }

        private void InitializeActivityId()
        {
            ActivityId = Guid.Empty;
        }

        private void InitializeMachineName()
        {
            MachineName = LogEntryContext.GetMachineNameSafe();
        }

        private void InitializeAppDomainName()
        {
            AppDomainName = LogEntryContext.GetAppDomainNameSafe();
        }

        private void InitializeProcessId()
        {
            if (UnmanagedCodePermissionAvailable)
            {
                ProcessId = LogEntryContext.GetProcessIdSafe();
            }
            else
            {
                ProcessId = string.Format(CultureInfo.CurrentCulture,
                    Properties.Resources.IntrinsicPropertyError,
                    Properties.Resources.LogEntryIntrinsicPropertyNoUnmanagedCodePermissionError);
            }
        }

        private void InitializeProcessName()
        {
            if (UnmanagedCodePermissionAvailable)
            {
                ProcessName = LogEntryContext.GetProcessNameSafe();
            }
            else
            {
                ProcessName = string.Format(CultureInfo.CurrentCulture,
                    Properties.Resources.IntrinsicPropertyError,
                    Properties.Resources.LogEntryIntrinsicPropertyNoUnmanagedCodePermissionError);
            }
        }

        private void InitializeThreadName()
        {
            try
            {
                ManagedThreadName = Thread.CurrentThread.Name;
            }
            catch (Exception e)
            {
                ManagedThreadName = string.Format(CultureInfo.CurrentCulture, Properties.Resources.IntrinsicPropertyError, e.Message);
            }
        }

        private void InitializeWin32ThreadId()
        {
            if (UnmanagedCodePermissionAvailable)
            {
                try
                {
                    Win32ThreadId = LogEntryContext.GetCurrentThreadId();
                }
                catch (Exception e)
                {
                    Win32ThreadId = string.Format(CultureInfo.CurrentCulture, Properties.Resources.IntrinsicPropertyError, e.Message);
                }
            }
            else
            {
                Win32ThreadId = string.Format(CultureInfo.CurrentCulture,
                    Properties.Resources.IntrinsicPropertyError,
                    Properties.Resources.LogEntryIntrinsicPropertyNoUnmanagedCodePermissionError);
            }
        }

        private static ICollection<string> BuildCategoriesCollection(string category)
        {
            if (string.IsNullOrEmpty(category))
                throw new ArgumentNullException("category");

            return new string[] { category };
        }
    }
}
