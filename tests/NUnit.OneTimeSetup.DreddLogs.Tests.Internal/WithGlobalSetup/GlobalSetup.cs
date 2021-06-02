using System;
using NUnit.Framework;
using Serilog;

namespace NUnit.OneTimeSetup.DreddLogs.Tests.Internal.WithGlobalSetup
{
    [SetUpFixture]
    public class GlobalSetup
    {
        [OneTimeSetUp]
        public static void GlobalOneTimeSetup()
        {
            using var logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

            logger.Information($"{nameof(GlobalOneTimeSetup)} of {nameof(GlobalSetup)}");

            throw new Exception("Global OneTimeSetup for sub-set of test suites.");
        }
    }
}
