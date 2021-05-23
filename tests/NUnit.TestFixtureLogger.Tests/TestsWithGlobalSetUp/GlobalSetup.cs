using NUnit.Framework;
using Serilog;
using System;

namespace NUnit.TestFixtureLogger.Tests.TestsWithGlobalSetUp
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
