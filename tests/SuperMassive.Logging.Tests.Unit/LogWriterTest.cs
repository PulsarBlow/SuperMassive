namespace SuperMassive.Logging.Tests.Unit
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using NUnit.Framework;

    public class LogWriterTest
    {
        [Test]
        public void WriteTest()
        {
            IList<TraceListener> traceListeners = new TraceListener[] { new TextWriterTraceListener("output.txt") };
            LogWriter target = new LogWriter("UnitTests", true, traceListeners);
            LogEntry log = new LogEntry("TestSuit", "Test message", "category", 1, 1000, TraceEventType.Information, "My Title", null);
            target.Write(log);
        }
    }
}
