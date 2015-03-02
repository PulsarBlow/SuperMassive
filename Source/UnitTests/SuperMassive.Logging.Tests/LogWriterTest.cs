using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace SuperMassive.Logging.Tests
{
    /// <summary>
    ///This is a test class for LogWriterTest and is intended
    ///to contain all LogWriterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LogWriterTest
    {
        /// <summary>
        ///A test for Write
        ///</summary>
        [TestMethod()]
        public void WriteTest()
        {
            IList<TraceListener> traceListeners = new TraceListener[] { new TextWriterTraceListener("output.txt") };
            LogWriter target = new LogWriter("UnitTests", true, traceListeners);
            LogEntry log = new LogEntry("TestSuit", "Test message", "category", 1, 1000, TraceEventType.Information, "My Title", null);
            target.Write(log);
        }
    }
}
