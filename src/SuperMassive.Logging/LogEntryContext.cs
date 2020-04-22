namespace SuperMassive.Logging
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Text;

    internal static class LogEntryContext
    {
        internal static string GetAppDomainNameSafe()
        {
            try
            {
                return AppDomain.CurrentDomain.FriendlyName;
            }
            catch (Exception e)
            {
                return string.Format(CultureInfo.CurrentCulture, Properties.Resources.IntrinsicPropertyError, e.Message);
            }
        }

        internal static string GetMachineNameSafe()
        {
            try
            {
                return Environment.MachineName;
            }
            catch (Exception e)
            {
                return string.Format(CultureInfo.CurrentCulture, Properties.Resources.IntrinsicPropertyError, e.Message);
            }
        }

        internal static string GetProcessIdSafe()
        {
            try
            {
                return GetCurrentProcessId();
            }
            catch (Exception e)
            {
                return string.Format(CultureInfo.CurrentCulture, Properties.Resources.IntrinsicPropertyError, e.Message);
            }
        }

        internal static string GetProcessNameSafe()
        {
            try
            {
                return GetProcessName();
            }
            catch (Exception e)
            {
                return string.Format(CultureInfo.CurrentCulture, Properties.Resources.IntrinsicPropertyError, e.Message);
            }
        }

        internal static Guid GetActivityId()
        {
            return Trace.CorrelationManager.ActivityId;
        }

        internal static string GetProcessName()
        {
            StringBuilder buffer = new StringBuilder(1024);
            PInvokes.GetModuleFileName(PInvokes.GetModuleHandle(null), buffer, buffer.Capacity);
            return buffer.ToString();
        }

        internal static string GetCurrentProcessId()
        {
            return PInvokes.GetCurrentProcessId().ToString(NumberFormatInfo.InvariantInfo);
        }

        internal static string GetCurrentThreadId()
        {
            return PInvokes.GetCurrentThreadId().ToString(NumberFormatInfo.InvariantInfo);
        }
    }
}
