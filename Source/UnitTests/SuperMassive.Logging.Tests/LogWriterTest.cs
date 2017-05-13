namespace SuperMassive.Logging.Tests
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Xunit;

    /// <summary>
    ///This is a test class for LogWriterTest and is intended
    ///to contain all LogWriterTest Unit Tests
    ///</summary>
    public class LogWriterTest
    {
        /// <summary>
        ///A test for Write
        ///</summary>
        [Fact]
        public void WriteTest()
        {
            IList<TraceListener> traceListeners = new TraceListener[] { new TextWriterTraceListener("output.txt") };
            LogWriter target = new LogWriter("UnitTests", true, traceListeners);
            LogEntry log = new LogEntry("TestSuit", "Test message", "category", 1, 1000, TraceEventType.Information, "My Title", null);
            target.Write(log);
        }
    }
}
