using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NUnit.OneTimeSetup.DreddLogs.Tests.Internal.WithGlobalSetup.AsyncSetup
{
    [TestFixture]
    public class TestFixtureWithAsyncNonStaticSetupMethod : BaseTest
    {
        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            Logger.Information($"{nameof(OneTimeSetup)} of {nameof(TestFixtureWithAsyncStaticSetupMethod)}");
            await Task.Delay(100);
            throw new Exception("Asynchronous non-static fixture setup method");
        }

        [Test]
        public void Test()
        { }
    }
}
