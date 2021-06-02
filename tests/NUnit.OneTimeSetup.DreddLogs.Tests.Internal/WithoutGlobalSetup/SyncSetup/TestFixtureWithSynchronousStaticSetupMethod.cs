using System;
using NUnit.Framework;
using NUnit.OneTimeSetup.DreddLogs.Tests.Internal;

namespace NUnit.OneTimeSetup.DreddLogs.Tests.Internal.WithoutGlobalSetup.SyncSetup
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
