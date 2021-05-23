using NUnit.Framework;
using Serilog;
using Serilog.Core;

namespace NUnit.OneTimeSetup.DreddLogs.Tests
{
    public abstract class BaseTest
    {
        protected static Logger Logger { get; private set; }

        [OneTimeSetUp]
        public void BaseOneTimeSetup()
        {
            Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        }

        [OneTimeTearDown]
        public void BaseOneTimeTearDown()
        {
            Logger.Dispose();
        }
    }
}
