using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.TestFixtureLogger.Attributes;

namespace NUnit.TestFixtureLogger.Tests
{
    [TestFixture]
    [GatherFixtureSetupLogs]
    public class TestFixtureWithSynchronousNonStaticSetupMethod : BaseTest
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Logger.Information($"{nameof(OneTimeSetup)} of {nameof(TestFixtureWithSynchronousNonStaticSetupMethod)}");
            throw new System.Exception("Synchronous non-static fixture setup method");
        }

        [Test]
        public void Test()
        { }
    }

    [TestFixture]
    [GatherFixtureSetupLogs]
    public class TestFixtureWithSynchronousStaticSetupMethod : BaseTest
    {
        [OneTimeSetUp]
        public static void OneTimeSetup()
        {
            Logger.Information($"{nameof(OneTimeSetup)} of {nameof(TestFixtureWithSynchronousStaticSetupMethod)}");
            throw new System.Exception("Synchronous static fixture setup method");
        }

        [Test]
        public void Test()
        { }
    }

    [TestFixture]
    [GatherFixtureSetupLogs]
    public class TestFixtureWithAsynchronousStaticSetupMethod : BaseTest
    {
        [OneTimeSetUp]
        public static async Task OneTimeSetup()
        {
            Logger.Information($"{nameof(OneTimeSetup)} of {nameof(TestFixtureWithAsynchronousStaticSetupMethod)}");
            await Task.Delay(100);
            throw new System.Exception("Asynchronous static fixture setup method");
        }

        [Test]
        public void Test()
        { }
    }

    [TestFixture]
    [GatherFixtureSetupLogs]
    public class TestFixtureWithAsynchronousNonStaticSetupMethod : BaseTest
    {
        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            Logger.Information($"{nameof(OneTimeSetup)} of {nameof(TestFixtureWithAsynchronousStaticSetupMethod)}");
            await Task.Delay(100);
            throw new System.Exception("Asynchronous non-static fixture setup method");
        }

        [Test]
        public void Test()
        { }
    }
}
