using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NUnit.TestFixtureLogger.Tests.TestsWithGlobalSetUp
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

    [TestFixture]
    public class TestFixtureWithAsynchronousStaticSetupMethod : BaseTest
    {
        [OneTimeSetUp]
        public static async Task OneTimeSetup()
        {
            Logger.Information($"{nameof(OneTimeSetup)} of {nameof(TestFixtureWithAsynchronousStaticSetupMethod)}");
            await Task.Delay(100);
            throw new Exception("Asynchronous static fixture setup method");
        }

        [Test]
        public void Test()
        { }
    }

    [TestFixture]
    public class TestFixtureWithAsynchronousNonStaticSetupMethod : BaseTest
    {
        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            Logger.Information($"{nameof(OneTimeSetup)} of {nameof(TestFixtureWithAsynchronousStaticSetupMethod)}");
            await Task.Delay(100);
            throw new Exception("Asynchronous non-static fixture setup method");
        }

        [Test]
        public void Test()
        { }
    }
}
