using System;
using NUnit.Framework;

namespace NUnit.OneTimeSetup.DreddLogs.Tests.Internal.WithGlobalSetup.SyncSetup
{
    [TestFixture]
    public class TestFixtureWithSynchronousNonStaticSetupMethod : BaseTest
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Logger.Information($"{nameof(OneTimeSetup)} of {nameof(TestFixtureWithSynchronousNonStaticSetupMethod)}");
            throw new Exception("Synchronous non-static fixture setup method");
        }

        [Test]
        public void Test()
        { }
    }
}
