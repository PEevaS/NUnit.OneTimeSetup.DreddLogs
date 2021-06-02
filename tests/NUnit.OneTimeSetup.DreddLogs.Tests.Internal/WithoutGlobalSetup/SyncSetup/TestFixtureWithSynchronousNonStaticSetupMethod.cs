using System;
using NUnit.Framework;
using NUnit.OneTimeSetup.DreddLogs.Tests.Internal;

namespace NUnit.OneTimeSetup.DreddLogs.Internal.Tests.WithoutGlobalSetup.SyncSetup
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
