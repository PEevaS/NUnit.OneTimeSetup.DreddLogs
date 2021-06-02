using System;
using NUnit.Framework;

namespace NUnit.OneTimeSetup.DreddLogs.Tests.Internal.WithGlobalSetup.SyncSetup
{
    [TestFixture]
    public class TestFixtureWithSynchronousStaticSetupMethod : BaseTest
    {
        [OneTimeSetUp]
        public static void OneTimeSetup()
        {
            Logger.Information($"{nameof(OneTimeSetup)} of {nameof(TestFixtureWithSynchronousStaticSetupMethod)}");
            throw new Exception("Synchronous static fixture setup method");
        }

        [Test]
        public void Test()
        { }
    }
}
