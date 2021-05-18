using NUnit.Framework;
using Serilog;
using Serilog.Core;

namespace NUnit.TestFixtureLogger.Tests
{
    public class BaseTest
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
