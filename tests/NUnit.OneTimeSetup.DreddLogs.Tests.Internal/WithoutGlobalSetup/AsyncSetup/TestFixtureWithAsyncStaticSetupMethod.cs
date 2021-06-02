using System;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.OneTimeSetup.DreddLogs.Tests.Internal;

namespace NUnit.OneTimeSetup.DreddLogs.Tests.Internal.WithoutGlobalSetup.AsyncSetup
{
    [TestFixture]
    public class TestFixtureWithAsyncStaticSetupMethod : BaseTest
    {
        [OneTimeSetUp]
        public static async Task OneTimeSetup()
        {
            Logger.Information($"{nameof(OneTimeSetup)} of {nameof(TestFixtureWithAsyncStaticSetupMethod)}");
            await Task.Delay(100);
            throw new Exception("Asynchronous static fixture setup method");
        }

        [Test]
        public void Test()
        { }
    }
}
