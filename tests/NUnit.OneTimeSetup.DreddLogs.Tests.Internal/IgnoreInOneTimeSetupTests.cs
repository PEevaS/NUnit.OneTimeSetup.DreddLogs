using NUnit.Framework;

namespace NUnit.OneTimeSetup.DreddLogs.Tests.Internal
{
    [TestFixture]
    public class IgnoreInOneTimeSetupTests
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Assert.Ignore();
        }

        [Test]
        public void Test() { }

        [Test]
        public void Test1() { }

    }
}
